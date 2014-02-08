using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StockProgram.DBObjects;
using System.Diagnostics;

namespace StockProgram.Banks
{
    public partial class ucMoneyTransfer : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        public delegate void MoneyTransferHandler(object sender, EventArgs e);
        public event MoneyTransferHandler MoneyTransferChanged;
        private int bank_id;
        private Bank b;
        private int payment_time; //0-> çek; 1-> yatır

        public ucMoneyTransfer(int bank_id)
        {
            InitializeComponent();
            this.bank_id = bank_id;
               
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }


        protected virtual void OnMoneyTransferChanged(EventArgs e)
        {
            if (MoneyTransferChanged != null)
                MoneyTransferChanged(this, e);
        }

        private void ucMoneyTransfer_Load(object sender, EventArgs e)
        {
            chk_cek.Checked = true;
            chk_yatir.Checked = false;
            FillBankProperties();
        }

        private void FillBankProperties()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = db.GetDataTable("select * from bank_details where bank_id= " + bank_id);
            b = new Bank();
            b.Name = dt.Rows[0]["bank_name"].ToString();
            b.Id = bank_id;
            b.Total = Convert.ToInt32(dt.Rows[0]["total"]);
            lbl_isim.Text = b.Name.Trim();
            db.Close();
            db = null;
   
        }

        private void btn_bankaEkle_Click(object sender, EventArgs e)
        {
            ErrorMessages.Message m = new ErrorMessages.Message();
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = string.Empty;

            if (spin_miktar.Value==0)
            {
                if (m.WriteMessage("Miktar giriniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                    return;
            }
            else if (chk_cek.Checked==true && chk_yatir.Checked==true)
            {           
                  if (m.WriteMessage("Aynı anda sadece 1 işlem yapabilirsiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                    return;
            }
            else if(chk_cek.Checked==false && chk_yatir.Checked==false)
            {
                if (m.WriteMessage("Yapmak istediğiniz şlemi seçiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                    return;
            }
            else if (chk_cek.Checked == true && chk_yatir.Checked == false && spin_miktar.Value!=0)
            {
                b.Total -=Convert.ToDouble( spin_miktar.Value);
                payment_time = 0;
            }
            else if (chk_cek.Checked == false && chk_yatir.Checked == true)
            {
                b.Total += Convert.ToDouble(spin_miktar.Value);
                payment_time = 1;
            }
            strSQL = "update bank_details set total=" + b.Total + " where bank_id=" + this.b.Id; //total miktarı update et
            try
            {
                db.ExecuteNonQuery(strSQL);
            }
            catch (Exception exc)
            {

                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(exc.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(exc, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at
            }
            finally
            {
                db.Close();
                db = null;
            }
            AddTransferLog();
        }

        /// <summary>
        /// yapılan transfer miktarını loglar
        /// </summary>
        private void AddTransferLog()
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string strSQL = "insert into bank_logs (bank_id,amount,type,modified_date)" +
            " values ('" + b.Id + "','" + Convert.ToDouble(spin_miktar.Value) + "',"+payment_time+",@modified_date)";
            try
            {
                db.CreateSetParameter("modified_date", MySql.Data.MySqlClient.MySqlDbType.DateTime, DateTime.Now);
                db.ExecuteNonQuery(strSQL);
                OnMoneyTransferChanged(EventArgs.Empty);// fire the gridChanged event
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(e.Message, excSource);// DB ye log yaz
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
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }

        private void chk_cek_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_cek.CheckState==CheckState.Checked)
            {
                chk_yatir.CheckState = CheckState.Unchecked;
            }
        }

        private void chk_yatir_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_yatir.CheckState == CheckState.Checked)
            {
                chk_cek.CheckState = CheckState.Unchecked;
            }
        }
    }
}
