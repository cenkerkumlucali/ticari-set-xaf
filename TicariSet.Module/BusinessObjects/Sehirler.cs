﻿using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("MapIt")]
    [DefaultProperty("Ad")]
    [CreatableItem(false)]

    [RuleIsReferenced("", DefaultContexts.Delete, typeof(Ilceler), "SehirId", InvertResult = true,CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, MessageTemplateMustBeReferenced = "{TargetObject} nesne referans alınmamalıdır.")]
    [RuleIsReferenced("RIR-Sehirler.01", DefaultContexts.Delete, typeof(Cariler), "SehirId", InvertResult = true, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction,MessageTemplateMustBeReferenced = "{TargetObject} nesne referans alınmamalıdır.")]
    [RuleCombinationOfPropertiesIsUnique("RCOPIU-Sehirler.01",DefaultContexts.Save, 
        "Lat,Lng,NortheastLat,NortheastLng,SouthwestLat,SouthwestLng", messageTemplate: "Bu kayıt zaten mevcuttur.")]

    public class Sehirler : XPObject
    { 
        public Sehirler(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string ad;
        private double lat;
        private double lng;
        private double northeastLat;
        private double northeastLng;
        private double southwestLat;
        private double southwestLng;

        [Size(64)]
        [RuleUniqueValue("RUV-Sehirler.01", DefaultContexts.Save)]
        public string Ad
        {
            get => ad;
            set => SetPropertyValue(nameof(Ad), ref ad, value);
        }
        [DbType("decimal(20, 8)")]
        [XafDisplayName("Enlem")]
        [RuleUniqueValue("RUV-Sehirler.02", DefaultContexts.Save)]
        [NonCloneable]
        public double Lat
        {
            get => lat;
            set => SetPropertyValue(nameof(Lat), ref lat, value);
        }

        [DbType("decimal(20, 8)")]
        [XafDisplayName("Boylam")]
        [RuleUniqueValue("RUV-Sehirler.03",DefaultContexts.Save)]
        [NonCloneable]
        public double Lng
        {
            get => lng;
            set => SetPropertyValue(nameof(Lng), ref lng, value);
        }
        [DbType("decimal(20, 8)")]
        [RuleUniqueValue("RUV-Sehirler.04", DefaultContexts.Save)]
        [NonCloneable]
        public double NortheastLat
        {
            get => northeastLat;
            set => SetPropertyValue(nameof(NortheastLat), ref northeastLat, value);
        }
        [DbType("decimal(20, 8)")]
        [RuleUniqueValue("RUV-Sehirler.05", DefaultContexts.Save)]
        [NonCloneable]
        public double NortheastLng
        {
            get => northeastLng;
            set => SetPropertyValue(nameof(NortheastLng), ref northeastLng, value);
        }
        [DbType("decimal(20, 8)")]
        [RuleUniqueValue("RUV-Sehirler.06", DefaultContexts.Save)]
        [NonCloneable]
        public double SouthwestLat
        {
            get => southwestLat;
            set => SetPropertyValue(nameof(SouthwestLat), ref southwestLat, value);
        }
        [DbType("decimal(20, 8)")]
        [RuleUniqueValue("RUV-Sehirler.07", DefaultContexts.Save)]
        [NonCloneable]
        public double SouthwestLng
        {
            get => southwestLat;
            set => SetPropertyValue(nameof(SouthwestLng), ref southwestLng, value);
        }
        [Association("Sehirler-Ilceler")]
        public XPCollection<Ilceler> Ilceler => GetCollection<Ilceler>(nameof(Ilceler));

        [Association("Sehirler-CariAdres")]
        public XPCollection<CariAdresBilgileri> CariAdresBilgileri =>
            GetCollection<CariAdresBilgileri>(nameof(CariAdresBilgileri));

        [Association("Sehirler-PersonelAdres")]
        public XPCollection<PersonelAdresBilgileri> PersonelAdresBilgileri =>
            GetCollection<PersonelAdresBilgileri>(nameof(PersonelAdresBilgileri));

    }
}