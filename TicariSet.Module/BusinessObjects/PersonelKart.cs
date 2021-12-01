using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Customer")]
    [DefaultProperty("AdSoyad")]
    [RuleCombinationOfPropertiesIsUnique("RCOPIU-AdSoyad", DefaultContexts.Save, "Ad,Soyad", messageTemplate: "Personel zaten mevcut")]

    #region Appearance
    [Appearance("PersonelAyrılma", Criteria = "AyrilmaTarihi==null", TargetItems = "AyrılmaNedeni", Enabled = false)]
    [Appearance("PersonelEhliyet", Criteria = "SurucuBelgesi!=true", TargetItems = "PersonelSurucuBelgesi", Enabled = false)]
    [Appearance("PersonelPasaport", Criteria = "Pasaport!=true", TargetItems = "PPasaportBilgileri", Enabled = false)]
    #endregion

    #region ListViewFilter
    [ListViewFilter("Tüm Personeller", "", isCurrentFilter: true)]
    [ListViewFilter("Aktif Personeller", "[Durum] == false")]
    [ListViewFilter("Pasif Personeller", "[Durum] == true")]
    #endregion

    #region RuleIsReferenced
    [RuleIsReferenced("RIR-PersonelKart.PersonelAile", DefaultContexts.Delete, typeof(PersonelAile), 
        "PersonelId", InvertResult = true, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, 
        MessageTemplateMustBeReferenced = "{TargetObject} nesne referans alınmamalıdır.")]
    [RuleIsReferenced("RIR-PersonelKart.PersonelAdres", DefaultContexts.Delete, typeof(PersonelAdresBilgileri),
        "PersonelId", InvertResult = true, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, 
        MessageTemplateMustBeReferenced = "{TargetObject} nesne referans alınmamalıdır.")]
    [RuleIsReferenced("RIR-PersonelKart.PersonelIletisim", DefaultContexts.Delete, typeof(PersonelIletisimBilgileri),
        "PersonelId", InvertResult = true, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, 
        MessageTemplateMustBeReferenced = "{TargetObject} nesne referans alınmamalıdır.")]
    [RuleIsReferenced("RIR-PersonelKart.PersonelKimlik", DefaultContexts.Delete, typeof(PersonelKimlikBilgileri),
        "PersonelId", InvertResult = true, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction,
        MessageTemplateMustBeReferenced = "{TargetObject} nesne referans alınmamalıdır.")]
    #endregion

    public class PersonelKart : BaseObject
    {
        public PersonelKart(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            IseGirisTarihi = DateTime.Now;
            base.AfterConstruction();
        }

        string ayrılmaNedeni;
        DateTime ayrilmaTarihi;
        bool pasaport;
        PersonelPasaportBilgileri pPasaportBilgileri;
        bool surucuBelgesi;
        string kod;
        string ad;
        string ad2;
        string soyad;
        Cinsiyet cinsiyet;
        DateTime iseGirisTarihi;
        string kartNumarasi;
        DateTime sgkBaslamaTarihi;
        DurumType durum;
        byte[] fotograf;
        PersonelKimlikBilgileri personelKimlikBilgileri;
        PersonelSurucuBelgesi personelSurucuBelgesi;

        [Size(32)]
        [VisibleInDetailView(false)]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }

        [Size(32)]
        [XafDisplayName("Adı")]
        [RuleRequiredField]
        public string Ad
        {
            get => ad;
            set => SetPropertyValue(nameof(Ad), ref ad, value);
        }
        [Size(32)]
        [XafDisplayName("İkinci Adı")]
        public string Ad2
        {
            get => ad2;
            set => SetPropertyValue(nameof(Ad2), ref ad2, value);
        }

        [Size(64)]
        [XafDisplayName("Soyadı")]
        [RuleRequiredField]
        public string Soyad
        {
            get => soyad;
            set => SetPropertyValue(nameof(Soyad), ref soyad, value);
        }

        [Calculated("Ad + ' ' + Ad2 + ' ' +Soyad")]
        [XafDisplayName("Adı Soyadı")]
        public string AdSoyad => $"{Ad} {Ad2} {Soyad}";

        public Cinsiyet Cinsiyet
        {
            get => cinsiyet;
            set => SetPropertyValue(nameof(Cinsiyet), ref cinsiyet, value);
        }

        [DbType("Date")]
        public DateTime IseGirisTarihi
        {
            get => iseGirisTarihi;
            set => SetPropertyValue(nameof(IseGirisTarihi), ref iseGirisTarihi, value);
        }

        [Size(32)]
        public string KartNumarası
        {
            get => kartNumarasi;
            set => SetPropertyValue(nameof(KartNumarası), ref kartNumarasi, value);
        }

        [DbType("Date")]
        public DateTime SgkBaslamaTarihi
        {
            get => sgkBaslamaTarihi;
            set => SetPropertyValue(nameof(SgkBaslamaTarihi), ref sgkBaslamaTarihi, value);
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
        [ImmediatePostData]
        public DateTime AyrilmaTarihi
        {
            get => ayrilmaTarihi;
            set => SetPropertyValue(nameof(AyrilmaTarihi), ref ayrilmaTarihi, value);
        }

        [XafDisplayName("Ayrılma Nedeni")]
        [Size(128)]
        public string AyrılmaNedeni
        {
            get => ayrılmaNedeni;
            set => SetPropertyValue(nameof(AyrılmaNedeni), ref ayrılmaNedeni, value);
        }

        [DevExpress.Xpo.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        [RuleRequiredField]
        [XafDisplayName("Kimlik Bilgileri")]
        public PersonelKimlikBilgileri PersonelKimlikBilgileri
        {
            get => personelKimlikBilgileri;
            set => SetPropertyValue(nameof(PersonelKimlikBilgileri), ref personelKimlikBilgileri, value);
        }

        [ImmediatePostData]
        public bool SurucuBelgesi
        {
            get => surucuBelgesi;
            set => SetPropertyValue(nameof(SurucuBelgesi), ref surucuBelgesi, value);
        }

        [DevExpress.Xpo.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        [XafDisplayName("Sürücü Belgesi")]
        public PersonelSurucuBelgesi PersonelSurucuBelgesi
        {
            get => personelSurucuBelgesi;
            set => SetPropertyValue(nameof(PersonelSurucuBelgesi), ref personelSurucuBelgesi, value);
        }
        [ImmediatePostData]
        public bool Pasaport
        {
            get => pasaport;
            set => SetPropertyValue(nameof(Pasaport), ref pasaport, value);
        }
        [DevExpress.Xpo.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        [XafDisplayName("Pasaport")]
        public PersonelPasaportBilgileri PPasaportBilgileri
        {
            get => pPasaportBilgileri;
            set => SetPropertyValue(nameof(PPasaportBilgileri), ref pPasaportBilgileri, value);
        }
        [Association("Personel-IletisimBilgileri")]
        [XafDisplayName("İletişim Bilgileri")]
        public XPCollection<PersonelIletisimBilgileri> PersonelIletisimBilgileri => GetCollection<PersonelIletisimBilgileri>(nameof(PersonelIletisimBilgileri));

        [Association("Personel-Aile")]
        public XPCollection<PersonelAile> PersonelAile => GetCollection<PersonelAile>(nameof(PersonelAile));

        [Association("Personel-Adres")]
        public XPCollection<PersonelAdresBilgileri> PersonelAdresBilgileri => GetCollection<PersonelAdresBilgileri>(nameof(PersonelAdresBilgileri));
        DateTime tarih = Convert.ToDateTime("01.01.2000");
        protected override void OnSaving()
        {
            Durum = AyrilmaTarihi > tarih ? DurumType.Pasif : DurumType.Aktif;

            if (!(Session is NestedUnitOfWork
                  && (Session.DataLayer != null)
                  && Session.IsNewObject(this))
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "PersonelKartServerPrefix");
                Kod = string.Format("PRSNL{0:D3}", deger);
            }
            base.OnSaving();
        }
    }
}