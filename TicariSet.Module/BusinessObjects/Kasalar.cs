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
using DevExpress.ExpressApp.SystemModule;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [DefaultProperty("Tanim")]
    [ListViewFilter("Tüm Liste", "")]
    [ListViewFilter("Aktif Kasalar", "[Durum] == true", true)]
    [ListViewFilter("Pasif Kasalar", "[Durum] == false")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Kasalar : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Kasalar(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Durum = true;
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        bool durum;
        string aciklama;
        string tanim;
        string kod;
        [VisibleInDetailView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Tanim
        {
            get => tanim;
            set => SetPropertyValue(nameof(Tanim), ref tanim, value);
        }

        [Size(SizeAttribute.Unlimited)]
        public string Aciklama
        {
            get => aciklama;
            set => SetPropertyValue(nameof(Aciklama), ref aciklama, value);
        }

        public bool Durum
        {
            get => durum;
            set => SetPropertyValue(nameof(Durum), ref durum, value);
        }
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double Tahsilat => KasaHareketleri.Where(c => c.Hareket == KasaHareketType.Tahsilat).Sum(c => c.Tutar);
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double Odeme => KasaHareketleri.Where(c => c.Hareket == KasaHareketType.Odeme).Sum(c => c.Tutar);
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double Bakiye => Tahsilat - Odeme;
        [Association("Kasalar-KasaHareketleri")]
        public XPCollection<KasaHareket> KasaHareketleri
        {
            get
            {
                return GetCollection<KasaHareket>(nameof(KasaHareketleri));
            }
        }
        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork
                  && (Session.DataLayer != null)
                  && Session.IsNewObject(this))
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "KasalarServerPrefix");
                Kod = string.Format("{0:D7}", deger);
            }
            base.OnSaving();
        }
    }
}