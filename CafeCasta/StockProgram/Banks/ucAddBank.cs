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
    public partial class ucAddBank : DevExpress.XtraEditors.XtraUserControl
    {
        ExceptionLogger excLogger;
        public delegate void BankGridHandler(object sender, EventArgs e);
        public event BankGridHandler BankGridChanged;
        public ucAddBank()
        {
            InitializeComponent();
        }

        private void btn_bankaEkle_Click(object sender, EventArgs e)
        {
            if (txt_isim.Text!="")
            {// create  bank object
                DBObjects.Bank b = new DBObjects.Bank();
                b.IsDeleted = 0;
                b.Name = txt_isim.Text.ToUpper();

                //add bank object
                AddBankToDB(ref b);
                AddInstalment(GetLastBankId());
                OnBankGridChanged(EventArgs.Empty);// fire the gridChanged event
            }
        }

        private void AddBankToDB(ref Bank b)
        { 
          MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string strSQL = "insert into bank_details (bank_name,bank_isDeleted,modified_date)"+
            " values ('" + b.Name + "',0,@modified_date)";
            try
            {
                db.CreateSetParameter("modified_date", MySql.Data.MySqlClient.MySqlDbType.DateTime, DateTime.Now);
                db.ExecuteNonQuery(strSQL);
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

        /// <summary>
        /// eklenen son bank_id yi döndürür
        /// </summary>
        /// <returns></returns>
        private int GetLastBankId()
        {
            int id = 0;
            MySqlDbHelper db = new MySqlDbHelper (StaticObjects.MySqlConn);
            string strSQL = "select bank_id from bank_details order by modified_date desc";
          
            try
            {
                id = Convert.ToInt32(db.Get_Scalar(strSQL));
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
            return id;
        }

        private void AddInstalment(int bank_id)
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string strSQL = "insert into bank_instalments (bank_id,instalment,payment_day)" +
            " values (@bank_id, @instalment,@payment_day)";
            try
            {
                db.CreateSetParameter("instalment", MySql.Data.MySqlClient.MySqlDbType.Text, txt_ay_oran.Text.Trim().Replace('.', ','));
                db.CreateSetParameter("bank_id", MySql.Data.MySqlClient.MySqlDbType.Int32,bank_id);
                db.CreateSetParameter("payment_day", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt32(spin_gun.Value));
                db.ExecuteNonQuery(strSQL);
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
        }

        protected virtual void OnBankGridChanged(EventArgs e)
        {
            if (BankGridChanged != null)
                BankGridChanged(this, e);
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }

        private void spin_gun_Click(object sender, EventArgs e)
        {
            this.spin_gun.SelectAll();
        }
    }
}
