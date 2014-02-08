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

namespace StockProgram.Suppliers
{
    public partial class ucAddSupplier : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        public delegate void SupplierGridHandler(object sender,EventArgs e);
        public event SupplierGridHandler SupplierGridChanged;
        public ucAddSupplier()
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

        private void btn_tedarikciEkle_Click(object sender, EventArgs e)
        {         
            if (txt_isim.Text == "")
            {
                ErrorMessages.Message m = new ErrorMessages.Message();
                if (m.WriteMessage("İsim alanını boş bırakmayınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                    return;
            }
            Supplier s = new Supplier();
            s.Address = txt_adres.Text.ToUpper();
            s.Desc = txt_tanim.Text.ToUpper();
            s.IsDeleted = 0;
            //s.ModifiedDate = DateTime.Now;
            s.Mail = txt_mail.Text.ToUpper();
            s.Phone = txt_tel.Text.ToUpper();
            s.Name = txt_isim.Text.ToUpper();
            AddSupplierToDB(ref s);
          
        }

        private void AddSupplierToDB(ref Supplier s)
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string strSQL = "insert into suppliers_details (suppliers_name,suppliers_desc,suppliers_address,suppliers_tel,suppliers_mail,suppliers_isDeleted,modified_date)"+
            " values ('" + s.Name + "','" + s.Desc + "','" + s.Address + "','" + s.Phone + "','" + s.Mail + "',0,@modified_date)";
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
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }
    }
}
