using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using TicariSet.Module.BusinessObjects.Cari;
using TicariSet.Module.BusinessObjects.Personel;

namespace TicariSet.Module.BusinessObjects.Tanım
{
    [DefaultClassOptions]
    [DefaultProperty("Ad")]
    [CreatableItem(false)]
    [ImageName("BO_Country_v92")]
    public class Ulkeler : XPObject
    {
        public Ulkeler(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private string ad;

        [Size(32)]
        public string Ad
        {
            get => ad;
            set => SetPropertyValue(nameof(Ad), ref ad, value);
        }

        [Association("Ulkeler-CariAdres")]
        public XPCollection<CariAdresBilgileri> CariAdresBilgileri =>
            GetCollection<CariAdresBilgileri>(nameof(CariAdresBilgileri));
        [Association("Ulkeler-PersonelAdres")]
        public XPCollection<PersonelAdresBilgileri> PersonelAdresBilgileri =>
            GetCollection<PersonelAdresBilgileri>(nameof(PersonelAdresBilgileri));
    }
}