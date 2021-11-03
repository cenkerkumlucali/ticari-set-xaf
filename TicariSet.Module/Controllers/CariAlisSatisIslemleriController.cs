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
using TicariSet.Module.BusinessObjects;

namespace TicariSet.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class CariAlisSatisIslemleriController : ViewController
    {
        private PopupWindowShowAction showListView;
        public CariAlisSatisIslemleriController()
        {
            showListView = new PopupWindowShowAction(this, "Alış Satış İşlemleri", PredefinedCategory.Edit);
            TargetObjectType = typeof(Cariler);
            showListView.ImageName = "Paid";
            showListView.CustomizePopupWindowParams += ShowListView_CustomizePopupWindowParams;
            InitializeComponent();

        }   

        private void ShowListView_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            e.DialogController.SaveOnAccept = false;
            Type objectType = typeof(Fisler);
            object selectedCari = ((Cariler) View.SelectedObjects[0]).Oid;
            string listViewId = ModelNodeIdHelper.GetListViewId(objectType);
            CollectionSource collectionSource = new CollectionSource(ObjectSpace, objectType);
            CriteriaOperator criteriaOperator = CriteriaOperator.Parse("CariID = ?",selectedCari);
            if(!(criteriaOperator is null))
                collectionSource.Criteria.Add("Criteria",criteriaOperator);
            e.View = Application.CreateListView(listViewId, collectionSource, false);
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
