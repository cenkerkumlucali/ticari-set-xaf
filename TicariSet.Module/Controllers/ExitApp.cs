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
using DevExpress.Office.Utils;
using DevExpress.Utils;
using DevExpress.Utils.CommonDialogs.Internal;
using DevExpress.XtraEditors;

namespace TicariSet.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
    public partial class ExitApp : WindowController
    {
        private DialogController controller = new DialogController();
        private PopupWindowShowAction popup;
        public ExitApp()
        {
            if (popup != null)
            {
                popup = new PopupWindowShowAction(this, "ShowPopup", PredefinedCategory.Windows);
                popup.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
                popup.IsSizeable = Active;

            }

            InitializeComponent();
            // Target required Windows (via the TargetXXX properties) and create their Actions.
        }
    
        protected override void OnActivated()
        {
            base.OnActivated();
            controller = Frame.GetController<DialogController>();
        }
        void controller_Accepting(object sender, DialogControllerAcceptingEventArgs e)
        {
            ShowNewView();
        }
        void ShowNewView()
        {

            var svp = new ShowViewParameters();
            var app = Application;
            svp.CreatedView = app.CreateDetailView(ObjectSpaceInMemory.CreateNew(), new WinMessageOptions().Caption);
            svp.CreatedView.Caption = "Request Submitted";
            svp.TargetWindow = TargetWindow.NewModalWindow;
            svp.Context = TemplateContext.PopupWindow;
            svp.CreateAllControllers = true;
            var dc = app.CreateController<DialogController>();
            svp.Controllers.Add(dc);
            app.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));


        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();

        }
    }
}
