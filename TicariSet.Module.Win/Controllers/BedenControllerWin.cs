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
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPivotGrid;
using TicariSet.Module.Controllers;

namespace TicariSet.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class BedenControllerWin : BedenController
    {
        public BedenControllerWin()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

        protected override void CreateEvent()
        {
            Subscribe();

        }

        private void Subscribe(bool isSubscribe = true)
        {
            if (View is DetailView)
            {
                ViewItem viewItem = ((DetailView)View).FindItem("Miktar");
                if (viewItem != null && viewItem.Control != null)
                {
                    if (viewItem.Control is SpinEdit editor)
                    {
                        if (isSubscribe)
                            editor.Enter += Editor_Enter;
                        else
                            editor.Enter -= Editor_Enter;
                    }
                }
                else if (viewItem != null)
                {
                    if (isSubscribe)
                        viewItem.ControlCreated -= ViewItem_ControlCreated;
                    else
                        viewItem.ControlCreated += ViewItem_ControlCreated;

                }
            }
            else if (View is ListView)
            {
                if (((ListView)View).Editor is GridListEditor gridList)
                {
                    if (gridList != null && gridList.GridView != null)
                    {
                        if (isSubscribe)
                        {
                            gridList.GridView.RowCellClick += GridView_RowCellClick;
                            gridList.GridView.FocusedColumnChanged += GridView_FocusedColumnChanged;
                            gridList.GridView.ShowingEditor += GridView_ShowingEditor;

                            //gridList.GridView.CellValueChanged += GridView_CellValueChanged;
                        }
                        else
                        {
                            gridList.GridView.RowCellClick -= GridView_RowCellClick;
                            gridList.GridView.FocusedColumnChanged -= GridView_FocusedColumnChanged;
                        }
                    }
                }
            }
        }

        private void GridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GridControl gridControl = (GridControl)((DevExpress.ExpressApp.ListView)View).Editor.Control;
            GridView gridView = (GridView)gridControl.FocusedView;
            e.Cancel = gridView.FocusedColumn.FieldName == "Miktar" && gridView.FocusedRowHandle % 2 == 0;
        }

        private void PivotGrid_EditValueChanged(object sender, DevExpress.XtraPivotGrid.EditValueChangedEventArgs e)
        {
            PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            for (int i = 0; i < ds.RowCount; i++)
            {
                ds.SetValue(i, e.DataField, Convert.ToBoolean(e.Editor.EditValue));
            }
        }

        private void PivotGrid_CustomCellValue(object sender, DevExpress.XtraPivotGrid.PivotCellValueEventArgs e)
        {
            if (e.DataField.Name == "Miktar" && e.Value != null)
            {
                e.Value = (Convert.ToInt32(e.Value) > 0) ? true : false;
            }
        }
        //private void GridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    GridControl gridControl = (GridControl)((DevExpress.ExpressApp.ListView)View).Editor.Control;
        //    GridView gridViewCore = (GridView)gridControl.FocusedView;
        //    foreach (GridColumn column in gridViewCore.Columns)


        //}
        private void ViewItem_ControlCreated(object sender, EventArgs e)
        {
            PropertyEditor propertyEditor = sender as PropertyEditor;
            propertyEditor.ControlCreated -= ViewItem_ControlCreated;
            if (propertyEditor != null)
            {
                CreateEvent();
            }
        }

        private void Editor_Enter(object sender, EventArgs e)
        {
            BedenPopup();
        }

        private void GridView_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (e.FocusedColumn != null && e.FocusedColumn.FieldName == "Miktar")
            {
                BedenPopup();
            }
        }

        private void GridView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column != null && e.Column.FieldName == "Miktar")
            {
                BedenPopup();
            }
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            Subscribe(false);
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }


    }
}
