using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Contract")]
    [DefaultProperty("EvrakNo")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Evrak : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Evrak(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        string evrakNo;

        [Size(64)]
        public string EvrakNo
        {
            get => evrakNo;
            set => SetPropertyValue(nameof(EvrakNo), ref evrakNo, value);
        }
        [Association("Evrak-EvrakSatir")]
        public XPCollection<EvrakSatir> EvrakSatir
        {
            get
            {
                return GetCollection<EvrakSatir>(nameof(EvrakSatir));
            }
        }
    }
}