using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using TicariSet.Module.BusinessObjects.Kasa;

namespace TicariSet.Module.BusinessObjects.Banka
{
    [DefaultClassOptions]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
  
    public class BankaHareket : KasaHareket
    { 
        public BankaHareket(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        BankaHesaplari hesapID;
        Bankalar bankaID;


        [XafDisplayName("Banka")]
        public Bankalar BankaID
        {
            get => bankaID;
            set => SetPropertyValue(nameof(BankaID), ref bankaID, value);
        }
        [XafDisplayName("Hesap Numarası")]
        public BankaHesaplari HesapID
        {
            get => hesapID;
            set => SetPropertyValue(nameof(HesapID), ref hesapID, value);
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