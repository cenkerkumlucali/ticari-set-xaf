﻿using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("Tanim")]
    [ListViewFilter("Tüm Liste", "")]
    [ListViewFilter("Aktif Birimler", "[Durum] == false", true)]
    [ListViewFilter("Pasif Birimler", "[Durum] == true")]
    [RuleCombinationOfPropertiesIsUnique("TanimRule",DefaultContexts.Save,"Tanim",messageTemplate:"Girilen birim zaten mevcuttur.")]
    public class Birimler : XPObject
    {
        public Birimler(Session session)
           : base(session)
        {
        }
        public override void AfterConstruction()
        {

            base.AfterConstruction();
            Durum = DurumType.Aktif;
        }

        BirimSetiTanimlari bSetiTanimlari;
        bool varsayilan;
        DurumType durum;
        string tanim;
        string kod;

        [VisibleInDetailView(false)]
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

        public DurumType Durum
        {
            get => durum;
            set => SetPropertyValue(nameof(Durum), ref durum, value);
        }
        [Association("BirimSetiTanimlari-Birimler")]
        [XafDisplayName("Birim Seti")]
        public BirimSetiTanimlari BSetiTanimlari
        {
            get => bSetiTanimlari;
            set => SetPropertyValue(nameof(BSetiTanimlari), ref bSetiTanimlari, value);
        }
        public bool Varsayilan
        {
            get => varsayilan;
            set => SetPropertyValue(nameof(Varsayilan), ref varsayilan, value);
        }
        protected override void OnSaving()
        {

            if (Varsayilan)
            {
                Birimler birim = Session.FindObject<Birimler>(new BinaryOperator(nameof(birim.Varsayilan), true));
                if (birim != null)
                {
                    birim.varsayilan = false;
                    birim.Save();
                }
            }
            base.OnSaving();
        }
    }
}