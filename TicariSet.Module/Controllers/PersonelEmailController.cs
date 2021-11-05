using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using TicariSet.Module.BusinessObjects;

namespace TicariSet.Module.Controllers
{
    public partial class EditPersonelIletisimBilgileriController : ViewController
    {
        public EditPersonelIletisimBilgileriController()
        {
            TargetObjectType = typeof(PersonelIletisimBilgileri);
            SimpleAction editPersonelIletisimBilgileriAction =
                new SimpleAction(this, "Seçili iletişim bilgisini düzenle", PredefinedCategory.Edit);
            editPersonelIletisimBilgileriAction.ImageName = "Action_Edit";
            editPersonelIletisimBilgileriAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            editPersonelIletisimBilgileriAction.Execute += editAddressBookRecordAction_Execute;
        }
        void editAddressBookRecordAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ListViewProcessCurrentObjectController.ShowObject(
                e.CurrentObject, e.ShowViewParameters, Application, Frame, View);
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
