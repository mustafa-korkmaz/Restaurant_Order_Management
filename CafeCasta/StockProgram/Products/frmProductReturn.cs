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
    public partial class frmProductReturn : DevExpress.XtraEditors.XtraForm
    {
        public delegate void ProductReturnGridHandler(object sender,EventArgs e);
        public event ProductReturnGridHandler ProductReturnGridChanged;
        private List<DBObjects.Supplier> MItemsList;
        private StockProgram.ControlHelper controlHelper;
        private ExceptionLogger excLogger;
        private decimal oldBirimFiyat;
        private int  p_code; // product_id 
        private ProductReturn pr;
        public frmProductReturn(string  code)
        {
            //this.p_code = code;
            //controlHelper = new ControlHelper();
            //InitializeComponent();
            //frmSettings();
            //FillSuppliers();
            //FillProductProperties();
        }

        public frmProductReturn(int code)
        {
            this.p_code = code;
            controlHelper = new ControlHelper();
            InitializeComponent();
            frmSettings();
            FillSuppliers();
            FillProductProperties();
        }

        private void frmSettings()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btn_tamamla_Click(object sender, EventArgs e)
        {
            if (cb_bagliTedarikci.Text=="SEÇİNİZ")
            {
                  ErrorMessages.Message message = new ErrorMessages.Message();
                 message.WriteMessage("Tedarikçi Seçiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                 return;
            }
            ProductReturn();
            this.Close();
        }

        private void frmColorSizePopup_Load(object sender, EventArgs e)
        {
       //todo 
        }

        /// <summary>
        /// İade kaydının atılması için gerekli prosedürür çalıştırır
        /// </summary>
        /// <param name="pr"></param>
        private void ProductReturn()
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string proc_name = "productReturn";
            pr.ProductAmount = Convert.ToDouble(spin_urun_adet.Value);
            if (spin_birim_fiyat.Value == oldBirimFiyat) //fiyatta değişiklik olmamış
            {
                pr.ProductPrice = pr.ProductAmount * pr.ProductPrice;
            }
            else
                pr.ProductPrice = Convert.ToDouble(spin_toplam.Value);
            try
            {
                db.CreateSetParameter("product_count", MySql.Data.MySqlClient.MySqlDbType.Double, pr.ProductAmount);
                //db.CreateSetParameter("product_size", MySql.Data.MySqlClient.MySqlDbType.Double, pr.ProductSize);
                //db.CreateSetParameter("product_color", MySql.Data.MySqlClient.MySqlDbType.Int32, pr.ProductColorId);
                db.CreateSetParameter("product_price", MySql.Data.MySqlClient.MySqlDbType.Double, pr.ProductPrice);
                db.CreateSetParameter("suppliers_id", MySql.Data.MySqlClient.MySqlDbType.Int32, pr.SuppliersId);
                //db.CreateSetParameter("product_code", MySql.Data.MySqlClient.MySqlDbType.Text, pr.ProductCode);
                db.CreateSetParameter("goods_id", MySql.Data.MySqlClient.MySqlDbType.Int32, pr.ProductId);
                db.CreateSetParameter("return_desc", MySql.Data.MySqlClient.MySqlDbType.Text,txt_desc.Text.Trim().ToUpper());
                db.ExecuteNonQuerySP(proc_name);

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
            }
            finally
            {
                db.Close();
                db = null;
            }

        }
        private void Success()
        {
            ErrorMessages.Message message = new ErrorMessages.Message();
            message.WriteMessage("Malzeme iadesi, başarılı bir şekilde gerçekleştirildi.", MessageBoxIcon.Information, MessageBoxButtons.OK);
            onProductReturnGridChanged(EventArgs.Empty);
        }

        protected virtual void onProductReturnGridChanged(EventArgs e)
        {
            if (ProductReturnGridChanged != null)
                ProductReturnGridChanged(this, e);
        }
        /// <summary>
        /// ürün özelliklerini initialize eder
        /// </summary>
        private void FillProductProperties()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select * from v_product_return where goods_id='" + this.p_code+"'";
            try
            {
                DataTable dt = db.GetDataTable(strSQL);
                pr = new ProductReturn();
                pr.ProductId = Convert.ToInt32(dt.Rows[0]["goods_id"]);
                pr.ProductName = dt.Rows[0]["product_name"].ToString();
                pr.ProductUnit = dt.Rows[0]["unit"].ToString();
                //pr.ProductColor = dt.Rows[0]["color_name"].ToString();
                //pr.ProductColorId = Convert.ToInt32(dt.Rows[0]["product_color"]);
                //pr.ProductSize = Convert.ToDouble(dt.Rows[0]["product_size"]);
                pr.ProductAmount = Convert.ToDouble(dt.Rows[0]["product_count"]);
                pr.ProductPrice = Convert.ToDouble(dt.Rows[0]["last_buy_price"]);
                pr.SuppliersId = MItemsList[0].Id;
                FillTextBoxes();
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
           
        }
        private void FillTextBoxes()
        {
            lbl_birim.Text = pr.ProductUnit;
            txt_numara.Text = pr.ProductSize.ToString();
            txt_renk.Text = pr.ProductColor;
            txt_urun_adi.Text = pr.ProductName;
            spin_urun_adet.Value = Convert.ToDecimal(pr.ProductAmount);
            cb_bagliTedarikci.Text = "SEÇİNİZ";
            spin_birim_fiyat.Value = Convert.ToDecimal(pr.ProductPrice.ToString("#0.00"));
            oldBirimFiyat = (spin_birim_fiyat.Value);
        }
        private void FillSuppliers()
        {
            //fill Suppliers
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select suppliers_id, suppliers_name from suppliers_details where suppliers_isDeleted=0 order by suppliers_name asc";
            MItemsList = new List<DBObjects.Supplier>();
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);
                if (dt.Rows.Count == 0)
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Tedarikçiler sayfsından en az 1 tedarikçi eklemelisiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    Parent.Controls["pnl_master"].Visible = true;
                    this.Dispose();
                    return;
                }
                controlHelper.FillControl(cb_bagliTedarikci, Enums.RepositoryItemType.ComboBox, ref dt, "suppliers_name");
              //  cb_bagliTedarikci.Text = cb_bagliTedarikci.Properties.Items[0].ToString();
                MItemsList = controlHelper.GetSuppliers(ref dt);
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
                dt.Dispose();
            }
        }

        private void spin_urun_adet_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(spin_urun_adet.Value)>pr.ProductAmount)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Stoktaki ürün adedini aştınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                spin_urun_adet.Value = Convert.ToDecimal(pr.ProductAmount);
                return;
            }
            else if (spin_urun_adet.Value<0)
            {
                spin_urun_adet.Value += spin_urun_adet.Properties.Increment;
            }
            else spin_toplam.Value = spin_urun_adet.Value * spin_birim_fiyat.Value;
         
        }

        private void cb_bagliTedarikci_SelectedIndexChanged(object sender, EventArgs e)
        {
            pr.SuppliersId=MItemsList[cb_bagliTedarikci.SelectedIndex].Id;
        }

        private void spin_birim_fiyat_EditValueChanged(object sender, EventArgs e)
        {
            if (spin_birim_fiyat.Value<0)
            {
                spin_birim_fiyat.Value += spin_birim_fiyat.Properties.Increment;
            }
            else spin_toplam.Value = spin_urun_adet.Value * spin_birim_fiyat.Value;
        }

        private void frmProductReturn_Shown(object sender, EventArgs e)
        {
            this.Activate();
            this.cb_bagliTedarikci.Focus();
        }

        private void spin_birim_fiyat_Click(object sender, EventArgs e)
        {
            this.spin_birim_fiyat.SelectAll();
        }

        private void spin_urun_adet_Click(object sender, EventArgs e)
        {
            this.spin_urun_adet.SelectAll();
        }
    }
}