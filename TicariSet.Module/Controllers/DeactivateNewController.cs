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
    public partial class DeactivateNewController : ViewController
    {
        private const string Key = "Deactivation in code";
        NewObjectViewController NewController;

        public DeactivateNewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            NewController = Frame.GetController<NewObjectViewController>();
            if (NewController != null)
            {
                NewController.Active[Key] = !(View.ObjectTypeInfo.Type == typeof(Ulkeler) && View is View ||
                                              View.ObjectTypeInfo.Type == typeof(Sehirler) && View is View ||
                                              View.ObjectTypeInfo.Type == typeof(Ilceler) && View is View ||
                                              View.ObjectTypeInfo.Type == typeof(Fisler) && View is View ||
                                              View.ObjectTypeInfo.Type == typeof(BankaHareket) && View is View ||
                                              View.ObjectTypeInfo.Type == typeof(BankaOdeme) && View is View ||
                                              View.ObjectTypeInfo.Type == typeof(StokHareketler) && View is View);
            }
        }
        protected override void OnDeactivated()
        {
            if (NewController != null)
            {
                NewController.Active.RemoveItem(Key);
                NewController = null;
            }
            base.OnDeactivated();
        }
    }
}
