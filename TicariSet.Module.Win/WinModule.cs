using System;
using System.ComponentModel;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.BaseImpl;

namespace TicariSet.Module.Win {
    [ToolboxItemFilter("Xaf.Platform.Win")]
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
    public sealed partial class TicariSetWindowsFormsModule : ModuleBase {
        //private void Application_CreateCustomModelDifferenceStore(Object sender, CreateCustomModelDifferenceStoreEventArgs e) {
        //    e.Store = new ModelDifferenceDbStore((XafApplication)sender, typeof(ModelDifference), true, "Win");
        //    e.Handled = true;
        //}
        private void Application_CreateCustomUserModelDifferenceStore(Object sender, CreateCustomModelDifferenceStoreEventArgs e) {
            e.Store = new ModelDifferenceDbStore((XafApplication)sender, typeof(ModelDifference), false, "Win");
            e.Handled = true;
        }
        public TicariSetWindowsFormsModule() {
            InitializeComponent();
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            return ModuleUpdater.EmptyModuleUpdaters;
        }
        public override void Setup(XafApplication application) {
            base.Setup(application);
            //application.CreateCustomModelDifferenceStore += Application_CreateCustomModelDifferenceStore;
            application.CreateCustomUserModelDifferenceStore += Application_CreateCustomUserModelDifferenceStore;
            // Manage various aspects of the application UI and behavior at the module level.
        }
    }
}
