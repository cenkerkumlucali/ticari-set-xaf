using System;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [CreatableItem(false)]
    [DefaultProperty("Kod")]
  
    public class KasaHareket : BaseObject
    { 
        public KasaHareket(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        string aciklama;
        DateTime tarih;
        double tutar;
        KasaHareketType hareket;
        Cariler cariID;
        Kasalar kasaID;
        string kod;

        [VisibleInDetailView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }
        [Association("Kasalar-KasaHareketleri")]
        [XafDisplayName("Kasa")]
        public Kasalar KasaID
        {
            get => kasaID;
            set => SetPropertyValue(nameof(KasaID), ref kasaID, value);
        }
        [Association("Cariler-ParasalHareket")]
        [XafDisplayName("Cari Hesap")]
        public Cariler CariID
        {
            get => cariID;
            set => SetPropertyValue(nameof(CariID), ref cariID, value);
        }
        public KasaHareketType Hareket
        {
            get => hareket;
            set => SetPropertyValue(nameof(Hareket), ref hareket, value);
        }

        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double Tutar
        {
            get => tutar;
            set => SetPropertyValue(nameof(Tutar), ref tutar, value);
        }
        [ModelDefault("DisplayFormat", "dd.MM.yyyy HH:mm:ss")]
        [ModelDefault("EditFormat", "dd.MM.yyyy HH:mm:ss")]
        public DateTime Tarih
        {
            get => tarih;
            set => SetPropertyValue(nameof(Tarih), ref tarih, value);
        }
        
        [Size(SizeAttribute.Unlimited)]
        public string Aciklama
        {
            get => aciklama;
            set => SetPropertyValue(nameof(Aciklama), ref aciklama, value);
        }
        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork
                  && (Session.DataLayer != null)
                  && Session.IsNewObject(this))
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "KasaHareketServerPrefix");
                Kod = string.Format("{0:D7}", deger);
            }
            base.OnSaving();
        }
    }
}