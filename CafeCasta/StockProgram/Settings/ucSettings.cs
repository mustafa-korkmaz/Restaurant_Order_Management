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

namespace StockProgram.Settings
{
    public partial class ucSettings : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        public ucSettings()
        {
            InitializeComponent();
        }

        private void btn_back_up_Click(object sender, EventArgs e)
        {
            BackUp();      
        }

        private void BackUp()
        {
            frm_backUp frm = new frm_backUp();
            frm.ShowDialog(this);
        }
        private void DumpDB()
        {
            string date = DateTime.Now.ToString("dd-MM-yyyy");
            string time = DateTime.Now.ToString("HH-mm-ss");
            string back_up_file_name = date + "_" + time+".sql";
            string root = Application.StartupPath + StaticObjects.MainBackupPath  + back_up_file_name;

            StreamWriter file = new StreamWriter(root);
            ProcessStartInfo proc = new ProcessStartInfo();
            ErrorMessages.Message message = new ErrorMessages.Message();

            try
            {
                string cmd = string.Format(" -u {0} -p {1} -h {2} {3}", "root", "", "localhost", "cafecasta");
                proc.FileName = @"C:\xampp\mysql\bin\mysqldump.exe";

                proc.RedirectStandardInput = false;
                proc.RedirectStandardOutput = true;

                proc.Arguments = cmd;//"-u root -p smartdb > testdb.sql";

                proc.UseShellExecute = false;
                Process p = Process.Start(proc);

                string res;
                res = p.StandardOutput.ReadToEnd();

                file.WriteLine(res);

                p.WaitForExit();
                message.WriteMessage("Yedekleme işlemi başarıyla gerçekleşti.", MessageBoxIcon.Information, MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at         
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                file.Close();
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ((MainForm)this.ParentForm).SettingStatus();
            this.Dispose();
        }

        private void btn_renk_Click(object sender, EventArgs e)
        {
            ucOptions ctrl = new ucOptions();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }

        private void btn_staff_Click(object sender, EventArgs e)
        {
            ucStaff ctrl = new ucStaff();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }

        private void btn_masa_Click(object sender, EventArgs e)
        {
            ucTables ctrl = new ucTables();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }

        private void btn_yerlesim_Click(object sender, EventArgs e)
        {
            ucTableCats ctrl = new ucTableCats();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }

        private void btn_yazdir_Click(object sender, EventArgs e)
        {
            ucPrinters ctrl = new ucPrinters();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }

        private void btn_settings_Click(object sender, EventArgs e)
        {
            ucGeneralSettings ctrl = new ucGeneralSettings();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }
   }
}
