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
using DevExpress.ExpressApp.Win.Templates;
using TicariSet.Module.BusinessObjects;

namespace TicariSet.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class IslemlerController : ViewController
    {

        public IslemlerController()
        {
            RegisterActions(components);
            InitializeComponent();


        }
        private void singleChoiceAction1_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            if (e.CurrentObject.GetType() == typeof(Stoklar))
            {
                switch (((DevExpress.ExpressApp.Actions.SingleChoiceAction)e.Action).SelectedItem.Id)
                {
                    case "stokHareketleri":
                        StokHareketleriByStokId_Execute(sender, e);
                        break;
                    case "stokHareketleriHepsi":
                        StokHareketleriHepsi_Execute(sender, e);
                        break;
                }
            }
            if (e.CurrentObject.GetType() == typeof(Cariler))
            {
                switch (((DevExpress.ExpressApp.Actions.SingleChoiceAction)e.Action).SelectedItem.Id)
                {
                    case "alisIslemleri":
                        AlimIslemleri_Execute(sender, e);
                        break;
                }
            }
        }

        #region Cari

        private void AlimIslemleri_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            Type objectType = typeof(Fisler);
            IObjectSpace objectSpace = Application.CreateObjectSpace(objectType);
            object selectedCari = ((Cariler)View.SelectedObjects[0]).Oid;
            string listViewId = ModelNodeIdHelper.GetListViewId(objectType);
            CollectionSource collectionSource = new CollectionSource(objectSpace, objectType);

            CriteriaOperator criteriaOperator = CriteriaOperator.Parse("[CariID] = ? AND [Turu] = 'Alis'", selectedCari);
            if (!(criteriaOperator is null))
                collectionSource.Criteria.Add("Criteria",criteriaOperator);
            e.ShowViewParameters.CreatedView = Application.CreateListView(listViewId, collectionSource, false);
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;

            e.ShowViewParameters.Context = TemplateContext.PopupWindow;
            DialogController dialogController = Application.CreateController<DialogController>();

            dialogController.Accepting += new EventHandler<DialogControllerAcceptingEventArgs>(dc_Accepting);
            dialogController.SaveOnAccept = false;
            e.ShowViewParameters.Controllers.Add(dialogController);
        }

        private void SatisIslemleri_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            Type objectType = typeof(Fisler);
            IObjectSpace objectSpace = Application.CreateObjectSpace(objectType);
            object selectedCari = ((Cariler) View.SelectedObjects[0]).Oid;

            string listViewId = ModelNodeIdHelper.GetListViewId(objectType);
            CollectionSource collectionSource = new CollectionSource(objectSpace, objectType);
            CriteriaOperator criteriaOperator = CriteriaOperator.Parse("[CariID] = ? AND [Turu] = 'Satis'",selectedCari);
            if (!(criteriaOperator is null))
            collectionSource.Criteria.Add("Criteria",criteriaOperator);
            e.ShowViewParameters.CreatedView = Application.CreateListView(listViewId, collectionSource, false);
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            e.ShowViewParameters.Context = TemplateContext.PopupWindow;
            DialogController dialogController = Application.CreateController<DialogController>();
            dialogController.Accepting += new EventHandler<DialogControllerAcceptingEventArgs>(dc_Accepting);
            dialogController.SaveOnAccept = false;
            e.ShowViewParameters.Controllers.Add(dialogController);
        }

        #endregion
        #region Stok
        private void StokHareketleriByStokId_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            Type objectType = typeof(StokHareketler);
            IObjectSpace objectSpace = Application.CreateObjectSpace(objectType);
            object selectedStokID = ((Stoklar)View.SelectedObjects[0]).Oid;
            string lvId = ModelNodeIdHelper.GetListViewId(objectType);
            CollectionSource cs = new CollectionSource(objectSpace, objectType);
            CriteriaOperator criteria = CriteriaOperator.Parse("StokID = ? ", selectedStokID);
            if (!(criteria is null))
                cs.Criteria.Add("Criteria", criteria);



            e.ShowViewParameters.CreatedView = Application.CreateListView(lvId, cs, false);
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            e.ShowViewParameters.Context = TemplateContext.PopupWindow;
            DialogController dc = Application.CreateController<DialogController>();
            dc.Accepting += new EventHandler<DialogControllerAcceptingEventArgs>(dc_Accepting);
            dc.SaveOnAccept = false;
            e.ShowViewParameters.Controllers.Add(dc);
        }

        private void StokHareketleriHepsi_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            Type objectType = typeof(StokHareketler);
            IObjectSpace objectSpace = Application.CreateObjectSpace(objectType);
            string listViewId = ModelNodeIdHelper.GetListViewId(objectType);
            CollectionSource collectionSource = new CollectionSource(objectSpace, objectType);
            e.ShowViewParameters.CreatedView = Application.CreateListView(listViewId, collectionSource, false);
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            e.ShowViewParameters.Context = TemplateContext.PopupWindow;
            DialogController dialogController = Application.CreateController<DialogController>();
            dialogController.Accepting += new EventHandler<DialogControllerAcceptingEventArgs>(dc_Accepting);
            dialogController.SaveOnAccept = false;
            e.ShowViewParameters.Controllers.Add(dialogController);
        }
        #endregion

        void dc_Accepting(object sender, DialogControllerAcceptingEventArgs e)
        {
            View popupView = ((Controller)sender).Frame.View;
            popupView.ObjectSpace.CommitChanges();
            if (View is ListView && View.ObjectTypeInfo == popupView.ObjectTypeInfo)
            {
                ((ListView)View).CollectionSource.Add(ObjectSpace.GetObject(popupView.CurrentObject));
            }

        }
        protected override void OnActivated()
        {
            base.OnActivated();

            foreach (ChoiceActionItem items in singleChoiceAction1.Items)
            {
                items.Active.SetItemValue("Active", false);
                foreach (var itm in items.Items)
                {
                    itm.Active.SetItemValue("Active", false);
                }
            }
            //singleChoiceAction1.Active.SetItemValue("Active", true);
            if (View.ObjectTypeInfo.Type == typeof(Cariler))
            {
                foreach (ChoiceActionItem items in singleChoiceAction1.Items)
                {
                    items.Active.SetItemValue("Active", items.Id == "alisSatisIslemleri");
                    foreach (ChoiceActionItem itm in items.Items)
                    {
                        itm.Active.SetItemValue("Active", true);
                    }
                }
            }
            if (View.ObjectTypeInfo.Type == typeof(Stoklar))
            {
                foreach (ChoiceActionItem items in singleChoiceAction1.Items)
                {
                    items.Active.SetItemValue("Active", items.Id == "stokDurumu");
                    foreach (ChoiceActionItem itm in items.Items)
                    {
                        itm.Active.SetItemValue("Active", true);
                    }
                }
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();

        }


    }
}
