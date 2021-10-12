using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [DefaultProperty("Hesap")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class BankaHesaplari : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public BankaHesaplari(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        string aciklama;
        BankaSubeleri subeID;
        Bankalar bankaID;
        string iban;
        string hesap;
        string kod;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Hesap
        {
            get => hesap;
            set => SetPropertyValue(nameof(Hesap), ref hesap, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Iban
        {
            get => iban;
            set => SetPropertyValue(nameof(Iban), ref iban, value);
        }
        [XafDisplayName("Banka")]
        [Association("Bankalar-Hesaplar")]
        public Bankalar BankaID
        {
            get => bankaID;
            set => SetPropertyValue(nameof(BankaID), ref bankaID, value);
        }
        [Association("BankaSubeleri-Hesaplar")]
        [XafDisplayName("Şube")]
        public BankaSubeleri SubeID
        {
            get => subeID;
            set => SetPropertyValue(nameof(SubeID), ref subeID, value);
        }
        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Aciklama
        {
            get => aciklama;
            set => SetPropertyValue(nameof(Aciklama), ref aciklama, value);
        }
     
        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork
                  && (Session.DataLayer != null)
                  && Session.IsNewObject(this))
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "BankaHesaplariServerPrefix");
                Kod = string.Format("BH{0:D7}", deger);
            }
            base.OnSaving();
        }
    }
}