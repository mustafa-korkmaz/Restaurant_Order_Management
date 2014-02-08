using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StockProgram.DBObjects;
using System.Diagnostics;

namespace StockProgram.Customers
{
    public partial class frmMoneyTransfer : DevExpress.XtraEditors.XtraForm
    {
        public delegate void MoneyTransferHandler(object sender, EventArgs e);
        public event MoneyTransferHandler MoneyTransferChanged;
        private int customer_id;
        private string customer_name;
        private string type;
        private bool restricted;
        private decimal value;
        private int payment_id;
        private string desc;
        private Bank b;
        private ExceptionLogger excLogger;

        public frmMoneyTransfer(int customer_id,string customer_name)
        {
            this.customer_id = customer_id;
            this.customer_name = customer_name;
            InitializeComponent();
            this.type = "new";
          //  FillBankProperties();
            frmSettings();
        }
        public frmMoneyTransfer(DataRow dr)
        {
            this.customer_id = Convert.ToInt32(dr["customer_id"]);
            this.customer_name = dr["name"].ToString();
            this.desc = dr["payment_desc"].ToString();
            this.payment_id = Convert.ToInt32(dr["payment_id"]);
            this.value=Convert.ToDecimal(dr["payment_price"]);
            this.type = "edit";
            InitializeComponent();
            frmSettings();
        }
        public frmMoneyTransfer(DataRow dr,bool restricted)
        {
            this.customer_id = Convert.ToInt32(dr["customer_id"]);
            this.customer_name = dr["name"].ToString();
            this.desc = dr["payment_desc"].ToString();
            this.payment_id = Convert.ToInt32(dr["payment_id"]);
            this.value = Convert.ToDecimal(dr["payment_price"]);
            this.type = "edit";
            this.restricted = restricted;
            InitializeComponent();
            frmSettings();
        }

        private void FillFormElements()
        {
            btn_duzenle.Visible = false;
            txt_aciklama.Text = this.desc;
            btn_tamamla.Text = "Düzenlemeyi Bitir";
            btn_tamamla.Width += 10;
            if (this.value < 0)
            {
                chk_ode.CheckState = CheckState.Unchecked;
                chk_kayit.CheckState = CheckState.Checked;
                spin_miktar.Value = -this.value;
            }
            else
            {
                chk_kayit.CheckState = CheckState.Unchecked;
                chk_ode.CheckState = CheckState.Checked;
                spin_miktar.Value = this.value;
            }

            if (this.restricted==true)
            {
                spin_miktar.Enabled = false;
                chk_kayit.Enabled = false;
                chk_ode.Enabled = false;
            }
        }
        /// <summary>
        /// set the satis form properties
        /// </summary>
        private void frmSettings()
        {
            this.lbl_musteri.Text = this.customer_name;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
         
        }
    
        private void FillBankProperties()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = db.GetDataTable("select * from bank_details where bank_id= " + customer_id);
            b = new Bank();
            b.Name = dt.Rows[0]["bank_name"].ToString();
            b.Id = customer_id;
            b.Total = Convert.ToInt32(dt.Rows[0]["total"]);
            lbl_isim.Text = b.Name.Trim();
            db.Close();
            db = null;

        }

        private void frmSatis_Load(object sender, EventArgs e)
        {
            if (this.type=="new")
            {
                chk_ode.Checked = true;
                chk_kayit.Checked = false;
            }
            else if (this.type == "edit")
            {
                FillFormElements();            
            }
        
        }


        #region Set Form Controls

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }

        #endregion


        private void btn_tamamla_Click(object sender, EventArgs e)
        {
            if (restricted==true) //sadece açıklamayı değiştir
            {
                editDesc();
                return;
            }

            if (spin_miktar.Value<=0)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Ödeme tutarını giriniz.", MessageBoxIcon.Error, MessageBoxButtons.OK);
                return;
            }

            if (this.type=="edit")
            {
                editCustomerPayment();
            }
            else if (this.type=="new")
            doCustomerPayment();
        }


        private void editDesc()
        { 
         DBObjects.MySqlCmd db = new DBObjects.MySqlCmd(StaticObjects.MySqlConn);
            string sql = "update customer_payments set payment_desc=@payment_desc where id=@id";
            try
            {
                db.CreateSetParameter("payment_desc", MySql.Data.MySqlClient.MySqlDbType.Text, txt_aciklama.Text.ToUpper());
                db.CreateSetParameter("id", MySql.Data.MySqlClient.MySqlDbType.Int32, this.payment_id);
                db.ExecuteNonQuery(sql);
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

                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                db.Close();
                //db = null;
            }
            this.Dispose();

        }

        private void editCustomerPayment()
        {
            DBObjects.MySqlCmd db = new DBObjects.MySqlCmd(StaticObjects.MySqlConn);
            string sql = "update customer_payments set payment_desc=@payment_desc, payment=@payment,type=@type where id=@id";
            try
            {
                db.CreateSetParameter("payment_desc", MySql.Data.MySqlClient.MySqlDbType.Text, txt_aciklama.Text.ToUpper());
                db.CreateSetParameter("id", MySql.Data.MySqlClient.MySqlDbType.Int32, this.payment_id);
                if (chk_kayit.CheckState == CheckState.Checked)
                {
                    db.CreateSetParameter("payment", MySql.Data.MySqlClient.MySqlDbType.Double, -spin_miktar.Value);
                    db.CreateSetParameter("type", MySql.Data.MySqlClient.MySqlDbType.Int32, 4);
                }
                else if (chk_ode.CheckState == CheckState.Checked)
                {
                    db.CreateSetParameter("payment", MySql.Data.MySqlClient.MySqlDbType.Double, spin_miktar.Value);
                    db.CreateSetParameter("type", MySql.Data.MySqlClient.MySqlDbType.Int32, 0);
                }
             
                db.ExecuteNonQuery(sql);
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

                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                db.Close();
                //db = null;
            }
            this.Dispose();
        }

        private void doCustomerPayment()
        {
            DBObjects.MySqlCmd db = new DBObjects.MySqlCmd(StaticObjects.MySqlConn);
            string proc_name = "customerPayment";
            try
            {
                db.CreateSetParameter("payment_desc", MySql.Data.MySqlClient.MySqlDbType.Text, txt_aciklama.Text.ToUpper());
                db.CreateSetParameter("customer_id", MySql.Data.MySqlClient.MySqlDbType.Text,this.customer_id);
                if (chk_kayit.CheckState==CheckState.Checked)
                {
                db.CreateSetParameter("payment", MySql.Data.MySqlClient.MySqlDbType.Double, -spin_miktar.Value);
                db.CreateSetParameter("type", MySql.Data.MySqlClient.MySqlDbType.Int32, 4);
                }
                else if (chk_ode.CheckState==CheckState.Checked)
                {
                    db.CreateSetParameter("payment", MySql.Data.MySqlClient.MySqlDbType.Double, spin_miktar.Value);
                    db.CreateSetParameter("type", MySql.Data.MySqlClient.MySqlDbType.Int32, 0);                    
                }
                db.CreateSetParameter("total_price", MySql.Data.MySqlClient.MySqlDbType.Double, 0);
                db.CreateSetParameter("sell_id", MySql.Data.MySqlClient.MySqlDbType.Int32, -1);
                db.ExecuteNonQuerySP(proc_name);
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

                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                db.Close();
                //db = null;
            }
            this.Dispose();
        }

        private void Success()
        {
            OnMoneyTransferChanged(EventArgs.Empty);
            this.Dispose();
        }


        protected virtual void OnMoneyTransferChanged(EventArgs e)
        {
            if (MoneyTransferChanged != null)
                MoneyTransferChanged(this, e);
        }

        private void btn_duzenle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chk_tahsilat_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_kayit.CheckState == CheckState.Checked)
            {
                chk_ode.CheckState = CheckState.Unchecked;
            }
        }

        private void chk_odeme_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ode.CheckState == CheckState.Checked)
            {
                chk_kayit.CheckState = CheckState.Unchecked;
            }
        }

        private void spin_miktar_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(spin_miktar.Text) < 0)
            {
                spin_miktar.Value += spin_miktar.Properties.Increment;
                return;
            }
        }

        private void frmMoneyTransfer_Shown(object sender, EventArgs e)
        {
            this.Activate();
        }
     
    }
}