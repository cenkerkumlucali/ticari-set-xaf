using System.ComponentModel;
using DevExpress.Data;

namespace TicariSet.Module.Interfaces
{
    public interface IModelColumnEx
    {
        [DefaultValue(SummaryItemType.None)]
        [Category("Behavior")]
        SummaryItemType GroupFooterSummaryType { get; set; }
    }
}