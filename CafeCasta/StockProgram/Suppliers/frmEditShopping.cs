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

namespace StockProgram.Suppliers
{
    public partial class frmEditShopping : DevExpress.XtraEditors.XtraForm
    {
        public delegate void SupplierShoppingHandler(object sender,EventArgs e);
        public event SupplierShoppingHandler ShoppingEdited;
        private StockProgram.ControlHelper controlHelper;
        private ExceptionLogger excLogger;
        private string  p_code; // product_id 
        private ProductReturn pr;
        private DataRow dr;
        private string supplier_name;
        private int payment_id;
        
        public frmEditShopping(string code)
        {
            this.p_code = code;
            controlHelper = new ControlHelper();
            InitializeComponent();
            frmSettings();
      //      FillSuppliers();
            FillProductProperties();
        }
        public frmEditShopping(string supplier_name,ref DataRow dr)
        {
            this.supplier_name = supplier_name;
            this.dr = dr;
        //    string p_code = StaticObjects.GetProductCode(ref dr);
            InitializeComponent();
            frmSettings();
            FillProductProperties();
        }
 
        private void frmSettings()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.lbl_tedarikci.Text = this.supplier_name;
        }

        private void frmColorSizePopup_Load(object sender, EventArgs e)
        {
          //  FillTextBoxes();
        }
  
        /// <summary>
        /// ürün özelliklerini initialize eder
        /// </summary>
        private void FillProductProperties()
        {
            try
            {
                FillTextBoxes();
                this.payment_id=Convert.ToInt32(dr["payment_id"]);
            }
            catch (Exception ex)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(ex.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(ex, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at
            }
           
        }
        private void FillTextBoxes()
        {
             txt_aciklama.Text=dr["payment_desc"].ToString();
            spin_miktar.Value=Convert.ToDecimal(dr["payment_price"]);
        }

        protected virtual void OnSupplierShoppingEdited(EventArgs e)
        {
            if (ShoppingEdited != null)
                ShoppingEdited(this, e);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (spin_miktar.Value <= 0)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Ödeme tutarını giriniz.", MessageBoxIcon.Error, MessageBoxButtons.OK);
                return;
            }
            //NewSupplierPayment
            Enums.SupplierPaymentType spt = new Enums.SupplierPaymentType();
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string proc_name = "editSuppliersPayment";
            double value = Convert.ToDouble(spin_miktar.Value);

            spt = Enums.SupplierPaymentType.Odeme;

            try //payment to supplier
            {
                db.CreateSetParameter("payment_price", MySql.Data.MySqlClient.MySqlDbType.Double, value);
                db.CreateSetParameter("type", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt32(spt));
                db.CreateSetParameter("payment_id", MySql.Data.MySqlClient.MySqlDbType.Int32, this.payment_id);
                db.CreateSetParameter("payment_desc", MySql.Data.MySqlClient.MySqlDbType.Text, txt_aciklama.Text.Trim().ToUpper());
                db.ExecuteNonQuerySP(proc_name);
            }
            catch (Exception ex)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(ex.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(ex, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at
            }
            finally
            {
                db.Close();
                db = null;
            }
            Success();
        }

        private void btn_ilave_Click(object sender, EventArgs e)
        {
            if (spin_miktar.Value <= 0)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Ödeme tutarını giriniz.", MessageBoxIcon.Error, MessageBoxButtons.OK);
                return;
            }
            //NewSupplierPayment
            Enums.SupplierPaymentType spt = new Enums.SupplierPaymentType();
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string proc_name = "editSuppliersPayment";
            double value = Convert.ToDouble(spin_miktar.Value);

            spt = Enums.SupplierPaymentType.Ilave;

            try //payment to supplier
            {
                db.CreateSetParameter("payment_price", MySql.Data.MySqlClient.MySqlDbType.Double, value);
                db.CreateSetParameter("type", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt32(spt));
                db.CreateSetParameter("payment_id", MySql.Data.MySqlClient.MySqlDbType.Int32, this.payment_id);
                db.CreateSetParameter("payment_desc", MySql.Data.MySqlClient.MySqlDbType.Text, txt_aciklama.Text.Trim().ToUpper());
                db.ExecuteNonQuerySP(proc_name);
            }
            catch (Exception ex)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(ex.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(ex, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at
            }
            finally
            {
                db.Close();
                db = null;
            }
            Success();
        }
        private void Success()
        {
            OnSupplierShoppingEdited(EventArgs.Empty);
            this.Dispose();
        }
    }
}