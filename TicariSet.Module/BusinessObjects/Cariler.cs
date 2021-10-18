using System.ComponentModel;
using System.Linq;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Customer")]
    [DefaultProperty("Tanim")]
    [RuleCombinationOfPropertiesIsUnique("RCOPIU-CariTanim", DefaultContexts.Save, "Tanim,KısaAd", messageTemplate: "Cari hesap zaten mevcut")]
    
    [Appearance("CariIndirim", Criteria = "Indirim!=true", TargetItems = "IndirimOran", Enabled = false)]
    
    #region ListViewFilter
    [ListViewFilter("Tüm Liste", "")]
    [ListViewFilter("Aktif Hesaplar", "[Durum] == false", true)]
    [ListViewFilter("Pasif Hesaplar", "[Durum] == true")]
    #endregion
    
    #region RuleIsReferenced
    [RuleIsReferenced("RIR-Cariler.BankaHesapları", DefaultContexts.Delete, typeof(BankaHesaplari), "CariId", InvertResult = true, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, MessageTemplateMustBeReferenced = "{TargetObject} nesne referans alınmamalıdır.")]
    [RuleIsReferenced("RIR-Cariler.AdresBilgileri", DefaultContexts.Delete, typeof(BankaHesaplari), "CariId", InvertResult = true, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, MessageTemplateMustBeReferenced = "{TargetObject} nesne referans alınmamalıdır.")]
    [RuleIsReferenced("RIR-Cariler.CariIletisimBilgileri", DefaultContexts.Delete, typeof(CariIletisimBilgileri), "CariId", InvertResult = true, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, MessageTemplateMustBeReferenced = "{TargetObject} nesne referans alınmamalıdır.")]
    [RuleIsReferenced("RIR-Cariler.Fisler", DefaultContexts.Delete, typeof(Fisler), "CariID", InvertResult = true, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, MessageTemplateMustBeReferenced = "{TargetObject} nesne referans alınmamalıdır.")]
    [RuleIsReferenced("RIR-Cariler.KasaHareket", DefaultContexts.Delete, typeof(KasaHareket), "CariID", InvertResult = true, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, MessageTemplateMustBeReferenced = "{TargetObject} nesne referans alınmamalıdır.")]
    #endregion

    //[RuleCriteria("", DefaultContexts.Save, "Durum != 1","Durum pasif olamaz", SkipNullOrEmptyValues = false)]
    public class Cariler : BaseObject
    {
        public const string CDC_OBJNAME_GNTIP = @"ObjectName = 'Cariler' ";
        public Cariler(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            Durum = 0;
            base.AfterConstruction();
        }

        GenelTipTanimlari kartTipi;
        string kısaAd;
        string digerAd;
        string yabanciAd;
        string aciklama;
        double indirimOran;
        bool indirim;
        DurumType durum;
        string tanim;
        string kod;
        byte[] fotograf;

        [VisibleInDetailView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }
        
        [DataSourceCriteria(CDC_OBJNAME_GNTIP)]
        public GenelTipTanimlari KartTipi
        {
            get => kartTipi;
            set => SetPropertyValue(nameof(KartTipi), ref kartTipi, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        [RuleUniqueValue("RUV-Tanim", DefaultContexts.Save)]
        public string Tanim
        {
            get => tanim;
            set => SetPropertyValue(nameof(Tanim), ref tanim, value);
        }

        [ImageEditor]
        public byte[] Fotograf
        {
            get => fotograf;
            set => SetPropertyValue(nameof(Fotograf), ref fotograf, value);
        }

        [Size(16)]
        public string KısaAd
        {
            get => kısaAd;
            set => SetPropertyValue(nameof(KısaAd), ref kısaAd, value);
        }

        [Size(32)]
        public string DigerAd
        {
            get => digerAd;
            set => SetPropertyValue(nameof(DigerAd), ref digerAd, value);
        }

        [Size(32)]
        public string YabanciAd
        {
            get => yabanciAd;
            set => SetPropertyValue(nameof(YabanciAd), ref yabanciAd, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Aciklama
        {
            get => aciklama;
            set => SetPropertyValue(nameof(Aciklama), ref aciklama, value);
        }

        //[Size(SizeAttribute.DefaultStringMappingFieldSize)]
        //[RuleRequiredField]
        //public string Telefon
        //{
        //    get => telefon;
        //    set => SetPropertyValue(nameof(Telefon), ref telefon, value);
        //}
        //[Size(SizeAttribute.DefaultStringMappingFieldSize)]
        //[RuleRegularExpression("RRE-Email.01", DefaultContexts.Save,
        //    @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
        //    messageTemplate:"Email formatınız yanlış lütfen kontrol edip tekrardan deneyin.")]
        //[RuleUniqueValue("RUV-Email.01", DefaultContexts.Save,CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        //public string Email
        //{
        //    get => email;
        //    set => SetPropertyValue(nameof(Email), ref email, value);
        //}

        public DurumType Durum
        {
            get => durum;
            set => SetPropertyValue(nameof(Durum), ref durum, value);
        }

        [VisibleInDetailView(false)]
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double Alacak
        {
            get
            {
                double temp = 0;
                temp += AlisSatisIslem.Where(c => c.Turu == FisHareketType.Alis).Sum(c => c.GenelToplam);
                temp += ParasalHareket.Where(c => c.Hareket == KasaHareketType.Tahsilat).Sum(c => c.Tutar);
                return temp;
            }
        }

        [VisibleInDetailView(false)]
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double Borc
        {
            get
            {
                double temp = 0;
                temp += AlisSatisIslem.Where(c => c.Turu == FisHareketType.Satis).Sum(c => c.GenelToplam);
                temp += ParasalHareket.Where(c => c.Hareket == KasaHareketType.Odeme).Sum(c => c.Tutar);
                return temp;
            }
        }
        [VisibleInDetailView(false)]
        [ModelDefault("DisplayFormat", "c3")]
        [ModelDefault("EditFormat", "c3")]
        public double Bakiye => Borc - Alacak;
        [ImmediatePostData]
        public bool Indirim
        {
            get => indirim;
            set => SetPropertyValue(nameof(Indirim), ref indirim, value);
        }
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditFormat", "n2")]
        public double IndirimOran
        {
            get => indirimOran;
            set => SetPropertyValue(nameof(IndirimOran), ref indirimOran, value);
        }
        [Association("Cariler-AlisSatisIslem"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<Fisler> AlisSatisIslem => GetCollection<Fisler>(nameof(AlisSatisIslem));

        [Association("Cariler-ParasalHareket"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<KasaHareket> ParasalHareket => GetCollection<KasaHareket>(nameof(ParasalHareket));

        [Association("Cariler-CariIletisimBilgileri"), DevExpress.ExpressApp.DC.Aggregated]
        [XafDisplayName("İletişim Bilgileri")]
        public XPCollection<CariIletisimBilgileri> CariIletisimBilgileri => GetCollection<CariIletisimBilgileri>(nameof(CariIletisimBilgileri));

        [Association("Cariler-BankaHesaplari"), DevExpress.ExpressApp.DC.Aggregated]
        [XafDisplayName("Banka Hesapları")]
        public XPCollection<BankaHesaplari> BankaHesaplari => GetCollection<BankaHesaplari>(nameof(BankaHesaplari));

        [Association("Cari-Adres")]
        [XafDisplayName("Adres Bilgileri")]
        public XPCollection<CariAdresBilgileri> CariAdres =>
            GetCollection<CariAdresBilgileri>(nameof(CariAdres));

        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork
                  && (Session.DataLayer != null)
                  && Session.IsNewObject(this))
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "CarilerServerPrefix");
                Kod = string.Format("{0:D7}", deger);
            }
            base.OnSaving();
        }
    }
}