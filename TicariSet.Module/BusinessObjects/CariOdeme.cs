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
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class CariOdeme : KasaHareket
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public CariOdeme(Session session)
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
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "CariOdemeServerPrefix");
                Kod = string.Format("CT{0:D8}", deger);
            }
            Aciklama = $"{Kod} nolu {Tarih} tarihli {KasaID} hesabından {CariID} hesabına {Tutar} TL tutarında ödeme yapılmıştır.";
            base.OnSaving();
        }
    }
}