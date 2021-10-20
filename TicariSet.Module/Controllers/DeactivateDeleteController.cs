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
using DevExpress.Data.Linq.Helpers;
using TicariSet.Module.BusinessObjects;

namespace TicariSet.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class DeactivateDeleteController : ObjectViewController
    {
        private const string Key = "Deactivation in code";
        DeleteObjectsViewController DeleteController;
        
        public DeactivateDeleteController()
        {
            InitializeComponent();
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            DeleteController =
                Frame.GetController<DeleteObjectsViewController>();

            if (DeleteController != null)
            {
                DeleteController.Active[Key] =
                    !(View.ObjectTypeInfo.Type == typeof(Sehirler) && View is View||
                      View.ObjectTypeInfo.Type == typeof(Ilceler) && View is View||
                      View.ObjectTypeInfo.Type == typeof(Ulkeler) && View is View);
            }
        }

        protected override void OnDeactivated()
        {
            if (DeleteController != null)
            {
                DeleteController.Active.RemoveItem(Key);
                DeleteController = null;
            }
            base.OnDeactivated();
        }
    }
}
