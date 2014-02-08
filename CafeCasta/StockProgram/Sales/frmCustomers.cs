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

namespace StockProgram.Sales
{
    public partial class frmCustomers : DevExpress.XtraEditors.XtraForm
    {
        private ExceptionLogger excLogger;
        public delegate void CustomerHandler(object sender, EventArgs e);
        public event CustomerHandler CustomerSelectionCompleted;
        private ControlHelper controlHelper;
        DBObjects.Customer selectedCustomer;

        public frmCustomers()
        {
            InitializeComponent();
            controlHelper = new ControlHelper();
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
            this.StartPosition = FormStartPosition.CenterScreen;
            FillCustomers();
       
        }
        private void frmSatis_Load(object sender, EventArgs e)
        {
       
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



        private void btn_tamamla_Click(object sender, EventArgs e)
        {
     
        }

        private void Success()
        {
            onCustomerSelectionCompleted(EventArgs.Empty);
            this.Dispose();
        }

        /// <summary>
        /// fires when purchasing process completed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void onCustomerSelectionCompleted(EventArgs e)
        {
            if (CustomerSelectionCompleted!=null)
            {
                CustomerSelectionCompleted(this.selectedCustomer, e);
            }
        }

        private void btn_duzenle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
     
        private void frmSatisPopup_Shown(object sender, EventArgs e)
        {
            this.Activate();
            this.gridView1.ShowFindPanel();
        }

        private void gridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                int customer_id = 0;
                //   int type = 0;
                int rowHandle = gridView1.FocusedRowHandle;
                DataRow dr;
                dr = gridView1.GetDataRow(rowHandle);
                customer_id = Convert.ToInt32(dr["customer_id"]);
                string customer_name = dr["customer_name"].ToString();
                string address = dr["customer_address"].ToString();
                string tel = dr["customer_tel"].ToString();
                string note = dr["customer_note"].ToString();
                DBObjects.Customer c = new Customer(customer_id,customer_name);
                c.adress = address;
                c.note = note;
                c.tel = tel;
                selectedCustomer = c;
                onCustomerSelectionCompleted(EventArgs.Empty);
                SubmitForm();
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                SubmitForm();
            }
        }

        private void SubmitForm()
        {
            this.Close();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            int customer_id = 0;
            //   int type = 0;
            int rowHandle = gridView1.FocusedRowHandle;
            DataRow dr;
            dr = gridView1.GetDataRow(rowHandle);
            customer_id = Convert.ToInt32(dr["customer_id"]);
            string customer_name = dr["customer_name"].ToString();
            string address = dr["customer_address"].ToString();
            string tel = dr["customer_tel"].ToString();
            string note = dr["customer_note"].ToString();
            DBObjects.Customer c = new Customer(customer_id, customer_name);
            c.adress = address;
            c.note = note;
            c.tel = tel;
            selectedCustomer = c;
            onCustomerSelectionCompleted(EventArgs.Empty);
            SubmitForm();
        }

    }
}