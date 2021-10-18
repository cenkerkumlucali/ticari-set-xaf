using System;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using TicariSet.Module.BusinessObjects.Tanım;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects.Stok
{
    [DefaultClassOptions]
    [DefaultProperty("FisID.Kod")]
    public class StokHareketler : XPObject
    { 
        public StokHareketler(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            Tarih = DateTime.Now;
            Miktar = 1;
            base.AfterConstruction();
        }


        string aciklama;
        Fisler.Fisler fisID;
        double genelTutar;
        double vergiTutar;
        double vergiOran;
        double netTutar;
        double indirimTutar;
        double indirimOran;
        double toplam;
        double birimFiyat;
        Birimler birimID;
        int miktar;
        Stoklar stokID;
        StokHareketType hareket;
        DateTime tarih;

        [ModelDefault("DisplayFormat", "dd.MM.yyyy HH:mm:ss")]
        [ModelDefault("EditFormat", "dd.MM.yyyy HH:mm:ss")]
        public DateTime Tarih
        {
            get => tarih;
            set => SetPropertyValue(nameof(Tarih), ref tarih, value);
        }

        public StokHareketType Hareket
        {
            get
            {
                if (fisID?.HareketTipi == StokHareketType.Giris)
                    return hareket = StokHareketType.Giris;
                else
                    return hareket = StokHareketType.Cikis;
            }
            set => SetPropertyValue(nameof(Hareket), ref hareket, value);
        }
        [XafDisplayName("Ürün")]
        [Association("Stoklar-StokHareket")]
        public Stoklar StokID
        {
            get => stokID;
            set
            {
                if (SetPropertyValue(nameof(StokID), ref stokID, value))
                {
                    BirimID = StokID?.BirimID;
                    if (Hareket == StokHareketType.Giris)
                    {
                        if (StokID?.StFiyatId != null)
                            foreach (var stFiyat in StokID?.StFiyatId)
                            {
                                if (stFiyat.FiyatGrubu == FisHareketType.Alis)
                                {
                                    BirimFiyat = stFiyat.Fiyat;
                                }
                            }
                    }
               
                    if (StokID != null) VergiOran = StokID.VergiOrani;
                }
            }
        }

        public int Miktar
        {
            get => miktar;
            set
            {
                if (SetPropertyValue(nameof(Miktar), ref miktar, value))
                {
                    //    if (StokID.Kalan >= 0)
                    //    {
                    //        if (Miktar >= StokID.Kalan)
                    //        {
                    //            Miktar = StokID.Kalan;
                    //            HesaplaToplam();
                    //        }
                    //    }
                    //    else
                    //    {
                    HesaplaToplam();
                    //}

                }
            }
        }

        [XafDisplayName("Birim")]
        public Birimler BirimID
        {
            get => birimID;
            set => SetPropertyValue(nameof(BirimID), ref birimID, value);
        }
        [ModelDefault("DisplayFormat", "c1")]
        [ModelDefault("EditFormat", "c1")]
        public double BirimFiyat
        {
            get => birimFiyat;
            set
            {
                if (SetPropertyValue(nameof(BirimFiyat), ref birimFiyat, value))
                    HesaplaToplam();
            }
        }
        [ModelDefault("DisplayFormat", "c1")]
        [ModelDefault("EditFormat", "c1")]
        public double Toplam
        {
            get => toplam;
            set => SetPropertyValue(nameof(Toplam), ref toplam, value);
        }

        public double IndirimOran
        {
            get => indirimOran;
            set
            {
                if (SetPropertyValue(nameof(IndirimOran), ref this.indirimOran, value))
                    HesaplaToplam();
            }
        }
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double IndirimTutar
        {
            get => indirimTutar;
            set => SetPropertyValue(nameof(IndirimTutar), ref this.indirimTutar, value);
        }

        public double NetTutar
        {
            get => netTutar;
            set => SetPropertyValue(nameof(NetTutar), ref this.netTutar, value);
        }

        public double VergiOran
        {
            get => vergiOran;
            set => SetPropertyValue(nameof(VergiOran), ref this.vergiOran, value);
        }
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double VergiTutar
        {
            get => vergiTutar;
            set
            {
                if (SetPropertyValue(nameof(VergiTutar), ref this.vergiTutar, value))
                    HesaplaToplam();
            }
        }
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double GenelTutar
        {
            get => genelTutar;
            set => SetPropertyValue(nameof(GenelTutar), ref this.genelTutar, value);
        }

        [Association("Fisler-Detay")]
        [XafDisplayName("Fiş Kodu")]
        public Fisler.Fisler FisID
        {
            get => fisID;
            set
            {
                Fisler.Fisler eskiFis = fisID;
                bool degistimi = SetPropertyValue(nameof(FisID), ref fisID, value);
                if (!IsLoading && !IsSaving && !IsDeleted && eskiFis != fisID && degistimi)
                {
                    eskiFis = eskiFis ?? fisID;
                    eskiFis.HesaplaAltToplam(true);
                    eskiFis.HesaplaGenelToplam(true);
                    eskiFis.HesaplaIndirimToplam(true);
                    eskiFis.HesaplaVergiToplam(true);
                    if (FisID.CariID.Indirim != null)
                        IndirimOran = FisID.CariID.IndirimOran;
                }
            }
        }

        protected override void OnDeleting()
        {
            Fisler.Fisler eskiFis = fisID;
            base.OnDeleting();

        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Aciklama
        {
            get => aciklama;
            set => SetPropertyValue(nameof(Aciklama), ref this.aciklama, value);
        }
        void HesaplaToplam()
        {
            toplam = miktar * birimFiyat;
            indirimTutar = (toplam / 100) * indirimOran;
            netTutar = toplam - indirimTutar;
            vergiTutar = (netTutar / 100) * vergiOran;
            genelTutar = netTutar + vergiTutar;
            if (FisID != null)
            {
                FisID.HesaplaAltToplam(true);
                FisID.HesaplaGenelToplam(true);
                FisID.HesaplaIndirimToplam(true);
                FisID.HesaplaVergiToplam(true);
            }
        }


    }
}