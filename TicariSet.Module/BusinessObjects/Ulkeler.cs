using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("Ad")]
    [CreatableItem(false)]
    [ImageName("BO_Country_v92")]
    public class Ulkeler : XPObject
    {
        public Ulkeler(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string ad;

        [Size(32)]
        [RuleUniqueValue("RUV-Ulkeler.01",DefaultContexts.Save,CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        [RuleRequiredField]
        public string Ad
        {
            get => ad;
            set => SetPropertyValue(nameof(Ad), ref ad, value);
        }

        [Association("Ulkeler-CariAdres")]
        public XPCollection<CariAdresBilgileri> CariAdresBilgileri =>
            GetCollection<CariAdresBilgileri>(nameof(CariAdresBilgileri));
        [Association("Ulkeler-PersonelAdres")]
        public XPCollection<PersonelAdresBilgileri> PersonelAdresBilgileri =>
            GetCollection<PersonelAdresBilgileri>(nameof(PersonelAdresBilgileri));
    }
}