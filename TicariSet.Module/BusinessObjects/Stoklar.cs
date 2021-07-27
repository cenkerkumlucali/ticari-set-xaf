using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Linq;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [DefaultProperty("Tanim")]
    [ListViewFilter("Tüm Liste", "")]
    [ListViewFilter("Aktif Stoklar", "[Durum] == true", true)]
    [ListViewFilter("Pasif Stoklar", "[Durum] == false")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Stoklar : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Stoklar(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Birimler birim = Session.FindObject<Birimler>(new BinaryOperator(nameof(birim.Varsayilan), true));
            if (birim != null)
                BirimID = birim;
            Durum = true;
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        bool durum;
        double satisFiyati;
        double alisFiyati;
        double vergiOrani;
        Birimler birimID;
        string tanim;
        string kod;

        [VisibleInDetailView(false)]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }

        [Size(200)]
        public string Tanim
        {
            get => tanim;
            set => SetPropertyValue(nameof(Tanim), ref tanim, value);
        }
        [XafDisplayName("Birim")]
        public Birimler BirimID
        {
            get => birimID;
            set => SetPropertyValue(nameof(BirimID), ref birimID, value);
        }

        public double VergiOrani
        {
            get => vergiOrani;
            set => SetPropertyValue(nameof(VergiOrani), ref vergiOrani, value);
        }
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double AlisFiyati
        {
            get => alisFiyati;
            set => SetPropertyValue(nameof(AlisFiyati), ref alisFiyati, value);
        }
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double SatisFiyati
        {
            get => satisFiyati;
            set => SetPropertyValue(nameof(SatisFiyati), ref satisFiyati, value);
        }

        public bool Durum
        {
            get => durum;
            set => SetPropertyValue(nameof(Durum), ref durum, value);
        }
        [VisibleInDetailView(false)]
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double Giren => StokHareket.Where(c => c.Hareket == EnumObjects.StokHareketType.Giris).Sum(c => c.Miktar);

        [VisibleInDetailView(false)]
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double Cikan => StokHareket.Where(c => c.Hareket == EnumObjects.StokHareketType.Cikis).Sum(c => c.Miktar);

        [VisibleInDetailView(false)]
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double Kalan => Giren - Cikan;

        [Association("Stoklar-StokHareket")]
        public XPCollection<StokHareketler> StokHareket => GetCollection<StokHareketler>(nameof(StokHareket));

        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork
                  && (Session.DataLayer != null)
                    && Session.IsNewObject(this))
                         && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "StoklarServerPrefix");
                Kod = string.Format("{0:D7}", deger);
            }
            base.OnSaving();
        }
    }
}