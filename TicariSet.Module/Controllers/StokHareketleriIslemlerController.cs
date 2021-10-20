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
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using TicariSet.Module.BusinessObjects;

namespace TicariSet.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class StokHareketleriIslemlerController : ViewController
    {
        //  private SimpleAction simpleAction;
        private PopupWindowShowAction showListViewAction;
        public StokHareketleriIslemlerController()
        {
            showListViewAction = new PopupWindowShowAction(this, "Stok Hareketleri", PredefinedCategory.Edit); 
            TargetObjectType = typeof(Stoklar);
            showListViewAction.ImageName = "BO_Quote";
            showListViewAction.CustomizePopupWindowParams += ShowListViewAction_CustomizePopupWindowParams;
            InitializeComponent();
        }
     
       
        private void ShowListViewAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            e.DialogController.SaveOnAccept = false;
            Type objectType = typeof(StokHareketler);
            IObjectSpace objectSpace = Application.CreateObjectSpace(objectType);
            object selectedStokID = ((Stoklar) View.SelectedObjects[0]).Oid;
            string lvId = ModelNodeIdHelper.GetListViewId(objectType); 
            CollectionSource cs = new CollectionSource(ObjectSpace, objectType);
            CriteriaOperator criteria = CriteriaOperator.Parse("StokID = ? ", selectedStokID);
            if (!(criteria is null))
                cs.Criteria.Add("Criteria", criteria);
            e.View = Application.CreateListView(lvId, cs, false);
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
