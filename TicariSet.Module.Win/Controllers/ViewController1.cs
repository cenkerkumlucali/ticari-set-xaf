using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.PivotGrid.Win;
using DevExpress.Persistent.BaseImpl;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPivotGrid;
using TicariSet.Module.BusinessObjects;

namespace TicariSet.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class PivotController : ViewController
    {
        public PivotController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            View.ControlsCreated += new EventHandler(View_ControlsCreated);

            //Frame.GetController<AnalysisDataBindController>().BindDataAction.Execute +=
            //    new SimpleActionExecuteEventHandler(BindDataAction_Execute);

        }

        RepositoryItemSpinEdit _repositoryItemSpinEdit;
        PivotGridControl _pivotGridControl;

        void View_ControlsCreated(object sender, EventArgs e)
        {
            if (!(View is DetailView))
            {
                PivotGridListEditor editor = ((ListView)View).Editor as PivotGridListEditor;
                if (editor != null)
                {
                    PivotGridControl pivot = (PivotGridControl)editor.PivotGridControl;
                    _pivotGridControl = pivot;

                }
            }
            _repositoryItemSpinEdit = new RepositoryItemSpinEdit();

            if (_pivotGridControl != null)
            {
                _pivotGridControl.ShowingEditor +=
                    new EventHandler<CancelPivotCellEditEventArgs>(_pivotGridControl_ShowingEditor);
                _pivotGridControl.EditValueChanged +=
                    new EditValueChangedEventHandler(_pivotGridControl_EditValueChanged);
            }

            CustomizePivotGrid();

        }

     
        private void CustomizePivotGrid()
        {
            if (_pivotGridControl != null)
            {
                foreach (PivotGridField field in _pivotGridControl.Fields)
                {
                    field.FieldEdit = _repositoryItemSpinEdit;
                }

                _pivotGridControl.OptionsView.ShowRowGrandTotals = false;
                _pivotGridControl.OptionsView.ShowColumnGrandTotals = false;
                _pivotGridControl.OptionsView.ShowFilterHeaders = false;
                _pivotGridControl.OptionsView.ShowColumnHeaders = false;
                _pivotGridControl.OptionsBehavior.BestFitMode = PivotGridBestFitMode.FieldHeader;
                _pivotGridControl.BestFitDataHeaders(false);
                _pivotGridControl.OptionsView.ShowRowHeaders = false;
                _pivotGridControl.OptionsView.ShowColumnGrandTotals = true;
                _pivotGridControl.OptionsView.ShowRowGrandTotals = true;
                _pivotGridControl.OptionsView.ShowDataHeaders = false;
                _pivotGridControl.Appearance.TotalCell.BackColor = Color.Azure;
                _pivotGridControl.Appearance.TotalCell.ForeColor = Color.Red;
                _pivotGridControl.Appearance.GrandTotalCell.BackColor = Color.Azure;
                _pivotGridControl.Appearance.GrandTotalCell.ForeColor = Color.Red;
                _pivotGridControl.OptionsView.ShowFilterHeaders = false;
                _pivotGridControl.OptionsView.ShowColumnHeaders = false;
                PivotGridControl.DefaultDataProcessingEngine = PivotDataProcessingEngine.Legacy;
            }
        }


        void _pivotGridControl_ShowingEditor(object sender, CancelPivotCellEditEventArgs e)
        {
            PivotCellEventArgs cellInfo = GetFocusedCellInfo(sender as PivotGridControl);
            if (cellInfo.RowValueType == PivotGridValueType.GrandTotal || cellInfo.ColumnValueType == PivotGridValueType.GrandTotal)
                e.Cancel = false;
        }

        PivotCellEventArgs GetFocusedCellInfo(PivotGridControl pivot)
        {
            Point focused = pivot.Cells.FocusedCell;
            return pivot.Cells.GetCellInfo(focused.X, focused.Y);
        }

        //private AnalysisEditorWin AnalysisEditor => (View as ListView).FindItem("Self");

        //private PivotGridControl PivotGridControl => AnalysisEditor.Control.PivotGrid;

        private void _pivotGridControl_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            for (int i = 0; i < ds.RowCount; i++)
            {
                ds.SetValue(i, e.DataField, Convert.ToInt32(e.Editor.EditValue));
            }

        }
        private void CustomizeField(PivotGridField field)
        {
            //switch (field.FieldName)
            //{

            //    case "orelavopDataRiferimento":
            //        field.Area = PivotArea.RowArea;                    
            //        break;

            //    case "anoprIDOperaio":
            //        field.Area = PivotArea.RowArea;
            //        break;

            //    case "anopzCodiceOperazione":
            //        field.Area = PivotArea.ColumnArea;
            //        break;

            //    case "orelavopLavorate":
            //        field.Area = PivotArea.DataArea;
            //        //field.FieldEdit = _repositoryItemTextEdit;
            //        //field.FieldEdit = _repCalc;
            //        break;

            //    default:
            //        field.Visible = false;
            //        break;
            //}
        }

    }
}
