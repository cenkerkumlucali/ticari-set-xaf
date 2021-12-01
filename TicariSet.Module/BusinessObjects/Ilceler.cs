using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("Ad")]
    [ImageName("Actions_Home")]
    [CreatableItem(true)]
    [RuleCombinationOfPropertiesIsUnique("RCOPIU-Ilceler.01",DefaultContexts.Save,
        "Lat,Lng,NortheastLat,NortheastLng,SouthwestLat,SouthwestLng",messageTemplate:"Bu kayıt zaten mevcuttur.")]
    public class Ilceler : XPObject
    { 
        public Ilceler(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        string ad;
        double lat;
        double lng;
        double northeastLat;
        double northeastLng;
        double southwestLat;
        double southwestLng;
        Sehirler sehirId;

        [Size(64)]
        [RuleRequiredField]
        [RuleUniqueValue("RUV-Ilceler.01",DefaultContexts.Save,CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        public string Ad
        {
            get => ad;
            set => SetPropertyValue(nameof(Ad), ref ad, value);
        }
        [DbType("decimal(20, 8)")]
        [XafDisplayName("Enlem")]
        [RuleRequiredField]
        public double Lat
        {
            get => lat;
            set => SetPropertyValue(nameof(Lat), ref lat, value);
        }

        [DbType("decimal(20, 8)")]
        [XafDisplayName("Boylam")]
        [RuleRequiredField]
        public double Lng
        {
            get => lng;
            set => SetPropertyValue(nameof(Lng), ref lng, value);
        }
        [DbType("decimal(20, 8)")]
        [XafDisplayName("Kuzeydoğu Sınırı Enlem")]
        public double NortheastLat
        {
            get => northeastLat;
            set => SetPropertyValue(nameof(NortheastLat), ref northeastLat, value);
        }
        [DbType("decimal(20, 8)")]
        [XafDisplayName("Kuzeydoğu Sınırı Boylam")]
        public double NortheastLng
        {
            get => northeastLng;
            set => SetPropertyValue(nameof(NortheastLng), ref northeastLng, value);
        }
        [DbType("decimal(20, 8)")]
        [XafDisplayName("Güneybatı Sınırı Enlem")]
        public double SouthwestLat
        {
            get => southwestLat;
            set => SetPropertyValue(nameof(SouthwestLat), ref southwestLat, value);
        }
        [DbType("decimal(20, 8)")]
        [XafDisplayName("Güneybatı Sınırı Boylam")]
        public double SouthwestLng
        {
            get => southwestLat;
            set => SetPropertyValue(nameof(SouthwestLng), ref southwestLng, value);
        }

        [Association("Sehirler-Ilceler")]
        [XafDisplayName("Şehir")]
        public Sehirler SehirId
        {
            get => sehirId;
            set => SetPropertyValue(nameof(SehirId), ref sehirId, value);
        }
        [Association("Ilceler-CariAdres")]
        public XPCollection<CariAdresBilgileri> CariAdresBilgileri =>
            GetCollection<CariAdresBilgileri>(nameof(CariAdresBilgileri));

        [Association("Ilceler-PersonelAdres")]
        public XPCollection<PersonelAdresBilgileri> PersonelAdresBilgileri =>
            GetCollection<PersonelAdresBilgileri>(nameof(PersonelAdresBilgileri));
    }
}