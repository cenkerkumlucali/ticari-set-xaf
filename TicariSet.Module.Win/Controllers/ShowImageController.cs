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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ListView = DevExpress.ExpressApp.ListView;

namespace TicariSet.Module.Win.Controllers
{
    public partial class ShowImageController : ViewController<ListView>
    {
        private ListViewProcessCurrentObjectController _listViewProcessCurrentObjectController;

        protected override void OnActivated()
        {
            base.OnActivated();
            this._listViewProcessCurrentObjectController = this.Frame.GetController<ListViewProcessCurrentObjectController>();
            this._listViewProcessCurrentObjectController.CustomProcessSelectedItem += new EventHandler<CustomProcessListViewSelectedItemEventArgs>(this.controller_CustomProcessSelectedItem);
        }

        protected override void OnDeactivated()
        {
            this._listViewProcessCurrentObjectController.CustomProcessSelectedItem -= new EventHandler<CustomProcessListViewSelectedItemEventArgs>(this.controller_CustomProcessSelectedItem);
            base.OnDeactivated();
        }

        private void controller_CustomProcessSelectedItem(
          object sender,
          CustomProcessListViewSelectedItemEventArgs e)
        {
            if (!(((ListView)View).Editor is GridListEditor editor))
                return;
            GridHitInfo gridHitInfo = editor.GridView.CalcHitInfo(editor.Grid.PointToClient(Control.MousePosition));
            if (!gridHitInfo.InRowCell)
                return;
            object rowCellValue = editor.GridView.GetRowCellValue(gridHitInfo.RowHandle, gridHitInfo.Column);
            if (!(rowCellValue is Image))
                return;
            this.ShowImageDialog((Image)rowCellValue);
            e.Handled = true;
        }

        private void ShowImageDialog(Image image)
        {
            Form form = new Form();
            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Image = image;
            form.Controls.Add((Control)pictureBox);
            form.Size = new Size(300, 300);
            form.ControlBox = true;
            pictureBox.DoubleClick += new EventHandler(this.pbox_DoubleClick);
            int num = (int)form.ShowDialog();
            form.Dispose();
        }

        private void pbox_DoubleClick(object sender, EventArgs e) => ((Control)sender).FindForm()?.Close();

    }
}
