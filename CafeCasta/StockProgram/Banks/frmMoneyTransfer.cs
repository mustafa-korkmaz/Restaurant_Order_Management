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

namespace StockProgram.Banks
{
    public partial class frmMoneyTransfer : DevExpress.XtraEditors.XtraForm
    {
        public delegate void MoneyTransferHandler(object sender, EventArgs e);
        public event MoneyTransferHandler MoneyTransferChanged;
        int payment_type;
        private int bank_id;
        private int log_id;
        private int type;
        private Bank b;
        private ExceptionLogger excLogger;

        public frmMoneyTransfer(int bank_id)
        {
            this.bank_id = bank_id;
            InitializeComponent();
            this.type = 0;//new log
            FillBankProperties();
            frmSettings();
        }
        public frmMoneyTransfer(int bank_id, int log_id)
        {
            this.bank_id = bank_id;
            this.log_id = log_id;
            this.type = 1;//edit log
            InitializeComponent();
            FillBankProperties();
            FillLogs();
            frmSettings();
        }

        /// <summary>
        /// set the satis form properties
        /// </summary>
        private void frmSettings()
        {
            this.lbl_banka.Text = b.Name;
            if (this.type == 1)
            {
                btn_tamamla.Text = "Düzenlemeyi Bitir";
                btn_tamamla.Width = btn_tamamla.Width + 10;
            }
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
    
         
         //   this.spin_pesin.Value = Convert.ToDecimal(this.tutar);
            //FillBanks();
            //SetBanksCombo();
        }

        private void FillLogs()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = db.GetDataTable("select * from v_bank_logs where bank_id= " + bank_id + " and log_id=" + log_id);

            switch (dt.Rows[0]["type"].ToString().Trim())
            {
                case "Para Çekme": this.chk_cek.CheckState = CheckState.Checked;
                    this.chk_yatir.CheckState = CheckState.Unchecked;
                    break;
                case "Elden Para Yatirma": this.chk_cek.CheckState = CheckState.Unchecked;
                    this.chk_yatir.CheckState = CheckState.Checked;
                    break;
                default:
                    break;
            }
            this.spin_miktar.Value = Convert.ToDecimal(dt.Rows[0]["payment_price"].ToString());
            this.txt_aciklama.Text = dt.Rows[0]["desc"].ToString().Trim();
            db.Close();
            db = null;

        }
        private void FillBankProperties()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = db.GetDataTable("select * from bank_details where bank_id= " + bank_id);
            b = new Bank();
            b.Name = dt.Rows[0]["bank_name"].ToString();
            b.Id = bank_id;
            b.Total = Convert.ToInt32(dt.Rows[0]["total"]);
            lbl_isim.Text = b.Name.Trim();
            db.Close();
            db = null;

        }

        private void frmSatis_Load(object sender, EventArgs e)
        {
            if (this.type == 0) // new log ise
            {
                chk_cek.Checked = true;
                chk_yatir.Checked = false;
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
            if (spin_miktar.Value <= 0)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Ödeme tutarını giriniz.", MessageBoxIcon.Error, MessageBoxButtons.OK);
                return;
            }
            ErrorMessages.Message m = new ErrorMessages.Message();
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string strSQL = string.Empty;

            if (chk_cek.Checked == true && chk_yatir.Checked == true)
            {
                if (m.WriteMessage("Aynı anda sadece 1 işlem yapabilirsiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                    return;
            }
            else if (chk_cek.Checked == false && chk_yatir.Checked == false)
            {
                if (m.WriteMessage("Yapmak istediğiniz şlemi seçiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                    return;
            }
            else if (chk_cek.Checked == true && chk_yatir.Checked == false && spin_miktar.Value != 0)
            {
                b.Total -= Convert.ToDouble(spin_miktar.Value);
                payment_type = 0;
            }
            else if (chk_cek.Checked == false && chk_yatir.Checked == true)
            {
                b.Total += Convert.ToDouble(spin_miktar.Value);
                payment_type = 2;
            }
            strSQL = "update bank_details set total=@total where bank_id=" + this.b.Id; //total miktarı update et
            try
            {
                db.CreateSetParameter("total", MySql.Data.MySqlClient.MySqlDbType.Double, b.Total);
                db.ExecuteNonQuery(strSQL);
            }
            catch (Exception exc)
            {

                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(exc.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(exc, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at
            }
            finally
            {
                db.Close();
                db = null;
            }
            if (btn_tamamla.Text == "Transfer Et")
            {
                AddTransferLog();
            }
            else // düzenle
                EditTransferLog();
        }

        private void EditTransferLog()
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string sql = "update bank_logs set amount=@amount, type=@type, description=@description, modified_date=@modified_date where bank_id=@bank_id and log_id=@log_id" ;
            try
            {
                db.CreateSetParameter("amount", MySql.Data.MySqlClient.MySqlDbType.Double, Convert.ToDouble(spin_miktar.Value));
                db.CreateSetParameter("type", MySql.Data.MySqlClient.MySqlDbType.Int16, payment_type);
                db.CreateSetParameter("bank_id", MySql.Data.MySqlClient.MySqlDbType.Int32, bank_id);
                db.CreateSetParameter("log_id", MySql.Data.MySqlClient.MySqlDbType.Int32, log_id);
                db.CreateSetParameter("description",MySql.Data.MySqlClient.MySqlDbType.Text,this.txt_aciklama.Text.Trim());
                db.CreateSetParameter("modified_date", MySql.Data.MySqlClient.MySqlDbType.DateTime, DateTime.Now);
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
            }
            finally
            {
                db.Close();
                db = null;
            }
            this.Dispose();
        }

        /// <summary>
        /// yapılan transfer miktarını loglar
        /// </summary>
        private void AddTransferLog()
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string strSQL = "insert into bank_logs (bank_id,amount,instalment,rate,type,description,modified_date)" +
            " values ('" + b.Id + "','" + Convert.ToDouble(spin_miktar.Value) + "', 0, 0," + payment_type + ",@description,@modified_date)";
            try
            {
                db.CreateSetParameter("modified_date", MySql.Data.MySqlClient.MySqlDbType.DateTime, DateTime.Now);
                db.CreateSetParameter("description", MySql.Data.MySqlClient.MySqlDbType.Text, this.txt_aciklama.Text.Trim());
                db.ExecuteNonQuery(strSQL);
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
            }
            finally
            {
                db.Close();
                db = null;
            }
       //     Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
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
            if (chk_yatir.CheckState == CheckState.Checked)
            {
                chk_cek.CheckState = CheckState.Unchecked;
            }
        }

        private void chk_odeme_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_cek.CheckState == CheckState.Checked)
            {
                chk_yatir.CheckState = CheckState.Unchecked;
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

        private void spin_miktar_Click(object sender, EventArgs e)
        {
            this.spin_miktar.SelectAll();
        }
     
    }
}