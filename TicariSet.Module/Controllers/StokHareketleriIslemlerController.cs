using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.Persistent.Base;
using System;
using TicariSet.Module.BusinessObjects;

namespace TicariSet.Module.Controllers
{
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
