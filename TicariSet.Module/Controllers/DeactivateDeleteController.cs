using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
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
