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
using System.Xml;

namespace StockProgram.Banks
{
    public partial class ucBanksMasterPage : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        private bool firstLoad;
        public ucBanksMasterPage()
        {
            InitializeComponent();
            firstLoad = true;
        }

        /// <summary>
        /// banka gridview i doldurur
        /// </summary>
        private void FillbankGrid()
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            string sql = "select * from bank_details where bank_isDeleted=0 order by bank_name asc";
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
            int bank_id = Convert.ToInt32(dr["bank_id"]);
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
                    ucEditBank ctrl = new ucEditBank(bank_id);                  
                    ctrl.Dock = DockStyle.Fill;
                    ctrl.BankGridChanged += new ucEditBank.BankGridHandler(ctrl_BankGridChanged);
                    this.pnl_master.Visible = false;
                    int count=spliter.Panel2.Controls.Count;
                    string ucName = spliter.Panel2.Controls[count - 1].GetType().Name;
                    if (ucName == "ucAddBank" || ucName == "ucEditBank"|| ucName=="ucBankDetails") 
                    {
                        spliter.Panel2.Controls[count-1].Dispose();
                    }
                    this.spliter.Panel2.Controls.Add(ctrl);
                    break;
                case "Sil":
                    ErrorMessages.Message msg = new ErrorMessages.Message();
                    if (msg.WriteMessage("Banka sistemden tamamen silinecektir.\nDevam etmek istiyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        DeleteBank(bank_id);
                    }
                    break;
                default:
                    break;
            }
        }

        private void DeleteBank(int id)
        { 
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string sql = "update bank_details set bank_isDeleted=1 where bank_id=" + id;
            try
            {
                db.ExecuteNonQuery(sql);
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
            FillbankGrid();
        }
        private void btn_urun_gir_Click(object sender, EventArgs e)
        {
            ucAddBank ctrl = new ucAddBank();
            ctrl.Dock = DockStyle.Fill;
            ctrl.BankGridChanged += new ucAddBank.BankGridHandler(ctrl_BankGridChanged);
            this.pnl_master.Visible = false;
            this.spliter.Panel2.Controls.Add(ctrl);
        }

        void ctrl_BankGridChanged(object sender, EventArgs e)
        {
            FillbankGrid();
        }

        private void ucBanksMasterPage_Load(object sender, EventArgs e)
        {
            FillbankGrid();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            int bank_id = 0;
            string bank_name = "";
            int rowHandle = gridView1.FocusedRowHandle;
            DataRow dr;
            dr = gridView1.GetDataRow(rowHandle);
            bank_id = Convert.ToInt32(dr["bank_id"]);
            bank_name=dr["bank_name"].ToString();

            int count = spliter.Panel2.Controls.Count;
            string ucName = spliter.Panel2.Controls[count - 1].GetType().Name;
            if (ucName == "ucBankDetails" || ucName == "ucAddBank" || ucName == "ucShoppingDetails")
            {
                spliter.Panel2.Controls[count - 1].Dispose();
            }
            ucBankDetails ctrl = new ucBankDetails(bank_id,bank_name);
            ctrl.ShoppingCompleted += new ucBankDetails.ShoppingHandler(ctrl_ShoppingCompleted);
            ctrl.Dock = DockStyle.Fill;
            this.pnl_master.Visible = false;
            this.spliter.Panel2.Controls.Add(ctrl);
        }

        void ctrl_ShoppingCompleted(object sender, EventArgs e)
        {
            FillbankGrid();
        }

        void ctrl_MoneyTransferChanged(object sender, EventArgs e)
        {
            FillbankGrid();//refresh after record
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            ((MainForm)this.ParentForm).SettingStatus();
            this.Dispose();
        }

    }
}
