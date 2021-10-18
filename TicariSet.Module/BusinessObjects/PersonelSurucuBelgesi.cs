using System;
using System.ComponentModel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("Travel_Car")]
    [DefaultProperty("SbBelgeNo")]
    [Appearance("EhliyetSınıfı", Criteria = "IsNullOrEmpty(EhliyetSınıfı)", TargetItems = "SbBelgeNo,SbSeriNo,SbSehir,SbIlce,SbVerildigiTarih", Enabled = false)]
    public class PersonelSurucuBelgesi : BaseObject
    {
        public PersonelSurucuBelgesi(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        PersonelKart personelId;
        [XafDisplayName("Personel")]
        public PersonelKart PersonelId
        {
            get => personelId;
            set => SetPropertyValue(nameof(PersonelId), ref personelId, value);
        }

        private string kod;
        [Size(32)]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }

        private EhliyetSınıfı ehliyetSınıfı;
        [ImmediatePostData]
        public EhliyetSınıfı EhliyetSınıfı
        {
            get => ehliyetSınıfı;
            set => SetPropertyValue(nameof(EhliyetSınıfı), ref ehliyetSınıfı, value);
        }
        private string sbBelgeNo;
        [XafDisplayName("Sb.Belge No")]
        [Size(6)]
        public string SbBelgeNo
        {
            get => sbBelgeNo;
            set => SetPropertyValue(nameof(SbBelgeNo), ref sbBelgeNo, value);
        }
        private string sbSeriNo;
        [XafDisplayName("Sb.Seri No")]
        [Size(3)]
        public string SbSeriNo
        {
            get => sbSeriNo;
            set => SetPropertyValue(nameof(SbSeriNo), ref sbSeriNo, value);
        }
        private Sehirler sbSehir;
        [XafDisplayName("Sb.Sehir")]
        public Sehirler SbSehir
        {
            get => sbSehir;
            set => SetPropertyValue(nameof(SbSehir), ref sbSehir, value);
        }
        private Ilceler sbIlce;
        [XafDisplayName("Sb.İlçe")]
        public Ilceler SbIlce
        {
            get => sbIlce;
            set => SetPropertyValue(nameof(SbIlce), ref sbIlce, value);
        }
        private DateTime sbVerildigiTarih;

        [DbType("Date")]
        [XafDisplayName("Sb.Verildiği Tarih")]
        public DateTime SbVerildigiTarih
        {
            get => sbVerildigiTarih;
            set => SetPropertyValue(nameof(SbVerildigiTarih), ref sbVerildigiTarih, value);
        }
        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork
                  && (Session.DataLayer != null)
                  && Session.IsNewObject(this))
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "PersonelSurucuBelgesiServerPrefix");
                Kod = string.Format("PRSNLSRC{0:D4}", deger);
            }
            base.OnSaving();
        }
    }
}