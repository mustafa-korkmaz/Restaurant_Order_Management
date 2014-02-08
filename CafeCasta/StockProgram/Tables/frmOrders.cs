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

namespace StockProgram.Tables
{
    public partial class frmOrders : DevExpress.XtraEditors.XtraForm
    {
        private ExceptionLogger excLogger;
        private int account_id;
        public frmOrders(int account_id)
        {
            this.account_id = account_id;
            InitializeComponent();
            frmSettings();
        }

        /// <summary>
        /// set the satis form properties
        /// </summary>
        private void frmSettings()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = false;
            this.KeyPreview = true;
            this.StartPosition = FormStartPosition.CenterScreen;
        //    FillCustomers();
       
        }
     
        #region Set Form Controls

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }

        private void FillCustomers()
        {
            //fill customers
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select * from customer_details where is_deleted=0 order by display_order, customer_name asc";
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);
                if (dt.Rows.Count == 0)
                {
                    return;
                }
                gridControl1.DataSource = dt;
               
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, ucMigo.FillSuppliers() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
                // retValue = 0;
            }
            finally
            {
                db.Close();
                db = null;
                dt.Dispose();
            }
        }
        #endregion


        private void frmSatisPopup_Shown(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void SubmitForm()
        {
            this.Close();
        }


        private void btn_duzenle_Click_1(object sender, EventArgs e)
        {
            SubmitForm();
        }

        private void frmOrders_Load(object sender, EventArgs e)
        {
            string strSQL = "select * from v_product_to_order where account_type=1 and is_deleted=0 and account_id="+this.account_id+" order by product_name asc";
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);
                gridControl1.DataSource = dt;
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new DBObjects.ExceptionLogger(ex.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(ex, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at

                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(ex.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);

            }
            finally
            {
                dt.Dispose();
                db.Close();
                db = null;
            }
        }

        private void frmOrders_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.SubmitForm();
            }
        }

    }
}