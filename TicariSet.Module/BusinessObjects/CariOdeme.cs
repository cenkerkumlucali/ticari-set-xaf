using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
  
    public class CariOdeme : KasaHareket
    {
        public CariOdeme(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            Hareket = KasaHareketType.Odeme;
            Tarih = DateTime.Now;
            base.AfterConstruction();
        }
        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork)
                && Session.DataLayer != null
                && Session.IsNewObject(this)
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "CariOdemeServerPrefix");
                Kod = string.Format("CT{0:D8}", deger);
            }
            Aciklama = $"{Kod} nolu {Tarih} tarihli {KasaID.Tanim} hesabından {CariID.Tanim} hesabına {Tutar} TL tutarında ödeme yapılmıştır.";
            base.OnSaving();
        }
    }
}