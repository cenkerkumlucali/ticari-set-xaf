using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;


namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Price")]
    public class SatisFaturasi : Fisler
    { 
        public SatisFaturasi(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            
            base.AfterConstruction();
            Tarih = DateTime.Now;
            Turu = EnumObjects.FisHareketType.Satis;
            HareketTipi = EnumObjects.StokHareketType.Cikis;
        }
        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork)
                && Session.DataLayer != null
                && Session.IsNewObject(this)
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "SatisFaturaPrefix");
                Kod = string.Format("SF{0:D8}", deger);
            }

            Aciklama = $"{Kod} nolu {Tarih} tarihli {CariID} hesaba {GenelToplam} tutarındaki satış faturası.";
            base.OnSaving();
        }
    }
}