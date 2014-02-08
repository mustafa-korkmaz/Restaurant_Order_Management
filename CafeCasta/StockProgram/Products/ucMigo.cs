using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StockProgram.DBObjects;
using System.Drawing.Printing;
using System.Diagnostics;

namespace StockProgram.Products
{
    public partial class ucMigo : DevExpress.XtraEditors.XtraUserControl
    {
        List<ProductAttributes> ProductAttributeList;
        public delegate void ProductTotalHandler(object sender,EventArgs e);
        public event ProductTotalHandler ProductTotalChanged;
        List<ucShoeSize> shoeSizeList;
        DataTable mainTable;
        private ExceptionLogger excLogger;
        private StockProgram.ControlHelper controlHelper;
       // private List<DBObjects.Warehouse> WItemsList;
        private List<DBObjects.Supplier> MItemsList;
     //   private List<DBObjects.Color> CItemsList;
        private List<Product> PItemsList;
        private Product product;
        private int goods_id { get; set; }
        private DevExpress.XtraGrid.Views.Grid.GridView detailsView;

        #region Constructors
        public ucMigo(int id)
        {
            this.goods_id = id;
            ProductAttributeList = new List<ProductAttributes>();
            controlHelper = new ControlHelper();
            InitializeComponent();
            FillProductProperties();
        }

        public ucMigo()
        {
            controlHelper = new ControlHelper();
            ProductAttributeList = new List<ProductAttributes>();
            InitializeComponent();
            goods_id = -1;
        }
        #endregion
        private void ucMIGO_Load(object sender, EventArgs e)
        {
         //   FillWarehouses();
         //   FillProducts();
            FillSuppliers();      
           // FillColors_Sizes();
            //spin_urun_adet.Properties.Increment = 5;
            //grpCtrl_numara.Focus();
            
        }
     
        #region Fill comboboxes

        private void FillProductProperties()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            try
            {
                DataTable dt = db.GetDataTable("select * from goods_details where goods_id= " + goods_id);
                //product = new Product();
                //product.Id = goods_id;
                //product.SalePrice = Convert.ToDouble(dt.Rows[0]["product_price"]);
                //product.Name = dt.Rows[0]["product_name"].ToString();
                //product.Code = dt.Rows[0]["product_code_manual"].ToString();
                //product.Desc = dt.Rows[0]["product_desc"].ToString();
                //product.ColorName = dt.Rows[0]["color_name"].ToString();
                //product.ColorId = Convert.ToInt32(dt.Rows[0]["product_color"].ToString());
                //cb_color.Text = dt.Rows[0]["color_name"].ToString();
                lbl_header.Text = "Malzeme Depo Girişi (" + dt.Rows[0]["goods_name"].ToString() + ")";
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Close();
                db = null;
            }
            //if (product.Id != -1)
            //{
            //  //  SetShoeSizeButtons(product.Id);
            //}
           
        }

        /// <summary>
        /// ekrana ayakkabı numaralarının seçilebileceği user controlleri getirir.
        /// </summary>
        private void SetShoeSizeButtons(int id)
        {
            shoeSizeList=new List<ucShoeSize>();
            DBObjects.MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            string sql = "SELECT * FROM product_size_limits  WHERE  product_id=" + id;
            int minSize = 0;
            int maxSize = 0;
            try
            {
                DataTable dt = db.GetDataTable(sql);
                if (dt.Rows.Count<=0)
                {
                    db.Close();
                    dt.Dispose();
                    return;
                }
                minSize= Convert.ToInt32(dt.Rows[0]["min_size"]);
                maxSize = Convert.ToInt32(dt.Rows[0]["max_size"]);
                ShoeSize sz = new ShoeSize();
                sz.MaxSize = maxSize;
                sz.MinSize = minSize;
                sz.ProductId = id;
                ShowSizeButtons(ref sz);
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
        private void FillWarehouses()
        {
            //StockProgram.DBObjects.CDB db = new DBObjects.CDB(StaticObjects.AccessConnStr);
            //DataTable dt = new DataTable();
            ////WItemsList = new List<DBObjects.Warehouse>();

            //string strSQL;
            //try
            //{
            //    //fill warehouses
            //    strSQL = "select WID,WName from [Warehouse] order by WName asc";
            //    dt = db.Get_DataTable(strSQL);
            //   // controlHelper.FillControl(cb_bagliDepo, Enums.RepositoryItemType.ComboBox, ref dt, "WName");
            //  //  cb_bagliDepo.Text = cb_bagliDepo.Properties.Items[0].ToString();
            //   // WItemsList = controlHelper.GetWarehouses(ref dt);
            //}
            //catch (Exception e)
            //{
            //    ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
            //    excMail.Subject = "Stok Programı, ucMigo.FillWarehouses() hata hk ";
            //    excMail.Send();
            //    ErrorMessages.Message message = new ErrorMessages.Message();
            //    message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            //    // retValue = 0;
            //}
            //finally
            //{
            //    db.CloseDB();
            //    db = null;
            //    dt.Dispose();
            //}

        }
        private void FillSuppliers()
        {
            //fill Suppliers
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select suppliers_id, suppliers_name from suppliers_details where suppliers_isDeleted=0  order by suppliers_name asc";
            MItemsList = new List<DBObjects.Supplier>();
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);
                if (dt.Rows.Count==0)
                {
                      ErrorMessages.Message message = new ErrorMessages.Message();
                       message.WriteMessage("Tedarikçiler sayfsından en az 1 tedarikçi eklemelisiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                       Parent.Controls["pnl_master"].Visible = true;
                       this.Dispose();
                       return;
                }
                controlHelper.FillControl(cb_bagliTedarikci, Enums.RepositoryItemType.ComboBox, ref dt, "suppliers_name");
                cb_bagliTedarikci.Text = "Tedarikçi Seçiniz";
                MItemsList = controlHelper.GetSuppliers(ref dt);
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
        private void FillProducts()
        {
            //fill Suppliers
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select  * from v_products order by product_name asc";
            PItemsList = new List<DBObjects.Product>();
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);
                PItemsList = controlHelper.GetProducts(ref dt);

                //controlHelper.FillControl(cb_urun, Enums.RepositoryItemType.ComboBox, ref dt, "product_name");
                //if (productId != -1)
                //{
                //    cb_urun.Text = product.Name;
                //}
                //else cb_urun.Text = cb_urun.Properties.Items[0].ToString();             
              
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, ucMigo.FillProducts() hata hk ";
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
        private void FillColors_Sizes()
        {
            //fill Suppliers
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            //string strSQL = "select * from v_product_to_color where product_id="+this.productId+"  order by color_name asc";
            //string strSQL2 = "select * from product_size_limits where product_id=" + this.productId ;
            //CItemsList = new List<DBObjects.Color>();
            DataTable dtColors = new DataTable();
            DataTable dtSizes = new DataTable();
            int min_size = 0;
            int max_size = 0;

            try
            {
                //dtColors = db.GetDataTable(strSQL);
                //dtSizes = db.GetDataTable(strSQL2);
                min_size = Convert.ToInt32(dtSizes.Rows[0]["min_size"]);
                max_size = Convert.ToInt32(dtSizes.Rows[0]["max_size"]);
                //this.mainTable = SetColorSizeTable(ref dtColors, min_size, max_size);
                //SetDetailsView("mainView");

                if (dtColors.Rows.Count == 0)
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Ayarlar-> Renkler sayfsından en az 1 renk eklemelisiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    Parent.Controls["pnl_master"].Visible = true;
                    this.Dispose();
                    return;
                }
              //  gridControl1.DataSource = this.mainTable;
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
                dtColors.Dispose();
            }
        }

        /// <summary>
        /// prepares the color/size table
        /// </summary>
        /// <param name="dtColors"></param>
        /// <param name="min_size"></param>
        /// <param name="max_size"></param>
        private DataTable  SetColorSizeTable(ref DataTable dtColors, int min_size, int max_size)
        {
            DataTable dt = new DataTable("colors_sizes");
            //önce kolonları hazırlayalım
            dt.Columns.Add("#", typeof(String));
            dt.Columns.Add("color_id", typeof(Int32));

            int index = min_size;
            while (index <= max_size)
            {
                dt.Columns.Add(index.ToString(), typeof(Int16));
                index += 2;
            }
            //şimdi rowları set edelim
            DataRow dr;
            for (int i = 0; i < dtColors.Rows.Count; i++)
            {
                dr = dt.NewRow();
                dr["#"] = dtColors.Rows[i]["color_name"].ToString();
                dr["color_id"] = dtColors.Rows[i]["color_id"].ToString();
                index = min_size;
                while (index <= max_size)
                {
                    dr[index.ToString()] = 0;
                    index += 2;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// details gridinin kolonlarını ve özelliklerini set eder
        /// </summary>
        /// <param name="viewHeader"></param>
        /// <param name="ds"></param>
        //private void SetDetailsView(string viewHeader)
        //{
        //    //add double click event
        //    detailsView = new DevExpress.XtraGrid.Views.Grid.GridView(gridControl1);
        //    detailsView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(detailsView_CellValueChanged);
        //    detailsView.ViewCaption = viewHeader;
        //    detailsView.OptionsView.ShowFooter = true;
        //    detailsView.PopulateColumns(this.mainTable);
        //    detailsView.Columns["color_id"].VisibleIndex = -1;
         
        //    //color_name column
        //   // detailsView.Columns["color_name"].Caption = "#";
        //    detailsView.Columns["#"].OptionsColumn.AllowEdit = false;
        //    detailsView.Columns["#"].OptionsColumn.ReadOnly = false;
        //    detailsView.Columns["#"].VisibleIndex = 0;

        //    for (int i = 0; i < detailsView.Columns.Count; i++)
        //    {
        //     detailsView.Columns[i].FieldName=mainTable.Columns[i].Caption;
        //    }


        //    for (int i = 2; i < this.mainTable.Columns.Count; i++)
        //    {
        //        detailsView.Columns[i].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
        //        detailsView.Columns[i].SummaryItem.DisplayFormat = "Toplam= {0}";
        //        detailsView.Columns[i].SummaryItem.FieldName = detailsView.Columns[i].FieldName;
        //    }
        //    detailsView.BestFitColumns();
        //    detailsView.OptionsView.ShowGroupPanel = false;
        //    //gridControl1.MainView = detailsView;

        //}

        void detailsView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            mainTable.AcceptChanges();
            CalculateProductCount();
        }


        private void CalculateProductCount()
        {
            int total = 0;
            for (int i = 0; i < mainTable.Rows.Count; i++)
            {
                for (int j = 2; j < mainTable.Columns.Count; j++)
                {
                    total += Convert.ToInt32(mainTable.Rows[i][j]);
                }
            }
            this.spinMiktar.Value = total;
        }
        void detailsView_DoubleClick(object sender, EventArgs e)
        {
            //GridView details = sender as GridView;
            //DataRow dr = details.GetFocusedDataRow();
            //Products.frmSellReturn ctrl = new Products.frmSellReturn(ref dr);
            //ctrl.ProductReturnGridChanged += new frmSellReturn.ProductReturnGridHandler(ctrl_ProductReturnGridChanged);
            //ctrl.ShowDialog(this);
        }
        private void ShowSizeButtons(ref ShoeSize size)
        {
           // int tab_index = cb_color.TabIndex;
            int index=size.MaxSize-size.MinSize;
            ucShoeSize ucSize;
            int x = 15;
            int y = 35;
            for (int i = 0; i <= index; i++)
            {
                ucSize = new ucShoeSize();
                ucSize.ProductAmountChanged += new ucShoeSize.ProductAmountHandler(ucSize_ProductAmountChanged);
            //    ucSize.TabIndex = ++tab_index;
                ucSize.Width = 73;
                ucSize.Height = 48;
                ucSize.Location = new System.Drawing.Point(x,y);
                ucSize.btn_numara.Text = (size.MinSize + i).ToString() ;
                //grpCtrl_numara.Controls.Add(ucSize);
                x += ucSize.Width + 5;
                ProductAttributeList.Add(ucSize.GetProductAttributeItem()); //adet ve numara bilgisini listemize ekleyelim.
            }
           //txt_toplam_adet.TabIndex = ++tab_index;
           // lbl_birim_fiyat.TabIndex = ++tab_index;
           // spin_KDV.TabIndex = ++tab_index;
           // spin_toplam.TabIndex = ++tab_index;
           // btn_malGirisi.TabIndex = ++tab_index;
        }

        void ucSize_ProductAmountChanged(object sender, EventArgs e)
        {
            int toplam = 0;
            foreach (ProductAttributes item in ProductAttributeList)
            {
                toplam+=item.Amount;
            }
          //  txt_toplam_adet.Text = toplam.ToString();
        }

        protected virtual void onProductTotalChanged(EventArgs e)
        {
            if (ProductTotalChanged != null)
                ProductTotalChanged(this, e);
        }

        #endregion

        private void btn_malGirisi_Click(object sender, EventArgs e)
        {
            ErrorMessages.Message message ;
            Product p = new Product();
             
            if (cb_bagliTedarikci.SelectedIndex==-1)
            {
                 message = new ErrorMessages.Message();
                message.WriteMessage("Tedarikçi seçiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                return;
            }
            if (cb_bagliTedarikci.Text==""||spin_toplam.Value<=0)
            {
                 message = new ErrorMessages.Message();
                message.WriteMessage("* İşaretli alanları boş bırakmayınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                return;
            }

            else
            { //MIGO YAPILACAK
                int buy_id=GetBuyID();
                double KDV=Convert.ToDouble(spin_KDV.Text);
                DBObjects.BuyProduct bp = new BuyProduct();
                bp.BuyId = buy_id;
             //   if (this.productId==-1)
             //   {
             ////    bp.ProductId=PItemsList[cb_urun.SelectedIndex].Id;
             //   }
                //else
                bp.Goods_id = this.goods_id;

                bp.SupplierId = MItemsList[cb_bagliTedarikci.SelectedIndex].Id;
                bp.Desc = txt_aciklama.Text;
                bp.KDV = Convert.ToDouble((KDV/Convert.ToDouble(spinMiktar.Value)));
                bp.ProductBuyPrice = Convert.ToDouble(lbl_birim_fiyat.Text);
               // bp.ProductColorId = this.product.ColorId;
             //   SetProductAttributeList();
                //    item.Barcode = (isBarcodeGenerated(bp.ProductId,Convert.ToInt32(item.Size),item.Color,ref barcode)) ? barcode : GenerateBarcode(bp.ProductId, Convert.ToInt32(item.Size),item.Color);

                Migo(ref bp);

                if (chk_pesin_ode.CheckState==CheckState.Checked)
                {//tedarikçiye para peşin ödensin mi?
                    PesinOde(bp.SupplierId);                    
                }
                
                message = new ErrorMessages.Message();

                //if (message.WriteMessage("Mal girişi yapılan ürünler için etiket basılsın mı?", MessageBoxIcon.Information, MessageBoxButtons.OKCancel)==DialogResult.OK)
                //{
                 //   PrintLabels(); şimdilik etiket bastırmıyoruz.
                //}

            }
            Success();               
        }

        private void SetProductAttributeList()
        {
            ProductAttributes item;
            for (int i = 0; i < mainTable.Rows.Count; i++)
            {
                for (int j = 2; j < mainTable.Columns.Count; j++)
                {
                    item=new ProductAttributes();
                    item.Color = Convert.ToInt32(mainTable.Rows[i]["color_id"]);
                    item.Size = Convert.ToInt32(mainTable.Columns[j].Caption);
                    item.Amount = Convert.ToInt32(mainTable.Rows[i][j]);
                    ProductAttributeList.Add(item);
                }
            }
        }

        /// <summary>
        /// ürüne ait 2 adet etiket bastırır.
        /// </summary>
        private void PrintLabels()
        {
            PrintDialog pd = new PrintDialog();
            pd.PrinterSettings = new PrinterSettings();
            if (DialogResult.OK == pd.ShowDialog(this))
            {
                Label lbl1=new Label();
                LabelPattern lblPattern;

                foreach (ProductAttributes item in ProductAttributeList)
                {
                    if (item.Amount == 0) // adedi 0 olan ürünler için migo yapma
                    {
                        continue;
                    }

                        lbl1.barcode = item.Barcode;
                        lbl1.product_name = this.product.Name;
                        lbl1.color = this.product.ColorName;
                        lbl1.product_code = this.product.Code;
                        lbl1.price = this.product.SalePrice;
                        lbl1.size = item.Size.ToString();

                        int index;
                        if (item.Amount % 2 == 0)
                        {
                            index = item.Amount / 2;  // miktarın yarısı kadar bastırıyoruz çünkü 2 şer tane çıkıyor
                        }
                        else index = item.Amount / 2 + 1; // miktarın yarısı+1 kadar bastırıyoruz 1 adet fazla basmış oluyoruz.

                        for (int i = 0; i < index; i++) 
			            {
                            lblPattern=new LabelPattern(ref lbl1,StaticObjects.LabelPatternPath,pd);
                            lblPattern.PrintLabel();
                        }                              
                    
                }
        
            }
        }


        private string GenerateBarcode(int id,int size,int color)
        {
            BarcodeGenerator bg = new BarcodeGenerator(id,size,color);
            return bg.GetBarcode();
        }
       /// <summary>
        /// alışveris için bir satın alma id si döndürür
       /// </summary>
       /// <returns></returns>
        private int   GetBuyID()
        {
            int buy_id = 0;
            MySqlCmd cmd = new MySqlCmd(StaticObjects.MySqlConn);
            try
            {
                cmd.CreateSetParameter("buy_desc", MySql.Data.MySqlClient.MySqlDbType.Text, txt_aciklama.Text.ToUpper());
                cmd.CreateSetParameter("suppliers_id", MySql.Data.MySqlClient.MySqlDbType.Int32, MItemsList[cb_bagliTedarikci.SelectedIndex].Id);
                cmd.CreateOuterParameter("buy_id", MySql.Data.MySqlClient.MySqlDbType.Int32);
                cmd.ExecuteNonQuerySP("newBuyDetail");
                buy_id = Convert.ToInt32(cmd.GetParameterValue("buy_id"));

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
         return buy_id;
        }

        /// <summary>
        /// mal girişini yapar
        /// </summary>
        /// <param name="p"></param>
        private void Migo(ref DBObjects.BuyProduct bp)
        {
            DBObjects.MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string proc_name = "migo";                                                                             
            try
            {
                db.CreateSetParameter("buy_id", MySql.Data.MySqlClient.MySqlDbType.Int32, bp.BuyId);
                db.CreateSetParameter("goods_id", MySql.Data.MySqlClient.MySqlDbType.Int32, bp.Goods_id);
                db.CreateSetParameter("buy_price", MySql.Data.MySqlClient.MySqlDbType.Double, bp.ProductBuyPrice);
                db.CreateSetParameter("kdv", MySql.Data.MySqlClient.MySqlDbType.Double, bp.KDV);
           //     db.CreateSetParameter("currency", MySql.Data.MySqlClient.MySqlDbType.VarChar,"TL"); //bp.Currency.ToString()
         //       db.CreateParameter("product_code", MySql.Data.MySqlClient.MySqlDbType.Text);
                db.CreateSetParameter("barcode", MySql.Data.MySqlClient.MySqlDbType.Text,"barcode");
              //  db.CreateParameter("product_color", MySql.Data.MySqlClient.MySqlDbType.Int32);             
               // db.CreateParameter("product_size", MySql.Data.MySqlClient.MySqlDbType.Double);
                db.CreateSetParameter("product_count", MySql.Data.MySqlClient.MySqlDbType.Double,Convert.ToDouble(spinMiktar.Value));

                //foreach (ProductAttributes item in ProductAttributeList)
                //{
                //    if (item.Amount==0) // adedi 0 olan ürünler için migo yapma
                //    {
                //        continue;
                //    }
                //    // o barkod varsa al yoksa yeni generate et
                //    string barcode = string.Empty;
                //    item.Barcode = (isBarcodeGenerated(bp.ProductId,Convert.ToInt32(item.Size),item.Color,ref barcode)) ? barcode : GenerateBarcode(bp.ProductId, Convert.ToInt32(item.Size),item.Color);
                // //   item.Barcode= GenerateBarcode(bp.ProductId, Convert.ToInt32(item.Size), bp.ProductColorId);
                //    db.SetParameterAt("product_size", item.Size);
                //    db.SetParameterAt("product_color", item.Color);
                //    db.SetParameterAt("product_count", item.Amount);
                //    db.SetParameterAt("product_code",bp.ProductId+":"+Convert.ToInt32(item.Size)+":"+item.Color); 
                //    db.SetParameterAt("barcode",item.Barcode);
                    db.ExecuteNonQuerySP(proc_name);
              //  }
              
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

            db = new MySqlCmd(StaticObjects.MySqlConn);
            proc_name = "NewSuppliersPayment";
            double value=Convert.ToDouble(spin_toplam.Value);
            try //payment to supplier
            {
                db.CreateSetParameter("unit", MySql.Data.MySqlClient.MySqlDbType.VarChar,"TL");
                db.CreateSetParameter("payment_price", MySql.Data.MySqlClient.MySqlDbType.Double,value);
                db.CreateSetParameter("suppliers_id", MySql.Data.MySqlClient.MySqlDbType.Int32,bp.SupplierId);
                db.CreateSetParameter("type", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt32(Enums.SupplierPaymentType.Borc));
                db.CreateSetParameter("process_id", MySql.Data.MySqlClient.MySqlDbType.Int32, bp.BuyId);
                db.CreateSetParameter("payment_desc", MySql.Data.MySqlClient.MySqlDbType.Text, "");
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
        }


        private bool isBarcodeGenerated(int id,int size,int color_id, ref string barcode)
        {
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "SELECT * FROM stock_details WHERE product_id=" + id + "  and product_size=" + size + "  and product_color=" + color_id;
            bool retValue = false;

            DataTable dt=cmd.GetDataTable(strSQL);

            if (dt.Rows.Count > 0)
            {
                barcode = dt.Rows[0]["barcode"].ToString();
                retValue = true;
            }
            else
                retValue = false;

                return retValue;
        }
        private void Success()
        {
            ErrorMessages.Message message = new ErrorMessages.Message();
            message.WriteMessage("Mal girişi, başarılı bir şekilde gerçekleştirildi.", MessageBoxIcon.Information, MessageBoxButtons.OK);
            onProductTotalChanged(EventArgs.Empty);
            Parent.Controls["pnl_malzeme"].Visible = true;
            this.Dispose();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_malzeme"].Visible = true;
            this.Dispose();
        }

        private void spin_toplam_EditValueChanged(object sender, EventArgs e)
        {
            double value = Convert.ToDouble(spinMiktar.Value);
            if (value == 0)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Ürün adedi giriniz", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                return;
            }        
        }

        decimal oldKDV=0;
        private void spin_KDV_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            oldKDV =Convert.ToDecimal( spin_KDV.Text);
        }

        private void spin_KDV_EditValueChanged(object sender, EventArgs e)
        {
            //if (Convert.ToDouble(spin_KDV.Text) < 0)
            //{
            //    spin_KDV.Value += spin_KDV.Properties.Increment;
            //    return;
            //}
            //int value = Convert.ToInt32(txt_toplam_adet.Text);
            //if (value == 0)
            //{
            //    ErrorMessages.Message message = new ErrorMessages.Message();
            //    message.WriteMessage("Ürün adedi giriniz", MessageBoxIcon.Warning, MessageBoxButtons.OK);
            //    return;
            //}
            //spin_toplam.Text = ((lbl_birim_fiyat.Value * value) + spin_KDV.Value).ToString("#0.0");     
            
        }

        private void cb_urun_TextChanged(object sender, EventArgs e)
        {
            //grpCtrl_numara.Controls.Clear();
            ProductAttributeList.Clear();
            //SetShoeSizeButtons(PItemsList[cb_urun.SelectedIndex].Id);
        }

        private void txt_toplam_adet_EditValueChanged(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(spinMiktar.Value);
            if (value== 0)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Ürün adedi giriniz", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                return;
            }
            spin_toplam.Text = ((lbl_birim_fiyat.Value * value) + spin_KDV.Value).ToString("#0.00");     
        }

        private void lbl_birim_fiyat_EditValueChanged(object sender, EventArgs e)
        {
            //if (Convert.ToDouble(lbl_birim_fiyat.Text) < 0)
            //{
            //    lbl_birim_fiyat.Value += lbl_birim_fiyat.Properties.Increment;
            //    return;
            //}
            //int value = Convert.ToInt32(txt_toplam_adet.Text);
            //if (value == 0)
            //{
            //    ErrorMessages.Message message = new ErrorMessages.Message();
            //    message.WriteMessage("Ürün adedi giriniz", MessageBoxIcon.Warning, MessageBoxButtons.OK);
            //    return;
            //}
            //spin_toplam.Text = ((lbl_birim_fiyat.Value * value) + spin_KDV.Value).ToString("#0.0");     
        }

        private void cb_color_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PesinOde(int supplier_id)
        {
            if (spin_toplam.Value <= 0)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Ödeme tutarını giriniz.", MessageBoxIcon.Error, MessageBoxButtons.OK);
                return;
            }
            //NewSupplierPayment
            Enums.SupplierPaymentType spt = new Enums.SupplierPaymentType();
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string proc_name = "NewSuppliersPayment";
            double value = Convert.ToDouble(spin_toplam.Value);

            spt = Enums.SupplierPaymentType.Odeme;

            try //payment to supplier
            {
                db.CreateSetParameter("unit", MySql.Data.MySqlClient.MySqlDbType.VarChar, "TL");
                db.CreateSetParameter("payment_price", MySql.Data.MySqlClient.MySqlDbType.Double, value);
                db.CreateSetParameter("suppliers_id", MySql.Data.MySqlClient.MySqlDbType.Int32, supplier_id);
                db.CreateSetParameter("type", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt32(spt));
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
        //    Success();
        }

        #region GotFocused
        private void groupControl2_Click(object sender, EventArgs e)
        {
            //grpCtrl_numara.Focus();
        }

        private void grpCtrl_numara_Click(object sender, EventArgs e)
        {
           // grpCtrl_numara.Focus();
        }

        private void gControlFiyat_Click(object sender, EventArgs e)
        {
            //grpCtrl_numara.Focus();
        }
        #endregion
   
        private void lbl_birim_fiyat_EditValueChanged_2(object sender, EventArgs e)
        {
            if (Convert.ToDouble(lbl_birim_fiyat.Text) < 0)
            {
                lbl_birim_fiyat.Value += lbl_birim_fiyat.Properties.Increment;
                return;
            }
            double value = Convert.ToDouble(spinMiktar.Value);
            if (value == 0)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Ürün adedi giriniz", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                return;
            }
            spin_toplam.Text = ((Convert.ToDouble(lbl_birim_fiyat.Value) * value) + Convert.ToDouble(spin_KDV.Value)).ToString("#0.00");   
  
        }

        private void spinMiktar_EditValueChanged_1(object sender, EventArgs e)
        {
            if (Convert.ToDouble(spinMiktar.Text) < 0)
            {
                spinMiktar.Value += spinMiktar.Properties.Increment;
                return;
            }
            double value = Convert.ToDouble(spinMiktar.Value);
            if (value == 0)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Ürün adedi giriniz", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                return;
            }
            spin_toplam.Text = ((Convert.ToDouble(lbl_birim_fiyat.Value) * value) + Convert.ToDouble(spin_KDV.Value)).ToString("#0.00");   
        }

        private void spin_KDV_EditValueChanged_2(object sender, EventArgs e)
        {
            if (Convert.ToDouble(spin_KDV.Text) < 0)
            {
                spin_KDV.Value += spin_KDV.Properties.Increment;
                return;
            }
            double value = Convert.ToDouble(spinMiktar.Value);
            if (value == 0)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Ürün adedi giriniz", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                return;
            }
            spin_toplam.Text = ((Convert.ToDouble(lbl_birim_fiyat.Value) * value) + Convert.ToDouble(spin_KDV.Value)).ToString("#0.00");   
        }

        private void lbl_birim_fiyat_Click(object sender, EventArgs e)
        {
            this.lbl_birim_fiyat.SelectAll();
        }

        private void spin_KDV_Click(object sender, EventArgs e)
        {
            this.spin_KDV.SelectAll();
        }

        private void spinMiktar_Click(object sender, EventArgs e)
        {
            this.spinMiktar.SelectAll();
        }

    }

}
