using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace StockProgram.Reports
{
    public partial class ucReportMainPage : DevExpress.XtraEditors.XtraUserControl
    {
        public ucReportMainPage()
        {
            InitializeComponent();
        }

        private void btn_urun_gir_Click(object sender, EventArgs e)
        {
            Reports.ucProductTracking ctrl = new Reports.ucProductTracking();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }

        private void btn_urun_sil_Click(object sender, EventArgs e)
        {
            Reports.ucTimeBasedStocksReport ctrl = new Reports.ucTimeBasedStocksReport();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Reports.ucTimeBasedStocksReport ctrl = new Reports.ucTimeBasedStocksReport();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            ((MainForm)this.ParentForm).SettingStatus();
            this.Dispose();
        }

        private void btn_gun_ay_yil_Click(object sender, EventArgs e)
        {
            Reports.ucKarYuzdelik ctrl = new Reports.ucKarYuzdelik();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }

        private void btn_gider_Click(object sender, EventArgs e)
        {
            Reports.ucExpenses ctrl = new Reports.ucExpenses();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }
    }
}
