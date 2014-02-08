using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StockProgram.DBObjects;

namespace StockProgram.Sales
{
    public partial class frmProductView : DevExpress.XtraEditors.XtraForm
    {
        private int Id;
        public frmProductView(int Id)
        {
            this.Id = Id;
            InitializeComponent();
            frmSettings();
        }

        private void frmSettings()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btn_tamamla_Click(object sender, EventArgs e)
        {
            //yapılan değişikliklerin (renk ve size larda ) tutulmsı gerekiyor.
            this.Close();
        }

        private void frmColorSizePopup_Load(object sender, EventArgs e)
        {
            Products.ucViewProduct ctrl = new Products.ucViewProduct(this.Id);
            ctrl.SetLabelsForSaleView(); // görünümü satış izleme ekranı için değiştir
            ctrl.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl);
            ctrl.SetProductPreviewEvent();
        }

    }
}