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
using StockProgram.Tables;

namespace StockProgram.Sales
{
    public partial class frmSatisPopup : DevExpress.XtraEditors.XtraForm
    {
        private ExceptionLogger excLogger;
        public delegate void PurchaseHandler(object sender, EventArgs e);
        public event PurchaseHandler PurchaseCompleted;
        private ucTableItem tableItem;
        private Orders order;
        Enums.OrderType order_type;
        private List <SiparisKalem> siparisList;
        private List<Bank> BItemsList;
        private List<BankInstalment> InstalmentList;
        private List<Customer> CItemsList;
        private List<Staff> StaffList;
        private ControlHelper controlHelper;
        double tutar;
        double discount;
        double indirimsizTutar;

        public frmSatisPopup(ref List <SiparisKalem> sList)
        {
            InitializeComponent();
            controlHelper = new ControlHelper();
            this.siparisList = sList;
            this.tutar = SiparisToplamHesapla();
            ShowDiscount();
            CalculateCost();
            frmSettings();
        }

        public frmSatisPopup(ref ucTableItem tableItem)
        {
            InitializeComponent();
            controlHelper = new ControlHelper();
            this.tableItem=tableItem;
            this.tutar = this.tableItem.GetOrderPrice();
            this.indirimsizTutar = this.tutar;
            this.lbl_owner_name.Text ="Masa: #"+ this.tableItem.GetTable().name;
            ShowDiscount();
            //CalculateCost();
            frmSettings();
        }

        public frmSatisPopup(ref Orders order)
        {
            InitializeComponent();
            controlHelper = new ControlHelper();
            this.order = order;
            this.siparisList = this.order.siparisList;
            this.order_type = this.order.type;
            this.tutar = this.order.GetOrderPrice();
            this.indirimsizTutar = this.tutar;
            this.lbl_owner_name.Text = (this.order_type==Enums.OrderType.Paket)?  this.order.owner_name : "Masa: #" + this.order.owner_name ;
            ShowDiscount();
            //CalculateCost();
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
            spin_pesin.Properties.Increment = 5;
            spin_pos.Properties.Increment = 5;
            spin_veresiye.Properties.Increment = 5;
            this.lbl_tutar.Text = this.tutar.ToString("#0.00") + " TL";
            this.spin_pesin.Value = Convert.ToDecimal(this.tutar);
       //     FillCustomers();
    //        FillStaff();
            FillBanks();
            SetBanksCombo();
          //  this.ActiveControl = this.btn_tamamla;
        }
        private void frmSatis_Load(object sender, EventArgs e)
        {
            lbl_maliyet.Visible = false;
            chk_maliyet.CheckState = CheckState.Unchecked;
        }

        /// <summary>
        /// calculates the cost of the order
        /// </summary>
        private void CalculateCost()
        {
            double cost = 0;
            foreach (SiparisKalem  kalem in this.siparisList)
            {
                cost+=(kalem.BuyPrice*kalem.Amount);
            }
            this.lbl_maliyet.Text = cost.ToString("#0.00")+" TL";
        }

        #region Set Form Controls

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }

        private void ShowDiscount()
        {
            this.discount = (100 * (this.indirimsizTutar - Convert.ToDouble(this.spin_pesin.Value + this.spin_pos.Value+this.spin_veresiye.Value))) / this.indirimsizTutar;
            this.lbl_indirim.Text = "%" + (discount.ToString("#0.0"));
        }


        //private void FillCustomers()
        //{
        //    //fill customers
        //    DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
        //    string strSQL = "select customer_id, customer_name from customer_details where is_deleted=0 order by display_order, customer_name asc";
        //    CItemsList = new List<DBObjects.Customer>();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dt = db.GetDataTable(strSQL);
        //        if (dt.Rows.Count == 0)
        //        {
        //            ErrorMessages.Message message = new ErrorMessages.Message();
        //            message.WriteMessage("Müşteriler sayfsından en az 1 Müşteri eklemelisiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
        //            Parent.Controls["pnl_master"].Visible = true;
        //            this.Dispose();
        //            return;
        //        }
        //        controlHelper.FillControl(cbo_customer, Enums.RepositoryItemType.ComboBox, ref dt, "customer_name");
        //        cbo_customer.Text = cbo_customer.Properties.Items[0].ToString();
        //        CItemsList = controlHelper.GetCustomers(ref dt);
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
        private void FillBanks()
        {
            //fill Suppliers
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select bank_id, bank_name from bank_details where bank_isDeleted=0 order by bank_name asc";
            BItemsList = new List<DBObjects.Bank>();
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);
                if (dt.Rows.Count == 0)
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Bankalar sayfsından en az 1 banka eklemelisiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    Parent.Controls["pnl_master"].Visible = true;
                    this.Dispose();
                    return;
                }
                controlHelper.FillControl(cbox_banka, Enums.RepositoryItemType.ComboBox, ref dt, "bank_name");
                BItemsList = controlHelper.GetBanks(ref dt);
                cbox_banka.Text = cbox_banka.Properties.Items[0].ToString();           
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
        private void spin_pesin_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(spin_pesin.Value) < 0)
            {
                spin_pesin.Value += spin_pesin.Properties.Increment;
                return;
            }
            else
            {
                ShowDiscount();
            }
        }

        private void spin_pos_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(spin_pos.Value) < 0)
            {
                spin_pos.Value += spin_pos.Properties.Increment;
                return;
            }
            else
                // this.spin_veresiye.Value = Convert.ToDecimal(this.tutar.ToString("#0.00")) - this.spin_pesin.Value - this.spin_pos.Value;
                ShowDiscount();
        }

        private void spin_veresiye_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(spin_veresiye.Value) < 0)
            {
                spin_veresiye.Value += spin_veresiye.Properties.Increment;
                return;
            }
            else
                ShowDiscount();
        }

        private void SetBanksCombo()
        {
            cbox_banka.Text = cbox_banka.Properties.Items[0].ToString();
        }
        #endregion


        private void FillStaff()
        {
            //fill staff
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select id, name from staff where is_deleted=0 AND type=1 order by display_order, name asc";
            StaffList = new List<DBObjects.Staff>();
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);
                if (dt.Rows.Count == 0)
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("PErsonel sayfsından en az 1 personel eklemelisiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    Parent.Controls["pnl_master"].Visible = true;
                    this.Dispose();
                    return;
                }
                controlHelper.FillControl(cb_staff, Enums.RepositoryItemType.ComboBox, ref dt, "name");
                cb_staff.Text = cb_staff.Properties.Items[0].ToString();
                StaffList = controlHelper.GetStaff(ref dt);
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
        private bool CheckTutar()
        {
            if (spin_pesin.Value + spin_pos.Value + spin_veresiye.Value <= Convert.ToDecimal(this.tutar.ToString("#.##")))
            {
                return true;
            }
            else
                return false;
            //if (spin_pesin.Value+spin_pos.Value+spin_veresiye.Value==Convert.ToDecimal(this.tutar))
            //{
            //    return true;
            //}
            //else
            //return false;
        }

        private void btn_tamamla_Click(object sender, EventArgs e)
        {
            ErrorMessages.Message msg = new ErrorMessages.Message();
            if (chk_hediye.CheckState == CheckState.Checked)
            {
                if (DialogResult.OK == msg.WriteMessage("Satışınızın hediye olarak görünmesini onaylıyor musunuz?", MessageBoxIcon.Information, MessageBoxButtons.OKCancel))
                {
                    SaleProducts();
                }
                else return;
            }
            else
            if (CheckTutar())
            {
                //todo: satışı gerçekleştir
                SaleProducts();
            }
            else
            {
                msg.WriteMessage("Toplam tutar ödeme şekli ile uyuşmuyor", MessageBoxIcon.Error, MessageBoxButtons.OK);
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
                db.CreateSetParameter("sell_desc", MySql.Data.MySqlClient.MySqlDbType.Text, txt_aciklama.Text.ToUpper());
                db.CreateSetParameter("account_id", MySql.Data.MySqlClient.MySqlDbType.Int32, this.order.account_id);
        //        db.CreateSetParameter("staff_id", MySql.Data.MySqlClient.MySqlDbType.Int32, StaffList[cb_staff.SelectedIndex].id);
                db.CreateSetParameter("staff_id", MySql.Data.MySqlClient.MySqlDbType.Int32, StaticObjects.User.Id);
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
                db.CreateParameter("product_amount", MySql.Data.MySqlClient.MySqlDbType.Double);
                db.CreateParameter("product_color", MySql.Data.MySqlClient.MySqlDbType.Int32);
                db.CreateParameter("product_size", MySql.Data.MySqlClient.MySqlDbType.Double);
                db.CreateParameter("product_price", MySql.Data.MySqlClient.MySqlDbType.Double);
                db.CreateParameter("product_code", MySql.Data.MySqlClient.MySqlDbType.Text);

                if (chk_hediye.CheckState == CheckState.Checked)
                {
                    foreach (SiparisKalem item in siparisList)
                    {
                        item.SalePrice = 0;// hediye ise satış fiyatını 0 yapıyoruz.
                    }
                }

                foreach (SiparisKalem item in siparisList)
                {
                    db.SetParameterAt("product_id", item.ProductId);
                    db.SetParameterAt("product_amount", item.Amount);
                    db.SetParameterAt("product_color", item.ColorId);
                    db.SetParameterAt("product_size", item.Porsion);
                    db.SetParameterAt("product_price", item.UndiscountedPrice * ((100 - this.discount) / 100));
                    db.SetParameterAt("product_code", item.ProductCode);
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
                if (this.order_type==Enums.OrderType.Paket) //paket servis ise o müşteriye ödeme kaydı at
                doCustomerPayment(sell_id, 1); //müşteri alışveriş kaydını al
                db = new MySqlCmd(StaticObjects.MySqlConn);
                db.CreateSetParameter("sell_id", MySql.Data.MySqlClient.MySqlDbType.Int32, sell_id);
                db.CreateParameter("instalment", MySql.Data.MySqlClient.MySqlDbType.Int16);
                db.CreateParameter("rate", MySql.Data.MySqlClient.MySqlDbType.Double);
                db.CreateParameter("payment_type",MySql.Data.MySqlClient.MySqlDbType.Int16);
                db.CreateParameter("payment_price", MySql.Data.MySqlClient.MySqlDbType.Double);
                db.CreateParameter("bank_id", MySql.Data.MySqlClient.MySqlDbType.Int32);

                if (spin_pesin.Value > 0 && chk_hediye.CheckState == CheckState.Unchecked)
                {//peşin ödeme
                    db.SetParameterAt("payment_type", Convert.ToInt16(Enums.PaymentType.Nakit));
                    db.SetParameterAt("payment_price", spin_pesin.Value);
                    db.SetParameterAt("instalment", 0);
                    db.SetParameterAt("rate", 0);
                    db.SetParameterAt("bank_id", 0);
                    db.ExecuteNonQuerySP(proc_name);
                }
                if (spin_pos.Value > 0 && chk_hediye.CheckState == CheckState.Unchecked)
                {//bankamatik
                    db.SetParameterAt("payment_type", Convert.ToInt16(Enums.PaymentType.Banka));
                    db.SetParameterAt("payment_price", spin_pos.Value);
                    db.SetParameterAt("instalment", InstalmentList[cb_taksit.SelectedIndex].instalment);
                    db.SetParameterAt("rate", InstalmentList[cb_taksit.SelectedIndex].rate);
                    db.SetParameterAt("bank_id", BItemsList[cbox_banka.SelectedIndex].Id);
                    db.ExecuteNonQuerySP(proc_name);
                }
                if (spin_veresiye.Value > 0 && chk_hediye.CheckState == CheckState.Unchecked)
                {//veresiye
                    db.SetParameterAt("payment_type", Convert.ToInt16(Enums.PaymentType.Veresiye));
                    db.SetParameterAt("payment_price", spin_veresiye.Value);
                    db.SetParameterAt("instalment", 0);
                    db.SetParameterAt("rate", 0);
                    db.SetParameterAt("bank_id", 0);
                    db.ExecuteNonQuerySP(proc_name);
                }
                if (chk_hediye.CheckState==CheckState.Checked)
                {//hediye
                    db.SetParameterAt("payment_type", Convert.ToInt16(Enums.PaymentType.Hediye));
                    db.SetParameterAt("payment_price", this.tutar);
                    db.SetParameterAt("bank_id", 0);
                    db.SetParameterAt("instalment", 0);
                    db.SetParameterAt("rate", 0);
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
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                db.Close();
             //   db = null;
            }
            #endregion

            #region Set Status
            proc_name = "setStatusAfterSale";
            try
            {         
                db = new MySqlCmd(StaticObjects.MySqlConn);
                db.CreateSetParameter("a_id", MySql.Data.MySqlClient.MySqlDbType.Int32, this.order.account_id);
                db.CreateSetParameter("o_id", MySql.Data.MySqlClient.MySqlDbType.Int32, this.order.order_id);
                db.CreateParameter("ow_id", MySql.Data.MySqlClient.MySqlDbType.Int32);
                if(this.order_type==Enums.OrderType.Restoran)
                    db.SetParameterAt("ow_id",this.order.owner_id);
                else
                db.SetParameterAt("ow_id",0);

                db.ExecuteNonQuerySP(proc_name);
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Cafe Casta Programı Hatası";
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
        /// müşteri veresiye alışveriş yapmış ise o müşteriye borç kaydı atar.
        /// </summary>
        private void doCustomerPayment(int sell_id,int type)
        {
            DBObjects.MySqlCmd db = new DBObjects.MySqlCmd(StaticObjects.MySqlConn);
            string proc_name = "customerPayment";
            double payment;
            double total_price;
            if (chk_hediye.CheckState == CheckState.Checked)
            {
                type = 2;// type=hediye
                payment = 0;
                total_price = 0;
            }
            else
            {
                payment = this.tutar - (Convert.ToDouble(spin_veresiye.Value));
                total_price = this.tutar;
            }

            try
            {
                db.CreateSetParameter("payment_desc", MySql.Data.MySqlClient.MySqlDbType.Text, txt_aciklama.Text.ToUpper());
                db.CreateSetParameter("customer_id", MySql.Data.MySqlClient.MySqlDbType.Int32,this.order.owner_id);
                db.CreateSetParameter("sell_id", MySql.Data.MySqlClient.MySqlDbType.Int32, sell_id);
                db.CreateSetParameter("payment",MySql.Data.MySqlClient.MySqlDbType.Double, payment);
                db.CreateSetParameter("total_price", MySql.Data.MySqlClient.MySqlDbType.Double, total_price);              
                db.CreateSetParameter("type", MySql.Data.MySqlClient.MySqlDbType.Int32,type);
                db.ExecuteNonQuerySP(proc_name);
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
        }

        /// <summary>
        /// sipariş listesi içerisindeki tüm tutarların alt toplamını hesaplar
        /// </summary>
        /// <returns></returns>
        double SiparisToplamHesapla()
        {
            double siparis_toplam = 0;
            this.indirimsizTutar = 0;
            foreach (SiparisKalem siparis in siparisList)
            {
                siparis_toplam += siparis.Amount * siparis.SalePrice;
                this.indirimsizTutar += siparis.Amount*siparis.UndiscountedPrice;
            }

            return siparis_toplam;
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

        private void btn_pesin_Click(object sender, EventArgs e)
        {
            spin_pesin.Value = Convert.ToDecimal(this.tutar);
            spin_pos.Value = 0;
            spin_veresiye.Value = 0;
        }

        private void btn_banka_Click(object sender, EventArgs e)
        {
            spin_pos.Value = Convert.ToDecimal(this.tutar);
            spin_pesin.Value = 0;
            spin_veresiye.Value = 0;
        }

        private void btn_veresiye_Click(object sender, EventArgs e)
        {
            spin_veresiye.Value = Convert.ToDecimal(this.tutar);
            spin_pos.Value = 0;
            spin_pesin.Value = 0;
        }

        private void chk_hediye_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_hediye.CheckState == CheckState.Checked)
            {
                lbl_indirim.Text = "%100";
                spin_pesin.Value = 0;
                spin_pos.Value = 0;
                spin_veresiye.Value = 0;
            }
        }

        private void chk_maliyet_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_maliyet.CheckState==CheckState.Checked)
            {
                lbl_maliyet.Visible = true;
            }else
                if (chk_maliyet.CheckState == CheckState.Unchecked)
                {
                    lbl_maliyet.Visible = false;
                }
        }

        private void cbox_banka_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bank_id = BItemsList[cbox_banka.SelectedIndex].Id;
            InstalmentList = new List<BankInstalment>();

            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            try
            {
                DataTable dt = db.GetDataTable("select instalment,payment_day from bank_instalments where bank_id=" + bank_id) ;
                string instalment=dt.Rows[0]["instalment"].ToString();
                int payment_day = Convert.ToInt32(dt.Rows[0]["payment_day"]);
                FillInstalments(instalment,payment_day);
            }
            catch (Exception ex)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(ex.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(ex, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at

                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(ex.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                db.Close();
                db = null;
            }
          
         
        }

        private void FillInstalments(string instalment, int payment_day)
        { //instalment= 3:2,5-6:4,5
            InstalmentList.Clear();  //liste yenilenmesi gere. o yüzden temizleyelim
            cb_taksit.Properties.Items.Clear();

            int instalmentValue=0;
            double rate=0;
            string[] actions = instalment.Split('-');
            for (int i = 0; i < actions.Length; i++)
            {
                string []values = actions[i].Split(':');
                instalmentValue=Convert.ToInt32(values[0]);
                rate = Convert.ToDouble(values[1]);
                InstalmentList.Add(new BankInstalment { instalment = instalmentValue, rate = rate, payment_day = payment_day });
                cb_taksit.Properties.Items.Add(instalmentValue);
            }
            cb_taksit.SelectedIndex = 0;
        }

        private void cb_staff_Click(object sender, EventArgs e)
        {
            this.cb_staff.SelectAll();
        }

        private void frmSatisPopup_Shown(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void cb_taksit_EditValueChanged(object sender, EventArgs e)
        {
            string val = (cb_taksit.EditValue).ToString() ;
            bool result=true;
            
            foreach (BankInstalment taksit in InstalmentList)
            {
                if (taksit.instalment.ToString() == val)
                {
                    result = true;
                    break;
                }
                else
                    result = false;
            }

            if (result==false)
            {
                 ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Tanımlanmamış taksit giremezsiniz.", MessageBoxIcon.Error, MessageBoxButtons.OK);
                cb_taksit.SelectedIndex = 0;
            }
        }

        private void spin_pos_Click(object sender, EventArgs e)
        {
            this.spin_pos.SelectAll();
        }

        private void spin_veresiye_Click(object sender, EventArgs e)
        {
            this.spin_veresiye.SelectAll();
        }

        private void spin_pesin_Click(object sender, EventArgs e)
        {
            this.spin_pesin.SelectAll();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}