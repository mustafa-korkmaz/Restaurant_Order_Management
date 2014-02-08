using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using DevExpress.XtraEditors;
using System.IO;
using StockProgram.DBObjects;
using StockProgram.Menu;

namespace StockProgram.Sales
{
    public partial class ucSaleOptions : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        public ucSaleOptions()
        {
            InitializeComponent();
        }

   
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ((MainForm)this.ParentForm).SettingStatus();
            this.Dispose();
        }

        private void btn_masa_Click(object sender, EventArgs e)
        {

        }

        private void btn_chekout_Click(object sender, EventArgs e)
        {
            Tables.ucTablesMainPage ctrl = new Tables.ucTablesMainPage();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }

        private void btn_paket_Click(object sender, EventArgs e)
        {
            ucMenu ctrl = new ucMenu(Enums.OrderType.Paket);
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }

        private void btn_restoranici_Click(object sender, EventArgs e)
        {
            DailySales.ucOpenOrders ctrl = new DailySales.ucOpenOrders();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }

 
   }
}
