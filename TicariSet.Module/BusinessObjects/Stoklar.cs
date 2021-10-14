using System;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Linq;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("Tanim")]
    [ListViewFilter("Tüm Liste", "")]
    [ListViewFilter("Aktif Stoklar", "[Durum] == false", true)]
    [ListViewFilter("Pasif Stoklar", "[Durum] == true")]
    public class Stoklar : BaseObject
    {
        public Stoklar(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Birimler birim = Session.FindObject<Birimler>(new BinaryOperator(nameof(birim.Varsayilan), true));
            if (birim != null)
                BirimID = birim;
            Durum = DurumType.Aktif;
        }

        DurumType durum;
        double vergiOrani;
        Birimler birimID;
        string tanim;
        string kod;
        byte[] fotograf;
        DateTime olusturulmaTarihi;
        string barkod;

        [VisibleInDetailView(false)]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }

        [Size(256)]
        public string Tanim
        {
            get => tanim;
            set => SetPropertyValue(nameof(Tanim), ref tanim, value);
        }
        [XafDisplayName("Birim")]
        public Birimler BirimID
        {
            get => birimID;
            set => SetPropertyValue(nameof(BirimID), ref birimID, value);
        }

        public double VergiOrani
        {
            get => vergiOrani;
            set => SetPropertyValue(nameof(VergiOrani), ref vergiOrani, value);
        }
        public DurumType Durum
        {
            get => durum;
            set => SetPropertyValue(nameof(Durum), ref durum, value);
        }

        [ImageEditor]
        public byte[] Fotograf
        {
            get => fotograf;
            set => SetPropertyValue(nameof(Fotograf), ref fotograf, value);
        }

        [DbType("Date")]
        public DateTime OlusturulmaTarihi   
        {
            get => olusturulmaTarihi;
            set => SetPropertyValue(nameof(OlusturulmaTarihi), ref olusturulmaTarihi, value);
        }

        [Size(32)]
        [VisibleInListView(false)]
        public string Barkod
        {
            get => barkod;
            set => SetPropertyValue(nameof(Barkod), ref barkod, value);
        }
        [Association("Stoklar-Fiyatlar")]
        [XafDisplayName("Fiyatlar")]
        public XPCollection<StFiyat> StFiyatId => GetCollection<StFiyat>(nameof(StFiyatId));

        [XafDisplayName("Renkler")]
        [Association("Stoklar-Renkler"),DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<StKartRenk> StKartRenk=> GetCollection<StKartRenk>(nameof(StKartRenk));

        [VisibleInDetailView(false)]
        public double Giren => StokHareket.Where(c => c.Hareket == EnumObjects.StokHareketType.Giris).Sum(c => c.Miktar);

        [VisibleInDetailView(false)]
        public double Cikan => StokHareket.Where(c => c.Hareket == EnumObjects.StokHareketType.Cikis).Sum(c => c.Miktar);

        [VisibleInDetailView(false)]
        public double Kalan => Giren - Cikan;

        [Association("Stoklar-StokHareket")]
        public XPCollection<StokHareketler> StokHareket => GetCollection<StokHareketler>(nameof(StokHareket));

        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork
                  && (Session.DataLayer != null)
                    && Session.IsNewObject(this))
                         && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "StoklarServerPrefix");
                Kod = string.Format("{0:D7}", deger);
            }
            base.OnSaving();
        }
    }
}