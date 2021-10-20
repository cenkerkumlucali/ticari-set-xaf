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
using DevExpress.Xpo;
using TicariSet.Module.BusinessObjects;


namespace TicariSet.Module.Controllers
{
    public partial class ShowListViewController : ViewController
    {
        public ShowListViewController() {
        PopupWindowShowAction showListViewAction = new PopupWindowShowAction(this, "ShowListView",
            PredefinedCategory.Edit);
        this.TargetObjectType = typeof(MasrafGrubu);
        showListViewAction.CustomizePopupWindowParams += ShowListViewAction_CustomizePopupWindowParams;
    }
    private void ShowListViewAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e) {
        Type objectType = typeof(Masraflar);
        e.View = Application.CreateListView(objectType, false);
    }
    }
}
