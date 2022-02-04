using System.ComponentModel;
using System.Drawing;
using DevExpress.ExpressApp.Model;

namespace TicariSet.Module.Interfaces
{
    public interface IModelOptionsEx : IModelNode
    {
        [DefaultValue(false)]
        [Category("Behavior")]
        bool HighlightFocusedItem { get; set; }

        [DefaultValue(true)]
        [Category("Behavior")]
        bool HideProtectedContent { get; set; }

        [DefaultValue(true)]
        [Category("Behavior")]
        bool LookupNewObjectActionActive { get; set; }

        [DefaultValue(true)]
        [Category("Behavior")]
        bool LookupCloneObjectActionActive { get; set; }

        [DefaultValue(false)]
        [Category("Behavior")]
        bool RestoreFullTextSearchFilter { get; set; }

        [Category("Appearance")]
        Font Font { get; set; }
    }
}