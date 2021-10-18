using System;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class BankaOdeme : BankaHareket
    { 
        public BankaOdeme(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Tarih = DateTime.Now;
            Hareket = KasaHareketType.BankaOdeme;
        }
        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork)
                && Session.DataLayer != null
                && Session.IsNewObject(this)
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "BankaOdemeServerPrefix");
                Kod = string.Format("BO{0:D8}", deger);
            }
            Aciklama = $"{Kod} nolu {Tarih} tarihli {BankaID.Tanim} bankadan {HesapID.Hesap} nolu hesaba {CariID.Tanim} hesabına {Tutar} tutarında ödeme yapılmıştır.";
            base.OnSaving();
        }
    }
}