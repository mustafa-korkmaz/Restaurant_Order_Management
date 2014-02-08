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

namespace StockProgram.Customers
{
    public partial class ucEditCustomer : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        public delegate void SupplierGridHandler(object sender, EventArgs e);
        public event SupplierGridHandler SupplierGridChanged;
        private int customer_id;
        Customer customer;


        public ucEditCustomer(int customer_id)
        { 
            customer = new Customer();
            this.customer_id = customer_id;
            InitializeComponent();
            FillCustomerProperties();
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

          private void  FillCustomerProperties()
          {
              DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
              DataTable dt = new DataTable();
              string sql = "select * from customer_details where customer_id=" + this.customer_id;
              try
              {
                  dt = db.GetDataTable(sql);
                  if (dt.Rows.Count > 0)
                  {
                      customer.adress = dt.Rows[0]["customer_address"].ToString();
                      customer.mail = dt.Rows[0]["customer_mail"].ToString();
                      customer.id = customer_id;
                      customer.name = dt.Rows[0]["customer_name"].ToString();
                      customer.tel = dt.Rows[0]["customer_tel"].ToString();
                      customer.note = dt.Rows[0]["customer_note"].ToString();
          
                      //fill textboxes
                      txt_adress.Text = customer.adress;
                      txt_mail.Text = customer.mail;
                      txt_name.Text = customer.name;
                      txt_tel.Text = customer.tel;
                      txt_note.Text = customer.note;
                  }
                             
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

          private void btn_addCustomer_Click(object sender, EventArgs e)
          {
              if (txt_name.Text == "")
              {
                  ErrorMessages.Message m = new ErrorMessages.Message();
                  if (m.WriteMessage("İsim alanını boş bırakmayınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                      return;
              }

              customer.name = txt_name.Text;
              customer.mail = txt_mail.Text;
              customer.adress = txt_adress.Text;
              customer.tel = txt_tel.Text;
              customer.note = txt_note.Text;

              EditCustomer();
          }

          private void EditCustomer()
          {
              MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
              string procName = "editCustomer";

              try
              {
                  db.CreateSetParameter("id", MySql.Data.MySqlClient.MySqlDbType.Int32, customer.id);
                  db.CreateSetParameter("cName", MySql.Data.MySqlClient.MySqlDbType.Text, customer.name.ToUpper());
                  db.CreateSetParameter("cNote", MySql.Data.MySqlClient.MySqlDbType.Text, customer.note.ToUpper());
                  db.CreateSetParameter("cTel", MySql.Data.MySqlClient.MySqlDbType.VarChar, customer.tel);
                  db.CreateSetParameter("cMail", MySql.Data.MySqlClient.MySqlDbType.VarChar, customer.mail);
                  db.CreateSetParameter("cAddress", MySql.Data.MySqlClient.MySqlDbType.Text, customer.adress.ToUpper());
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

          private void labelControl4_Click(object sender, EventArgs e)
          {

          }

          private void txt_mail_EditValueChanged(object sender, EventArgs e)
          {

          }

    }
}
