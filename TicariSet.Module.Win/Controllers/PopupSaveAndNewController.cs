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

namespace TicariSet.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class PopupSaveAndNewController : ViewController
    {
        SimpleAction popupSaveAndNewAction;

        public PopupSaveAndNewController()
        {
            TargetViewId = "StokHareketler_DetailView";
            popupSaveAndNewAction = new SimpleAction(this, "PopupSaveAndNew",
                DevExpress.Persistent.Base.PredefinedCategory.PopupActions);
            popupSaveAndNewAction.Caption = "Save and New";
            popupSaveAndNewAction.Execute += popupSaveAndNewAction_Execute;
        }

        void popupSaveAndNewAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ObjectSpace.CommitChanges();
            LinkToListViewController controller = Frame.GetController<LinkToListViewController>();
            if (controller != null && controller.Link != null && controller.Link.ListView != null)
            {
                ListView sourceListView = controller.Link.ListView;
                sourceListView.CollectionSource.Add(sourceListView.ObjectSpace.GetObject(View.CurrentObject));
            }

            IObjectSpace newObjectSpace = null;
            if (ObjectSpace is INestedObjectSpace)
            {
                newObjectSpace = ((INestedObjectSpace)ObjectSpace).ParentObjectSpace.CreateNestedObjectSpace();
            }
            else
            {
                newObjectSpace = Application.CreateObjectSpace(View.ObjectTypeInfo.Type);
            }

            StokHareketler newContact = newObjectSpace.CreateObject<StokHareketler>();
            DetailView newDetailView = Application.CreateDetailView(newObjectSpace, newContact);
            newDetailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            e.ShowViewParameters.CreatedView = newDetailView;
            e.ShowViewParameters.TargetWindow = TargetWindow.Current;
        }

        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
            popupSaveAndNewAction.Active["PopupWindowContext"] = Frame.Context == TemplateContext.PopupWindow;
        }
    }
}
