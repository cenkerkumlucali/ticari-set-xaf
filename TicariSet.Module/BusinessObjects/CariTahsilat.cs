using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
            Aciklama = $"{Kod} nolu {Tarih} tarihli {CariID} hesabından {KasaID} hesabına {Tutar} TL tutarında tahsilat yapılmıştır.";
            base.OnSaving();
        }

    }
}