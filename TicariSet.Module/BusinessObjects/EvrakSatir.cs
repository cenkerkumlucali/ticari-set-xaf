using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [DefaultProperty("Kod")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class EvrakSatir : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public EvrakSatir(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        int miktar;
        EvrakBeden evrakBeden;
        Evrak evrakId;
        string kod;

        [Size(64)]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }

        [Association("Evrak-EvrakSatir")]
        public Evrak EvrakId
        {
            get => evrakId;
            set => SetPropertyValue(nameof(EvrakId), ref evrakId, value);
        }
        
        public int Miktar
        {
            get => miktar;
            set
            {
                SetPropertyValue(nameof(Miktar), ref miktar, value);

            }
        }
        [XafDisplayName("Beden")]
        public EvrakBeden BedenId
        {
            get => evrakBeden;
            set => SetPropertyValue(nameof(BedenId), ref evrakBeden, value);
        }

        public static void HesaplaMiktar(EvrakSatir evrakSatir,int toplam)
        {
            evrakSatir.Miktar = toplam;
        }
    }
}