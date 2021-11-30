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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Win.SystemModule;
using DevExpress.XtraEditors;

namespace TicariSet.Module.Win.Controllers
{
    public partial class ApplicationClosing : WindowController
    {
        private EditModelController _editModelController;
        public ApplicationClosing()
        {
            InitializeComponent();
        }
        private bool execute;
        private void EditModelAction_ExecuteCompleted(object sender, ActionBaseEventArgs e)
        {
            this.execute = false;
        }
        private void EditModelAction_Executing(object sender, CancelEventArgs e)
        {
            this.execute = true;
        }

        protected override void OnWindowChanging(Window window)
        {
            base.OnWindowChanging(window);
            window.TemplateChanged += this.OnWindowTemplateChanged;
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            if (((FormClosingEventArgs)e).CloseReason != CloseReason.UserClosing || this.execute || 
                ((WinShowViewStrategyBase)this.Application.ShowViewStrategy).Explorers.Count != 1)
                return;
            if (XtraMessageBox.Show("Kapatmak istediğinize emin misiniz ?",
                GetLocalized("Attention"), 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Asterisk) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        private void OnWindowClosed(object sender, EventArgs e)
        {
            ((WinWindow)sender).Closing -= this.WindowClosing;
            ((WinWindow)sender).Closed -= this.OnWindowClosed;
        }
        private void OnWindowTemplateChanged(object sender, EventArgs e)
        {
            this.Window.TemplateChanged -= new EventHandler(this.OnWindowTemplateChanged);
            ((WinWindow)this.Window).Closing += this.WindowClosing;
            ((WinWindow)this.Window).Closed += this.OnWindowClosed;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            this._editModelController = this.Window.GetController<EditModelController>();
            if (this._editModelController == null)
                return;
            this._editModelController.EditModelAction.Executing += this.EditModelAction_Executing;
            this._editModelController.EditModelAction.ExecuteCompleted += this.EditModelAction_ExecuteCompleted;
        }
        protected override void OnDeactivated()
        {
            if (this._editModelController != null)
            {
                this._editModelController.EditModelAction.Executing -= this.EditModelAction_Executing;
                this._editModelController.EditModelAction.ExecuteCompleted -= this.EditModelAction_ExecuteCompleted;
                this._editModelController = null;
            }
            this.Window.TemplateChanged -= this.OnWindowTemplateChanged;
            ((WinWindow)this.Window).Closing -= this.WindowClosing;
            ((WinWindow)this.Window).Closed -= this.OnWindowClosed;
            base.OnDeactivated();
        }
        public static string GetLocalized(string name)
        {
            return CaptionHelper.GetLocalizedText("CustomLocalizer", name);
        }
    }
}
