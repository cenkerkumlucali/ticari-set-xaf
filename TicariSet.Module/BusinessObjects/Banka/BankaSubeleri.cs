using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace TicariSet.Module.BusinessObjects.Banka

{
    [DefaultClassOptions]
    [DefaultProperty("Tanim")]

    public class BankaSubeleri : XPObject
    {
        public BankaSubeleri(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        string aciklama;
        Bankalar bankaID;
        string tanim;
        string kod;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        public string Tanim
        {
            get => tanim;
            set => SetPropertyValue(nameof(Tanim), ref tanim, value);
        }

        [Association("Bankalar-Subeler")]
        [XafDisplayName("Banka")]
        public Bankalar BankaID
        {
            get => bankaID;
            set => SetPropertyValue(nameof(BankaID), ref bankaID, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Aciklama
        {
            get => aciklama;
            set => SetPropertyValue(nameof(Aciklama), ref aciklama, value);
        }

        [Association("BankaSubeleri-Hesaplar")]
        public XPCollection<BankaHesaplari> Hesaplar => GetCollection<BankaHesaplari>(nameof(Hesaplar));

        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork
                  && (Session.DataLayer != null)
                  && Session.IsNewObject(this))
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "BankaSubeleriServerPrefix");
                Kod = string.Format("BS{0:D7}", deger);
            }
            base.OnSaving();
        }
    }
}