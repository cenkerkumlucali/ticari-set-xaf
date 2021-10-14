using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.Editors;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("StokID")]
    public class StFiyat : BaseObject
    { 
        public StFiyat(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private Stoklar stokID;
        private double fiyat;
        private Doviz dovizID;
        private DateTime baslamaTarihi;
        private DateTime bitisTarihi;
        private DurumType durum;
        private FisHareketType fiyatGrubu;
        string _ChCriteria;

        [Association("Stoklar-Fiyatlar")]
        [XafDisplayName("Stok")]
        public Stoklar StokID
        {
            get => stokID;
            set => SetPropertyValue(nameof(StokID), ref stokID, value);
        }
        public double Fiyat
        {
            get => fiyat;
            set => SetPropertyValue(nameof(Fiyat), ref fiyat, value);
        }
        [XafDisplayName("Döviz")]
        public Doviz DovizID
        {
            get => dovizID;
            set => SetPropertyValue(nameof(DovizID), ref dovizID, value);
        }
        [XafDisplayName("Başlama Tarihi")]
        public DateTime BaslamaTarihi
        {
            get => baslamaTarihi;
            set => SetPropertyValue(nameof(BaslamaTarihi),ref baslamaTarihi, value);
        }
        [XafDisplayName("Bitiş Tarihi")]
        public DateTime BitisTarihi
        {
            get => bitisTarihi;
            set => SetPropertyValue(nameof(BitisTarihi), ref bitisTarihi, value);
        }

        public DurumType Durum
        {
            get => durum;
            set => SetPropertyValue(nameof(Durum), ref durum, value);
        }

        public FisHareketType FiyatGrubu
        {
            get => fiyatGrubu;
            set => SetPropertyValue(nameof(FiyatGrubu), ref fiyatGrubu, value);
        }
        [MemberDesignTimeVisibility(false)]
        public Type ObjectName => typeof(Cariler);

        [XafDisplayName("Kriter")]
        [Size(SizeAttribute.Unlimited), ObjectValidatorIgnoreIssue(typeof(ObjectValidatorLargeNonDelayedMember))]
        [EditorAlias(EditorAliases.PopupCriteriaPropertyEditor)]
        [CriteriaOptions("ObjectName")]
        [ModelDefault("RowCount", "0")]
        public string ChCriteria
        {
            get { return _ChCriteria; }
            set { SetPropertyValue(nameof(ChCriteria), ref _ChCriteria, value); }
        }
    }
}