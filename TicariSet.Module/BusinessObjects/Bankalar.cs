using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Validation;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("Tanim")]
  
    public class Bankalar : XPObject
    { 
        public Bankalar(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        string shiftKod;
        string aciklama;
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

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Aciklama
        {
            get => aciklama;
            set => SetPropertyValue(nameof(Aciklama), ref aciklama, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        public string ShiftKod
        {
            get => shiftKod;
            set => SetPropertyValue(nameof(ShiftKod), ref shiftKod, value);
        }
        [Association("Bankalar-Subeler")]
        public XPCollection<BankaSubeleri> Subeler => GetCollection<BankaSubeleri>(nameof(Subeler));

        [Association("Bankalar-Hesaplar")]
        public XPCollection<BankaHesaplari> Hesaplar => GetCollection<BankaHesaplari>(nameof(Hesaplar));

        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork
                  && (Session.DataLayer != null)
                  && Session.IsNewObject(this))
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "BankalarServerPrefix");
                Kod = string.Format("B{0:D7}", deger);
            }
            base.OnSaving();
        }
    }
}