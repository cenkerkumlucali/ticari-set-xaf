using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Phone")]
    [DefaultProperty("IletisimTipi")]
    public class PersonelIletisimBilgileri : XPObject
    {
        public PersonelIletisimBilgileri(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private PersonelKart personelId;
        private string iletisimBilgileri;
        private IletisimTipleri iletisimTipi;
        private string aciklama;

        [Size(64)]
        [RuleRequiredField]
        [RuleUniqueValue("RUV-Iletisim.01", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        public string IletisimBilgileri
        {
            get => iletisimBilgileri;
            set => SetPropertyValue(nameof(IletisimBilgileri), ref iletisimBilgileri, value);
        }
        [RuleRequiredField]
        public IletisimTipleri IletisimTipi
        {
            get => iletisimTipi;
            set => SetPropertyValue(nameof(IletisimTipi), ref iletisimTipi, value);
        }

        [Size(128)]
        public string Aciklama
        {
            get => aciklama;
            set => SetPropertyValue(nameof(Aciklama), ref aciklama, value);
        }
        [Association("Personel-IletisimBilgileri")]
        [XafDisplayName("Personel")]
        public PersonelKart PersonelId
        {
            get => personelId;
            set => SetPropertyValue(nameof(PersonelId), ref personelId, value);
        }
    }
}