using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("Actions_Home")]
    [DefaultProperty("AdresDetay")]
    public class PersonelAdresBilgileri : BaseObject
    { 
        public PersonelAdresBilgileri(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        private string kod;
        private string adresDetay;
        private Sehirler sehirId;
        private Ilceler ilceId;
        private Ulkeler ulkeId;
        private PersonelKart personelId;
        private AdresTipi adresTipi;

        [Size(256)]
        [XafDisplayName("Adres Detayı")]
        [RuleRequiredField]
        public string AdresDetay
        {
            get => adresDetay;
            set => SetPropertyValue(nameof(AdresDetay), ref adresDetay, value);
        }

        [Size(32)]
        [RuleRequiredField]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }
        [Association("Sehirler-PersonelAdres")]
        [XafDisplayName("Şehir")]
        public Sehirler SehirId
        {
            get => sehirId;
            set => SetPropertyValue(nameof(SehirId), ref sehirId, value);
        }

        [Association("Ilceler-PersonelAdres")]
        [XafDisplayName("İlçe")]
        public Ilceler IlceId
        {
            get => ilceId;
            set => SetPropertyValue(nameof(IlceId), ref ilceId, value);
        }

        [Association("Ulkeler-PersonelAdres")]
        [XafDisplayName("Ülke")]
        public Ulkeler UlkeId
        {
            get => ulkeId;
            set => SetPropertyValue(nameof(UlkeId), ref ulkeId, value);
        }
        [Association("Personel-Adres")]
        [XafDisplayName("Adres Bilgileri")]
        public PersonelKart PersonelId
        {
            get => personelId;
            set => SetPropertyValue(nameof(PersonelId), ref personelId, value);
        }

        public AdresTipi AdresTipi
        {
            get => adresTipi;
            set => SetPropertyValue(nameof(AdresTipi), ref adresTipi, value);
        }
        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork
                  && (Session.DataLayer != null)
                  && Session.IsNewObject(this))
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "CariAdresServerPrefix");
                Kod = string.Format("CHADRS{0:D4}", deger);
            }
            base.OnSaving();
        }
    }
}