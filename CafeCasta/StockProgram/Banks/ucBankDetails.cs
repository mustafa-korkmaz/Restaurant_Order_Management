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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;

namespace StockProgram.Banks
{
    public partial class ucBankDetails : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void ShoppingHandler(object sender,EventArgs e);
        public event ShoppingHandler ShoppingCompleted;
        private int bank_id;
        private bool firstLoad;
        private string bank_name;
        public ucBankDetails(int id,string bank_name)
        {
            this.bank_name = bank_name;
            this.bank_id = id;
            this.firstLoad = true;
            InitializeComponent();
        }

        private void btn_AlisverisEkle_Click(object sender, EventArgs e)
        {
            using (Banks.frmMoneyTransfer transfer =new frmMoneyTransfer(this.bank_id))
            {
                transfer.MoneyTransferChanged += new frmMoneyTransfer.MoneyTransferHandler(transfer_MoneyTransferChanged);
                transfer.ShowDialog(this);
            }
        }

        void transfer_MoneyTransferChanged(object sender, EventArgs e)
        {
            FillGrid();
            OnShoppingCompleted(EventArgs.Empty);
        }

        private void ucShoppingDetails_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void FillGrid()
        {
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select *,if(rate>0,toplam_borc-(toplam_borc*rate/100),toplam_borc) as kalan from v_bank_logs where bank_id="+this.bank_id+" order by modified_date desc";

            DataTable dt = new DataTable();
        
            try
            {
                dt = cmd.GetDataTable(strSQL);
                this.lbl_header.Text = this.bank_name + " İşlem Detayları";
                this.lbl_bakiye.Text = CalculateBakiye(ref dt);
                gridControl1.DataSource = dt;
        //        gridView1.ShowFindPanel();
                SetConditionalFormatting("[payment_date]");
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
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, InsertProduct() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);          
            }
            finally
            {
                cmd.Close();
                cmd = null;
                dt.Dispose();
            }
        }

        private string CalculateBakiye(ref DataTable dt)
        {
            double total = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToDateTime(row["payment_date"])<=DateTime.Now)
                {
                    total+=Convert.ToDouble(row["kalan"]);
                }
            }
            return total.ToString("#0.00") + " TL";
        }

        /// <summary>
        /// colon tarihi "Gelecek" ise satırı kırmızı boyar
        /// </summary>
        /// <param name="columnName"></param>
        private void SetConditionalFormatting(string columnName)
        {
            StyleFormatCondition condition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            condition1.Column = this.gridColumn9;
            condition1.Appearance.ForeColor = System.Drawing.Color.Red;
            condition1.Appearance.Options.UseForeColor = true;
            condition1.Condition = FormatConditionEnum.Greater;
            condition1.Value1 = DateTime.Now;
            gridView1.FormatConditions.Add(condition1);
            StyleFormatCondition condition2 = new DevExpress.XtraGrid.StyleFormatCondition();
            condition2.Appearance.ForeColor = System.Drawing.Color.Green;
            condition2.Column = this.gridColumn9;
            condition2.Appearance.Options.UseForeColor = true;
            condition2.Condition = FormatConditionEnum.LessOrEqual;
            condition2.Value1 = DateTime.Now;
            gridView1.FormatConditions.Add(condition2);
        }

        void repo_button_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ErrorMessages.Message msg = new ErrorMessages.Message();
            DataRow dr = gridView1.GetFocusedDataRow();
            int log_id = Convert.ToInt32(dr["log_id"]);
            string type = dr["type"].ToString().Trim();
            if (type == "POS Satis")
            {
                msg.WriteMessage("Bu ilşem grubu, düzenleme ve silmeye karşı korumalıdır.", MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            else
            {
                switch (e.Button.Caption)
                {
                    case "Düzenle":
                        using (Banks.frmMoneyTransfer transfer = new frmMoneyTransfer(this.bank_id, log_id))
                        {
                            transfer.MoneyTransferChanged += new frmMoneyTransfer.MoneyTransferHandler(transfer_MoneyTransferChanged);
                            transfer.ShowDialog(this);
                        }
                        break;
                    case "Sil":
                        if (msg.WriteMessage("Banka işlemi tamamen silinecektir.\nDevam etmek istiyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            DeleteProcess(log_id);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void DeleteProcess(int log_id)
        {
            ExceptionLogger excLogger;
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            string sql = "delete from bank_logs where bank_id=" + bank_id + " and log_id=" + log_id;
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
            FillGrid();
        }

        /// <summary>
        /// fires when amount  successfully changed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnShoppingCompleted(EventArgs e)
        {
            if (ShoppingCompleted != null)
                ShoppingCompleted(this, e);
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }
    }
}
