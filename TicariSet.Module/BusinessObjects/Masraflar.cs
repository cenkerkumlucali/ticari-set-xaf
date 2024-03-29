﻿using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("Tanim")]
    [ImageName("CostAnalysis")]
    public class Masraflar : XPObject
    { 
        public Masraflar(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        MasrafGrubu grupID;
        string tanim;
        string kod;

        [Size(32)]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }

        [Size(128)]
        [RuleRequiredField]
        public string Tanim
        {
            get => tanim;
            set => SetPropertyValue(nameof(Tanim), ref tanim, value);
        }

        [Association("MasrafGrubu-Masraf")]
        [XafDisplayName("Grup")]
        [RuleRequiredField]
        public MasrafGrubu GrupID
        {
            get => grupID;
            set => SetPropertyValue(nameof(GrupID), ref grupID, value);
        }

        [Association("Masraflar-MasrafDetay")]
        public XPCollection<KasaMasrafOdeme> MasrafDetay => GetCollection<KasaMasrafOdeme>(nameof(MasrafDetay));

        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork
                  && (Session.DataLayer != null)
                  && Session.IsNewObject(this))
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "MasrafServerPrefix");
                Kod = string.Format("MS{0:D7}", deger);
            }
            base.OnSaving();
        }
    }
}