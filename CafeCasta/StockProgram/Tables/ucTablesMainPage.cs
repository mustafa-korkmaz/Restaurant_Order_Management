using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using StockProgram.DBObjects;
using StockProgram.Sales;
using System.Threading;

namespace StockProgram.Tables
{
    public partial class ucTablesMainPage : DevExpress.XtraEditors.XtraUserControl
    {
        private  DateTime allOrdersLastModifiedTime;
        private int SelectedTabPageIndex = 0;
        ucTableItem selectedTableItem;
        private StockProgram.ControlHelper controlHelper;
        private List<DBObjects.TableCategory> tCatList;
        public delegate void SupplierGridHandler(object sender, EventArgs e);
        public event SupplierGridHandler SupplierGridChanged;
        private int tableCounter=0;
        private bool isTableChangeSelected;
        private TableWithAccount changingTable;
        private double allTableChecksTotal = 0;
        public ucTablesMainPage()
        {
            InitializeComponent();
            FillTableCategories();
            isTableChangeSelected = false;
            if (StaticObjects.Settings.tables_refresh_time>0) //oto yenileme açık
            {
                SetTimer();
            }
        }

        /// <summary>
        /// otomatik sayfa yenileme prosesini başlatır.
        /// </summary>
        private void SetTimer()
        {
            if (StaticObjects.Settings.tables_refresh_time==0)
            {
                return;
            }
       
            Timer.timer.Tick += new EventHandler(CheckDbChanges);
            Timer.exitFlag = false;
            // Sets the timer interval to X seconds.
            Timer.timer.Interval = 1000*StaticObjects.Settings.tables_refresh_time;
            Timer.timer.Start();
        }

        public void CheckDbChanges(object o,EventArgs e)
        {
                Timer.timer.Stop();

                if (!Timer.exitFlag)
                {
                    Timer.timer.Enabled = true;
                }
                else
                {
                    Timer.timer.Enabled = false;
                    Timer.timer.Tick -= new EventHandler(CheckDbChanges);
                    return;
                }
                DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
                string strSQL = "select  ifnull(MAX(order_modified_date),DATE('2000-12-31'))  from v_accounts_master where  account_type=1 and account_status=1 ";
                try
                {
                    object obj=db.Get_Scalar(strSQL);
                    DateTime orders_last_modified_date =Convert.ToDateTime(obj);
                    if (orders_last_modified_date.Subtract(this.allOrdersLastModifiedTime).TotalSeconds>0 && orders_last_modified_date!=Convert.ToDateTime("2000-12-31"))
                    {//son sayfa yenilemeden itibaren değişiklikler olmuş. Tekrar yenile
                        this.Refresh();
                    }
                }
                catch (Exception ex)
                {
                    string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
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
        private void btn_back_Click(object sender, EventArgs e)
        {
            ucSaleOptions ctrl = new ucSaleOptions();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }

        protected virtual void OnSupplierGridChanged(EventArgs e)
        {
            if (SupplierGridChanged != null)
                SupplierGridChanged(this, e);
        }

        private void FillTableCategories()
        {
            //fill Suppliers
            controlHelper = new ControlHelper();
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select tcategory_id, tcategory_name from table_categories where is_deleted=0 and tcategory_status<>2 order by display_order, tcategory_name asc";
            tCatList = new List<DBObjects.TableCategory>();
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
            
                tCatList = controlHelper.GetTableCategories(ref dt);
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

        private void LoadCategoryTabs()
        {
            this.allTableChecksTotal = 0;
            try
            {
                if (tCatList.Count == 0)
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Yerleşim alanı eklemelisiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    ucSaleOptions ctrl = new ucSaleOptions();
                    ctrl.Dock = DockStyle.Fill;
                    ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
                    this.Dispose();
                    return;
                }
                CategoryTab ct;

                foreach (TableCategory  cat in tCatList)
                {
                    ct = new CategoryTab(cat.Id, cat.Name, cat.Name);
                    ct.Click += new EventHandler(ct_Click);
                    this.LoadCategoryTabItems(ref ct);
                    this.tabControl.TabPages.Add(ct);
                }
                SetTableCounter();
                SetAllTableChecksTotal();
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, LoadCategoryTabs() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
                // retValue = 0;
            }
    
        }

        private void SetTableCounter()
        {
            this.lbl_table_counter.Text = this.tableCounter.ToString();
        }

        private void SetAllTableChecksTotal()
        {
            this.lbl_all_checks_total.Text = this.allTableChecksTotal.ToString("#0.00")+" TL";
        }

        void ct_Click(object sender, EventArgs e)
        {
            CategoryTab ct = sender as CategoryTab;
            ct.Focus();
        }
        private void LoadCategoryTabItems(ref CategoryTab ct)
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select* from v_table_details where (is_deleted=0 and table_status<>2 and table_category="+ct.cat_id+") order by display_order, table_name asc";
            DataTable dt = new DataTable();
            
            try
            {
                dt = db.GetDataTable(strSQL);
                int x = 20, y = -StaticObjects.Settings.table_height, itemCountPerLine = 0;

                if (dt.Rows.Count > 0)
                {
                    Table t;
                    ucTableItem ti;
                    foreach (DataRow row in dt.Rows)
                    {
                        t = new Table(Convert.ToInt32(row["table_id"]));
                        t.name = row["table_name"].ToString();
                        t.category.Id = Convert.ToInt32(row["table_category"]);
                        t.category.Name = row["table_category_name"].ToString();
                        t.status.id = Convert.ToInt32(row["table_status"]);
                        t.status.name = row["status_name"].ToString();
                        t.create_date=Convert.ToDateTime(row["create_date"]);

                        ti = new ucTableItem(t);
                        CheckLastOrderTime( ti.GetOrderLastModifiedTime());
                        ti.TableItemClicked += new ucTableItem.TableItemHandler(ti_TableItemClicked);
                        ti.TableItemDoubleClicked += new ucTableItem.TableItemHandler(ti_TableItemDoubleClicked); 
                        ti.NewOrderSelected += new ucTableItem.TableItemHandler(ti_NewOrderSelected); //yeni adisyon açmak için
                        ti.EditOrderSelected += new ucTableItem.TableItemHandler(ti_EditOrderSelected); //siparişe yeni kalemler eklemek için-- hesaba yeni sipariş eklemek için
                        ti.CheckoutSelected += new ucTableItem.TableItemHandler(ti_CheckoutSelected); //hesap kesmek için
                        ti.ViewOrderSelected += new ucTableItem.TableItemHandler(ti_ViewOrderSelected); //sipariş listesini görmek için
                        ti.ChangeTableSelected += new ucTableItem.TableItemHandler(ti_ChangeTableSelected); //masa değiştirebilmek için
                        ti.CheckCanceledSelected += new ucTableItem.TableItemHandler(ti_CheckCanceledSelected);
                        ti.PrintAdisyonSelected += new ucTableItem.TableItemHandler(ti_PrintAdisyonSelected); //adisyon yazdırmak için
                        if (itemCountPerLine % StaticObjects.Settings.table_count_per_line == 0)
                        {
                            y += StaticObjects.Settings.table_height+20;
                            x = 20;
                        }
                        else x += StaticObjects.Settings.table_width+20;

                        ti.Location = new System.Drawing.Point(x, y);
                        ct.Controls.Add(ti);
                        itemCountPerLine++;
                     
                        if(ti.GetStatus()==Enums.TableStatus.Dolu)
                        this.tableCounter++;
                        this.allTableChecksTotal += ti.GetOrderPrice();
                    }
                }

            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, LoadCategoryTabıtems() hata hk ";
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

        void ti_ChangeTableSelected(object sender, EventArgs e)
        {
                SetStatusForTableChanging(); //hesap ektarılacak masanın rengini değiştir
        }

        void ti_CheckCanceledSelected(object sender, EventArgs e)
        {
            ErrorMessages.Message msg = new ErrorMessages.Message();

            if (msg.WriteMessage("Hesabı iptal etme işlemini onaylıyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                selectedTableItem = sender as ucTableItem;
                int account_id = selectedTableItem.GetAccountId();
                if (account_id==0) //hesap açılmış fakat hiç sipariş verilmemiş
                {
                    account_id = selectedTableItem.GetAccountIdWithNoOrder();
                }
                int table_id = selectedTableItem.GetTable().id;
                DeleteAccount(account_id, table_id);
            }
             
        }


        private void DeleteAccount(int account_id, int table_id)
        {
            string sql = "update account_details  set account_status=7 where account_id=" + account_id; //status =KAPANDI
            sql += " ; update table_details set table_status=3 where is_deleted=0 and table_id=" + table_id;
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);

            try
            {
                cmd.ExecuteNonQuery(sql);
                this.Refresh();
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, DeleteAccount() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                cmd.Close();
                cmd = null;
            }
        }

        private void CheckLastOrderTime(DateTime d1)
        {
            TimeSpan ts;
            if (this.allOrdersLastModifiedTime != null) //ilk sayfa load edildiğinde null geliyor
            {
                ts = d1.Subtract(this.allOrdersLastModifiedTime);
                if (ts.TotalSeconds>0) 
                {//siparişlerin son değiştirilme tarihi parametre ile gelen saat olacak
                    this.allOrdersLastModifiedTime = d1;
                }
            }
            else
                this.allOrdersLastModifiedTime = d1;
        }
        void ti_PrintAdisyonSelected(object sender, EventArgs e)
        {
            selectedTableItem = sender as ucTableItem;
            PrintCheckSlip();
        }

        void ti_TableItemClicked(object sender, EventArgs e)
        {
           selectedTableItem= sender as ucTableItem;
           if (isTableChangeSelected)
           { //masa değiştir
               if (selectedTableItem.GetTable().status.name=="DOLU")
               {
                   ErrorMessages.Message msg = new ErrorMessages.Message();
                   msg.WriteMessage("Hesabı, sadece boş bir masaya aktarabilirsiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                   isTableChangeSelected = false;
                   SetBorderColors(changingTable.GetAccountID());
                   return;
               }
               ChangeTable();
               isTableChangeSelected = false;
               return;
           }
           SetBorderColors();
           Enums.TableStatus status = selectedTableItem.GetStatus();
           this.lbl_table.Text = "Masa: " +selectedTableItem.GetTable().category.Name+" #" + selectedTableItem.GetTable().name;
           if (status == Enums.TableStatus.Uygun || status == Enums.TableStatus.Rezerve)
           {
               btn_print.Enabled = false;
               btn_order_list.Enabled = false;
               btn_change_table.Enabled = false;
               btn_cancel.Enabled = false;
               btn_new_order.Enabled = true;
               btn_checkout.Enabled = false;
               btn_add_order.Enabled = false;
           }
           else if (status == Enums.TableStatus.Dolu)
           {
               if (selectedTableItem.GetOrderPrice() == 0) //hesap açılmıs sipariş bekleniyor durumu
               {
                   btn_print.Enabled = false;
                   btn_order_list.Enabled = false;
                   btn_cancel.Enabled = true;
                   btn_new_order.Enabled = false;
                   btn_checkout.Enabled = false;
                   btn_change_table.Enabled = true;
                   btn_add_order.Enabled = true;
               }
               else
               {
                   btn_print.Enabled = true;
                   btn_order_list.Enabled = true;
                   btn_change_table.Enabled = true;
                   btn_cancel.Enabled = true;
                   btn_new_order.Enabled = false;
                   btn_change_table.Enabled = true;
                   btn_add_order.Enabled = true;
                   btn_checkout.Enabled = true;
               }
           }
        }

      
        void ti_ViewOrderSelected(object sender, EventArgs e)
        {
            ucTableItem ti =sender as ucTableItem;
            ViewOrderList(ti.GetAccountId()); //? hangi siparişler-hepsi gösterilmeli-
        }

        void ti_CheckoutSelected(object sender, EventArgs e)
        {
            ti_TableItemDoubleClicked(sender, e);
        }

        void ti_EditOrderSelected(object sender, EventArgs e)
        {
            ucTableItem ti = sender as ucTableItem;
            AddNewOrder(ti.GetTable().id, ti.GetTable().name, Enums.OrderType.Restoran);
         //   EditOrder(ref ti);
        }

        void ti_NewOrderSelected(object sender, EventArgs e)
        {
            ti_TableItemDoubleClicked(sender, e);
        }

        void ti_TableItemDoubleClicked(object sender, EventArgs e)
        {
            selectedTableItem = sender as ucTableItem;
            Enums.TableStatus s = selectedTableItem.GetStatus();
            switch (s)
            {
                case Enums.TableStatus.Acik:
                    break;
                case Enums.TableStatus.Kapali:
                    break;
                case Enums.TableStatus.Uygun:
                    TakeNewOrder( selectedTableItem.GetTable());
                    break;
                case Enums.TableStatus.Dolu:
                    Checkout();
                    break;
                case Enums.TableStatus.Rezerve:
                    break;
                case Enums.TableStatus.Alindi:
                    break;
                case Enums.TableStatus.Kapandi:
                    break;
                default:
                    break;
            }
        }
        private void ucTablesMainPage_Load(object sender, EventArgs e)
        {
            this.LoadCategoryTabs();
        }

        private void SetBorderColors()
        {
            foreach (DevExpress.XtraTab.XtraTabPage page in tabControl.TabPages) 
            {
                foreach (Control control in page.Controls)
                {
                    if (control.Name=="ucTableItem")
                    {
                        ((ucTableItem)control).GetBackPanel().BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;

                        if (control as ucTableItem==selectedTableItem)
                        {
                            ((ucTableItem)control).GetBackPanel().Appearance.BorderColor = System.Drawing.Color.Red;
                        }
                        else
                            ((ucTableItem)control).GetBackPanel().Appearance.BorderColor = System.Drawing.Color.CornflowerBlue;
                    }
                }
            }
        }

        private void SetBorderColors(int account_id)
        {
            foreach (DevExpress.XtraTab.XtraTabPage page in tabControl.TabPages)
            {
                foreach (Control control in page.Controls)
                {
                    if (control.Name == "ucTableItem")
                    {
                        int acc_id = (control as ucTableItem).GetAccountId();
                        if (acc_id==0)
                        {
                            acc_id = (control as ucTableItem).GetAccountIdWithNoOrder();
                        }
                        if (acc_id == account_id)
                        {
                            ((ucTableItem)control).GetBackPanel().BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                            ((ucTableItem)control).GetBackPanel().Appearance.BorderColor = System.Drawing.Color.CornflowerBlue;
                            selectedTableItem.GetBackPanel().Appearance.BorderColor = System.Drawing.Color.Red;
                        }
                        else continue;
                    }
                }
            }
        }

        private void TakeNewOrder(Table table)
        {
            Menu.ucMenu ctrl = new Menu.ucMenu(Enums.OrderType.Restoran);
            ctrl.table = table;
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }

        private void EditOrder(ref ucTableItem ti)
        {
            int order_id = ti.GetOrderId(); //hangi siparişi düzenleyecek?
            int account_id = ti.GetAccountId();
            string sql = "select * from v_product_to_order where is_deleted=0 and account_status=1 and order_id=" + order_id;
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt=new DataTable();
            try
            {
                dt = cmd.GetDataTable(sql);
                SiparisHandler sh = new SiparisHandler(ref dt);
                Sales.Orders order = new Sales.Orders(sh.GetSiparisList(), ti.GetTable().id, order_id,account_id);
                order.owner_name = ti.GetTable().name ;
                order.type = Enums.OrderType.Restoran;
                Menu.ucMenu ctrl = new Menu.ucMenu(order);
                ctrl.table = ti.GetTable();
                ctrl.Dock = DockStyle.Fill;
                ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
                this.Dispose();
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, EditOrder() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                dt.Dispose();
                cmd.Close();
                cmd = null;
            }
        }

    
        private void AddNewOrder(int owner_id, string owner_name, Enums.OrderType edit_type)
        {
            try
            {
                Table t = selectedTableItem.GetTable();
                Menu.ucMenu ctrl = new Menu.ucMenu(t);
                ctrl.Dock = DockStyle.Fill;

                ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
                this.Dispose();
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, EditOrder() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }

        }

        private void ViewOrderList(int account_id)
        {
            frmOrders frm = new frmOrders(account_id);
            frm.ShowDialog(this);
        }

        private void Checkout()
        {
            int account_id = selectedTableItem.GetAccountId();
            int owner_id = selectedTableItem.GetTable().id;
            string owner_name = selectedTableItem.GetTable().name;
            ErrorMessages.Message message = new ErrorMessages.Message();

            string sql = "select * from v_product_to_order where is_deleted=0 and account_status=1 and  account_id=" + account_id;
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt;
            try
            {
                dt = cmd.GetDataTable(sql);
                if (dt.Rows.Count<=0)
                {
                  if( message.WriteMessage("Henüz sipariş eklenmemiş.", MessageBoxIcon.Error, MessageBoxButtons.OK)==DialogResult.OK)
                     return;
                }
                Sales.SiparisKalem sk;
                List<Sales.SiparisKalem> sList = new List<Sales.SiparisKalem>();
                foreach (DataRow row in dt.Rows)
                {
                    sk = new Sales.SiparisKalem();
                    sk.Id = Convert.ToInt32(row["log_id"]);
                    sk.ProductId = Convert.ToInt32(row["product_id"]);
                    sk.ProductName = row["product_name"].ToString();
                    sk.Amount = Convert.ToInt32(row["amount"]);
                    sk.Porsion = Convert.ToDouble(row["porsion"]);
                    sk.SalePrice = sk.UndiscountedPrice = Convert.ToDouble(row["product_price"]);
                    sk.Desc = row["product_desc"].ToString();
                    sList.Add(sk);
                }
                SelectedTabPageIndex = tabControl.SelectedTabPageIndex;
                int order_id = Convert.ToInt32(dt.Rows[0]["order_id"]);
                Sales.Orders order = new Sales.Orders(sList, owner_id, order_id,account_id);
                order.owner_name = owner_name;
                order.type = Enums.OrderType.Restoran;
                Sales.frmSatisPopup frm = new Sales.frmSatisPopup(ref order);
                frm.PurchaseCompleted += new frmSatisPopup.PurchaseHandler(frm_PurchaseCompleted);
                frm.ShowDialog(this);
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, EditOrder() hata hk ";
                excMail.Send();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                cmd.Close();
                cmd = null;
            }
       
        }

        private void PrintCheckSlip()
        { 
            int account_id = selectedTableItem.GetAccountId();
            Table t=selectedTableItem.GetTable();

            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt1 = new DataTable();  //orders
            DataTable dt2 = new DataTable(); //order products
            string sql = "select *from v_orders_to_accounts where is_order_deleted=0 and account_id=" + account_id;
            try
            {
                dt1 = cmd.GetDataTable(sql);
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, EditOrder() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                cmd.Close();
                cmd = null;
            }
            isPrinterReady = true;
            try
            {
                cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
                List<Sales.Orders> ordersList = new List<Sales.Orders>();
                SiparisHandler sh;
                Sales.Orders order;
                int order_id = 0;
                foreach (DataRow row in dt1.Rows)
                {
                    order_id = Convert.ToInt32(row["order_id"]);
                    sql = "select * from v_product_to_order where is_deleted=0 and order_id=" + order_id;
                    dt2 = cmd.GetDataTable(sql);
                    sh = new SiparisHandler(ref dt2);
                    order = new Sales.Orders(sh.GetSiparisList(), t.id, order_id, account_id);
                    order.type = Enums.OrderType.Restoran;
                    order.owner_name = t.name;
             //       order.staff_name = dt2.Rows[0]["order_staff_name"].ToString();
                    order.staff_name = "none";
                    ordersList.Add(order);
                }
                Adisyon a = new Adisyon(ordersList);
                a.SetTable(t);
                a.ConnectionFailed += new Adisyon.ConnectionHandler(a_ConnectionFailed);
                a.SetPrinter();
                a.waiter_name = StaticObjects.User.Name;
                if (isPrinterReady)
                    a.PrintCheckSlip();
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, EditOrder() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                cmd.Close();
                cmd = null;
                dt1.Dispose();
                dt2.Dispose();
            }

        
        }

        bool isPrinterReady;
        void a_ConnectionFailed(object sender, EventArgs e)
        { //yazıcıya baglanılamadı
            isPrinterReady = false;
            Printer p = sender as Printer;
            ErrorMessages.Message message = new ErrorMessages.Message();
            message.WriteMessage(p.name + " yazıcısına erişilemiyor.\nLütfen yazıcınızın bağlantı ayarlarını kontrol ediniz", MessageBoxIcon.Warning, MessageBoxButtons.OK);
        }

        void frm_PurchaseCompleted(object sender, EventArgs e)
        {
           this.Refresh();
        }

        private void ShowTableSelectionWarning()
        {
            ErrorMessages.Message message = new ErrorMessages.Message();
            message.WriteMessage("İşlem yapmak istediğiniz masayı seçiniz.", MessageBoxIcon.Error, MessageBoxButtons.OK);
        }


        private void btn_refresh_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }

        public override void Refresh()
        {
            this.tableCounter = 0;
            this.allTableChecksTotal = 0;
            foreach (DevExpress.XtraTab.XtraTabPage tp in tabControl.TabPages)
            {
                CategoryTab ct = tp as CategoryTab;
                ct.Controls.Clear();
                this.LoadCategoryTabItems(ref ct);
            }
            SetTableCounter();
            SetAllTableChecksTotal();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (!(null == this.selectedTableItem))
            {
                ErrorMessages.Message msg = new ErrorMessages.Message();

                if (msg.WriteMessage("Hesabı iptal etme işlemini onaylıyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    int account_id = selectedTableItem.GetAccountId();
                    if (account_id == 0) //hesap açılmış fakat hiç sipariş verilmemiş
                    {
                        account_id = selectedTableItem.GetAccountIdWithNoOrder();
                    }
                    int table_id = selectedTableItem.GetTable().id;
                    DeleteAccount(account_id, table_id);
                }
                else
                {
                    return;
                }
            }
            else
                ShowTableSelectionWarning();
        }

        private void btn_add_menu_item_Click(object sender, EventArgs e)
        {
            if (!(null == this.selectedTableItem))
                EditOrder(ref this.selectedTableItem);
            else
                ShowTableSelectionWarning();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            if (!(null == this.selectedTableItem))
            {
                PrintCheckSlip();
            }
            else
                ShowTableSelectionWarning();
        }

        private void btn_order_list_Click(object sender, EventArgs e)
        {
            if (!(null == this.selectedTableItem))
                ViewOrderList(this.selectedTableItem.GetAccountId()); //tüm sipariş listesini görmeli
            else
                ShowTableSelectionWarning();
        }

        private void btn_change_table_Click(object sender, EventArgs e)
        {
            if (!(null == this.selectedTableItem))
                SetStatusForTableChanging(); //hesap ektarılacak masanın rengini değiştir
            else
                ShowTableSelectionWarning();
        }

        private void SetStatusForTableChanging()
        {
            isTableChangeSelected = true;
            changingTable = (selectedTableItem.GetAccountId() == 0) ? new TableWithAccount(selectedTableItem.GetTable(), selectedTableItem.GetAccountIdWithNoOrder()) : new TableWithAccount(selectedTableItem.GetTable(), selectedTableItem.GetAccountId());
            selectedTableItem.GetBackPanel().BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            selectedTableItem.GetBackPanel().Appearance.BorderColor = System.Drawing.Color.LawnGreen;
        }
        private void ChangeTable()
        {
            ErrorMessages.Message msg = new ErrorMessages.Message();

            if (msg.WriteMessage("'Masa: " + changingTable.GetTable().category.Name + " #" + changingTable.GetTable().name + "', '" + selectedTableItem.GetTable().category.Name + " #" + selectedTableItem.GetTable().name + "' masasına aktarılacak, onaylıyor musunuz?", MessageBoxIcon.Information, MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                int new_table_id = this.selectedTableItem.GetTable().id;
                int ex_table_id = changingTable.GetTable().id;
                int account_id = changingTable.GetAccountID();

                #region run procedure changeTable
                DBObjects.MySqlCmd cmd = new MySqlCmd(StaticObjects.MySqlConn);
                string proc_name = "changeTable";
                try
                {
                    cmd.CreateSetParameter("acc_id", MySql.Data.MySqlClient.MySqlDbType.Int32, account_id);
                    cmd.CreateSetParameter("ex_table_id", MySql.Data.MySqlClient.MySqlDbType.Int32, ex_table_id);
                    cmd.CreateSetParameter("new_table_id", MySql.Data.MySqlClient.MySqlDbType.Int32, new_table_id);
                    cmd.ExecuteNonQuerySP(proc_name);
                }
                catch (Exception e)
                {
                    ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                    excMail.Subject = "Stok Programı, ChangeTable() hata hk ";
                    excMail.Send();
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
                    //retValue = 0;
                }
                finally
                {
                    cmd.Close();
                    cmd = null;
                }

                #endregion
                this.Refresh();
            }
            else
            {
                SetBorderColors(changingTable.GetAccountID());
                return; 
            }
         
        }

        private void btn_checkout_Click(object sender, EventArgs e)
        {
            if (!(null == this.selectedTableItem))
            {
                Checkout();
            }
            else
                ShowTableSelectionWarning();
        }

        private void btn_new_order_Click(object sender, EventArgs e)
        {
            if (!(null == this.selectedTableItem))
                TakeNewOrder(this.selectedTableItem.GetTable());
            else
                ShowTableSelectionWarning();
        }

        private void btn_add_order_Click(object sender, EventArgs e)
        {
            if (!(null == this.selectedTableItem))
                AddNewOrder(this.selectedTableItem.GetTable().id, this.selectedTableItem.GetTable().name, Enums.OrderType.Restoran);
            else
                ShowTableSelectionWarning();
        }

    }
}
