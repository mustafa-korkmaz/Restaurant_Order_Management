using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using StockProgram.DBObjects;

namespace StockProgram.Settings
{
    public partial class ucPrinters : DevExpress.XtraEditors.XtraUserControl
    {
        private bool firstLoad;
        private ExceptionLogger excLogger;
        public ucPrinters()
        {
            InitializeComponent();
            gridView1.OptionsView.ShowGroupPanel = false;
            firstLoad = true;
            FillGrid();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           ucSettings ctrl = new ucSettings();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }

        private void FillGrid()
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            string sql = "select *,if(type=0,'MUTFAK YAZICISI',if(type=1,'KASA YAZICISI',''))as type_text from printer_details where is_deleted=0 order by printer_desc asc";
            try
            {
                dt = db.GetDataTable(sql);
                gridControl1.DataSource = dt;

                if (firstLoad)
                {
                    //money button
                    //repo_button.Buttons[0].Image = global::StockProgram.Properties.Resources._case;
                    //repo_button.Buttons[0].Caption = "Alışveriş";
                    //repo_button.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;

                    //edit button
                    //     repo_button.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());
                    repo_button.Buttons[0].Image = global::StockProgram.Properties.Resources.edit_small;
                    repo_button.Buttons[0].Caption = "Düzenle";
                    repo_button.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;

                    //erase button
                    repo_button.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());
                    repo_button.Buttons[1].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    repo_button.Buttons[1].Image = global::StockProgram.Properties.Resources.delete;
                    repo_button.Buttons[1].Caption = "Sil";
                    repo_button.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repo_button_ButtonClick);
                }
                firstLoad = false;
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new DBObjects.ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at
            }
            finally
            {
                db.Close();
                db = null;
            }
        }

        private void repo_button_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            int printer_id = Convert.ToInt32(dr["printer_id"]);
            int count = split.Panel2.Controls.Count;
            string ucName = split.Panel2.Controls[count - 1].GetType().Name;
            switch (e.Button.Caption)
            {
                case "Alışveriş":
                    //ucMoneyTransfer ctrl = new ucMoneyTransfer(bank_id);
                    //ctrl.Dock = DockStyle.Fill;
                    //ctrl.MoneyTransferChanged += new ucMoneyTransfer.MoneyTransferHandler(ctrl_BankGridChanged);
                    //this.pnl_master.Visible = false;
                    //int count=spliter.Panel2.Controls.Count;
                    //if (spliter.Panel2.Controls[count-1].GetType().Name=="ucAddBank") 
                    //{
                    //    spliter.Panel2.Controls[count-1].Dispose();
                    //}
                    //this.spliter.Panel2.Controls.Add(ctrl);
                    break;
                case "Düzenle":
                    //todo: edit operations
                    if ( ucName == "ucEditPrinter" || ucName == "ucAddPrinter" )
                    {
                        split.Panel2.Controls[count - 1].Dispose();
                    }
                    EditTable(printer_id);
                    break;
                case "Sil":
                    ErrorMessages.Message msg = new ErrorMessages.Message();
                    if (msg.WriteMessage("Yazıcı sistemden tamamen silinecektir.\nDevam etmek istiyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        DeletePrinter(printer_id);
                    }
                    break;
                default:
                    break;
            }
        }


        private void DeletePrinter(int id)
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string sql = "update printer_details set is_deleted=1 where printer_id=" + id;
            try
            {
                db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new DBObjects.ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "CastaStok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at
            }
            finally
            {
                db.Close();
                db = null;
            }
            FillGrid();
        }
        private void btn_urun_gir_Click(object sender, EventArgs e)
        {
            ucAddPrinter ctrl = new ucAddPrinter();
            ctrl.Dock = DockStyle.Fill;
            ctrl.SupplierGridChanged += new ucAddPrinter.SupplierGridHandler(ctrl_ColorGridChanged);
            this.pnl_master.Visible = false;
            this.split.Panel2.Controls.Add(ctrl);
        }

        void ctrl_ColorGridChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void EditTable(int id)
        {
            ucEditPrinter ctrl = new ucEditPrinter(id);
            ctrl.Dock = DockStyle.Fill;
            ctrl.SupplierGridChanged += new ucEditPrinter.SupplierGridHandler(ctrl_ColorGridChanged);
            this.pnl_master.Visible = false;
            this.split.Panel2.Controls.Add(ctrl);
        }

        private void btn_category_add_Click(object sender, EventArgs e)
        {
        
        }

    }
}
