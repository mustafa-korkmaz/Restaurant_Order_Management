using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace StockProgram.Warehouses
{
    public partial class ucWarehousesMainPage : DevExpress.XtraEditors.XtraUserControl
    {
        public ucWarehousesMainPage()
        {
            InitializeComponent();
        }

        private void btn_depo_gir_Click(object sender, EventArgs e)
        {
            Warehouses.ucAddWarehouse ctrl = new Warehouses.ucAddWarehouse();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }
    }
}
