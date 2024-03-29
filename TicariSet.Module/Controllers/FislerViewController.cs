﻿using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using System;
using System.Collections.Generic;
using TicariSet.Module.BusinessObjects;

namespace TicariSet.Module.Controllers
{
    public partial class FislerViewController : ViewController
    {
        public FislerViewController()
        {
                InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            NewObjectViewController controller = Frame.GetController<NewObjectViewController>();
            if (controller != null)
            {
                controller.CollectCreatableItemTypes += Controller_CollectCreatableItemTypes;
                controller.CollectDescendantTypes += Controller_CollectDescendantTypes;
                if(controller.Active)
                    controller.UpdateNewObjectAction();
            }
            // Perform various tasks depending on the target View.
        }

      
        void CustomizeList(ICollection<Type> types)
        {
            List<Type> _list = new List<Type>();
            foreach (Type item in types)
            {
                if(item==typeof(Fisler))
                    _list.Add(item);
            }

            foreach (Type item in _list)
            {
                types.Remove(item);
            }
        }
        private void Controller_CollectDescendantTypes(object sender, CollectTypesEventArgs e)
        {
            CustomizeList(e.Types);
        }
        private void Controller_CollectCreatableItemTypes(object sender, CollectTypesEventArgs e)
        {
            CustomizeList(e.Types);
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            NewObjectViewController controller = Frame.GetController<NewObjectViewController>();
            if (controller != null)
            {
                controller.CollectCreatableItemTypes -= Controller_CollectCreatableItemTypes;
                controller.CollectDescendantTypes -= Controller_CollectDescendantTypes;
            }
            base.OnDeactivated();
        }
    }
}
