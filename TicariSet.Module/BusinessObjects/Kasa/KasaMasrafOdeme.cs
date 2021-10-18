using System;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using TicariSet.Module.BusinessObjects.Masraf;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects.Kasa
{
    [DefaultClassOptions]
    [DefaultProperty("Kod")]
    [ImageName("Business_DollarCircled")]

    public class KasaMasrafOdeme : KasaHareket
    {
        public KasaMasrafOdeme(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Tarih = DateTime.Now;
            Hareket = KasaHareketType.Odeme;
        }

        Masraflar masrafID;

        [Association("Masraflar-MasrafDetay")]
        [XafDisplayName("Masraf Türü")]
        public Masraflar MasrafID
        {
            get => masrafID;
            set => SetPropertyValue(nameof(MasrafID), ref masrafID, value);
        }

        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork)
                && Session.DataLayer != null
                && Session.IsNewObject(this)
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "KasaMasrafOdemeServerPrefix");
                Kod = string.Format("MO{0:D8}", deger);
            }
            Aciklama = $"{Kod} nolu {Tarih} tarihli {KasaID.Tanim} hesabından {MasrafID.Tanim} hesabına {Tutar} TL tutarında ödeme yapılmıştır.";
            base.OnSaving();
        }
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}
    }
}