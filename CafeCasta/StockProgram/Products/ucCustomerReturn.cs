using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StockProgram.DBObjects;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;


namespace StockProgram.Products
{
    public partial class ucCustomerReturn : DevExpress.XtraEditors.XtraUserControl
    {
        private bool firstLoad;
        private bool repofirstLoad;
        public delegate void ProductTotalHandler(object sender, EventArgs e);
        public event ProductTotalHandler ProductTotalChanged;
        private int product_id;
        private GridView detailsView;
        private ExceptionLogger excLogger;
        private StockProgram.ControlHelper controlHelper;
        private List<DBObjects.Supplier> MItemsList;
        private List<DBObjects.Color> CItemsList;
        private List<Product> PItemsList;
        private Product product;
        private string type = string.Empty;

        public ucCustomerReturn()
        {
         
            InitializeComponent();
            controlHelper = new ControlHelper();
            product_id = -1;
        }
        public ucCustomerReturn(string type)
        {
            this.type = type;
            InitializeComponent();
            controlHelper = new ControlHelper();
            product_id = -1;
          
        }
        public ucCustomerReturn(int id)
        {
          
            InitializeComponent();
            this.product_id = id;
            controlHelper = new ControlHelper();
            FillProductProperties();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }

        private void ucProductReturn_Load(object sender, EventArgs e)
        {
            gridView1.OptionsView.ShowGroupPanel = false;
            firstLoad = true;
            repofirstLoad = true;
            FillGrid();          
            if (this.type=="aktar")
            {
                  SetUserControlForSaleSwitch(); //satış aktarma işlemeine göre formu hazırla
            }
            else
                this.ActiveControl = txt_barkod;
        }

        private void SetUserControlForSaleSwitch()
        {
            this.pnl_barkod.Visible = false;
            this.aktar_column.Visible = true;
            this.aktar_column.VisibleIndex = 0;
            gridView1.BestFitColumns();
            lbl_header.Text = "Müşteriler Arası Alışveriş Aktarma";
            detailsView.DoubleClick -= new EventHandler(detailsView_DoubleClick);     //artık bu eventi çalıştırmayacagız

        }

        /// <summary>
        ///  id si belli olan ürünün (gridden seçilmiş) özelliklerini doldurur
        /// </summary>
        private void FillProductProperties()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = db.GetDataTable("select * from product_details where product_id= " + product_id);
            product = new Product();
            product.Id = product_id;
            product.Name = dt.Rows[0]["product_name"].ToString();
            product.Desc = dt.Rows[0]["product_desc"].ToString();
            db.Close();
            db = null;
        }

        protected virtual void onProductTotalChanged(EventArgs e)
        {
            if (ProductTotalChanged != null)
                ProductTotalChanged(this, e);
        }

        private void FillSuppliers()
        {
            //fill Suppliers
            gridView1.OptionsView.ShowGroupPanel = false;
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
                //cb_bagliTedarikci.Text = cb_bagliTedarikci.Properties.Items[0].ToString();
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

        private void repo_button_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
          
            switch (e.Button.Caption)
            {
                case "Aktar":
                      DataRow dr = gridView1.GetFocusedDataRow();
                      int customer_id = Convert.ToInt32(dr["customer_id"]);
                      int sell_id = Convert.ToInt32(dr["sell_id"]);
                      SwitchSale(sell_id, customer_id);
                    break;
             
                case "Sil":
                    ErrorMessages.Message msg = new ErrorMessages.Message();
                    if (msg.WriteMessage("Müşteri silme işleminden sonra o müşteri ile yapılan tüm alışverişler silinecektir. \nDevam etmek istiyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        //sil
                    }
                    break;
                default:
                    break;
            }
        }

        private void SwitchSale(int sell_id, int customer_id)
        {/*  Products.frmSellReturn ctrl = new Products.frmSellReturn(ref dr);
            ctrl.ProductReturnGridChanged += new frmSellReturn.ProductReturnGridHandler(ctrl_ProductReturnGridChanged);
            ctrl.ShowDialog(this);*/
            using (frmSaleSwitch _switch = new frmSaleSwitch(sell_id,customer_id))
            {
                _switch.PurchaseCompleted += new frmSaleSwitch.PurchaseHandler(_switch_PurchaseCompleted);
                _switch.ShowDialog(this);
            }
        }

        private void _switch_PurchaseCompleted(object sender, EventArgs e)
        {
            FillGrid(); //refresh grid on product amounts changed
        }
        private void btn_iade_Click(object sender, EventArgs e)
        {
        //    string product_code = "";
            DataRow dr;
            dr = (detailsView.GetFocusedDataRow() == null) ? (DataRow)null : detailsView.GetFocusedDataRow();
            if (dr==null)
            {
                        ErrorMessages.Message message = new ErrorMessages.Message();
                        message.WriteMessage("İade yapmak istediğiniz ürünü seçiniz.", MessageBoxIcon.Information, MessageBoxButtons.OK);
                        return;
            }
           // product_code= (dr["product_code"]).ToString();
            Products.frmSellReturn ctrl = new Products.frmSellReturn(ref dr);
            ctrl.ProductReturnGridChanged+=new frmSellReturn.ProductReturnGridHandler(ctrl_ProductReturnGridChanged);
            ctrl.ShowDialog(this);
        }

        void ctrl_ProductReturnGridChanged(object sender, EventArgs e)
        {
            FillGrid(); //refresh grid on product amounts changed
            onProductTotalChanged(EventArgs.Empty);
        }


        private void FillGrid()
        {
            refresh_view = true;
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "";
            if (product_id == -1)
            {
                strSQL = "select * from v_sell_lists_master order by sell_id desc; ";
                strSQL += "select * from v_sell_lists order by sell_id desc; ";
            }
            else
            {
                strSQL = "select * from v_sell_lists_master where product_id=" + product_id + " order by sell_id desc; ";
                strSQL += "select * from v_sell_lists where product_id=" + product_id + " order by sell_id desc";
            }

            DataSet ds = new DataSet();
            ds = cmd.GetDataSet(strSQL);

            if (repofirstLoad)
            {
                repo_button.Buttons[0].Image = global::StockProgram.Properties.Resources.switch_small;
                repo_button.Buttons[0].Caption = "Aktar";
                repo_button.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                repo_button.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repo_button_ButtonClick);
            }
            repofirstLoad = false;

            try
            {
                //set sold product codes
                SetProductCodesColumn(ref ds);

                //set relations
                DataColumn keyColumn = ds.Tables[0].Columns["sell_id"];
                DataColumn foreignKeyColumn = ds.Tables[1].Columns["sell_id"];
                ds.Relations.Add("PurchaseDetails", keyColumn, foreignKeyColumn);

                gridControl1.DataSource = ds.Tables[0];
                SetConditionalFormatting("[total]");
                gridControl1.ForceInitialize();

                detailsView = new GridView(gridControl1);
                gridControl1.LevelTree.Nodes.Add("PurchaseDetails", detailsView);

                //Specify text to be displayed within detail tabs.
                SetDetailsView("Satış Kalemleri", ref ds);

                gridView1.ShowFindPanel();

                if (ds.Tables[0].Rows.Count <= 0)
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Henüz satış yapılmamış", MessageBoxIcon.Information, MessageBoxButtons.OK);
                    return;
                }
                if (product_id != -1)//ürünler gridinden özel olarak seçilmiş ürün label e ismini yazdıralım
                {
                    lbl_header.Text = "Ürün İade (" + ds.Tables[0].Rows[0]["product_name"].ToString() + ")";
                }

            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, InsertProduct() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {

                ds.Dispose();
                cmd.Close();
                cmd = null;
            }
        }

        void detailsView_DoubleClick(object sender, EventArgs e)
        {
            GridView details = sender as GridView;
            DataRow dr = details.GetFocusedDataRow();
            if (Convert.ToInt32(dr["product_amount"])<=0)
            {// satılan ürünlerin hepsi iade edilmiş
                  ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("İade edilecek ürün bulunamadı.", MessageBoxIcon.Error, MessageBoxButtons.OK);
                return;
            }

            Products.frmSellReturn ctrl = new Products.frmSellReturn(ref dr);
            ctrl.ProductReturnGridChanged += new frmSellReturn.ProductReturnGridHandler(ctrl_ProductReturnGridChanged);
            ctrl.ShowDialog(this);
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
            detailsView.Columns["product_color"].VisibleIndex = -1;
            detailsView.Columns["color_name"].VisibleIndex = -1;
            detailsView.Columns["customer_id"].VisibleIndex = -1;
            detailsView.Columns["customer_name"].VisibleIndex = -1;
            detailsView.Columns["product_cat"].VisibleIndex = -1;
            detailsView.Columns["modified_date"].VisibleIndex = -1;
            detailsView.Columns["product_size"].VisibleIndex = -1;
            detailsView.Columns["product_id"].VisibleIndex = -1;
            detailsView.Columns["sell_desc"].VisibleIndex = -1;

            //sell_id column
            detailsView.Columns["sell_id"].Caption = "Satış Numarası";
            detailsView.Columns["sell_id"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["sell_id"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["sell_id"].VisibleIndex = 0;

            //product_name column
            detailsView.Columns["product_name"].Caption = "Ürün Adı";
            detailsView.Columns["product_name"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_name"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["product_name"].VisibleIndex = 1;

            //product_code_manual column
            detailsView.Columns["product_code_manual"].Caption = "Ürün Kodu";
            detailsView.Columns["product_code_manual"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_code_manual"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["product_code_manual"].VisibleIndex = 2;

            //product_desc column
            detailsView.Columns["product_desc"].Caption = "Ürün Açıklama";
            detailsView.Columns["product_desc"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_desc"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["product_desc"].VisibleIndex = 3;

            //cat_name column
            detailsView.Columns["cat_name"].Caption = "Kategori";
            detailsView.Columns["cat_name"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["cat_name"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["cat_name"].VisibleIndex = 4;

            //product_price column
            detailsView.Columns["product_price"].Caption = "Birim Fiyat";
            detailsView.Columns["product_price"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_price"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["product_price"].VisibleIndex = 5;

            //currency_text column
            detailsView.Columns["currency_text"].Caption = "Para Birimi";
            detailsView.Columns["currency_text"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["currency_text"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["currency_text"].VisibleIndex = 6;

            //product_amount column
            detailsView.Columns["product_amount"].Caption = "Miktar";
            detailsView.Columns["product_amount"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_amount"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["product_amount"].VisibleIndex = 7;

            //unit_amount column
            detailsView.Columns["unit_amount"].Caption = "Birim Gramaj";
            detailsView.Columns["unit_amount"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["unit_amount"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["unit_amount"].VisibleIndex = 8;

            //total_weight column
            detailsView.Columns["total_weight"].Caption = " Gram Bazlı Toplam";
            detailsView.Columns["total_weight"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["total_weight"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["total_weight"].VisibleIndex = 9;
      

            detailsView.BestFitColumns();
            detailsView.OptionsView.ShowGroupPanel = false;

        }

        private bool refresh_view;
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (firstLoad)
            {
                firstLoad = false;
                refresh_view = false;
            }
            else
                if (refresh_view) // amac grid refresh ise hiç bir row expand etme
                {
                    refresh_view = false;
                    return;
                }
            GridView View = gridControl1.FocusedView as GridView;
            int rHandle = View.FocusedRowHandle;
            CollapseDetailRows(rHandle, View.RowCount);
            gridView1.SetMasterRowExpanded(rHandle, !gridView1.GetMasterRowExpanded(rHandle));
        }

        /// <summary>
        /// collapses all the grid rows except the selected one
        /// </summary>
        /// <param name="rowHandle"></param>
        private void CollapseDetailRows(int rowHandle, int masterViewRowCount)
        {
            int rowCount = masterViewRowCount;
            for (int i = 0; i < rowCount; i++)
            {
                if (i == rowHandle)
                {
                    continue;
                }
                gridView1.SetMasterRowExpanded(i, false);
            }
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
            condition1.Expression = columnName + "<=0";
            gridView1.FormatConditions.Add(condition1);
        }

        private void SetProductCodesColumn(ref DataSet ds)
        {
            ds.Tables[0].Columns.Add("p_codes", typeof(String)); //ürün kodları için bir kolon ekleyelim
            int sell_id = 0;
            List<string> product_codes = new List<string>();

            foreach (DataRow row in ds.Tables[0].Rows) //değerleri hesapla
            {
                sell_id = Convert.ToInt32(row["sell_id"]);
                foreach (DataRow row1 in ds.Tables[1].Rows)
                {
                    if (sell_id == Convert.ToInt32(row1["sell_id"]))
                    {
                        product_codes.Add(row1["product_code_manual"].ToString());
                    }
                }
                row["p_codes"] = GetCodesFormCodeList(product_codes);
                product_codes.Clear();
            }
        }

        private string GetCodesFormCodeList(List<string> product_codes)
        {
            string ret_value = string.Empty;
            for (int i = 0; i < product_codes.Count; i++)
            {
                if (i != product_codes.Count - 1)
                {
                    ret_value += product_codes[i] + " - ";
                }
                else
                    ret_value += product_codes[i];
            }
            return ret_value;
        }
        void ucSalesMasterPage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.ActiveControl is DevExpress.XtraGrid.Controls.FindControl)  //ürün arama barkod ile yapılmışsa
            {
                //string product_code_manual=string.Empty;
                //product_code_manual = GetProductManualCode(gridView1);
                //gridView1.ApplyFindFilter(product_code_manual);
            }
     
        }
        /// <summary>
        /// girilen barcode numarasından ürün kodu döndürür
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private string GetProductManualCode(string barcode)
        {
            string p_code = string.Empty;
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable("v_stocs");
            MySqlCmd cmd = db.CreateCommand();
            try
            {
                cmd.CreateSetParameter("barcode", MySql.Data.MySqlClient.MySqlDbType.Text, barcode);
                string SQL = "select product_code_manual from v_stocks where  barcode=@barcode";
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
                cmd.Close();
            }
            if (dt.Rows.Count <= 0)
            {
                p_code = "";
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Bu ürün stoklarda mevcut değil.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
            }
            else
            {
                p_code = dt.Rows[0]["product_code_manual"].ToString();
            
            }
            return p_code;
        }

        private void txt_barkod_KeyPress(object sender, KeyPressEventArgs e)
        {
            string product_code_manual = string.Empty;
            if (e.KeyChar == (char)13)
            {
                product_code_manual = GetProductManualCode(txt_barkod.Text);
                gridView1.ApplyFindFilter(product_code_manual);
                txt_barkod.Focus();
                txt_barkod.SelectAll();
            }
        }
    }
}
