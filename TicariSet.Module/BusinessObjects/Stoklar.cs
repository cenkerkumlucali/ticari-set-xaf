using System;
using System.ComponentModel;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Product_Group")]
    [DefaultProperty("Tanim")]
    [ListViewFilter("Tüm Liste", "")]
    [ListViewFilter("Aktif Stoklar", "[Durum] == false", true)]
    [ListViewFilter("Pasif Stoklar", "[Durum] == true")]
    [Appearance("RedPriceObject", AppearanceItemType = "ViewItem", TargetItems = "*", Criteria = "Kalan<1000", Context = "ListView", BackColor = "Yellow", FontColor = "Maroon", Priority = 2)]


    #region RuleIsReferenced
    [RuleIsReferenced("RIR-Stoklar.StokHareketler", DefaultContexts.Delete, typeof(StokHareketler), "StokID", InvertResult = true, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, MessageTemplateMustBeReferenced = "{TargetObject} nesne referans alınmamalıdır.")]
    [RuleIsReferenced("RIR-Stoklar.StKartRenk", DefaultContexts.Delete, typeof(StKartRenk), "StokId", InvertResult = true, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, MessageTemplateMustBeReferenced = "{TargetObject} nesne referans alınmamalıdır.")]
    [RuleIsReferenced("RIR-Stoklar.StFiyat", DefaultContexts.Delete, typeof(StFiyat), "StokID", InvertResult = true, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, MessageTemplateMustBeReferenced = "{TargetObject} nesne referans alınmamalıdır.")]
    #endregion

        
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
         GenelTipTanimlari kartTipi;

        [VisibleInDetailView(false)]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }
        [DataSourceCriteria(@"ObjectName = 'Stoklar'")]
        public GenelTipTanimlari KartTipi
        {
            get => kartTipi;
            set => SetPropertyValue(nameof(KartTipi), ref kartTipi, value);
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
        [RuleUniqueValue("RUV-Barkod.01",DefaultContexts.Save,CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
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
        [RuleUniqueValue("RUV-StKartRenk.01",DefaultContexts.Save,
            CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction,TargetPropertyName = "RenkTanimId")]
        public XPCollection<StKartRenk> StKartRenk=> GetCollection<StKartRenk>(nameof(StKartRenk));

        [Association("Stoklar-StokHareket")]
        public XPCollection<StokHareketler> StokHareket => GetCollection<StokHareketler>(nameof(StokHareket));

        [VisibleInDetailView(false)]
        public double Giren => StokHareket.Where(c => c.Hareket == EnumObjects.StokHareketType.Giris).Sum(c => c.Miktar);

        [VisibleInDetailView(false)]
        public double Cikan => StokHareket.Where(c => c.Hareket == EnumObjects.StokHareketType.Cikis).Sum(c => c.Miktar);

        [VisibleInDetailView(false)]
        public double Kalan => Giren - Cikan;

        

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