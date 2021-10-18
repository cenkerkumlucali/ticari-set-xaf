using System;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class PersonelPasaportBilgileri : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public PersonelPasaportBilgileri(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        PersonelKart personelId;
        string pVerenMakam;
        DateTime pGecerlilikTarihi;
        DateTime pTarih;
        Ulkeler pUlke;
        PersonelPasaportBilgileri pPasaportBilgileri;
        string pNo;


        [XafDisplayName("Personel")]        
        public PersonelKart PersonelId
        {
            get => personelId;
            set => SetPropertyValue(nameof(PersonelId), ref personelId, value);
        }
        [Size(16)]
        [XafDisplayName("Pasaport No")]
        public string PNo
        {
            get => pNo;
            set => SetPropertyValue(nameof(PNo), ref pNo, value);
        }
        [XafDisplayName("Pasaport Tipi")]
        public PersonelPasaportBilgileri PPasaportBilgileri
        {
            get => pPasaportBilgileri;
            set => SetPropertyValue(nameof(PPasaportBilgileri), ref pPasaportBilgileri, value);
        }
        [XafDisplayName("Pasaport Ülke")]
        public Ulkeler PUlke
        {
            get => pUlke;
            set => SetPropertyValue(nameof(PUlke), ref pUlke, value);
        }

        [DbType("Date")]
        [XafDisplayName("Pasaport Tarih")]
        public DateTime PTarih
        {
            get => pTarih;
            set => SetPropertyValue(nameof(PTarih), ref pTarih, value);
        }
        [DbType("Date")]
        [XafDisplayName("Pasaport Geçerlilik Tarih")]
        public DateTime PGecerlilikTarihi
        {
            get => pGecerlilikTarihi;
            set => SetPropertyValue(nameof(PGecerlilikTarihi), ref pGecerlilikTarihi, value);
        }
        [Size(32)]
        [XafDisplayName("Pasaport Veren Makam")]
        public string PVerenMakam
        {
            get => pVerenMakam;
            set => SetPropertyValue(nameof(PVerenMakam), ref pVerenMakam, value);
        }
        
    }
}