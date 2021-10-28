
using DevExpress.ExpressApp.Actions;
using DevExpress.XtraBars.Navigation;

namespace TicariSet.Module.Controllers
{
    partial class IslemlerController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem1 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem2 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem3 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem4 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem5 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem6 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem7 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem8 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem9 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem10 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem11 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            this.singleChoiceAction1 = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            


            this.singleChoiceAction1.Items.Add(choiceActionItem1);
            choiceActionItem1.Items.Add(choiceActionItem2);
            choiceActionItem1.Items.Add(choiceActionItem3);

            this.singleChoiceAction1.Items.Add(choiceActionItem4);
            choiceActionItem4.Items.Add(choiceActionItem5);
            choiceActionItem4.Items.Add(choiceActionItem6);

            this.singleChoiceAction1.Items.Add(choiceActionItem7);
            choiceActionItem7.Items.Add(choiceActionItem8);
            choiceActionItem7.Items.Add(choiceActionItem9);
            choiceActionItem7.Items.Add(choiceActionItem10);
            choiceActionItem7.Items.Add(choiceActionItem11);

            this.singleChoiceAction1.ShowItemsOnClick = true;
            this.singleChoiceAction1.ToolTip = null;
            this.singleChoiceAction1.ImageName = "SearchSettingButton";

            this.singleChoiceAction1.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.singleChoiceAction1_Execute);
            //this.singleChoiceAction1.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.StokHareketleriByStokId_Execute);

            this.singleChoiceAction1.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.singleChoiceAction1.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            // 
            // IslemlerController
            // 
            this.singleChoiceAction1.Caption = "İşlemler";
            this.singleChoiceAction1.ConfirmationMessage = null;
            this.singleChoiceAction1.Id = "834d9df4-1f87-4b91-af78-ec99c10d30e0";

            #region Stok
            choiceActionItem1.Caption = "Stok Hareketleri";
            choiceActionItem1.Id = "stokDurumu";
            choiceActionItem1.ImageName = "BO_Report";
            choiceActionItem1.Shortcut = null;
            choiceActionItem1.ToolTip = null;

            choiceActionItem2.Caption = "Stok Hareketleri";
            choiceActionItem2.Id = "stokHareketleri";
            choiceActionItem2.Data = 10001;
            choiceActionItem2.ImageName = "BO_Report";
            choiceActionItem2.Shortcut = null;
            choiceActionItem2.ToolTip = null;

            choiceActionItem3.Caption = "Stok Hareketleri (Hepsi)";
            choiceActionItem3.Id = "stokHareketleriHepsi";
            choiceActionItem3.Data = 10002;
            choiceActionItem3.ImageName = "BO_Report";
            choiceActionItem3.Shortcut = null;
            choiceActionItem3.ToolTip = null;

            #endregion

            #region Cari

            choiceActionItem4.Caption = "Alış Satış İşlemleri";
            choiceActionItem4.Id = "alisSatisIslemleri";
            choiceActionItem4.ImageName = "Paid";
            choiceActionItem4.Shortcut = null;
            choiceActionItem4.ToolTip = null;

            choiceActionItem5.Caption = "Alış İşlemleri";
            choiceActionItem5.Id = "alisIslemleri";
            choiceActionItem5.ImageName = "Paid";
            choiceActionItem5.Shortcut = null;
            choiceActionItem5.ToolTip = null;

            choiceActionItem6.Caption = "Satış İşlemleri";
            choiceActionItem6.Id = "satisIslemleri";
            choiceActionItem6.ImageName = "Business_Cash";
            choiceActionItem6.Shortcut = null;
            choiceActionItem6.ToolTip = null;

            choiceActionItem7.Caption = "Kasa Hareketleri";
            choiceActionItem7.Id = "kasaHareketleri";
            choiceActionItem7.ImageName = "Currency";
            choiceActionItem7.Shortcut = null;
            choiceActionItem7.ToolTip = null;

            choiceActionItem8.Caption = "Kasa Ödeme";
            choiceActionItem8.Id = "kasaOdeme";
            choiceActionItem8.ImageName = "Currency";
            choiceActionItem8.Shortcut = null;
            choiceActionItem8.ToolTip = null;

            choiceActionItem9.Caption = "Kasa Tahsilat";
            choiceActionItem9.Id = "kasaTahsilat";
            choiceActionItem9.ImageName = "Currency";
            choiceActionItem9.Shortcut = null;
            choiceActionItem9.ToolTip = null;

            choiceActionItem10.Caption = "Banka Ödeme";
            choiceActionItem10.Id = "bankaOdeme";
            choiceActionItem10.ImageName = "Currency";
            choiceActionItem10.Shortcut = null;
            choiceActionItem10.ToolTip = null;

            choiceActionItem11.Caption = "Banka Tahsilat";
            choiceActionItem11.Id = "bankaTahsilat";
            choiceActionItem11.ImageName = "Currency"; 
            choiceActionItem11.Shortcut = null;
            choiceActionItem11.ToolTip = null;

            #endregion
            this.Actions.Add(this.singleChoiceAction1);
            

        }
        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction singleChoiceAction1;
    }
}
