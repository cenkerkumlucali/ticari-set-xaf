using DevExpress.ExpressApp.Web.Templates;

namespace TicariSet.Web
{
    public partial class LoginPage : BaseXafPage {
        public override System.Web.UI.Control InnerContentPlaceHolder {
            get {
                return Content;
            }
        }
    }
}
