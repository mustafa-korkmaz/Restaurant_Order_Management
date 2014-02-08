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

namespace StockProgram.Customers
{
    public partial class ucAddPayment : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void VeresiyeGridHandler(object sender, EventArgs e);
        public event VeresiyeGridHandler VeresiyeGridChanged;
        private ExceptionLogger excLogger;
        public ucAddPayment()
        {
            InitializeComponent();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }

        protected virtual void OnVeresiyeGridChanged(EventArgs e)
        {
            if (VeresiyeGridChanged != null)
                VeresiyeGridChanged(this, e);
        }

        private void btn_giderEkle_Click(object sender, EventArgs e)
        {
            if (spin_fiyat.Value <= 0)
            {
                ErrorMessages.Message m = new ErrorMessages.Message();
                if (m.WriteMessage("Masraf tutarını boş bırakmayınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                    return;
            }
            Gider gider = new Gider();
            gider.Desc = txt_tanim.Text.ToUpper();
            gider.Price = Convert.ToDouble(spin_fiyat.Value);
            gider.ProcessId = GetProcessID();
            AddExpenseToDB(ref gider);
        }

        private int GetProcessID()
        {
            int process_id = 0;
            MySqlDbHelper db=new MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "";
            strSQL = "select process_id from process_details where process_name='veresiye_type'";
            process_id=Convert.ToInt32(db.Get_Scalar(strSQL));
            return process_id;
        }

        private void AddExpenseToDB(ref Gider g)
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string strSQL = "";
            strSQL = "insert into expense_details (payment_cat,payment_desc,payment_price,modified_date)" +
               " values ('" + g.ProcessId + "','" + g.Desc + "',@price,@modified_date)";
            try
            {
                db.CreateSetParameter("modified_date", MySql.Data.MySqlClient.MySqlDbType.DateTime, DateTime.Now);
                db.CreateSetParameter("price", MySql.Data.MySqlClient.MySqlDbType.Double, g.Price);
                db.ExecuteNonQuery(strSQL);
                OnVeresiyeGridChanged(EventArgs.Empty);// fire the gridChanged event
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
    }
}
