using System;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("Kod")]
    [CreatableItem(false)]
    public class Fisler : XPObject
    {
        public Fisler(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Tarih = DateTime.Now;
        }

        FisHareketType turu;
        StokHareketType hareketTipi;
        string aciklama;
        double genelToplam;
        double vergiToplam;
        double indirimToplam;
        double altToplam;
        Cariler cariID;
        DateTime tarih;
        string kod;

        [VisibleInDetailView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }
        [ModelDefault("DisplayFormat", "dd.MM.yyyy HH:mm:ss")]
        [ModelDefault("EditFormat", "dd.MM.yyyy HH:mm:ss")]
        [RuleRequiredField]
        public DateTime Tarih
        {
            get => tarih;
            set => SetPropertyValue(nameof(Tarih), ref tarih, value);
        }
        [XafDisplayName("Cari Hesap")]
        [Association("Cariler-AlisSatisIslem")]
        [RuleRequiredField]
        public Cariler CariID
        {
            get => cariID;
            set => SetPropertyValue(nameof(CariID), ref cariID, value);
        }
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double AltToplam
        {
            get
            {
                if (!IsLoading && !IsSaving)
                    HesaplaAltToplam(false);
                return altToplam;
            }
            set => SetPropertyValue(nameof(AltToplam), ref altToplam, value);
        }
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double IndirimToplam
        {
            get
            {
                if (!IsLoading && !IsSaving)
                    HesaplaIndirimToplam(false);
                return this.indirimToplam;
            }
            set => SetPropertyValue(nameof(IndirimToplam), ref indirimToplam, value);
        }
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double VergiToplam
        {
            get
            {
                if (!IsLoading && !IsSaving)
                    HesaplaVergiToplam(false);
                return this.vergiToplam;
            }
            set => SetPropertyValue(nameof(VergiToplam), ref vergiToplam, value);
        }
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double GenelToplam
        {
            get
            {
                if (!IsLoading && !IsSaving)
                    HesaplaGenelToplam(false);
                return this.genelToplam;
            }
            set => SetPropertyValue(nameof(GenelToplam), ref genelToplam, value);
        }

        [Size(500)] 
        public string Aciklama
        {
            get => aciklama;
            set => SetPropertyValue(nameof(Aciklama), ref aciklama, value);
        }
        [Association("Fisler-Detay")]
        public XPCollection<StokHareketler> Detay => GetCollection<StokHareketler>(nameof(Detay));

        [VisibleInDetailView(false), VisibleInListView(false)]
        public StokHareketType HareketTipi
        {
            get => hareketTipi;
            set => SetPropertyValue(nameof(HareketTipi), ref hareketTipi, value);
        }
        [VisibleInDetailView(false)]
        public FisHareketType Turu
        {
            get => turu;
            set => SetPropertyValue(nameof(Turu), ref turu, value);
        }

        public void HesaplaAltToplam(bool disposing)
        {
            double? eskiAltToplam = altToplam;
            double temp = 0;

            foreach (StokHareketler item in Detay)
            {
                temp += item.Toplam;
            }

            altToplam = temp;
            if (disposing)
                OnChanged(nameof(AltToplam), eskiAltToplam, altToplam);
        }
        public void HesaplaIndirimToplam(bool disposing)
        {
            double? eskiIndirimToplam = indirimToplam;
            double temp = 0;
            foreach (StokHareketler item in Detay)
            {
                temp += item.IndirimTutar;
            }
            indirimToplam = temp;
            if (disposing)
                OnChanged(nameof(IndirimToplam), eskiIndirimToplam, indirimToplam);
        }
        public void HesaplaVergiToplam(bool disposing)
        {
            double? eskiVergiToplam = vergiToplam;
            double temp = 0;
            foreach (StokHareketler item in Detay)
            {
                temp += item.VergiTutar;
            }
            vergiToplam = temp;
            if (disposing)
                OnChanged(nameof(VergiToplam), eskiVergiToplam, vergiToplam);
        }
        public void HesaplaGenelToplam(bool disposing)
        {
            double? eskiGenelToplam = genelToplam;
            double temp = 0;
            foreach (StokHareketler item in Detay)
            {
                temp += item.GenelTutar;
            }
            genelToplam = temp;
            if (disposing)
                OnChanged(nameof(GenelToplam), eskiGenelToplam, genelToplam);
        }
        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork
                  && (Session.DataLayer != null)
                      && Session.IsNewObject(this))
                        && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "FislerServerPrefix");
                Kod = string.Format("{0:D7}", deger);
            }

            
            base.OnSaving();
        }
    }
}