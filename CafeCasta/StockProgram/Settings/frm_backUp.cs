using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StockProgram.Settings
{
    public partial class frm_backUp : Form
    {
        MySqlBackup mb;
        int curPercentage;
        string root = Application.StartupPath + StaticObjects.MainBackupPath;

        public frm_backUp()
        {
            InitializeComponent(); 
            mb = new MySqlBackup();
            mb.ExportProgressChanged += new MySqlBackup.exportProgressChange(mb_ExportProgressChanged);
            mb.ExportCompleted += new MySqlBackup.exportComplete(mb_ExportCompleted);
            mb.ImportProgressChanged += new MySqlBackup.importProgressChange(mb_ImportProgressChanged);
            mb.ImportCompleted += new MySqlBackup.importComplete(mb_ImportCompleted);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btRestore_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void mb_ExportCompleted(object sender, ExportCompleteArg e)
        {
            timerStop.Start();
            string errMsg = null;
            if (e.Error != null)
            {
                errMsg = e.Error.ToString();
                MessageBox.Show("Export " + e.CompletedType.ToString() + Environment.NewLine + errMsg);
            }
            else
            {
                errMsg = "";
            }
            //MessageBox.Show("Export " + e.CompletedType.ToString() + Environment.NewLine + errMsg);
        }

        private void mb_ImportCompleted(object sender, ImportCompleteArg e)
        {
            timerStop.Start();
            string errMsg = null;
            if (e.LastError != null)
            {
                errMsg = e.LastError.ToString();
                MessageBox.Show("Import " + e.CompletedType.ToString() + Environment.NewLine + errMsg);
            }
            else
            {
                errMsg = "";
            }
        //    MessageBox.Show("Import " + e.CompletedType.ToString() + Environment.NewLine + errMsg);
        }

        private void mb_ImportProgressChanged(object sender, ImportProgressArg e)
        {
            curPercentage = e.PercentageCompleted;
        }

        private void mb_ExportProgressChanged(object sender, ExportProgressArg e)
        {
            curPercentage = e.PercentageCompleted;
        }

        private void timerRead_Tick(System.Object sender, System.EventArgs e)
        {
            ProgressBar1.Value = curPercentage;
            lb_Progress.Text = "Progress " + curPercentage.ToString() + "%";
        }

        private void timerStop_Tick(System.Object sender, System.EventArgs e)
        {
            timerRead.Stop();
            timerStop.Stop();
        }

        private void lb_Progress_Click(object sender, EventArgs e)
        {

        }

        private void ProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void btn_backUp_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("dd-MM-yyyy");
            string time = DateTime.Now.ToString("HH-mm-ss");
            string back_up_file_name = date + "_" + time + ".sql";

            SaveFileDialog sf = new SaveFileDialog();
            sf.InitialDirectory = root;
            sf.FileName = back_up_file_name;
            if (sf.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            ProgressBar1.Value = 0;
            timerRead.Start();
            mb.Connection = new MySqlConnection(StaticObjects.MySqlConn);
            mb.ExportInfo.FileName = sf.FileName;
            mb.ExportInfo.AsynchronousMode = true;
            mb.ExportInfo.CalculateTotalRowsFromDatabase = true;
            mb.ExportInfo.ExportTableStructure = true;
            mb.ExportInfo.ExportRows = true;
            mb.Export();

        }

        private void btn_restore_Click(object sender, EventArgs e)
        {
            OpenFileDialog oof = new OpenFileDialog();
            oof.Multiselect = false;
            if (oof.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            ProgressBar1.Value = 0;
            timerRead.Start();
            mb.Connection = new MySqlConnection(StaticObjects.MySqlConn);
            mb.ImportInfo.AsynchronousMode = true;
            mb.ImportInfo.FileName = oof.FileName;
            mb.Import();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
