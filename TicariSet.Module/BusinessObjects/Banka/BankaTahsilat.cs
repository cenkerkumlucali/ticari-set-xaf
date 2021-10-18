using System;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects.Banka
{
    [DefaultClassOptions]
   
    public class BankaTahsilat : BankaHareket
    {
        public BankaTahsilat(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Tarih = DateTime.Now;
            Hareket = KasaHareketType.BankaTahsilat;
        }
        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork)
                && Session.DataLayer != null
                && Session.IsNewObject(this)
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "BankaTahsilatServerPrefix");
                Kod = string.Format("BT{0:D8}", deger);
            }
            Aciklama = $"{Kod} nolu {Tarih} tarihli {CariID.Tanim} hesabından {BankaID.Tanim} bankanın {HesapID.Hesap} nolu hesaba {Tutar} tutarında tahsilat yapılmıştır.";
            base.OnSaving();
        }
    }
}