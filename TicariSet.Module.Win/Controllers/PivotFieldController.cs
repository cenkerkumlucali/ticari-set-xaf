//using System;
//using System.ComponentModel;

//using DevExpress.ExpressApp;
//using DevExpress.ExpressApp.Actions;
//using DevExpress.ExpressApp.PivotGrid;
//using DevExpress.ExpressApp.PivotChart;
//using DevExpress.ExpressApp.PivotChart.Win;
//using DevExpress.XtraPivotGrid;
//using System.Drawing;
//using DevExpress.XtraEditors.Repository;
//using DevExpress.Persistent.BaseImpl;
//using DevExpress.ExpressApp.PivotGrid.Win;

//namespace GestTintXAF2.Module.Controllers
//{
//    public partial class PivotFieldController : ViewController
//    {
//        public PivotFieldController()
//        {
//            InitializeComponent();
//            RegisterActions(components);
//            TargetViewId = "OreLavorateOperai_ListView";
//        }

//        protected override void OnActivated()
//        {
//            base.OnActivated();
  
//            View.ControlsCreated += new EventHandler(View_ControlsCreated);
//            //Frame.GetController<AnalysisDataBindController>().BindDataAction.Execute += new SimpleActionExecuteEventHandler(BindDataAction_Execute);
//        }

//        RepositoryItemSpinEdit _repositoryItemSpinEdit;
       
//        PivotGridControl _pivotGridControl;

//        void View_ControlsCreated(object sender, EventArgs e)
//        {
            
        
//                AnalysisEditor.PivotGridSettingsLoaded += new EventHandler(AnalysisEditor_PivotGridSettingsLoaded);

//                _pivotGridControl = PivotGridControl;
//                _repositoryItemSpinEdit = new RepositoryItemSpinEdit();             
//                _pivotGridControl.RepositoryItems.Add(_repositoryItemSpinEdit);            
//                _pivotGridControl.ShowingEditor += new EventHandler<CancelPivotCellEditEventArgs>(_pivotGridControl_ShowingEditor);
//                _pivotGridControl.EditValueChanged += new EditValueChangedEventHandler(_pivotGridControl_EditValueChanged);
           
//        }

//        void AnalysisEditor_PivotGridSettingsLoaded(object sender, EventArgs e)
//        {
//            CustomizePivotGrid();
//        }

//        void BindDataAction_Execute(object sender, SimpleActionExecuteEventArgs e)
//        {
//            CustomizePivotGrid();
//        }

//        private void CustomizePivotGrid()
//        {
//            if (((Analysis)View.CurrentObject).DataType == typeof(OreLavorateOperai))
//            {
//                foreach (PivotGridField field in _pivotGridControl.Fields)
//                {
//                    field.FieldEdit = _repositoryItemTextEdit;
                 
//                }
//                _pivotGridControl.OptionsView.ShowRowGrandTotals = false;
//                _pivotGridControl.OptionsView.ShowColumnGrandTotals = false;
                
//            }
//        }

//        private void CustomizeField(PivotGridField field)
//        {
//            //switch (field.FieldName)
//            //{

//            //    case "orelavopDataRiferimento":
//            //        field.Area = PivotArea.RowArea;                    
//            //        break;

//            //    case "anoprIDOperaio":
//            //        field.Area = PivotArea.RowArea;
//            //        break;

//            //    case "anopzCodiceOperazione":
//            //        field.Area = PivotArea.ColumnArea;
//            //        break;

//            //    case "orelavopLavorate":
//            //        field.Area = PivotArea.DataArea;
//            //        //field.FieldEdit = _repositoryItemTextEdit;
//            //        //field.FieldEdit = _repCalc;
//            //        break;

//            //    default:
//            //        field.Visible = false;
//            //        break;
//            //}
//        }

//        void _pivotGridControl_ShowingEditor(object sender, CancelPivotCellEditEventArgs e)
//        {
//            PivotCellEventArgs cellInfo = GetFocusedCellInfo(sender as PivotGridControl);
//            if (cellInfo.RowValueType == PivotGridValueType.GrandTotal || cellInfo.ColumnValueType == PivotGridValueType.GrandTotal)
//                e.Cancel = true;
//        }

//        PivotCellEventArgs GetFocusedCellInfo(PivotGridControl pivot)
//        {
//            Point focused = pivot.Cells.FocusedCell;
//            return pivot.Cells.GetCellInfo(focused.X, focused.Y);
//        }

//        private AnalysisEditorWin AnalysisEditor
//        {
//            get
//            {
//                return (View as ListView).FindItem("Self") as AnalysisEditorWin;
//            }
//        }

//        private PivotGridControl PivotGridControl
//        {
//            get { return AnalysisEditor.Control.PivotGrid; }
//        }

//        private void _pivotGridControl_EditValueChanged(object sender, EditValueChangedEventArgs e)
//        {
//            PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
//            for (int j = 0; j < ds.RowCount; j++)
//            {
//                ds[j][e.DataField] = e.Editor.EditValue;
//            }
//        }
//    }
//}
