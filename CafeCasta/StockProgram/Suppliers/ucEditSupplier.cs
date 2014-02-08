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

namespace StockProgram.Suppliers
{
    public partial class ucEditSupplier : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        public delegate void SupplierGridHandler(object sender, EventArgs e);
        public event SupplierGridHandler SupplierGridChanged;
        private int s_id;
        private Supplier s;
        public ucEditSupplier(int s_id)
        {
            InitializeComponent();
            this.s_id = s_id;
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }

        private void ucEditSupplier_Load(object sender, EventArgs e)
        {
            FillSupplierProperties();
        }
        private void FillSupplierProperties()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt= db.GetDataTable("select * from suppliers_details where suppliers_id= " + s_id);
            s = new Supplier();
            s.Id = this.s_id;
            s.Mail=dt.Rows[0]["suppliers_mail"].ToString();
            s.Address= dt.Rows[0]["suppliers_address"].ToString();
            s.Name = dt.Rows[0]["suppliers_name"].ToString();
            s.Desc = dt.Rows[0]["suppliers_desc"].ToString();
            s.Phone = dt.Rows[0]["suppliers_tel"].ToString();

            txt_adres.Text = s.Address;
            txt_isim.Text = s.Name;
            txt_mail.Text = s.Mail;
            txt_tanim.Text = s.Desc;
            txt_tel.Text = s.Phone;
        }

        private void btn_urunEkle_Click(object sender, EventArgs e)
        {
            if (txt_isim.Text == "")
            {
                ErrorMessages.Message m = new ErrorMessages.Message();
                if (m.WriteMessage("İsim alanını boş bırakmayınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                    return;
            }
            s.Address = txt_adres.Text.ToUpper();
            s.Desc = txt_tanim.Text.ToUpper();
            s.IsDeleted = 0;
            //s.ModifiedDate = DateTime.Now;
            s.Mail = txt_mail.Text.ToUpper();
            s.Phone = txt_tel.Text.ToUpper();
            s.Name = txt_isim.Text.ToUpper();
            AddSupplierToDB(ref s);
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }

        private void AddSupplierToDB(ref Supplier s)
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string strSQL = "update suppliers_details set suppliers_name='" + s.Name + "',suppliers_desc='" + s.Desc + "',suppliers_address='" + s.Address + "',suppliers_tel='" + s.Phone + "',suppliers_mail='" + s.Mail + "',modified_date=@modified_date where suppliers_id=" + this.s_id;
            try
            {
                db.CreateSetParameter("modified_date", MySql.Data.MySqlClient.MySqlDbType.DateTime, DateTime.Now);
                db.ExecuteNonQuery(strSQL);
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
        }

        protected virtual void OnSupplierGridChanged(EventArgs e)
        {
            if (SupplierGridChanged != null)
                SupplierGridChanged(this, e);
        }

    }
}
