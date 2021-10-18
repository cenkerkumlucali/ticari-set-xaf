using System.ComponentModel;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("Kod")]
    [ListViewFilter("Tüm Dövizler","")]
    [ListViewFilter("Aktif Dövizler","[Durum]==0",true)]
    [ListViewFilter("Pasif Dövizler","[Durum]==1")]
    [RuleCombinationOfPropertiesIsUnique("DovizAdRule",DefaultContexts.Save,"Ad",messageTemplate:"Girilen döviz zaten mevcuttur.")]
    public class Doviz : BaseObject
    { 
        public Doviz(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string kod;
        private string ad;
        private DurumType durum;

        [Size(8)]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }

        [Size(32)]
        [RuleRequiredField]
        public string Ad
        {
            get => ad;
            set => SetPropertyValue(nameof(Ad), ref ad, value);
        }

        public DurumType Durum
        {
            get => durum;
            set => SetPropertyValue(nameof(Durum), ref durum, value);
        }
    }
}