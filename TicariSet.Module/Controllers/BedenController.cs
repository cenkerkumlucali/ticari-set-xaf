using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using System;
using System.Collections;
using System.Collections.Generic;
using TicariSet.Module.BusinessObjects;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.XtraPivotGrid;

namespace TicariSet.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public abstract partial class BedenController : ViewController
    {

        public BedenController()
        {
            InitializeComponent();
            TargetObjectType = typeof(EvrakSatir);
            //BedenPopup();
        }
        public void BedenPopup()
        {

            Type objectType = typeof(EvrakBeden);
            if (View != null)
            {
                if (View.SelectedObjects.Count != 0)
                {
                    object selected = ((EvrakSatir)View.SelectedObjects[0]).Oid;
                    string listViewId = ModelNodeIdHelper.GetListViewId(objectType);
                    CollectionSource collectionSource = new CollectionSource(ObjectSpace, objectType);
                    CriteriaOperator criteriaOperator = CriteriaOperator.Parse("EvrakSatirId = ?", selected);
                    collectionSource.Criteria.Add("Criteria", criteriaOperator);

                    ListView listView = Application.CreateListView(listViewId, collectionSource, false);
                    ShowViewParameters svp = new ShowViewParameters(listView)
                    {
                        CreatedView = listView,
                        TargetWindow = TargetWindow.NewModalWindow,
                        Context = TemplateContext.PopupWindow
                    };
                    DialogController dialogController = Application.CreateController<DialogController>();
                    dialogController.CloseOnCurrentObjectProcessing = false;
                    dialogController.Accepting += DialogController_Accepting;
                    dialogController.Cancelling += DialogController_Cancelling;
                    dialogController.SaveOnAccept = false;
                    svp.Controllers.Add(dialogController);
                    Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(
                        Application.CreateFrame(TemplateContext.PopupWindow),
                        null));
                }
            }
        }
        private void DialogController_Accepting(object sender, DialogControllerAcceptingEventArgs e)
        {
            IList list = ((ListView)((DialogController)sender).AcceptAction.SelectionContext).CollectionSource.List;
            int total = 0;
            foreach (EvrakBeden item in list)
            {
                total += item.Miktar;
            }

            EvrakSatir.HesaplaMiktar((EvrakSatir) View.CurrentObject, total);

            //showing editor
            //var miktar = 
            //var mikt = ((EvrakBeden) (new System.Linq.SystemCore_EnumerableDebugView(list).Items[1])).miktar;
            //XPCollection<int> miktar = ((EvrakBeden)((XPBaseObject) new System.Linq(list).Items[0]).This).Miktar;
            //var toplamMiktar = ((EvrakBeden)new System.Linq(list).Items[0]).evrakSatirId.miktar;
            //int miktar = ((EvrakBeden)(new System.Linq.Expressions(list).Items)).Miktar;

            //object selected = ((EvrakSatir)View.SelectedObjects[0]).Miktar;
        }
        private void DialogController_Cancelling(object sender, EventArgs e)
        {
            DialogController controller = (DialogController)sender;
            if (controller != null)
            {
                controller.Frame.View.ObjectSpace.Rollback();
            }
        }

        protected abstract void CreateEvent();
     
        protected override void OnActivated()
        {
            base.OnActivated();

            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            CreateEvent();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
