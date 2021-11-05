using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;

namespace TicariSet.Module.Controllers
{
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
            //NewController = Frame.GetController<NewObjectViewController>();
            //if (NewController != null)
            //{
            //    NewController.Active[Key] = !(View.ObjectTypeInfo.Type == typeof(Ulkeler) && View is View ||
            //                                  View.ObjectTypeInfo.Type == typeof(Sehirler) && View is View ||
            //                                  View.ObjectTypeInfo.Type == typeof(Ilceler) && View is View ||
            //                                  View.ObjectTypeInfo.Type == typeof(Fisler) && View is View ||
            //                                  View.ObjectTypeInfo.Type == typeof(BankaHareket) && View is View ||
            //                                  View.ObjectTypeInfo.Type == typeof(BankaOdeme) && View is View ||
            //                                  View.ObjectTypeInfo.Type == typeof(StokHareketler) && View is View);
            //}
        }
        protected override void OnDeactivated()
        {
            //if (NewController != null)
            //{
            //    NewController.Active.RemoveItem(Key);
            //    NewController = null;
            //}
            base.OnDeactivated();
        }
    }
}
