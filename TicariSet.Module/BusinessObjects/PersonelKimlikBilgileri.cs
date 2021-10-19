using System;
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
    [ImageName("Business_CreditCard")]
    [DefaultProperty("KimlikNo")]
    
    public class PersonelKimlikBilgileri : BaseObject
    { 
        public PersonelKimlikBilgileri(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private PersonelKart personelId;
        [XafDisplayName("Personel")]

        public PersonelKart PersonelId
        {
            get => personelId;
            set => SetPropertyValue(nameof(PersonelId), ref personelId, value);
        }

        private string kimlikNo;

        [Size(11)]
        [RuleRequiredField]
        [RuleUniqueValue("RUV-KimlikNo.01", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        public string KimlikNo
        {
            get => kimlikNo;
            set => SetPropertyValue(nameof(KimlikNo), ref kimlikNo, value);
        } 

        private string ad;
        [Size(32)]
        [RuleRequiredField]
        public string Ad
        {
            get => ad;
            set => SetPropertyValue(nameof(Ad), ref ad, value);
        }

        private string soyad;
        [Size(32)]
        [RuleRequiredField]
        public string Soyad
        {
            get => soyad;
            set => SetPropertyValue(nameof(Soyad), ref soyad, value);
        }
        private string babaAdı;

        [Size(32)]
        public string BabaAdı
        {
            get => babaAdı;
            set => SetPropertyValue(nameof(BabaAdı), ref babaAdı, value);
        }
        private string anneAdı;

        [Size(32)]
        public string AnneAdı
        {
            get => anneAdı;
            set => SetPropertyValue(nameof(AnneAdı), ref anneAdı, value);
        }
        private Ilceler ilceId;
        [XafDisplayName("Doğum Yeri")]


        public Ilceler IlceId
        {
            get => ilceId;
            set => SetPropertyValue(nameof(IlceId), ref ilceId, value);
        }

        private DateTime dogumTarihi;
        [DbType("Date")]
        [XafDisplayName("Doğum Tarihi")]
        public DateTime DogumTarihi
        {
            get => dogumTarihi;
            set => SetPropertyValue(nameof(DogumTarihi), ref dogumTarihi, value);
        }

        private MedeniDurum medeniDurum;
        public MedeniDurum MedeniDurum
        {
            get => medeniDurum;
            set => SetPropertyValue(nameof(MedeniDurum), ref medeniDurum, value);
        }

        private Din din;
        public Din Din
        {
            get => din;
            set => SetPropertyValue(nameof(Din), ref din, value);
        }
        
        private Sehirler ncKayitliOlduguSehir;
        [XafDisplayName("Nc.Kayıtlı Olduğu Şehir")]
        public Sehirler NcKayitliOlduguSehir
        {
            get => ncKayitliOlduguSehir;
            set => SetPropertyValue(nameof(ncKayitliOlduguSehir), ref ncKayitliOlduguSehir, value);
        }

        private Ilceler ncKayitliOlduguIlce;
        [DataSourceProperty("NcKayitliOlduguSehir.Ilceler")]
        [XafDisplayName("Nc.Kayıtlı Olduğu İlçe")]
        public Ilceler NcKayitliOlduguIlce
        {
            get => ncKayitliOlduguIlce;
            set => SetPropertyValue(nameof(NcKayitliOlduguIlce), ref ncKayitliOlduguIlce, value);
        }

        private string ncKayıtliOlduguMahalleKoy;
        [XafDisplayName("Nc.Kayıtlı Olduğu Mahalle/Köy")]
        [Size(64)]
        public string NcKayıtliOlduguMahalleKoy
        {
            get => ncKayıtliOlduguMahalleKoy;
            set => SetPropertyValue(nameof(NcKayıtliOlduguMahalleKoy), ref ncKayıtliOlduguMahalleKoy, value);
        }
        [XafDisplayName("Nc.Cilt No")]
        private string ncCiltNo;

        [Size(5)]
        public string NcCiltNo
        {
            get => ncCiltNo;
            set => SetPropertyValue(nameof(NcCiltNo), ref ncCiltNo, value);
        }

        private string ncAileSıraNo;
        [XafDisplayName("Nc.Aile Sıra No")]
        [Size(5)]
        [Nullable(true)]
        public string NcAileSıraNo
        {
            get => ncAileSıraNo;
            set => SetPropertyValue(nameof(NcAileSıraNo), ref ncAileSıraNo, value);
        }

        private Sehirler ncVerildigiYer;
        [XafDisplayName("Nc.Verildiği Yer")]
        public Sehirler NcVerildigiYer
        {
            get => ncVerildigiYer;
            set => SetPropertyValue(nameof(NcVerildigiYer), ref ncVerildigiYer, value);
        }
        private PersonelKimlikVerilisNedeni personelKimlikVerilisNedeni;
        public PersonelKimlikVerilisNedeni PersonelKimlikVerilisNedeni
        {
            get => personelKimlikVerilisNedeni;
            set => SetPropertyValue(nameof(PersonelKimlikVerilisNedeni), ref personelKimlikVerilisNedeni, value);
        }
    }
}