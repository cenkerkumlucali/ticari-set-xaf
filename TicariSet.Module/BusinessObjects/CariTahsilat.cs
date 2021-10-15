using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class CariTahsilat : KasaHareket
    {
        public CariTahsilat(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            Hareket = KasaHareketType.Tahsilat;
            Tarih = DateTime.Now;
            base.AfterConstruction();
        }

        public double CariBakiye
        {
            get
            {
                if (CariID != null)
                    return CariID.Bakiye;
                return 0;
            }
        }
        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork)
                && Session.DataLayer != null
                && Session.IsNewObject(this)
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "CariTahsilatServerPrefix");
                Kod = string.Format("CT{0:D8}", deger);
            }
            base.OnSaving();
        }

    }
}