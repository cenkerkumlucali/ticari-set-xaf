using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using TicariSet.Module.BusinessObjects.Tanım;

namespace TicariSet.Module.BusinessObjects.Cari
{
    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class CariAdresBilgileri : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public CariAdresBilgileri(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private string kod;
        private string adresDetay;
        private Sehirler sehirId;
        private Ilceler ilceId;
        private Ulkeler ulkeId;
        private Cariler cariId;

        [Size(256)]
        [XafDisplayName("Adres Detayı")]
        [RuleRequiredField]
        public string AdresDetay
        {
            get => adresDetay;
            set => SetPropertyValue(nameof(AdresDetay), ref adresDetay, value);
        }

        [Size(32)]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }
        [Association("Sehirler-CariAdres")]
        [XafDisplayName("Şehir")]
        public Sehirler SehirId
        {
            get => sehirId;
            set => SetPropertyValue(nameof(SehirId), ref sehirId, value);
        }

        [Association("Ilceler-CariAdres")]
        [XafDisplayName("İlçe")]
        public Ilceler IlceId
        {
            get => ilceId;
            set => SetPropertyValue(nameof(IlceId), ref ilceId, value);
        }

        [Association("Ulkeler-CariAdres")]
        [XafDisplayName("Ülke")]
        public Ulkeler UlkeId
        {
            get => ulkeId;
            set => SetPropertyValue(nameof(UlkeId), ref ulkeId, value);
        }

        [Association("Cari-Adres")]
        [XafDisplayName("Cari Hesap")]
        public Cariler CariId
        {
            get => cariId;
            set => SetPropertyValue(nameof(CariId), ref cariId, value);
        }
        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork
                  && (Session.DataLayer != null)
                  && Session.IsNewObject(this))
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "CariAdresServerPrefix");
                Kod = string.Format("CHADRS{0:D4}", deger);
            }
            base.OnSaving();
        }
    }
}