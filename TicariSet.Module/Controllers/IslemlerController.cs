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
using System.Reflection;
using System.Text;
using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using TicariSet.Module.BusinessObjects;

namespace TicariSet.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class IslemlerController : ViewController
    {
        //  private SimpleAction simpleAction;
        private PopupWindowShowAction showListViewAction;
        public IslemlerController()
        {
            //  simpleAction = new SimpleAction(this,"İşlemler",PredefinedCategory.Edit);
            showListViewAction = new PopupWindowShowAction(
                this, "Stok Hareketleri", PredefinedCategory.Edit);
            TargetObjectType = typeof(Stoklar);
            // simpleAction.Execute += islemlerAction_Execute;
            showListViewAction.Execute += popupWindowShowAction1_Execute;
            showListViewAction.CustomizePopupWindowParams += ShowListViewAction_CustomizePopupWindowParams;
            InitializeComponent();
        }
        private void islemlerAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            
            //object stokHareket = (StokHareketler)Session.DefaultSession.GetObjectsByKeyFromQuery
            //    (classInfo: null, true,
            //    $"select * from [TicariSet].[dbo].[StokHareketler] where StokID = '{stokIdHareketler}' ");
            //e.ShowViewParameters.CreatedView = Application.CreateListView();

            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;

        }
        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Stoklar selectedStok = e.SelectedObjects[0] as Stoklar;
            object selectedStokID = selectedStok.Oid;
            StokHareketler stkHareket = e.PopupWindowView.ObjectSpace.GetObjectByKey<StokHareketler>(selectedStokID);
        }
        private void ShowListViewAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace newObjectSpace = Application.CreateObjectSpace(View.ObjectTypeInfo.Type);
            Object selectedStok = newObjectSpace.GetObject(View.SelectedObjects[0]);

            if (selectedStok != null)
            {
                DetailView createdView = Application.CreateDetailView(newObjectSpace, selectedStok);
                createdView.ViewEditMode = ViewEditMode.Edit;
                e.View = createdView;
            }

            Type objectType = typeof(StokHareketler);
            object selectedStokID = ((Stoklar)View.SelectedObjects[0]).Oid;


            e.View = Application.CreateListView(objectType, false);
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
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

     
    }
}
