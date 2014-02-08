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
    public partial class frmSellReturn : DevExpress.XtraEditors.XtraForm
    {
        public delegate void ProductReturnGridHandler(object sender,EventArgs e);
        public event ProductReturnGridHandler ProductReturnGridChanged;
        private List<DBObjects.Supplier> MItemsList;
        private StockProgram.ControlHelper controlHelper;
        private ExceptionLogger excLogger;
        private string  p_code; // product_id 
        private ProductReturn pr;
        private DataRow dr;
        
        public frmSellReturn(string code)
        {
            this.p_code = code;
            controlHelper = new ControlHelper();
            InitializeComponent();
            frmSettings();
      //      FillSuppliers();
            FillProductProperties();
        }
        public frmSellReturn(ref DataRow dr)
        {
            this.dr = dr;
            string p_code = StaticObjects.GetProductCode(ref dr);
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
        }

        private void btn_tamamla_Click(object sender, EventArgs e)
        {
            SellReturn();
            this.Close();
        }

        private void frmColorSizePopup_Load(object sender, EventArgs e)
        {
          //  FillTextBoxes();
        }

        /// <summary>
        /// İade kaydının atılması için gerekli prosedürür çalıştırır
        /// </summary>
        /// <param name="pr"></param>
        private void SellReturn()
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string proc_name = "sellReturn";
            pr.ProductAmount = Convert.ToDouble(spin_urun_adet.Value);
            pr.ProductPrice = Convert.ToDouble(spin_toplam.Value);
            try
            {
                db.CreateSetParameter("product_amount", MySql.Data.MySqlClient.MySqlDbType.Double, pr.ProductAmount);
                db.CreateSetParameter("product_size", MySql.Data.MySqlClient.MySqlDbType.Double, pr.ProductSize);
                db.CreateSetParameter("product_color", MySql.Data.MySqlClient.MySqlDbType.Int32, pr.ProductColorId);
                db.CreateSetParameter("product_price", MySql.Data.MySqlClient.MySqlDbType.Double, pr.ProductPrice);
                db.CreateSetParameter("sell_id", MySql.Data.MySqlClient.MySqlDbType.Int32, pr.sell_id);
                db.CreateSetParameter("product_code", MySql.Data.MySqlClient.MySqlDbType.Text, pr.ProductCode);
                db.CreateSetParameter("product_id", MySql.Data.MySqlClient.MySqlDbType.Int32, pr.ProductId);
                db.CreateSetParameter("return_desc", MySql.Data.MySqlClient.MySqlDbType.Text, txt_desc.Text.ToUpper());
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

            doCustomerPayment(3);// customer_payments tablosunda müşterinin bakiyesini ve işlemlerini güncelle
            Success();
        }
        private void Success()
        {
            ErrorMessages.Message message = new ErrorMessages.Message();
            message.WriteMessage("Ürün iadesi, başarılı bir şekilde gerçekleştirildi.", MessageBoxIcon.Information, MessageBoxButtons.OK);
            onProductReturnGridChanged(EventArgs.Empty);
        }

        /// <summary>
        /// adds money transfer log to customer_payments table.
        /// </summary>
        private void doCustomerPayment(int type)
        {
            double VERESIYE = GetVeresiyePriceOnSale(); //eğer iade alışverişi veresiye ile yapılmışsa önce veresiyeleri borçtan düş.

            DBObjects.MySqlCmd db = new DBObjects.MySqlCmd(StaticObjects.MySqlConn);
            int customer_id= Convert.ToInt32(this.dr["customer_id"]);
            ErrorMessages.Message message = new ErrorMessages.Message();

            string proc_name = "customerPayment";
            try
            {
                db.CreateSetParameter("payment_desc", MySql.Data.MySqlClient.MySqlDbType.Text, txt_desc.Text.ToUpper());
                db.CreateSetParameter("customer_id", MySql.Data.MySqlClient.MySqlDbType.Int32,customer_id);
                db.CreateSetParameter("sell_id", MySql.Data.MySqlClient.MySqlDbType.Int32,this.pr.sell_id);
                db.CreateSetParameter("payment", MySql.Data.MySqlClient.MySqlDbType.Double, Convert.ToDouble(-this.spin_toplam.Value));
                db.CreateSetParameter("total_price", MySql.Data.MySqlClient.MySqlDbType.Double, Convert.ToDouble(-this.spin_toplam.Value));
                db.CreateSetParameter("type", MySql.Data.MySqlClient.MySqlDbType.Int32, type); //type=3 (iade)
                db.ExecuteNonQuerySP(proc_name); 
                // iade yaptık ve müşteriye parasını verdik ancak satış veresiye ile olduysa veresiye miktarını tekrar alalım

                if (VERESIYE>0) //veresiye yapılmışsa
                {
                    if (Convert.ToDouble(this.spin_toplam.Value)>=VERESIYE)
                    {
                         db.SetParameterAt("payment", VERESIYE);
                         message.WriteMessage("İşlem sonrasında " + this.dr["customer_name"].ToString() + " adlı müşterinize\n" + (Convert.ToDouble(this.spin_toplam.Value)- VERESIYE)+" TL vermeniz gerekmektedir.", MessageBoxIcon.Information, MessageBoxButtons.OK);
                    }
                    else
                    db.SetParameterAt("payment", Convert.ToDouble(this.spin_toplam.Value));
                  
                    db.SetParameterAt("total_price",0);
                  //  db.SetParameterAt("sell_id", -1);
                    db.SetParameterAt("type", 0); //elden ödeme
                    db.ExecuteNonQuerySP(proc_name); 
                }
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at

                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                db.Close();
                //db = null;
            }
        }

        /// <summary>
        /// satıştaki veresiye tutarını getirir.
        /// </summary>
        /// <returns></returns>
        private double GetVeresiyePriceOnSale()
        {
            double veresiye = 0;
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            //  string strSQL = "select ifnull(sum(total_price-payment),0) as payment from customer_payments where sell_id=" + this.pr.sell_id; //satış bazlı ödeme olursa bu şekilde olmalı
            string strSQL = "select payment_price from v_customers_master where customer_id="+this.pr.customer_id;

            try
            {
                veresiye= Convert.ToDouble(db.Get_Scalar(strSQL));          
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
            }
            return veresiye;
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
            //MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            //string strSQL = "select * from v_stocks where product_code='" + this.p_code+"'";
            try
            {
              //  DataTable dt = db.GetDataTable(strSQL);
                pr = new ProductReturn();
                pr.sell_id = Convert.ToInt32(dr["sell_id"]);
                pr.customer_id = Convert.ToInt32(dr["customer_id"]);
                pr.ProductId = Convert.ToInt32(dr["product_id"]);
                pr.ProductName = dr["product_name"].ToString();
                pr.ProductColor = dr["color_name"].ToString();
                pr.ProductColorId = Convert.ToInt32(dr["product_color"]);
                pr.ProductSize = Convert.ToDouble(dr["product_size"]);
                pr.ProductPrice = Convert.ToDouble(dr["product_price"]);
                pr.ProductAmount = Convert.ToDouble(dr["product_amount"]);
               // pr.SuppliersId = MItemsList[0].Id;
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
           
        }
        private void FillTextBoxes()
        {
            txt_numara.Text = pr.ProductSize.ToString();
            txt_renk.Text = pr.ProductColor;
            txt_urun_adi.Text = pr.ProductName;
            spin_urun_adet.Value = Convert.ToDecimal(pr.ProductAmount);
            spin_birim_fiyat.Value = Convert.ToDecimal(pr.ProductPrice);
          //  cb_bagliTedarikci.Text = cb_bagliTedarikci.Properties.Items[0].ToString();
        }
        private void FillSuppliers()
        {
            //fill Suppliers
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select suppliers_id, suppliers_name from suppliers_details order by suppliers_name asc";
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
                //controlHelper.FillControl(cb_bagliTedarikci, Enums.RepositoryItemType.ComboBox, ref dt, "suppliers_name");
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
            if (spin_urun_adet.Value ==0)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Ürün adedi giriniz", MessageBoxIcon.Warning, MessageBoxButtons.OK);
            //    spin_urun_adet.Value = pr.ProductAmount;
                return;
            }
            if (Convert.ToDouble(spin_urun_adet.Value)>pr.ProductAmount)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Satıştaki ürün adedini aştınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                spin_urun_adet.Value = Convert.ToDecimal(pr.ProductAmount);
                return;
            }
            else
                if (spin_urun_adet.Value < 0)
                {
                    spin_urun_adet.Value += spin_urun_adet.Properties.Increment;
                }
                else spin_toplam.Value = spin_urun_adet.Value * spin_birim_fiyat.Value;
         
        }

        private void cb_bagliTedarikci_SelectedIndexChanged(object sender, EventArgs e)
        {
           // pr.SuppliersId=MItemsList[cb_bagliTedarikci.SelectedIndex].Id;
        }

        private void spin_birim_fiyat_EditValueChanged(object sender, EventArgs e)
        {
            if (spin_birim_fiyat.Value < 0)
            {
                spin_birim_fiyat.Value += spin_birim_fiyat.Properties.Increment;
            }
            else spin_toplam.Value = spin_urun_adet.Value * spin_birim_fiyat.Value;
        }

        private void spin_toplam_EditValueChanged(object sender, EventArgs e)
        {
            //pr.ProductPrice = Convert.ToInt32(spin_toplam.Value);
        }
    }
}