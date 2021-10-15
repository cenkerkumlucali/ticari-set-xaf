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
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Department")]
    [DefaultProperty("AdSoyad")]

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

        private string kod;
        private string ad;
        private string ad2;
        private string soyad;
        private Cinsiyet cinsiyet;
        private DateTime iseGirisTarihi;
        private string kartNumarasi;
        private DateTime sgkBaslamaTarihi;
        private DurumType durum;
        private byte[] fotograf;

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
            set => SetPropertyValue(nameof(Ad), ref ad,value);
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
        public String AdSoyad => $"{Ad} {Ad2} {Soyad}";

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

        [Association("Personel-IletisimBilgileri")]
        [XafDisplayName("İletişim Bilgileri")]
        public XPCollection<PersonelIletisimBilgileri> PersonelIletisimBilgileri =>
            GetCollection<PersonelIletisimBilgileri>(nameof(PersonelIletisimBilgileri));

        protected override void OnSaving()
        {
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