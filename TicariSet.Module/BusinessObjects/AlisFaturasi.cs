using System;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Invoice")]
    public class AlisFaturasi : Fisler.Fisler
    { 
        public AlisFaturasi(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Tarih = DateTime.Now;
            Turu = EnumObjects.FisHareketType.Alis;
            HareketTipi = EnumObjects.StokHareketType.Giris;
        }
        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork)
                && (Session.DataLayer != null)
                  && Session.IsNewObject(this)
                     && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "AlisFaturaPrefix");
                Kod = string.Format("AF{0:D8}", deger);
            }
            Aciklama = $"{Kod} nolu {Tarih} tarihli {CariID} hesaba {GenelToplam} tutarındaki alış faturası.";

            base.OnSaving();
        }
    }
}