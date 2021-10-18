using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("Business_Bank")]
    [DefaultProperty("Hesap")]
    [RuleCombinationOfPropertiesIsUnique("IbanRule",DefaultContexts.Save,"Iban",messageTemplate:"Girilen iban zaten mevcuttur.")]
    public class BankaHesaplari : XPObject
    {
        public BankaHesaplari(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        string aciklama;
        BankaSubeleri subeID;
        Bankalar bankaID;
        string iban;
        string hesap;
        string kod;
        Cariler cariId;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        public string Hesap
        {
            get => hesap;
            set => SetPropertyValue(nameof(Hesap), ref hesap, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        public string Iban
        {
            get => iban;
            set => SetPropertyValue(nameof(Iban), ref iban, value);
        }
        [XafDisplayName("Banka")]
        [Association("Bankalar-Hesaplar")]
        [RuleRequiredField]
        public Bankalar BankaID
        {
            get => bankaID;
            set => SetPropertyValue(nameof(BankaID), ref bankaID, value);
        }
        [Association("BankaSubeleri-Hesaplar")]
        [XafDisplayName("Şube")]
        [RuleRequiredField]
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
        [Association("Cariler-BankaHesaplari")]
        [XafDisplayName("Cari")]
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
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "BankaHesaplariServerPrefix");
                Kod = string.Format("BH{0:D7}", deger);
            }
            base.OnSaving();
        }
    }
}