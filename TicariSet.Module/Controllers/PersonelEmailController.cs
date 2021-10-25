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
