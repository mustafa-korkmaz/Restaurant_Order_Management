using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Diagnostics;
using StockProgram.Sales;
using StockProgram.DBObjects;

namespace StockProgram.Tables
{
    public partial class ucTableItem : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void TableItemHandler(object sender, EventArgs e);
        public event TableItemHandler NewOrderSelected;
        public event TableItemHandler EditOrderSelected;
        public event TableItemHandler CheckoutSelected;
        public event TableItemHandler ViewOrderSelected;
        public event TableItemHandler PrintAdisyonSelected;
        public event TableItemHandler ChangeTableSelected;
        public event TableItemHandler CheckCanceledSelected;
        public event TableItemHandler TableItemDoubleClicked;
        public event TableItemHandler TableItemClicked;

        private List<Orders> orderList; 
        private Table table;
        private double price;
        private int order_id;
        private DateTime order_last_modified;
        private int account_id;
        private int account_id_with_no_order;
        private DateTime order_time;
        Enums.TableStatus status;
     
        public ucTableItem(Table t)
        {
            InitializeComponent();
            this.table = t;
            Settings(); 
        }

        private void Settings()
        {
            this.lbl_table_number.Text = this.table.name;
            this.Size = new System.Drawing.Size(StaticObjects.Settings.table_width,StaticObjects.Settings.table_height);
            this.lbl_price.Appearance.Font = new System.Drawing.Font("Tahoma", (Convert.ToInt16(StaticObjects.Settings.table_width/7)));
            this.lbl_price.Location = new System.Drawing.Point(Convert.ToInt16(StaticObjects.Settings.table_width/5.8), (StaticObjects.Settings.table_height / 2) - (Convert.ToInt16(lbl_price.Font.Size)));
            SetTableStatus();
            GetOrderDetails();
            SetTimeOfDay();
            SetBackColor();
            SetPrice();
            SetContextMenuItems();
        }

        /// <summary>
        /// fires when table clicked
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnTableItemDoubleClicked(EventArgs e)
        {
            if (TableItemDoubleClicked != null)
                TableItemDoubleClicked(this, e);
        }

        /// <summary>
        /// fires when table clicked
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnTableItemClicked(EventArgs e)
        {
            if (TableItemClicked != null)
                TableItemClicked(this, e);
        }

        /// <summary>
        /// fires when new order clicked
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnNewOrderSelected(EventArgs e)
        {
            if (NewOrderSelected != null)
                NewOrderSelected(this, e);
        }
      
        /// <summary>
        /// fires when edit order selected
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnEditOrderSelected(EventArgs e)
        {
            if (EditOrderSelected != null)
                EditOrderSelected(this, e);
        }

        /// <summary>
        /// fires when print order selected
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPrintAdisyonSelected(EventArgs e)
        {
            if (PrintAdisyonSelected != null)
                PrintAdisyonSelected(this, e);
        }

        /// <summary>
        /// fires when checkout clicked
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCheckoutSelected(EventArgs e)
        {
            if (CheckoutSelected != null)
                CheckoutSelected(this, e);
        }
      
     
        /// <summary>
        /// fires when view order clicked
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnViewOrderSelected(EventArgs e)
        {
            if (ViewOrderSelected != null)
                ViewOrderSelected(this, e);
        }

        /// <summary>
        /// fires when view check  canceled
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCheckCanceledSelected(EventArgs e)
        {
            if (CheckCanceledSelected != null)
                CheckCanceledSelected(this, e);
        }

        /// <summary>
        /// fires when table change selected
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnChangeTableSelected(EventArgs e)
        {
            if (ChangeTableSelected != null)
                ChangeTableSelected(this, e);
        }
   
        /// <summary>
        /// sets the back color by table status
        /// </summary>
        private void SetBackColor()
        {
            switch (this.status)
            {
                case Enums.TableStatus.Acik:
                    break;
                case Enums.TableStatus.Kapali:
                    break;
                case Enums.TableStatus.Uygun:
                    break;
                case Enums.TableStatus.Dolu:
                    this.pnl_back.Appearance.BackColor2 = System.Drawing.Color.SkyBlue;
                    break;
                case Enums.TableStatus.Rezerve:
                    this.pnl_back.Appearance.BackColor = System.Drawing.Color.PeachPuff;
                    break;
                case Enums.TableStatus.Alindi:
                    break;
                case Enums.TableStatus.Kapandi:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// prepares the options for item right click 
        /// </summary>
        private void SetContextMenuItems()
        {
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("Yeni Adisyon Aç");
            cm.MenuItems[0].Name="new";
            cm.MenuItems.Add("Sipariş Ekle");
            cm.MenuItems[1].Name = "add";
            cm.MenuItems.Add("Hesap Kes");
            cm.MenuItems[2].Name = "checkout";
            cm.MenuItems.Add("Adisyon Yazdır");
            cm.MenuItems[3].Name = "print";
            cm.MenuItems.Add("Sipariş Listesi");
            cm.MenuItems[4].Name = "orders";
            cm.MenuItems.Add("Masa Değiştir");
            cm.MenuItems[5].Name = "change";
            cm.MenuItems.Add("İptal Et");
            cm.MenuItems[6].Name = "cancel";

            //set status and events
            if (this.status==Enums.TableStatus.Uygun ||this.status==Enums.TableStatus.Rezerve)
            {
                cm.MenuItems[0].Click += new EventHandler(ucTableContextMenuItem_Click);
                cm.MenuItems[1].Enabled = false;
                cm.MenuItems[2].Enabled = false;
                cm.MenuItems[3].Enabled = false;
                cm.MenuItems[4].Enabled = false;
                cm.MenuItems[5].Enabled = false;
                cm.MenuItems[6].Enabled = false;
            }
            else if (this.status == Enums.TableStatus.Dolu)
            {
                if (this.price == 0) //hesap açılmış sipariş verilmemiş
                {
                    cm.MenuItems[1].Click += new EventHandler(ucTableContextMenuItem_Click);
                    cm.MenuItems[5].Click += new EventHandler(ucTableContextMenuItem_Click);
                    cm.MenuItems[6].Click += new EventHandler(ucTableContextMenuItem_Click);
                    cm.MenuItems[0].Enabled = false;
                    cm.MenuItems[2].Enabled = false;
                    cm.MenuItems[3].Enabled = false;
                    cm.MenuItems[4].Enabled = false;
                }
                else
                {
                    cm.MenuItems[1].Click += new EventHandler(ucTableContextMenuItem_Click);
                    cm.MenuItems[2].Click += new EventHandler(ucTableContextMenuItem_Click);
                    cm.MenuItems[3].Click += new EventHandler(ucTableContextMenuItem_Click);
                    cm.MenuItems[4].Click += new EventHandler(ucTableContextMenuItem_Click);
                    cm.MenuItems[5].Click += new EventHandler(ucTableContextMenuItem_Click);
                    cm.MenuItems[6].Click += new EventHandler(ucTableContextMenuItem_Click);
                    cm.MenuItems[0].Enabled = false;
                }
            }
    
            this.ContextMenu = cm;
        }

        void ucTableContextMenuItem_Click(object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            switch (mi.Name)
            {
                case "new":
                    OnNewOrderSelected(EventArgs.Empty);
                    break;
                case "add":
                    OnEditOrderSelected(EventArgs.Empty);
                    break;
                case "checkout":
                    OnCheckoutSelected(EventArgs.Empty);
                    break;
                case "print":
                    OnPrintAdisyonSelected(EventArgs.Empty);
                    break;
                case "orders":
                    OnViewOrderSelected(EventArgs.Empty);
                    break;
                case "change":
                    OnChangeTableSelected(EventArgs.Empty);
                    break;
                case "cancel":
                    OnCheckCanceledSelected(EventArgs.Empty);
                    break;
                default:
                    break;
            }
        }

        private void SetTimeOfDay()
        {
            if (this.status == Enums.TableStatus.Dolu || this.status == Enums.TableStatus.Rezerve)
            {
                this.lbl_time.Text = this.order_time.TimeOfDay.ToString();
            }
            else this.lbl_time.Visible = false;
        }

        private void SetTableStatus()
        {
         status=new Enums.TableStatus();
         switch (this.table.status.id)
         {
             case 1: status = Enums.TableStatus.Acik;
                 break;
             case 2: status = Enums.TableStatus.Kapali;
                 break;
             case 3: status = Enums.TableStatus.Uygun;
                 break;
             case 4: status = Enums.TableStatus.Dolu;
                 break;
             case 5: status = Enums.TableStatus.Rezerve;
                 break;
             case 6: status = Enums.TableStatus.Alindi;
                 break;
             case 7: status = Enums.TableStatus.Kapandi;
                 break;
             default:
                 break;
         }
        }

        private void SetPrice()
        {
            switch (status)
            {
                case Enums.TableStatus.Acik:
                    break;
                case Enums.TableStatus.Kapali:
                    break;
                case Enums.TableStatus.Uygun:
                    this.lbl_price.Visible = false;
                    break;
                case Enums.TableStatus.Dolu:
                    if (this.price > 0)
                    {
                        this.lbl_price.Text = this.price.ToString("#0.00") + " TL";
                    }
                    else
                    {
                        this.lbl_price.Text ="Sipariş Bekleniyor";
                        this.lbl_price.Font = new System.Drawing.Font("Tahoma",(this.lbl_price.Font.Size/2)); // yazı boyutunu yarıya indirdik
                        lbl_time.Visible = false;
                    }
                    break;
                case Enums.TableStatus.Rezerve:
                    this.lbl_price.Visible = false;
                    break;
                case Enums.TableStatus.Alindi:
                    break;
                case Enums.TableStatus.Kapandi:
                    break;
                default:
                    break;
            }
        }

        private void GetOrderDetails()
        {
            this.orderList = new List<Orders>();
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select * from v_accounts_master where  account_type=1 and account_status=1 and owner_id=" + this.table.id + " order by account_create_date desc";
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);
                if (dt.Rows.Count<=0)
                {
                    if (this.status==Enums.TableStatus.Dolu)
                    SetAccountIdWithNoOrder(); //hiç sipariş eklenmemiş ise view üzerinde account_id boş geliyor. account_details tablosundan çekmek zorundayız
                    return;
                }
              
                this.account_id = Convert.ToInt32(dt.Rows[0]["account_id"]);
                foreach (DataRow row in dt.Rows)
                {
                    this.price += Convert.ToDouble(row["total_order_price"]);
                    this.order_time = Convert.ToDateTime(row["account_create_date"]);
                    this.order_last_modified = Convert.ToDateTime(row["order_modified_date"]);
                }
             
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
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
        }

        public void SetAccountIdWithNoOrder()
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select account_id from account_details where  account_type=1 and account_status=1 and account_owner=" + this.table.id + " order by create_date desc";
            try
            {
                this.account_id_with_no_order = Convert.ToInt32(db.Get_Scalar(strSQL)) ;
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message+ " (iptal edilecek hesap bulunamadı)", MessageBoxIcon.Error, MessageBoxButtons.OK);
                this.account_id_with_no_order = 0;
            }
            finally
            {
                db.Close();
                db = null;
            }
        }

        public double GetOrderPrice()
        {
            return this.price;
        }

        public Enums.TableStatus GetStatus()
        {
            return this.status;
        }

        public Table GetTable()
        {
            return this.table;
        }

        public int GetOrderId()
        {
            return this.order_id;
        }
        public int GetAccountId()
        {
            return this.account_id ;
        }
        public int GetAccountIdWithNoOrder()
        {
            return this.account_id_with_no_order;
        }
        public DateTime GetOrderLastModifiedTime()
        {
            return this.order_last_modified;
        }
        private void pnl_back_Click(object sender, EventArgs e)
        {
            OnTableItemClicked(EventArgs.Empty);
        }

        private void lbl_price_Click(object sender, EventArgs e)
        {
            OnTableItemClicked(EventArgs.Empty);
        }

        private void pnl_back_DoubleClick(object sender, EventArgs e)
        {
            OnTableItemDoubleClicked(EventArgs.Empty);
        }

        private void lbl_price_DoubleClick(object sender, EventArgs e)
        {
            OnTableItemDoubleClicked(EventArgs.Empty);
        }

        public DevExpress.XtraEditors.PanelControl GetBackPanel()
        {
            return this.pnl_back;
        }
    }
}
