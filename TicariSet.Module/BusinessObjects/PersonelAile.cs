using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Department")]
    [DefaultProperty("PrsYakınlık")]
    [CreatableItem(false)]
    public class PersonelAile : BaseObject
    { 
        public PersonelAile(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        string kod;
        string ad;
        string soyad;
        PrsYakınlık prsYakınlık;
        Cinsiyet cinsiyet;
        string meslegi;
        PersonelKart personelId;

        [Size(32)]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }

        [Size(32)]
        public string Ad
        {
            get => ad;
            set => SetPropertyValue(nameof(Ad), ref ad,value);
        }

        [Size(32)]
        public string Soyad
        {
            get => soyad;
            set => SetPropertyValue(nameof(Soyad), ref soyad, value);
        }

        [Calculated("Ad + ' ' + Soyad")] public string AdSoyad => $"{Ad} {Soyad}";
        [XafDisplayName("Yakınlık")]

        public PrsYakınlık PrsYakınlık
        {
            get => prsYakınlık;
            set => SetPropertyValue(nameof(PrsYakınlık), ref prsYakınlık, value);
        }

        public Cinsiyet Cinsiyet
        {
            get => cinsiyet;
            set => SetPropertyValue(nameof(Cinsiyet), ref cinsiyet, value);
        }

        [Size(32)]
        public string Meslegi
        {
            get => meslegi;
            set => SetPropertyValue(nameof(Meslegi), ref meslegi, value);
        }
        [Association("Personel-Aile")]
        [XafDisplayName("Personel")]
        public PersonelKart PersonelId
        {
            get => personelId;
            set => SetPropertyValue(nameof(PersonelId), ref personelId, value);
        }
        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork
                  && (Session.DataLayer != null)
                  && Session.IsNewObject(this))
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "PersonelAileServerPrefix");
                Kod = string.Format("PRSA{0:D3}", deger);
            }
            base.OnSaving();
        }
    }
}