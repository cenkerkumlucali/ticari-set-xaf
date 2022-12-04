using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using System;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using TicariSet.Module.Controllers;

namespace TicariSet.Module.Win.Controllers
{
    public partial class BedenControllerWin : BedenController
    {
        public BedenControllerWin()
        {
            InitializeComponent();
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

        private void GridView_FocusedColumnChanged(object sender,FocusedColumnChangedEventArgs e)
        {
            if (e.FocusedColumn != null && e.FocusedColumn.FieldName == "Miktar")
            {
                BedenPopup();
            }
        }

        private void GridView_RowCellClick(object sender,RowCellClickEventArgs e)
        {
            if (e.Column != null && e.Column.FieldName == "Miktar")
            {
                BedenPopup();
            }
        }

        protected override void OnDeactivated()
        {
            Subscribe(false);
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }


    }
}
