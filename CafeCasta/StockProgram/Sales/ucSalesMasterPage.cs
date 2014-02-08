using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using StockProgram.DBObjects;
using System.Reflection;

namespace StockProgram.Sales
{
    public partial class ucSalesMasterPage : DevExpress.XtraEditors.XtraUserControl
    {
        DBObjects.ExceptionLogger excLogger;
        List<CategoryItem> CItemList;
        private DataTable StocksDt; //main grid view
        private GridView detailsView;// details grid view
        private frmProductView ProductView; 
        private List<SiparisKalem> siparisList;
        private bool isPreviewFormOpen;
        public ucSalesMasterPage()
        {
            siparisList = new List<SiparisKalem>();
            //adisyonList = new List<ucAdisyon>();
            InitializeComponent();
            FillCategory();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            ((MainForm)this.ParentForm).SettingStatus();
            this.Dispose();
        }

        private void ucSalesMasterPage_Load(object sender, EventArgs e)
        {
            gridView1.OptionsView.ShowGroupPanel = false;
            ((MainForm)this.ParentForm).KeyPreview = true;// ekran üzerinden basılan her hangi bir tuşu algılaması ve keyPressed eventi içine girmesi için
            ((MainForm)this.ParentForm).KeyUp += new KeyEventHandler(ucSalesMasterPage_KeyUp);
            FillGrid();
            txt_barkod.Focus();
         //   isPreviewFormOpen = false;
        }

        bool isFindPanelDisplayed = false;
        void ucSalesMasterPage_KeyUp(object sender, KeyEventArgs e)
        {     
            if (e.KeyCode == Keys.F1)  //ürün izleme ekranına getirelecek
            {       
                    DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    int product_id = Convert.ToInt32(dr["product_id"]);
                    ProductView = new frmProductView(product_id);
                    ProductView.ShowDialog(this);
                 //   this.isPreviewFormOpen = true;                            
            }
            if (e.KeyCode == Keys.F5)  //ürün izleme ekranına getirelecek
            {
                txt_barkod.Focus();
                txt_barkod.SelectAll();
            }
            if (e.KeyCode == Keys.F2)  //ürün izleme ekranına getirelecek
            {
                if (!isFindPanelDisplayed)
                {
                    gridView1.ShowFindPanel();
                    isFindPanelDisplayed = true;
                }
                else
                {
                    gridView1.HideFindPanel();
                    isFindPanelDisplayed = false;
                }
            }
        }

        /// <summary>
        /// Form üzerindeki Key up eventleri kaldırır.
        /// </summary>
        private void RemoveKeyUpEvent()
        {
         //    var handler = (EventHandler)GetDelegate(this.ParentForm, "KeyUp");
            try
            {
                ((MainForm)this.ParentForm).KeyUp -= new KeyEventHandler(ucSalesMasterPage_KeyUp);
            }
            catch (NullReferenceException)
            {
              //if remove event fails do nothing.
            }
               
        }
        private int FillCategory()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            int retValue = 0;
         //   controlHelper = new ControlHelper();
            try
            {
                string strSQL = "select cat_id,parent_id,cat_name from category_details order by display_order ,cat_name asc";
                dt = db.GetDataTable(strSQL);
                InitializeCategoryItems(ref dt);
               // FillCategoryTree(ref dt);
                dt.Dispose();
                retValue = 1;
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
            return retValue;
        }

        private static object GetDelegate(Component issuer, string keyName)
        {
            // Get key value for a Click Event
            var key = issuer
                .GetType()
                .GetField(keyName, BindingFlags.Static |
                BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)
                .GetValue(null);
            // Get events value to get access to subscribed delegates list
            var events = typeof(Component)
                .GetField("events", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(issuer);
            // Find the Find method and use it to search up listEntry for corresponding key
            var listEntry = typeof(EventHandlerList)
                .GetMethod("Find", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(events, new object[] { key });
            // Get handler value from listEntry 
            var handler = listEntry
                .GetType()
                .GetField("handler", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(listEntry);
            return handler;
        }
        private void FillGrid()
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataSet ds = new DataSet();

       //     StocksDt = db.GetDataTable("select * from v_sale where product_isDeleted=0 and product_count>0 order by product_size asc");

            StocksDt = db.GetDataTable("select * from v_sale_master where product_isDeleted=0 and total>0"); 
            StockProgram.Categories.CategoryFamilyTree cft = new StockProgram.Categories.CategoryFamilyTree(ref this.CItemList);

            StocksDt.Columns.Add("top_cat_name", typeof(String));// ana kategori adı için
            for (int i = 0; i < StocksDt.Rows.Count; i++) // fill  top category names
            {
                StocksDt.Rows[i]["top_cat_name"] = cft.GetTopCategoryItem(Convert.ToInt32(StocksDt.Rows[i]["product_cat"])).Name;
            }

            StocksDt.Columns.Add("Image", typeof(Image));

            DataTable detail = db.GetDataTable("select * from v_sale where product_isDeleted=0 and product_count>0 order by product_size,color_name asc");

            ds.Tables.Add(StocksDt);
            ds.Tables.Add(detail);
           // set relations
            DataColumn keyColumn = ds.Tables[0].Columns["product_id"];
            DataColumn foreignKeyColumn = ds.Tables[1].Columns["product_id"];
            ds.Relations.Add("PurchaseDetails", keyColumn, foreignKeyColumn);

            Image myImage;
            try
            {
                string file_name;
                for (int i = 0; i < StocksDt.Rows.Count; i++)
                {
                    file_name = Application.StartupPath + StaticObjects.MainImagePath + StocksDt.Rows[i]["product_img_path"].ToString();
                    if (File.Exists(file_name))
                    {
                        myImage = Image.FromFile(file_name);
                        myImage = StaticObjects.ResizeImage(myImage, 150, 70);
                        StocksDt.Rows[i]["Image"] = myImage;
                    }
                    else continue;
                }
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new DBObjects.ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at

                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
         
            //DevExpress.XtraGrid.Columns.GridColumn clm=new DevExpress.XtraGrid.Columns.GridColumn();
            //clm.FieldName = "Image";
            //gridView1.Columns.Add();

            gridControl1.DataSource = StocksDt;
            SetConditionalFormatting("[product_count]");
            gridControl1.ForceInitialize();

            detailsView = new GridView(gridControl1);
            gridControl1.LevelTree.Nodes.Add("PurchaseDetails", detailsView);

            //Specify text to be displayed within detail tabs.
            SetDetailsView("Ürün Detayları", ref ds);
            //gridView1.ShowFindPanel();
            gridView1.Columns["Image"].ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            gridView1.RowHeight = 70;
         //   mainView = gridView1;
        //    gridControl1.FocusedView = gridView1;

            detail.Dispose();
            ds.Dispose();
            db.Close();
            db = null;
        }

        #region grid_double_cklick
        ///// <summary>
        ///// seçilen ürünü sipariş listesine ve ekler
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void gridView1_DoubleClick(object sender, EventArgs e)
        //{
        //    string product_code = string.Empty;
        //    int rowHandle = gridView1.FocusedRowHandle;
        //    DataRow dr;
        //    dr = gridView1.GetFocusedDataRow();
        //    // dr = StocksDt.Rows[rowHandle];
        //    product_code = (dr["product_code"]).ToString();
        //    if (siparisList.Count > 0 && IsOrderInList(product_code))// sipariş listesi dolu ise ve yeni verilen sipariş liste içinde ise
        //    {
        //        return;
        //    }
        //    Sales.SiparisKalem sk = new SiparisKalem();
        //    sk.Amount = 1;
        //    sk.ProductId = Convert.ToInt32(dr["product_id"]);
        //    sk.TotalAmount = Convert.ToInt32(dr["product_count"]);
        //    if (sk.TotalAmount <= 0)
        //    {
        //        return;
        //    }
        //    sk.ColorId = Convert.ToInt32(dr["product_color"]);
        //    sk.ProductCode = dr["product_code"].ToString();
        //    sk.Color = dr["color_name"].ToString();
        //    sk.Size = Convert.ToInt32(dr["product_size"]);
        //    sk.SalePrice = Convert.ToDouble(dr["product_price"]);
        //    sk.ProductName = dr["product_name"].ToString();
        //    Sales.ucAdisyon ctrl = new ucAdisyon(sk);
        //    ctrl.SiparisTutarChanged += new ucAdisyon.SiparisHandler(adisyon_SiparisTutarChanged);
        //    ctrl.Dock = DockStyle.Top;
        //    ctrl.BringToFront();
        //    siparisList.Add(sk); // sipariş listemize siparişimizi ekliyoruz ki ileride + - tuşları ile aynı siparişe ekleme cıkarma yapabilelim.
        //    SiparisToplamYazdir(); //sipariş toplamını en üstteki labale yazdır
        //    pnl_content.Controls.Add(ctrl);
        //}
#endregion
   

        /// <summary>
        ///  ilgili label e sipariş toplamını yazdırır
        /// </summary>
        private void SiparisToplamYazdir()
        {
            lbl_siparis_toplam.Text = SiparisToplamHesapla().ToString() + " TL";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (pnl_content.Controls.Count<=0)
            {
                return;
            }
            using (frmSatisPopup satis = new frmSatisPopup(ref siparisList))
            {
                satis.PurchaseCompleted += new frmSatisPopup.PurchaseHandler(satis_PurchaseCompleted);
                satis.ShowDialog(this);
            }
            FillGrid(); //refresh grid
            txt_barkod.Focus();
            txt_barkod.SelectAll();
        }

        void satis_PurchaseCompleted(object sender, EventArgs e)
        { //clean all items
            this.siparisList.Clear();
            this.pnl_content.Controls.Clear();
            this.lbl_siparis_toplam.Text = "0,0 TL";
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


        /// <summary>
        /// verilen yeni siparişin, sipariş listesinin içerisinde olup olmadıgını kontrol eder 
        /// </summary>
        /// <returns></returns>
        private bool IsOrderInList(string product_code)
        {
            bool retValue = false;
            foreach (SiparisKalem  sk in siparisList)
            {
                if (sk.ProductCode==product_code)
                {
                    sk.Amount++;
                    sk.RefreshAmount();
                    retValue = true;
                    break;
                }
            }

            return retValue;
        }

        private void InitializeCategoryItems(ref DataTable dt)
        {
            CItemList = new List<CategoryItem>();
            // CItemList.Add(new CategoryItem { ParentId = 0, Id = 0, Name = "En Üst Kategori" });
            CategoryItem CItem;
            foreach (DataRow row in dt.Rows)
            {
                CItem = new CategoryItem();
                CItem.Id = Convert.ToInt32(row["cat_id"].ToString());
                CItem.ParentId = Convert.ToInt32(row["parent_id"].ToString());
                CItem.Name = row["cat_name"].ToString();
                CItemList.Add(CItem);
            }
        }

        /// <summary>
        /// details gridinin kolonlarını ve özelliklerini set eder
        /// </summary>
        /// <param name="viewHeader"></param>
        /// <param name="ds"></param>
        private void SetDetailsView(string viewHeader, ref DataSet ds)
        {
            //add double click event
            detailsView.DoubleClick += new EventHandler(detailsView_DoubleClick);
            detailsView.ViewCaption = viewHeader;
            detailsView.PopulateColumns(ds.Tables[1]);
            detailsView.RowHeight = 35;

            detailsView.Columns["product_color"].VisibleIndex = -1;
            detailsView.Columns["product_desc"].VisibleIndex = -1;
            detailsView.Columns["product_cat"].VisibleIndex = -1;
            detailsView.Columns["buy_price"].VisibleIndex = -1;
            detailsView.Columns["product_price"].VisibleIndex = -1;
            detailsView.Columns["product_code_manual"].VisibleIndex = -1;
            detailsView.Columns["product_code"].VisibleIndex = -1;
            detailsView.Columns["product_id"].VisibleIndex = -1;
            detailsView.Columns["product_name"].VisibleIndex = -1;
            detailsView.Columns["barcode"].VisibleIndex = -1;
            detailsView.Columns["product_img_path"].VisibleIndex = -1;
            detailsView.Columns["product_isDeleted"].VisibleIndex = -1;


            //product_code_manual column
            detailsView.Columns["product_code_manual"].Caption = "Ürün Kodu";
            detailsView.Columns["product_code_manual"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_code_manual"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["product_code_manual"].VisibleIndex = 0;

            //color_name column
            detailsView.Columns["color_name"].Caption = "Renk";
            detailsView.Columns["color_name"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["color_name"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["color_name"].VisibleIndex = 1;

            //product_size column
            detailsView.Columns["product_size"].Caption = "Numara";
            detailsView.Columns["product_size"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_size"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["product_size"].VisibleIndex = 2;

            //product_amount column
            detailsView.Columns["product_count"].Caption = "Miktar";
            detailsView.Columns["product_count"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_count"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["product_count"].VisibleIndex = 3;

            //set conditonal formmatting
            StyleFormatCondition condition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            condition1.Appearance.ForeColor = System.Drawing.Color.Red;
            condition1.Appearance.Options.UseForeColor = true;
            condition1.Condition = FormatConditionEnum.Expression;
            condition1.Expression = "[product_count]<=1";
            detailsView.FormatConditions.Add(condition1);

            detailsView.BestFitColumns();
            detailsView.OptionsView.ShowGroupPanel = false;

        }


        private DataRow GetStockData(string barcode,ref int controller)
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataRow row;
            DataTable dt = new DataTable("v_sale");
            MySqlCmd cmd = db.CreateCommand();
            try
            {
                cmd.CreateSetParameter("barcode", MySql.Data.MySqlClient.MySqlDbType.Text, barcode);
                string SQL = "select * from v_sale where product_isDeleted=0 and barcode=@barcode";
                dt = cmd.GetDataTable(SQL);
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new DBObjects.ExceptionLogger(e.Message, excSource);// DB ye log yaz
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

            if (dt.Rows.Count <= 0)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Bu ürün stoklarda mevcut değil.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                txt_barkod.Focus();
                txt_barkod.SelectAll();
                row = dt.NewRow();
                controller = 0;
            }
            else
            {
                row = dt.Rows[0];
                controller = 1;
            }
            return row;
        }
        /// <summary>
        /// Barkod okuyucu barkodu okuduktan sonra gerekli işlemleri yapar
        /// </summary>
        private void BarcodeRead()
        {
           string barcode = string.Empty;
           barcode = txt_barkod.Text.Trim();
           int controller=1;
           DataRow dr = GetStockData(barcode,ref controller);

           if (siparisList.Count > 0 && IsOrderInList(dr["product_code"].ToString()))// sipariş listesi dolu ise ve yeni verilen sipariş liste içinde ise
           {
               return;
           }
           if (controller==0) // Bu üründen stoklara hiç girilmemiş
           {
               return;
           }
           Sales.SiparisKalem sk = new SiparisKalem();
           sk.Amount = 1;
           sk.ProductId = Convert.ToInt32(dr["product_id"]);
           sk.TotalAmount = Convert.ToInt32(dr["product_count"]);
           if (sk.TotalAmount <= 0)
           {
               ErrorMessages.Message message = new ErrorMessages.Message();
               message.WriteMessage("Bu ürün stoklarda mevcut değil.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
               txt_barkod.Focus();
               txt_barkod.SelectAll();
               return;
           }
           sk.ColorId = Convert.ToInt32(dr["product_color"]);
           sk.ProductCode = dr["product_code"].ToString();
           sk.Color = dr["color_name"].ToString();
           sk.Size = Convert.ToInt32(dr["product_size"]);
           sk.SalePrice = Convert.ToDouble(dr["product_price"]);
           sk.UndiscountedPrice = Convert.ToDouble(dr["product_price"]); //İndirimsiz satış fiyatı
           sk.ProductName = dr["product_name"].ToString();
           Sales.ucAdisyon ctrl = new ucAdisyon(sk);
           ctrl.SiparisTutarChanged += new ucAdisyon.SiparisHandler(adisyon_SiparisTutarChanged);
           ctrl.SiparisCanceled += new ucAdisyon.SiparisHandler(ctrl_SiparisCanceled);
           ctrl.Dock = DockStyle.Top;
           ctrl.BringToFront();
           siparisList.Add(sk); // sipariş listemize siparişimizi ekliyoruz ki ileride + - tuşları ile aynı siparişe ekleme cıkarma yapabilelim.
           SiparisToplamYazdir(); //sipariş toplamını en üstteki labale yazdır
           pnl_content.Controls.Add(ctrl);
        }

        void detailsView_DoubleClick(object sender, EventArgs e)
        {
            string product_code = string.Empty;
            GridView details = sender as GridView;
            DataRow dr = details.GetFocusedDataRow();
            // dr = StocksDt.Rows[rowHandle];
            product_code = (dr["product_code"]).ToString();
            if (siparisList.Count > 0 && IsOrderInList(product_code))// sipariş listesi dolu ise ve yeni verilen sipariş liste içinde ise
            {
                txt_barkod.Focus();
                txt_barkod.SelectAll();
                return;
            }
            Sales.SiparisKalem sk = new SiparisKalem();
            sk.Amount = 1;
            sk.ProductId = Convert.ToInt32(dr["product_id"]);
            sk.TotalAmount = Convert.ToInt32(dr["product_count"]);
            if (sk.TotalAmount <= 0)
            {
                txt_barkod.Focus();
                txt_barkod.SelectAll();
                return;
            }
            sk.ColorId = Convert.ToInt32(dr["product_color"]);
            sk.ProductCode = dr["product_code"].ToString();
            sk.Color = dr["color_name"].ToString();
            sk.BuyPrice = Convert.ToDouble(dr["buy_price"]);
            sk.Size = Convert.ToInt32(dr["product_size"]);
            sk.SalePrice = Convert.ToDouble(dr["product_price"]);
            sk.UndiscountedPrice = Convert.ToDouble(dr["product_price"]); //İndirimsiz satış fiyatı
            sk.ProductName = dr["product_name"].ToString();
            Sales.ucAdisyon ctrl = new ucAdisyon(sk);
            ctrl.SiparisTutarChanged += new ucAdisyon.SiparisHandler(adisyon_SiparisTutarChanged);
            ctrl.SiparisCanceled += new ucAdisyon.SiparisHandler(ctrl_SiparisCanceled);
            ctrl.Dock = DockStyle.Top;
            ctrl.BringToFront();
            siparisList.Add(sk); // sipariş listemize siparişimizi ekliyoruz ki ileride + - tuşları ile aynı siparişe ekleme cıkarma yapabilelim.
            SiparisToplamYazdir(); //sipariş toplamını en üstteki labale yazdır
            pnl_content.Controls.Add(ctrl);
            txt_barkod.Focus();
            txt_barkod.SelectAll();
        //    adisyonList.Add(ctrl);
        }

        void ctrl_SiparisCanceled(object sender, EventArgs e)
        {
            ucAdisyon a = sender as ucAdisyon;
          //  adisyonList.Remove(a);
            pnl_content.Controls.Remove(a);
            siparisList.Remove(a.GetSiparisKalem());
            SiparisToplamYazdir();
            txt_barkod.Focus();
            txt_barkod.SelectAll();
        }

        void adisyon_SiparisTutarChanged(object sender, EventArgs e)
        {
          //  SiparisKalem sk = sender as SiparisKalem;
            SiparisToplamYazdir();
            //txt_barkod.Focus();
            //txt_barkod.SelectAll();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView View = gridControl1.FocusedView as GridView;
            int rHandle = View.FocusedRowHandle;
          //  this.focusedRowHandle = rHandle;
            gridView1.SetMasterRowExpanded(rHandle, !gridView1.GetMasterRowExpanded(rHandle));
            txt_barkod.Focus();
           txt_barkod.SelectAll();   //burası taki için değiştirildi ve alt satırlar eklendi
        /*    string product_code = string.Empty;
            GridView details = sender as GridView;
            DataRow dr = details.GetFocusedDataRow();
            // dr = StocksDt.Rows[rowHandle];
            product_code = (dr["product_code"]).ToString();
            if (siparisList.Count > 0 && IsOrderInList(product_code))// sipariş listesi dolu ise ve yeni verilen sipariş liste içinde ise
            {
                txt_barkod.Focus();
                txt_barkod.SelectAll();
                return;
            }
            Sales.SiparisKalem sk = new SiparisKalem();
            sk.Amount = 1;
            sk.ProductId = Convert.ToInt32(dr["product_id"]);
            sk.TotalAmount = Convert.ToInt32(dr["product_count"]);
            if (sk.TotalAmount <= 0)
            {
                txt_barkod.Focus();
                txt_barkod.SelectAll();
                return;
            }
            sk.ColorId = Convert.ToInt32(dr["product_color"]);
            sk.ProductCode = dr["product_code"].ToString();
            sk.Color = dr["color_name"].ToString();
            sk.Size = Convert.ToInt32(dr["product_size"]);
            sk.SalePrice = Convert.ToDouble(dr["product_price"]);
            sk.UndiscountedPrice = Convert.ToDouble(dr["product_price"]); //İndirimsiz satış fiyatı
            sk.ProductName = dr["product_name"].ToString();
            Sales.ucAdisyon ctrl = new ucAdisyon(sk);
            ctrl.SiparisTutarChanged += new ucAdisyon.SiparisHandler(adisyon_SiparisTutarChanged);
            ctrl.SiparisCanceled += new ucAdisyon.SiparisHandler(ctrl_SiparisCanceled);
            ctrl.Dock = DockStyle.Top;
            ctrl.BringToFront();
            siparisList.Add(sk); // sipariş listemize siparişimizi ekliyoruz ki ileride + - tuşları ile aynı siparişe ekleme cıkarma yapabilelim.
            SiparisToplamYazdir(); //sipariş toplamını en üstteki labale yazdır
            pnl_content.Controls.Add(ctrl);
            txt_barkod.Focus();
            txt_barkod.SelectAll();*/
        }

        /// <summary>
        /// colon toplamı 0 ise satırı kırmızı boyar
        /// </summary>
        /// <param name="columnName"></param>
        private void SetConditionalFormatting(string columnName)
        {
            StyleFormatCondition condition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            condition1.Appearance.ForeColor = System.Drawing.Color.Red;
            condition1.Appearance.Options.UseForeColor = true;
            condition1.Condition = FormatConditionEnum.Expression;
            condition1.Expression = columnName + "<=1";
            gridView1.FormatConditions.Add(condition1);
      //      condition1.Expression = "[product_count]<=0";
            //detailsView.FormatConditions.Add(condition1);
        }

        private void txt_barkod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
            //    btn_ekle_Click(sender, e);           
                txt_barkod.Focus();
                txt_barkod.SelectAll();
                this.BarcodeRead();
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            txt_barkod.Focus();
            txt_barkod.SelectAll();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            txt_barkod.Focus();
            txt_barkod.SelectAll();
        }

    }     
}
