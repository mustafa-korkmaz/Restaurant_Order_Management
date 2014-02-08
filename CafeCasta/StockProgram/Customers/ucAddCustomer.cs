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
    public partial class ucAddCustomer : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        public delegate void SupplierGridHandler(object sender, EventArgs e);
        public event SupplierGridHandler SupplierGridChanged;

        public ucAddCustomer()
        {
            InitializeComponent();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }

        protected virtual void OnSupplierGridChanged(EventArgs e)
        {
            if (SupplierGridChanged != null)
                SupplierGridChanged(this, e);
        }

        private void btn_addCustomer_Click(object sender, EventArgs e)
        {
            if (txt_name.Text=="")
            {
                ErrorMessages.Message m = new ErrorMessages.Message();
                if (m.WriteMessage("İsim alanını boş bırakmayınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                    return;
            }

            DBObjects.Customer customer = new DBObjects.Customer();
            customer.name = txt_name.Text;
            customer.mail = txt_mail.Text;
            customer.adress = txt_adress.Text;
            customer.tel = txt_tel.Text;
            customer.note = txt_note.Text;

            AddCustomer(ref customer);
        }

        private void AddCustomer(ref DBObjects.Customer c)
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string procName = "addCustomer";
          
            try
            {
                db.CreateSetParameter("cName", MySql.Data.MySqlClient.MySqlDbType.Text, c.name.ToUpper());
                db.CreateSetParameter("cTel", MySql.Data.MySqlClient.MySqlDbType.VarChar,c.tel);
                db.CreateSetParameter("cNote", MySql.Data.MySqlClient.MySqlDbType.Text, c.note.ToUpper());
                db.CreateSetParameter("cMail", MySql.Data.MySqlClient.MySqlDbType.VarChar,c.mail);
                db.CreateSetParameter("cAddress", MySql.Data.MySqlClient.MySqlDbType.Text, c.adress.ToUpper());
                db.CreateOuterParameter("id", MySql.Data.MySqlClient.MySqlDbType.Int32);
                db.ExecuteNonQuerySP(procName);
                OnSupplierGridChanged(EventArgs.Empty);// fire the gridChanged event
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

        private void ucAddCustomer_Load(object sender, EventArgs e)
        {
            txt_name.Focus();
        }
    }
}
