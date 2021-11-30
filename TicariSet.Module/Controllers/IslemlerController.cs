using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using System;
using System.Collections;
using TicariSet.Module.BusinessObjects;
using ListView = DevExpress.ExpressApp.ListView;
using View = DevExpress.ExpressApp.View;

namespace TicariSet.Module.Controllers
{
    public partial class IslemlerController : ViewController
    {

        public IslemlerController()
        {
            RegisterActions(components);
            InitializeComponent();
        }
        private void singleChoiceAction1_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            if (e.CurrentObject != null && e.CurrentObject.GetType() == typeof(Stoklar))
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
            if (e.CurrentObject != null && e.CurrentObject.GetType() == typeof(Cariler))
            {
                switch (((DevExpress.ExpressApp.Actions.SingleChoiceAction)e.Action).SelectedItem.Id)
                {
                    case "alisIslemleri":
                        AlimIslemleri_Execute(sender, e);
                        break;
                    case "satisIslemleri":
                        SatisIslemleri_Execute(sender, e);
                        break;
                    case "kasaOdeme":
                        KasaOdeme_Execute(sender, e);
                        break;
                    case "kasaTahsilat":
                        KasaTahsilat_Execute(sender, e);
                        break;
                    case "bankaOdeme":
                        BankaOdeme_Execute(sender, e);
                        break;
                    case "bankaTahsilat":
                        BankaTahsilat_Execute(sender, e);
                        break;
                }
            }
        }

        #region Cari
        private void AlimIslemleri_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            Type objectType = typeof(Fisler);
            GetCollectionById(objectType, e, turu: "'Alis'", criteriaParameterName: "CariID", criteriaParameterName2: "[Turu] =", and: "AND");
        }
        private void SatisIslemleri_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            Type objectType = typeof(Fisler);
            GetCollectionById(objectType, e, turu: "'Satis'", criteriaParameterName: "CariID", criteriaParameterName2: "[Turu] =", and: "AND");
        }
        private void KasaOdeme_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            Type objectType = typeof(KasaHareket);
            GetCollectionById(objectType, e, turu: "'Odeme'", criteriaParameterName: "CariID", criteriaParameterName2: "[Hareket] =", and: "AND");
        }
        private void KasaTahsilat_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            Type objectType = typeof(KasaHareket);
            GetCollectionById(objectType, e, turu: "'Tahsilat'", criteriaParameterName: "CariID", criteriaParameterName2: "[Hareket] =", and: "AND");
        }
        private void BankaOdeme_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            Type objectType = typeof(KasaHareket);
            GetCollectionById(objectType, e, turu: "'BankaOdeme'", criteriaParameterName: "CariID", criteriaParameterName2: "[Hareket] =", and: "AND");
        }
        private void BankaTahsilat_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            Type objectType = typeof(KasaHareket);
            GetCollectionById(objectType, e, turu: "'BankaTahsilat'", criteriaParameterName: "CariID", criteriaParameterName2: "[Hareket] =", and: "AND");
        }
        void CariAlisIslemlerView_SelectionChanged()
        {

            Type fisler = typeof(Fisler);
            IObjectSpace objectSpace = Application.CreateObjectSpace(fisler);
            CollectionSource collectionSource = new CollectionSource(objectSpace, fisler);
            object selectedCari = ((Cariler)View.SelectedObjects[0]).Oid;
            CriteriaOperator criteria = CriteriaOperator.Parse("CariID = ? ", selectedCari);
            if (!(criteria is null))
                collectionSource.Criteria.Add("Criteria", criteria);
            IList result = ObjectSpace.GetObjects(type: fisler, criteria: criteria);
            if (result.Count == 0)
            {
                foreach (ChoiceActionItem items in singleChoiceAction1.Items)
                {

                    if (items.Id == "alisSatisIslemleri")
                    {
                        items.Active.SetItemValue("Active", true);
                    }

                    if (items.Id == "kasaHareketleri")
                    {
                        items.Active.SetItemValue("Active", true);
                    }

                    foreach (ChoiceActionItem itm1 in items.Items)
                    {

                        if (itm1.Id == "alisIslemleri")
                        {
                            itm1.Active.SetItemValue("Active", true);
                            itm1.Enabled.SetItemValue("Enable", false);
                        }

                        if (itm1.Id == "satisIslemleri")
                        {
                            itm1.Active.SetItemValue("Active", true);
                            itm1.Enabled.SetItemValue("Enable", false);
                        }
                    }
                }
            }
            else if (result.Count > 0)
            {
                foreach (ChoiceActionItem items in singleChoiceAction1.Items)
                {
                    if (items.Id == "alisSatisIslemleri")
                    {
                        items.Active.SetItemValue("Active", true);
                    }

                    if (items.Id == "kasaHareketleri")
                    {
                        items.Active.SetItemValue("Active",true);
                    }
                    foreach (ChoiceActionItem itm1 in items.Items)
                    {

                        if (itm1.Id == "alisIslemleri")
                        {
                            itm1.Active.SetItemValue("Active", true);
                            itm1.Enabled.SetItemValue("Enable", true);
                        }

                        if (itm1.Id == "satisIslemleri")
                        {
                            itm1.Active.SetItemValue("Active", true);
                            itm1.Enabled.SetItemValue("Enable", true);
                        }
                        if (itm1.Id == "kasaOdeme")
                        {
                            itm1.Active.SetItemValue("Active", true);
                            itm1.Enabled.SetItemValue("Enable", true);
                        }
                        if (itm1.Id == "kasaTahsilat")
                        {
                            itm1.Active.SetItemValue("Active", true);
                            itm1.Enabled.SetItemValue("Enable", true);
                        }
                        if (itm1.Id == "bankaOdeme")
                        {
                            itm1.Active.SetItemValue("Active", true);
                            itm1.Enabled.SetItemValue("Enable", true);
                        }
                        if (itm1.Id == "bankaTahsilat")
                        {
                            itm1.Active.SetItemValue("Active", true);
                            itm1.Enabled.SetItemValue("Enable", true);
                        }
                    }
                }
            }
        }
        #endregion

        #region Stok
        private void StokHareketleriByStokId_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            Type objectType = typeof(StokHareketler);
            GetCollectionById(objectType, e, "StokID");
        }
        private void StokHareketleriHepsi_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            Type objectType = typeof(StokHareketler);
            GetCollection(objectType, e);
        }
        void GetStokHarekelerView_SelectionChanged()
        {
            Type objectType = typeof(StokHareketler);
            IObjectSpace objectSpace = Application.CreateObjectSpace(objectType);
            object selectedStokID = ((Stoklar)View.SelectedObjects[0]).Oid;
            CollectionSource collectionSource = new CollectionSource(objectSpace, objectType);
            CriteriaOperator criteria = CriteriaOperator.Parse("StokID = ? ", selectedStokID);
            collectionSource.Criteria.Add("Criteria", criteria);
            int result = objectSpace.GetObjectsCount(objectType, criteria);
            
            if (result == 0)
            {
                foreach (ChoiceActionItem items in singleChoiceAction1.Items)
                {

                    items.Active.SetItemValue("Active", items.Id == "stokDurumu");
                    foreach (ChoiceActionItem itm1 in items.Items)
                    {

                        if (itm1.Id == "stokHareketleri")
                        {
                            itm1.Active.SetItemValue("Active", true);
                            itm1.Enabled.SetItemValue("Enable", false);
                        }

                        if (itm1.Id == "stokHareketleriHepsi")
                        {
                            itm1.Active.SetItemValue("Active", true);
                            itm1.Enabled.SetItemValue("Enable", true);
                        }
                    }
                }
            }
            else if (result > 0)
            {
                foreach (ChoiceActionItem items in singleChoiceAction1.Items)
                {
                    items.Active.SetItemValue("Active", items.Id == "stokDurumu");
                    foreach (ChoiceActionItem itm1 in items.Items)
                    {
                        if (itm1.Id == "stokHareketleri")
                        {
                            itm1.Active.SetItemValue("Active", true);
                            itm1.Enabled.SetItemValue("Enable", true);
                        }

                        if (itm1.Id == "stokHareketleriHepsi")
                        {
                            itm1.Active.SetItemValue("Active", true);
                            itm1.Enabled.SetItemValue("Enable", true);
                        }
                    }
                }
            }
        }

        #endregion

        void GetCollectionById(Type objectType, SingleChoiceActionExecuteEventArgs e, string criteriaParameterName, string turu = "",
           string criteriaParameterName2 = "", string and = "")
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace(objectType);
            string listViewId = ModelNodeIdHelper.GetListViewId(objectType);
            CollectionSource collectionSource = new CollectionSource(objectSpace, objectType);
            if (View.CurrentObject.GetType() == typeof(Cariler))
            {
                object selectedCari = ((Cariler)View.SelectedObjects[0]).Oid;
                CriteriaOperator criteriaOperator = CriteriaOperator
                    .Parse($"[{criteriaParameterName}] = ? {and} {criteriaParameterName2}{turu}", selectedCari);
                if (!(criteriaOperator is null))
                    collectionSource.Criteria.Add("Criteria", criteriaOperator);
            }
            if (View.CurrentObject.GetType() == typeof(Stoklar))
            {
                object selectedStok = ((Stoklar)View.SelectedObjects[0]).Oid;
                CriteriaOperator criteriaOperator = 
                    CriteriaOperator.Parse($"[{criteriaParameterName}] = ? {and} {criteriaParameterName2}{turu}", selectedStok);
                if (!(criteriaOperator is null))
                    collectionSource.Criteria.Add("Criteria", criteriaOperator);
            }
            e.ShowViewParameters.CreatedView = Application.CreateListView(listViewId, collectionSource, false);
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            e.ShowViewParameters.Context = TemplateContext.PopupWindow;
            DialogController dialogController = Application.CreateController<DialogController>();
            dialogController.FrameAssigned += new EventHandler(DialogController_FrameAssigned);
            dialogController.Accepting += new EventHandler<DialogControllerAcceptingEventArgs>(dc_Accepting);
            dialogController.SaveOnAccept = false;
            e.ShowViewParameters.Controllers.Add(dialogController);

        }
        void GetCollection(Type objectType, SingleChoiceActionExecuteEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace(objectType);
            string listViewId = ModelNodeIdHelper.GetListViewId(objectType);
            CollectionSource collectionSource = new CollectionSource(objectSpace, objectType);
            e.ShowViewParameters.CreatedView = Application.CreateListView(listViewId, collectionSource, false);
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            e.ShowViewParameters.Context = TemplateContext.PopupWindow;
            DialogController dialogController = Application.CreateController<DialogController>();
            dialogController.FrameAssigned += new EventHandler(DialogController_FrameAssigned);
            dialogController.Accepting += new EventHandler<DialogControllerAcceptingEventArgs>(dc_Accepting);
            dialogController.SaveOnAccept = false;
            e.ShowViewParameters.Controllers.Add(dialogController);
        }
        void DialogController_FrameAssigned(object sender, EventArgs e)
        {
            ListViewProcessCurrentObjectController listViewProcessCurrentObjectController =
                ((Controller)sender).Frame.GetController<ListViewProcessCurrentObjectController>();
            listViewProcessCurrentObjectController.CustomProcessSelectedItem += listViewProcessCurrentObjectController_CustomProcessSelectedItem;
        }
        void listViewProcessCurrentObjectController_CustomProcessSelectedItem(object sender, CustomProcessListViewSelectedItemEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            object obj = objectSpace.GetObject(e.InnerArgs.CurrentObject);
            e.InnerArgs.ShowViewParameters.CreatedView = Application.CreateDetailView(objectSpace, obj);
            e.InnerArgs.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            e.Handled = true;
        }
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
            if (View.ObjectTypeInfo.Type == typeof(Cariler))
            {
                foreach (ChoiceActionItem items in singleChoiceAction1.Items)
                {
                    if (items.Id == "kasaHareketleri")
                    {
                        items.Active.SetItemValue("Active", true);
                    }
                    if (items.Id == "alisSatisIslemleri")
                    {
                        items.Active.SetItemValue("Active", true);
                    }
                    foreach (ChoiceActionItem itm in items.Items)
                    {
                        itm.Active.SetItemValue("Active", true);
                    }
                }
            }
            //View.CurrentObjectChanged += View_CurrentObjectChanged;
            View.SelectionChanged += View_SelectionChanged;

        }

        //private void View_CurrentObjectChanged(object sender, EventArgs e)
        //{
        //    if (View.CurrentObject is )
        //    {
        //        View.AllowEdit["CurrentUser"] = (()View.CurrentObject).Owner.Id == SecuritySystem.CurrentUserId;
        //    }
        //}

        private void View_SelectionChanged(object sender, EventArgs e)
        {
            if (View.CurrentObject != null && View.CurrentObject.GetType() == typeof(Cariler))
                CariAlisIslemlerView_SelectionChanged();

            if (View.CurrentObject != null && View.CurrentObject.GetType() == typeof(Stoklar))
                GetStokHarekelerView_SelectionChanged();
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            //View.CurrentObjectChanged += View_CurrentObjectChanged;
            View.SelectionChanged -= View_SelectionChanged;
            base.OnDeactivated();

        }
    }
}
