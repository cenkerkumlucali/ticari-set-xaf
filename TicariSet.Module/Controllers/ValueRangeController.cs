
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using TicariSet.Module.Interfaces;

namespace TicariSet.Module.Controllers
{

    public partial class ValueRangeController : ViewController, IModelExtender
    {
        private IContainer component;
        public ValueRangeController()
        {
            InitializeComponent();
        }

        protected static Decimal GetMinValue(IModelMember member)
        {
            return (member as IModelMemberRange).MinValue;
        }

        protected static Decimal GetMaxValue(IModelMember member)
        {
            return (member as IModelMemberRange).MaxValue;
        }

        protected static Decimal GetMinValue()
        {
            return 0M;
        }

        protected static Decimal GetMaxValue()
        {
            return 0M;
        }

        void IModelExtender.ExtendModelInterfaces(ModelInterfaceExtenders extenders)
        {
            extenders.Add<IModelMember, IModelMemberRange>();
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            if (this.View is DetailView)
            {
                if (((DetailView)this.View).ViewEditMode != ViewEditMode.Edit)
                    return;
                foreach (PropertyEditor editor in (IEnumerable<PropertyEditor>)((CompositeView)this.View).GetItems<PropertyEditor>())
                    this.SetRange(editor);
            }
            else
            {
                if (!(this.View is ListView) || !(this.View.AllowEdit == true))
                    return;
                this.SetListEditorRange(((ListView)this.View).Editor);
            }
        }
        protected virtual void SetRange(PropertyEditor editor)
        {
        }

        protected virtual void SetListEditorRange(ListEditor editor)
        {
        }

    }
}
