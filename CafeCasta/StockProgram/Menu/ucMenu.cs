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
using System.Drawing.Printing;

namespace StockProgram.Menu
{
    public partial class ucMenu : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        private StockProgram.ControlHelper controlHelper;
        private List<Customer> CItemsList;
        private List<DBObjects.TableCategory> tCatList;
        public Table table;//restoran içi siparişler için table 
        private List<SiparisKalem> siparisList;
        private Orders order;
        public delegate void SupplierGridHandler(object sender, EventArgs e);
        public event SupplierGridHandler SupplierGridChanged;
        private bool editMode = false;
        private bool addNewOrderMode = false;
        private Enums.OrderType orderType;//1 restoran; 2  paket

        public ucMenu(Table t) // aynı hesaba yeni sipariş ekleme işlemleri için
        {
            this.table = t;
            this.orderType = Enums.OrderType.Restoran;
            InitializeComponent();

            addNewOrderMode = true;
        }

        public ucMenu(Enums.OrderType orderType) // yeni sipariş işlemleri için
        {
            this.orderType = orderType;
            InitializeComponent();
     
        }
        public ucMenu(Orders order) //edit işlemleri için
        {
            this.editMode=true;
            this.order = order;
            this.orderType = this.order.type;
            InitializeComponent();
       //     FillTableCategories();
          
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
            string strSQL = "select tcategory_id, tcategory_name from table_categories where is_deleted=0  order by tcategory_name asc";
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

        private void FillCustomers()
        {
            controlHelper = new ControlHelper();
            cbo_customer.Properties.Items.Clear();
            //fill customers
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select * from customer_details where is_deleted=0 order by display_order, customer_name asc";
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
                CItemsList = controlHelper.GetCustomers(ref dt);
                controlHelper.FillControl(cbo_customer, Enums.RepositoryItemType.ComboBox, ref dt, "customer_name");
                cbo_customer.Text = (this.editMode)? this.order.owner_name : cbo_customer.Properties.Items[0].ToString();
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
       
        private void ucCourier_Load(object sender, EventArgs e)
        {
            LoadCategoryTabs();
            siparisList = new List<SiparisKalem>();
            //SetEnterPressedEventEvent();
            switch (orderType)
            {
                case Enums.OrderType.Paket:
                    FillCustomers();
                    break;
                case Enums.OrderType.Restoran:
                    pnl_musteri.Visible = false;
                    this.lbl_header.Text = "Yeni Adisyon Aç: "+this.table.category.Name+" #"+this.table.name;
                    break;
                default:
                    break;
            }
            if (editMode)
            {
                btn_cancel.Visible = false;
                btn_order.Dock = DockStyle.Fill;
                pnl_musteri.Enabled = false;
            FillPanelContent();//sipariş listesindeki ürünleri panele at                
            this.btn_order.Text = "Sipariş Düzenle";
            lbl_header.Text = this.order.owner_name + " Sipariş Düzenleme Sip. No: #" + order.order_id;
            }
            if (addNewOrderMode)
            {             
                this.btn_order.Text = "Yeni Sipariş Ekle";//yeni adisyon ac da olabilirdi
                lbl_header.Text = "Hesaba Yeni Sipariş Ekle | Masa: " + this.table.category.Name + " #" + this.table.name;
            }
        }

        private void SetEnterPressedEventEvent()
        {
            ((MainForm)this.ParentForm).KeyPreview = true;// ekran üzerinden basılan her hangi bir tuşu algılaması ve keyPressed eventi içine girmesi için
            ((MainForm)this.ParentForm).KeyUp += new KeyEventHandler(ParentForm_KeyUp);
        }

        void ParentForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)  //ürün izleme ekranına getirelecek
            {
                btn_order_Click(sender, e);
            //    this.KeyUp -= new KeyEventHandler(ParentForm_KeyUp);
            }
        }

        private void LoadCategoryTabs()
        {
        
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select cat_id, cat_name from category_details where is_deleted=0  order by display_order, cat_name asc";
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);
                if (dt.Rows.Count == 0)
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Kategoriler sayfsından en az 1 kategori eklemelisiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    ucSaleOptions ctrl = new ucSaleOptions();
                    ctrl.Dock = DockStyle.Fill;
                    ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
                    this.Dispose();
                    return;
                }
                CategoryTab ct;

                foreach (DataRow row in dt.Rows)
                {
                    ct = new CategoryTab(Convert.ToInt32(row["cat_id"]), row["cat_name"].ToString(), row["cat_name"].ToString());
                    ct.Click += new EventHandler(ct_Click);
                    this.LoadCategoryTabItems(ref ct);
                    this.tabControl.TabPages.Add(ct);
                }
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
            finally
            {
                db.Close();
                db = null;
                dt.Dispose();
            }
          
        }

        void ct_Click(object sender, EventArgs e)
        {
            CategoryTab ct = sender as CategoryTab;
            ct.Focus();
        }
        private void LoadCategoryTabItems(ref CategoryTab ct)
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select* from v_products where (product_isDeleted='Evet' and isOnMenu=1 and  product_cat="+ct.cat_id+") order by product_name asc";
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);
           //     int x=10, y=-165, itemCountPerLine = 0;
                int x = 20, y = -(StaticObjects.Settings.menu_item_height + StaticObjects.Settings.menu_item_name_panel_height), itemCountPerLine = 0;
                if (dt.Rows.Count > 0)
                {
                    Product p;
                    ucMenuItem mi;
                    foreach (DataRow row in dt.Rows)
                    {
                        p = new Product();
                        p.Id=Convert.ToInt32(row["product_id"]);
                        p.ImagePath = row["product_img_path"].ToString();
                        p.Name = row["product_name"].ToString();
                        GetProductPrices(ref p);
                        mi = new ucMenuItem(p);
                        mi.MenuItemClicked += new ucMenuItem.MenuItemHandler(mi_MenuItemClicked);

                        if (itemCountPerLine % StaticObjects.Settings.menu_item_count_per_line == 0)
                        {
                            y += StaticObjects.Settings.menu_item_height+StaticObjects.Settings.menu_item_name_panel_height + 20;
                            x = 20;
                        }
                        else x += StaticObjects.Settings.menu_item_width + 20;

                        mi.Location = new System.Drawing.Point(x,y);
                        ct.Controls.Add(mi);
                        itemCountPerLine++;
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

        void mi_MenuItemClicked(object sender, EventArgs e)
        {
            ucMenuItem mi = sender as ucMenuItem;
            //if (siparisList.Count > 0 && IsOrderInList(mi.GetProduct().Id, mi.porsion))// sipariş listesi dolu ise ve yeni verilen sipariş liste içinde ise
            //{
            //    return;
            //}
            Sales.SiparisKalem sk = new SiparisKalem();
            DBObjects.Product miProduct = mi.GetProduct();
            sk.Id = -1;//db de olmayan kalemler için id=-1
            sk.Amount = 1;
            sk.TotalAmount = 999;// ürün girerken stok miktarı karşılaştırması yok
            sk.Porsion = mi.porsion;
            sk.ProductId = miProduct.Id;
            sk.SalePrice = miProduct.SelectedSalePrice;
            sk.UndiscountedPrice = miProduct.SelectedSalePrice; //İndirimsiz satış fiyatı
            sk.ProductName = miProduct.Name;
            Sales.ucAdisyon ctrl = new ucAdisyon(sk);
            ctrl.SiparisTutarChanged += new ucAdisyon.SiparisHandler(adisyon_SiparisTutarChanged);
            ctrl.SiparisCanceled += new ucAdisyon.SiparisHandler(ctrl_SiparisCanceled);
            ctrl.Dock = DockStyle.Top;
            ctrl.BringToFront();
            siparisList.Add(sk); // sipariş listemize siparişimizi ekliyoruz ki ileride + - tuşları ile aynı siparişe ekleme cıkarma yapabilelim.
            SiparisToplamYazdir(); //sipariş toplamını en üstteki labale yazdır
            pnl_content.Controls.Add(ctrl);
        }

        /// <summary>
        /// editlenmek için açılmışsa eski siparişleri sipariş listesinde (panelContent içinde) göster
        /// </summary>
        private void FillPanelContent()
        {
            foreach (SiparisKalem sk in order.siparisList)
            {
                Sales.ucAdisyon ctrl = new ucAdisyon(sk);
                ctrl.SiparisTutarChanged += new ucAdisyon.SiparisHandler(adisyon_SiparisTutarChanged);
                //ctrl.SiparisCanceled += new ucAdisyon.SiparisHandler(ctrl_SiparisCanceled);
                ctrl.Dock = DockStyle.Top;
                ctrl.BringToFront();
                ctrl.Enabled = false;
                pnl_content.Controls.Add(ctrl);
            }
            this.siparisList = this.order.siparisList;
            this.SiparisToplamYazdir();
        }
        /// <summary>
        /// ürünün 1-1.5-2 porsion için fiyatlarını set eder.
        /// </summary>
        /// <param name="p"></param>
        private void GetProductPrices(ref DBObjects.Product p)
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select product_id,product_price,porsion from price_to_product where product_id="+p.Id;
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToDouble(row["porsion"]) == 1)
                    {
                        p.SalePrice = Convert.ToDouble(row["product_price"]);
                    }
                    else if (Convert.ToDouble(row["porsion"]) == 1.5)
                    {
                        p.SalePrice_bucuk = Convert.ToDouble(row["product_price"]);
                    }
                    else if (Convert.ToDouble(row["porsion"]) == 2)
                    {
                        p.SalePrice_double = Convert.ToDouble(row["product_price"]);
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
   
        private void tabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            this.tabControl.SelectedTabPage.Focus();
        }

        /// <summary>
        /// verilen yeni siparişin, sipariş listesinin içerisinde olup olmadıgını kontrol eder 
        /// </summary>
        /// <returns></returns>
        private bool IsOrderInList(int product_id,double porsion)
        {
            bool retValue = false;
            foreach (SiparisKalem sk in siparisList)
            {
                if (sk.ProductId == product_id && sk.Porsion==porsion)
                {
                    sk.Amount++;
                    sk.RefreshAmount();
                    retValue = true;
                    break;
                }
            }

            return retValue;
        }

        /// <summary>
        ///  ilgili label e sipariş toplamını yazdırır
        /// </summary>
        private void SiparisToplamYazdir()
        {
            lbl_siparis_toplam.Text = SiparisToplamHesapla().ToString("N2") + " TL";
        }

        /// <summary>
        /// sipariş listesi içerisindeki tüm tutarların alt toplamını hesaplar
        /// </summary>
        /// <returns></returns>
        double SiparisToplamHesapla()
        {
            double siparis_toplam = 0;
            foreach (SiparisKalem siparis in siparisList)
            {
                siparis_toplam += siparis.Amount * siparis.SalePrice;
            }

            return siparis_toplam;
        }

        void ctrl_SiparisCanceled(object sender, EventArgs e)
        {
            ucAdisyon a = sender as ucAdisyon;
            //  adisyonList.Remove(a);
            pnl_content.Controls.Remove(a);
            siparisList.Remove(a.GetSiparisKalem());
            SiparisToplamYazdir();
      
        }

        void adisyon_SiparisTutarChanged(object sender, EventArgs e)
        {
            //  SiparisKalem sk = sender as SiparisKalem;
            SiparisToplamYazdir();
            //txt_barkod.Focus();
            //txt_barkod.SelectAll();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.siparisList.Clear();
            this.pnl_content.Controls.Clear();
            this.SiparisToplamYazdir();
        }

        private void cbo_customer_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_adres.Text = "";
            txt_tel.Text = "";
            txt_note.Text = "";
            if (cbo_customer.SelectedIndex>=0)
            {
                DBObjects.Customer c = CItemsList[cbo_customer.SelectedIndex];
                txt_adres.Text = c.adress;
                txt_tel.Text = c.tel;
                txt_note.Text = c.note;
            }
        }

        private void btn_desc_Click(object sender, EventArgs e)
        {
            using (frmCustomers frm = new frmCustomers())
            {
                frm.CustomerSelectionCompleted += new frmCustomers.CustomerHandler(frm_CustomerSelectionCompleted);
                frm.ShowDialog(this);
                frm.Focus();
            }
        }

        void frm_CustomerSelectionCompleted(object sender, EventArgs e)
        {
            DBObjects.Customer c = sender as DBObjects.Customer;
            txt_adres.Text = c.adress;
            txt_tel.Text = c.tel;
            txt_note.Text = c.note;
            cbo_customer.Text = c.name;
        }
        private void QuickAddCustomer(ref DBObjects.Customer c)
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string procName = "addCustomer";

            try
            {
                db.CreateSetParameter("cName", MySql.Data.MySqlClient.MySqlDbType.Text, c.name.ToUpper());
                db.CreateSetParameter("cTel", MySql.Data.MySqlClient.MySqlDbType.VarChar, c.tel);
                db.CreateSetParameter("cNote", MySql.Data.MySqlClient.MySqlDbType.Text, c.note.ToUpper());
                db.CreateSetParameter("cMail", MySql.Data.MySqlClient.MySqlDbType.VarChar, c.mail);
                db.CreateSetParameter("cAddress", MySql.Data.MySqlClient.MySqlDbType.Text, c.adress.ToUpper());
                db.CreateOuterParameter("id", MySql.Data.MySqlClient.MySqlDbType.Int32);
                db.ExecuteNonQuerySP(procName);
                c.id = Convert.ToInt32(db.GetParameterValue("id"));
                OnSupplierGridChanged(EventArgs.Empty);// fire the gridChanged event
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
            ErrorMessages.Message message;
         
                message = new ErrorMessages.Message();
                message.WriteMessage("Müşteri kaydedildi.", MessageBoxIcon.Information, MessageBoxButtons.OK);
       
        }

        private void btn_quick_customer_add_Click(object sender, EventArgs e)
        {
            DBObjects.Customer customer = new DBObjects.Customer();
            customer.name =cbo_customer.Text;
            customer.adress = txt_adres.Text;
            customer.tel = txt_tel.Text;
            customer.note = txt_note.Text;
            customer.mail = "";

            QuickAddCustomer(ref customer);
            CItemsList.Add(customer);
            cbo_customer.Properties.Items.Add(customer.name);
            cbo_customer.Text = cbo_customer.Properties.Items[cbo_customer.Properties.Items.Count-1].ToString();
        }

        int order_id;
        int owner_id;
        private void btn_order_Click(object sender, EventArgs e)
        {
            int account_id = 0; // yeni açılan hesaplar için

            if (this.siparisList.Count<=0)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Menüden en az 1 yemek seçmelisiniz", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                return;
            }
            if (this.editMode)
            {
                EditOrders();
                SubmitForm();
                return;
            }
            if (this.addNewOrderMode)
            {
                AddOrder();
                SubmitForm();
                return;
            }
            #region openNewCheck
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string procName = "openNewCheck";
            if (cbo_customer.SelectedIndex<0 && this.orderType==Enums.OrderType.Paket) // kayıtlı olmayan müşteri girişi 
            {
                this.owner_id= CItemsList[0].id;
            }
            else
            this.owner_id =(this.orderType==Enums.OrderType.Paket)? CItemsList[cbo_customer.SelectedIndex].id : this.table.id;

            try
            {
                db.CreateSetParameter("owner_id", MySql.Data.MySqlClient.MySqlDbType.Int32, owner_id);
                db.CreateSetParameter("staff_id", MySql.Data.MySqlClient.MySqlDbType.Int32, StaticObjects.User.Id);
                db.CreateSetParameter("type", MySql.Data.MySqlClient.MySqlDbType.Int16, Convert.ToInt16(this.orderType));
                db.CreateOuterParameter("a_id", MySql.Data.MySqlClient.MySqlDbType.Int32);
                db.ExecuteNonQuerySP(procName);
                account_id = Convert.ToInt32(db.GetParameterValue("a_id"));
             
                OnSupplierGridChanged(EventArgs.Empty);// fire the gridChanged event
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
            #endregion

            #region newOrder
            this.order_id = 0;
            db = new MySqlCmd(StaticObjects.MySqlConn);
            procName = "newOrder";
            //this.owner_id = (this.orderType == Enums.OrderType.Paket) ? CItemsList[cbo_customer.SelectedIndex].id : this.table.id;
            try
            {
                db.CreateSetParameter("a_id", MySql.Data.MySqlClient.MySqlDbType.Int32, account_id);
                db.CreateOuterParameter("order_id", MySql.Data.MySqlClient.MySqlDbType.Int32);
                db.CreateSetParameter("owner_id", MySql.Data.MySqlClient.MySqlDbType.Int32, owner_id);
                db.CreateSetParameter("staff_id", MySql.Data.MySqlClient.MySqlDbType.Int32, StaticObjects.User.Id);
                db.CreateSetParameter("order_type", MySql.Data.MySqlClient.MySqlDbType.Int16, Convert.ToInt16(this.orderType));
                db.CreateSetParameter("order_desc", MySql.Data.MySqlClient.MySqlDbType.Text, txt_note.Text.ToUpper());
                db.ExecuteNonQuerySP(procName);
                order_id = Convert.ToInt32(db.GetParameterValue("order_id"));
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
            #endregion
            #region addMenuItems
            db = new MySqlCmd(StaticObjects.MySqlConn);
            procName = "addMenuItem";
            try
            {
                db.CreateSetParameter("order_id", MySql.Data.MySqlClient.MySqlDbType.Int32,order_id);
                db.CreateParameter("product_id", MySql.Data.MySqlClient.MySqlDbType.Int32);
                db.CreateParameter("amount", MySql.Data.MySqlClient.MySqlDbType.Int32);
                db.CreateParameter("price", MySql.Data.MySqlClient.MySqlDbType.Double);
                db.CreateParameter("porsion", MySql.Data.MySqlClient.MySqlDbType.Double);
                db.CreateParameter("product_desc", MySql.Data.MySqlClient.MySqlDbType.Text);

                foreach (SiparisKalem  menuItem in siparisList)
                {
                    db.SetParameterAt("product_id", menuItem.ProductId);
                    db.SetParameterAt("amount",menuItem.Amount);
                    db.SetParameterAt("price",menuItem.SalePrice);
                    db.SetParameterAt("porsion", menuItem.Porsion);
                    db.SetParameterAt("product_desc", menuItem.Desc);
                    db.ExecuteNonQuerySP(procName);             
                }
              
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
            #endregion
            SubmitForm();
        }
        private void EditOrders()
        {
            this.order_id = this.order.order_id ;
            this.owner_id = this.order.owner_id;
            MySqlCmd db ;
          
            #region addNewMenuItems
             db = new MySqlCmd(StaticObjects.MySqlConn);
             string procName = "addMenuItem";
            try
            {
                db.CreateSetParameter("order_id", MySql.Data.MySqlClient.MySqlDbType.Int32, this.order_id);
                db.CreateParameter("product_id", MySql.Data.MySqlClient.MySqlDbType.Int32);
                db.CreateParameter("amount", MySql.Data.MySqlClient.MySqlDbType.Int32);
                db.CreateParameter("price", MySql.Data.MySqlClient.MySqlDbType.Double);
                db.CreateParameter("porsion", MySql.Data.MySqlClient.MySqlDbType.Double);
                db.CreateParameter("product_desc", MySql.Data.MySqlClient.MySqlDbType.Text);

                foreach (SiparisKalem menuItem in siparisList)
                {
                    if (menuItem.Id!=-1)
                    {
                        continue;  //zaten var olan siparişleri tekrar ekleme
                    }
                    db.SetParameterAt("product_id", menuItem.ProductId);
                    db.SetParameterAt("amount", menuItem.Amount);
                    db.SetParameterAt("price", menuItem.SalePrice);
                    db.SetParameterAt("porsion", menuItem.Porsion);
                    db.SetParameterAt("product_desc", menuItem.Desc);
                    db.ExecuteNonQuerySP(procName);
                }

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
            #endregion
        }

        private void AddOrder()
        {
            #region newOrder
            this.order_id = 0;
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string procName = "newOrder";
            this.owner_id = (this.orderType == Enums.OrderType.Paket) ? CItemsList[cbo_customer.SelectedIndex].id : this.table.id;
            try
            {
                db.CreateSetParameter("a_id", MySql.Data.MySqlClient.MySqlDbType.Int32, 0);
                db.CreateOuterParameter("order_id", MySql.Data.MySqlClient.MySqlDbType.Int32);
                db.CreateSetParameter("owner_id", MySql.Data.MySqlClient.MySqlDbType.Int32, owner_id);
                db.CreateSetParameter("staff_id", MySql.Data.MySqlClient.MySqlDbType.Int32, StaticObjects.User.Id);
                db.CreateSetParameter("order_type", MySql.Data.MySqlClient.MySqlDbType.Int16, Convert.ToInt16(this.orderType));
                db.CreateSetParameter("order_desc", MySql.Data.MySqlClient.MySqlDbType.Text, txt_note.Text.ToUpper());
                db.ExecuteNonQuerySP(procName);
                order_id = Convert.ToInt32(db.GetParameterValue("order_id"));
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
            #endregion
            #region addNewMenuItems
            db = new MySqlCmd(StaticObjects.MySqlConn);
            procName = "addMenuItem";
            try
            {
                db.CreateSetParameter("order_id", MySql.Data.MySqlClient.MySqlDbType.Int32, this.order_id);
                db.CreateParameter("product_id", MySql.Data.MySqlClient.MySqlDbType.Int32);
                db.CreateParameter("amount", MySql.Data.MySqlClient.MySqlDbType.Int32);
                db.CreateParameter("price", MySql.Data.MySqlClient.MySqlDbType.Double);
                db.CreateParameter("porsion", MySql.Data.MySqlClient.MySqlDbType.Double);
                db.CreateParameter("product_desc", MySql.Data.MySqlClient.MySqlDbType.Text);

                foreach (SiparisKalem menuItem in siparisList)
                {
                    if (menuItem.Id != -1)
                    {
                        continue;  //zaten var olan siparişleri tekrar ekleme
                    }
                    db.SetParameterAt("product_id", menuItem.ProductId);
                    db.SetParameterAt("amount", menuItem.Amount);
                    db.SetParameterAt("price", menuItem.SalePrice);
                    db.SetParameterAt("porsion", menuItem.Porsion);
                    db.SetParameterAt("product_desc", menuItem.Desc);
                    db.ExecuteNonQuerySP(procName);
                }

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
            #endregion
        }
        private void SubmitForm()
        {
            PrintKitchenSlip();
            if (this.orderType==Enums.OrderType.Paket)
            {
                DailySales.ucOpenOrders ctrl = new DailySales.ucOpenOrders();
                ctrl.Dock = DockStyle.Fill;
                ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            }
            else if (this.orderType == Enums.OrderType.Restoran)
            {
                Tables.ucTablesMainPage ctrl = new Tables.ucTablesMainPage();
                ctrl.Dock = DockStyle.Fill;
                ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            }
            this.Dispose();
        }
        private void pnl_content_Paint(object sender, PaintEventArgs e)
        {

        }
        private void PrintKitchenSlip()
        { 
                isPrinterReady = true;
                Sales.Orders order = new Sales.Orders(this.siparisList, this.owner_id, this.order_id, 0); //account id ye adisyon için gerek yok
                order.owner_name = (this.orderType == Enums.OrderType.Restoran) ? this.table.name : "";
                order.type = this.orderType;
                order.staff_name = (this.editMode==true)? this.order.staff_name: StaticObjects.User.Name;
                Adisyon a = new Adisyon(order);
                if (order.type == Enums.OrderType.Paket)
                {
                    a.SetCustomer(GetCustomer());
                }
                else if (order.type == Enums.OrderType.Restoran)
                    a.SetTable(this.table);
                a.ConnectionFailed += new Adisyon.ConnectionHandler(a_ConnectionFailed);
                a.SetPrinter();
                a.waiter_name = StaticObjects.User.Name;
           
               if (isPrinterReady)
                a.PrintKitchenSlip();
        }

        bool isPrinterReady;
        void a_ConnectionFailed(object sender, EventArgs e)
        { //yazıcıya baglanılamadı
            isPrinterReady = false;
            Printer p = sender as Printer;
            ErrorMessages.Message message = new ErrorMessages.Message();
            message.WriteMessage(p.name+" yazıcısına erişilemiyor.\nLütfen yazıcınızın bağlantı ayarlarını kontrol ediniz", MessageBoxIcon.Warning, MessageBoxButtons.OK);
        }

        private Customer GetCustomer()
        {
            Customer c = new Customer();
            c.name = cbo_customer.Text.ToUpper().Trim();
            c.tel = txt_tel.Text.Trim();
            c.note = txt_note.Text.ToUpper().Trim();
            c.adress = txt_adres.Text.ToUpper().Trim();
            return c;
        }
    }
}
