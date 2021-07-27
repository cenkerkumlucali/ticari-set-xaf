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
    [ListViewFilter("Tüm Liste", "")]
    [ListViewFilter("Aktif Hesaplar", "[Durum] == true", true)]
    [ListViewFilter("Pasif Hesaplar", "[Durum] == false")]
    [DefaultProperty("Tanim")]
    
    public class Cariler : BaseObject
    { 
        public Cariler(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            Durum = true;
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        double indirimOran;
        bool indirim;
        bool durum;
        string email;
        string telefon;
        string ulke;
        string sehir;
        string adres;
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

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Adres
        {
            get => adres;
            set => SetPropertyValue(nameof(Adres), ref adres, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Sehir
        {
            get => sehir;
            set => SetPropertyValue(nameof(Sehir), ref sehir, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Ulke
        {
            get => ulke;
            set => SetPropertyValue(nameof(Ulke), ref ulke, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Telefon
        {
            get => telefon;
            set => SetPropertyValue(nameof(Telefon), ref telefon, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Email
        {
            get => email;
            set => SetPropertyValue(nameof(Email), ref email, value);
        }

        public bool Durum
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
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double Bakiye => Alacak - Borc;

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
        [Association("Cariler-AlisSatisIslem")]
        public XPCollection<Fisler> AlisSatisIslem => GetCollection<Fisler>(nameof(AlisSatisIslem));

        [Association("Cariler-ParasalHareket")]
        public XPCollection<KasaHareket> ParasalHareket => GetCollection<KasaHareket>(nameof(ParasalHareket));

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