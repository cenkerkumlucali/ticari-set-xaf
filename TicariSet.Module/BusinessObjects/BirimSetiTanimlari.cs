using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("Kod")]
    [RuleCombinationOfPropertiesIsUnique("BirimSetiAdRule",DefaultContexts.Save,"Ad",messageTemplate:"Girilen birim seti zaten mevcuttur.")]
    public class BirimSetiTanimlari : BaseObject
    { 
        public BirimSetiTanimlari(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string kod;
        private string ad;
        private string anaBirim;
        private DurumType durum;

        [Size(8)]
        [RuleRequiredField]
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

        [Size(8)]
        [RuleRequiredField]
        public string AnaBirim
        {
            get => anaBirim;
            set => SetPropertyValue(nameof(AnaBirim), ref anaBirim, value);
        }
        public DurumType Durum
        {
            get => durum;
            set => SetPropertyValue(nameof(Durum), ref durum, value);
        }
    }
}