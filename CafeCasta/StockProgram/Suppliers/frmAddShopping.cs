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
    public partial class frmAddShopping : DevExpress.XtraEditors.XtraForm
    {
        public delegate void SupplierShoppingHandler(object sender, EventArgs e);
        public event SupplierShoppingHandler SupplierShoppingCompleted;
        private ExceptionLogger excLogger;
        private int supplierId;
        private string supplierName;

        public frmAddShopping(int supplier_id,string name)
        {
            this.supplierId = supplier_id;
            this.supplierName = name;
            InitializeComponent();
            frmSettings();
        }

        /// <summary>
        /// set the satis form properties
        /// </summary>
        private void frmSettings()
        {
            this.lbl_tedarikci.Text = this.supplierName;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
    
         
         //   this.spin_pesin.Value = Convert.ToDecimal(this.tutar);
            //FillBanks();
            //SetBanksCombo();
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

        #endregion


        private void btn_tamamla_Click(object sender, EventArgs e)
        {
            if (spin_miktar.Value<=0)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Ödeme tutarını giriniz.", MessageBoxIcon.Error, MessageBoxButtons.OK);
                return;
            }
            //NewSupplierPayment
            Enums.SupplierPaymentType spt =new Enums.SupplierPaymentType();
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string proc_name = "NewSuppliersPayment";
            double value = Convert.ToDouble(spin_miktar.Value);
    
            spt = Enums.SupplierPaymentType.Odeme;

            if (chk_odeme.CheckState == CheckState.Checked)
            {
                spt = Enums.SupplierPaymentType.Odeme;
            }
            else if (chk_tahsilat.CheckState == CheckState.Checked)
            {
              //  spt = Enums.SupplierPaymentType.Tahsilat;
            }

            try //payment to supplier
            {
                db.CreateSetParameter("payment_price", MySql.Data.MySqlClient.MySqlDbType.Double, value);
                db.CreateSetParameter("suppliers_id", MySql.Data.MySqlClient.MySqlDbType.Int32, this.supplierId);
                db.CreateSetParameter("type", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt32(spt));
                db.CreateSetParameter("process_id", MySql.Data.MySqlClient.MySqlDbType.Int32, 0);
                db.CreateSetParameter("unit", MySql.Data.MySqlClient.MySqlDbType.VarChar, "TL");
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


        private void SaleProducts()
        {
            DBObjects.MySqlCmd db;
            int sell_id = 0;

            #region Get sell_id
            db = new DBObjects.MySqlCmd(StaticObjects.MySqlConn);
            string proc_name = "newSellDetail";
            try
            {
             
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

                //foreach (Sales.SiparisKalem item in siparisList)
                //{
                //    db.SetParameterAt("product_id", item.ProductId);
                //    db.SetParameterAt("product_amount", item.Amount);
                //    db.SetParameterAt("product_color", item.ColorId);
                //    db.SetParameterAt("product_size", item.Size);
                //    db.SetParameterAt("product_price", item.SalePrice);
                //    db.SetParameterAt("product_code", item.ProductCode);
                //    db.ExecuteNonQuerySP(proc_name);
                //}

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

            #region Do Payment

            proc_name = "doPayment";
            try
            {
                db = new MySqlCmd(StaticObjects.MySqlConn);
                db.CreateSetParameter("sell_id", MySql.Data.MySqlClient.MySqlDbType.Int32, sell_id);
                db.CreateParameter("payment_type",MySql.Data.MySqlClient.MySqlDbType.Int16);
                db.CreateParameter("payment_price", MySql.Data.MySqlClient.MySqlDbType.Double);
                db.CreateParameter("bank_id", MySql.Data.MySqlClient.MySqlDbType.Int32);

                //if (spin_pesin.Value>0)
                //{//peşin ödeme
                //    db.SetParameterAt("payment_type", Convert.ToInt16(Enums.PaymentType.Nakit));
                //    db.SetParameterAt("payment_price", spin_pesin.Value);
                //    db.SetParameterAt("bank_id", 0);
                //    db.ExecuteNonQuerySP(proc_name);
                //}
                //if (spin_pos.Value>0)
                //{//bankamatik
                //    db.SetParameterAt("payment_type", Convert.ToInt16(Enums.PaymentType.Banka));
                //    db.SetParameterAt("payment_price", spin_pos.Value);
                //    db.SetParameterAt("bank_id", BItemsList[cbox_banka.SelectedIndex].Id);
                //    db.ExecuteNonQuerySP(proc_name);
                //}
                //if (spin_veresiye.Value>0)
                //{//veresiye
                //    db.SetParameterAt("payment_type", Convert.ToInt16(Enums.PaymentType.Veresiye));
                //    db.SetParameterAt("payment_price", spin_veresiye.Value);
                //    db.SetParameterAt("bank_id", 0);
                //    db.ExecuteNonQuerySP(proc_name);
                //}
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
                db = null;
            }
            #endregion
            Success();
        }

        /// <summary>
        /// sipariş listesi içerisindeki tüm tutarların alt toplamını hesaplar
        /// </summary>
        /// <returns></returns>
        double SiparisToplamHesapla()
        {
            return 0;
            //double siparis_toplam = 0;
            //foreach (Sales.SiparisKalem siparis in siparisList)
            //{
            //    siparis_toplam += siparis.Amount * siparis.SalePrice;
            //}

            //return siparis_toplam;
        }

        private void Success()
        {
            OnSupplierShoppingChanged(EventArgs.Empty);
            this.Dispose();
        }


        protected virtual void OnSupplierShoppingChanged(EventArgs e)
        {
            if (SupplierShoppingCompleted != null)
                SupplierShoppingCompleted(this, e);
        }

        private void btn_duzenle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chk_tahsilat_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tahsilat.CheckState == CheckState.Checked)
            {
                chk_odeme.CheckState = CheckState.Unchecked;
            }
        }

        private void chk_odeme_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_odeme.CheckState == CheckState.Checked)
            {
                chk_tahsilat.CheckState = CheckState.Unchecked;
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

        private void btn_ilave_Click(object sender, EventArgs e)
        {
            if (spin_miktar.Value <= 0)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("İlave tutarını giriniz.", MessageBoxIcon.Error, MessageBoxButtons.OK);
                return;
            }
            //NewSupplierPayment
            Enums.SupplierPaymentType spt = new Enums.SupplierPaymentType();
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string proc_name = "NewSuppliersPayment";
            double value = Convert.ToDouble(spin_miktar.Value);

            spt = Enums.SupplierPaymentType.Ilave;

            if (chk_odeme.CheckState == CheckState.Checked)
            {
                spt = Enums.SupplierPaymentType.Ilave;
            }
            else if (chk_tahsilat.CheckState == CheckState.Checked)
            {
               // spt = Enums.SupplierPaymentType.Tahsilat;
            }

            try //payment to supplier
            {
                db.CreateSetParameter("payment_price", MySql.Data.MySqlClient.MySqlDbType.Double, value);
                db.CreateSetParameter("suppliers_id", MySql.Data.MySqlClient.MySqlDbType.Int32, this.supplierId);
                db.CreateSetParameter("type", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt32(spt));
                db.CreateSetParameter("unit", MySql.Data.MySqlClient.MySqlDbType.VarChar, "TL");
                db.CreateSetParameter("process_id", MySql.Data.MySqlClient.MySqlDbType.Int32, 0);
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
     
    }
}