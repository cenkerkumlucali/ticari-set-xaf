using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;

namespace TicariSet.Module.Interfaces
{
    public interface IModelListViewEx : IModelNode
    {
        [Category("Behavior")]
        bool IsGroupFooterVisible { get; set; }

        [Category("Behavior")]
        int HeaderRows { get; set; }

        [DefaultValue(true)]
        [Category("Behavior")]
        bool ShowDetailView { get; set; }

        [Category("Data")]
        [DataSourceProperty("ModelClass.AllMembers")]
        IModelMember DefaultFocusedItem { get; set; }
    }
}