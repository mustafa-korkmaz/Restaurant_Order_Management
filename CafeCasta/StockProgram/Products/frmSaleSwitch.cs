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

namespace StockProgram.Products
{
    public partial class frmSaleSwitch : DevExpress.XtraEditors.XtraForm
    {
        private ExceptionLogger excLogger;
        private ControlHelper controlHelper;
        public delegate void PurchaseHandler(object sender, EventArgs e);
        public event PurchaseHandler PurchaseCompleted;
        private List<Customer> CItemsList;
        private int sell_id;
        private int customer_id;

        /// <summary>
        /// set the satis form properties
        /// </summary>
        private void frmSettings()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.lbl_header.Text = this.sell_id + " nolu satış aktarım işlemi "; 
            //FillBanks();
          //  this.ActiveControl = this.btn_tamamla;
        }
        private void frmSatis_Load(object sender, EventArgs e)
        {
            cbo_customer.Focus();
        }

        public frmSaleSwitch(int sell_id,int customer_id)
        {
            InitializeComponent();
            this.sell_id = sell_id;
            this.customer_id = customer_id;
            frmSettings();
            
            controlHelper = new ControlHelper();
            FillCustomers();
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
            string strSQL = "select customer_id, customer_name from customer_details where is_deleted=0 order by display_order,customer_name asc";
            CItemsList = new List<DBObjects.Customer>();
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);
                if (dt.Rows.Count == 0)
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Müşteriler sayfsından en az 1 Müşteri eklemelisiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    Parent.Controls["pnl_master"].Visible = true;
                    this.Dispose();
                    return;
                }
                controlHelper.FillControl(cbo_customer, Enums.RepositoryItemType.ComboBox, ref dt, "customer_name");
                cbo_customer.Text = cbo_customer.Properties.Items[0].ToString();
                CItemsList = controlHelper.GetCustomers(ref dt);
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
                dt.Dispose();
            }
        }
        //private void FillBanks()
        //{
        //    //fill Suppliers
        //    DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
        //    string strSQL = "select bank_id, bank_name from bank_details where bank_isDeleted=0 order by bank_name asc";
        //    BItemsList = new List<DBObjects.Bank>();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dt = db.GetDataTable(strSQL);
        //        if (dt.Rows.Count == 0)
        //        {
        //            ErrorMessages.Message message = new ErrorMessages.Message();
        //            message.WriteMessage("Tedarikçiler sayfsından en az 1 tedarikçi eklemelisiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
        //            Parent.Controls["pnl_master"].Visible = true;
        //            this.Dispose();
        //            return;
        //        }
             
        //    }
        //    catch (Exception e)
        //    {
        //        ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
        //        excMail.Subject = "Stok Programı, ucMigo.FillSuppliers() hata hk ";
        //        excMail.Send();
        //        ErrorMessages.Message message = new ErrorMessages.Message();
        //        message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
        //        // retValue = 0;
        //    }
        //    finally
        //    {
        //        db.Close();
        //        db = null;
        //        dt.Dispose();
        //    }
        //}
   
             
        #endregion

    
        private void btn_tamamla_Click(object sender, EventArgs e)
        {
            ErrorMessages.Message message = new ErrorMessages.Message();

            if (this.customer_id==CItemsList[cbo_customer.SelectedIndex].id)
            {
                if (DialogResult.OK == message.WriteMessage("Aktarım yaptığınız müşteri ile kayıtlı olan müşteri aynı olmamalıdır.", MessageBoxIcon.Error, MessageBoxButtons.OK))
                {
                    return;
                }
                                                                                                        
            }
            DBObjects.MySqlCmd db = new DBObjects.MySqlCmd(StaticObjects.MySqlConn);
            string sql = "update customer_payments set customer_id=@customer_id where sell_id=@sell_id; ";
            sql += " update sell_details set customer_id=@customer_id where sell_id=@sell_id;";
            try
            {
                db.CreateSetParameter("customer_id", MySql.Data.MySqlClient.MySqlDbType.Text, CItemsList[cbo_customer.SelectedIndex].id);
                db.CreateSetParameter("sell_id", MySql.Data.MySqlClient.MySqlDbType.Int32,this.sell_id);
                db.ExecuteNonQuery(sql);
                Success();
            }
            catch (Exception ex)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(ex.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(ex, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at

                message.WriteMessage(ex.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                db.Close();
                //db = null;
            }
         
        }


        private void SaleProducts()
        {
            DBObjects.MySqlCmd db;
            int sell_id = 0;

            #region Get sell_id
            db = new DBObjects.MySqlCmd(StaticObjects.MySqlConn);
            string proc_name = "newSellDetail";
            try
            {
                db.CreateSetParameter("customer_id", MySql.Data.MySqlClient.MySqlDbType.Text, CItemsList[cbo_customer.SelectedIndex].id);
                db.CreateOuterParameter("sell_id", MySql.Data.MySqlClient.MySqlDbType.Int32);
                db.ExecuteNonQuerySP(proc_name);
                sell_id = Convert.ToInt32(db.GetParameterValue("sell_id"));
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
            #endregion

            #region Sell Products

            proc_name="sellProduct";
            try
            {
                db = new MySqlCmd(StaticObjects.MySqlConn);
                db.CreateSetParameter("sell_id", MySql.Data.MySqlClient.MySqlDbType.Int32, sell_id);
                db.CreateParameter("product_id", MySql.Data.MySqlClient.MySqlDbType.Int32);
                db.CreateParameter("product_amount", MySql.Data.MySqlClient.MySqlDbType.Int32);
                db.CreateParameter("product_color", MySql.Data.MySqlClient.MySqlDbType.Int32);
                db.CreateParameter("product_size", MySql.Data.MySqlClient.MySqlDbType.Int32);
                db.CreateParameter("product_price", MySql.Data.MySqlClient.MySqlDbType.Double);
                db.CreateParameter("product_code", MySql.Data.MySqlClient.MySqlDbType.Text);

             

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
            }
        
            #endregion

         Success();
        }

        private void Success()
        {
            onPurchaseCompleted(EventArgs.Empty);
            this.Dispose();
        }

        /// <summary>
        /// fires when purchasing process completed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void onPurchaseCompleted(EventArgs e)
        {
            if (PurchaseCompleted!=null)
            {
                PurchaseCompleted(this, e);
            }
        }

        private void btn_duzenle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}