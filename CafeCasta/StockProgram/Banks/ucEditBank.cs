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
    public partial class ucEditBank : DevExpress.XtraEditors.XtraUserControl
    {
        ExceptionLogger excLogger;
        private int bank_id;
        public delegate void BankGridHandler(object sender, EventArgs e);
        public event BankGridHandler BankGridChanged;
        public ucEditBank(int bank_id)
        {
            this.bank_id = bank_id;
            InitializeComponent();
            FillBankProperties();
        }


        private void FillBankProperties()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            dt = db.GetDataTable("select * from v_bank_details where bank_id="+this.bank_id);
            txt_isim.Text = dt.Rows[0]["bank_name"].ToString();
            txt_ay_oran.Text = dt.Rows[0]["instalment"].ToString();
            spin_gun.Value = Convert.ToDecimal(dt.Rows[0]["payment_day"]);
        }
        private void btn_bankaEkle_Click(object sender, EventArgs e)
        {
            if (txt_isim.Text!="")
            {// create  bank object
                DBObjects.Bank b = new DBObjects.Bank();
                b.IsDeleted = 0;
                b.Id = this.bank_id;
                b.Name = txt_isim.Text.ToUpper();

                //add bank object
                EditBank(ref b);
            }
        }

        private void EditBank(ref Bank b)
        { 
          MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
          string strSQL = "update bank_details set bank_name=@bank_name where bank_id=@bank_id; ";
          strSQL += " update bank_instalments set instalment=@instalment, payment_day=@payment_day  where bank_id=@bank_id; ";
            try
            {
                db.CreateSetParameter("instalment", MySql.Data.MySqlClient.MySqlDbType.Text, txt_ay_oran.Text.Trim().Replace('.',','));
                db.CreateSetParameter("bank_id", MySql.Data.MySqlClient.MySqlDbType.Int32, b.Id);
                db.CreateSetParameter("payment_day", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt32(spin_gun.Value));
                db.CreateSetParameter("bank_name", MySql.Data.MySqlClient.MySqlDbType.VarChar, b.Name);
                db.ExecuteNonQuery(strSQL);
                OnBankGridChanged(EventArgs.Empty);// fire the gridChanged event
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Casta Stok Programı Hatası";
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
