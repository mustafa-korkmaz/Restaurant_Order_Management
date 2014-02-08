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
    public partial class ucProductReturn : DevExpress.XtraEditors.XtraUserControl
    {
        private int goods_id;
        public delegate void ProductTotalHandler(object sender, EventArgs e);
        public event ProductTotalHandler ProductTotalChanged;
        private GridView detailsView;
        private ExceptionLogger excLogger;
        private StockProgram.ControlHelper controlHelper;
        private List<DBObjects.Supplier> MItemsList;
        private List<DBObjects.Color> CItemsList;
        private List<Product> PItemsList;
        private Goods goods;
        private bool firstLoad;

        public ucProductReturn()
        {
            InitializeComponent();
            controlHelper = new ControlHelper();
            goods_id = -1;
        }
        public ucProductReturn(int id)
        {
            InitializeComponent();
            this.goods_id = id;
            controlHelper = new ControlHelper();
            FillProductProperties();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_malzeme"].Visible = true;
            this.Dispose();
        }

        private void ucProductReturn_Load(object sender, EventArgs e)
        {
            firstLoad = true;
            FillGrid();
            this.ActiveControl = txt_barkod;
        }
        protected virtual void onProductTotalChanged(EventArgs e)
        {
            if (ProductTotalChanged != null)
                ProductTotalChanged(this, e);
        }

        /// <summary>
        ///  id si belli olan ürünün (gridden seçilmiş) özelliklerini doldurur
        /// </summary>
        private void FillProductProperties()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = db.GetDataTable("select * from goods_details where goods_id= " + this.goods_id);
            goods = new Goods();
            goods.id = this.goods_id;
            goods.name = dt.Rows[0]["goods_name"].ToString();
            //product.Desc = dt.Rows[0]["product_desc"].ToString();
            db.Close();
            db = null;
        }
        private void FillProducts()
        {
            ////fill Suppliers
            //DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            //string strSQL = "select  * from v_products order by product_name asc";
            //PItemsList = new List<DBObjects.Product>();
            //DataTable dt = new DataTable();
            //try
            //{
            //    dt = db.GetDataTable(strSQL);
            //    controlHelper.FillControl(cb_urunAdi, Enums.RepositoryItemType.ComboBox, ref dt, "product_name");
            //    if (product_id != -1)
            //    {
            //        cb_urunAdi.Text = product.Name;
            //    }
            //    else cb_urunAdi.Text = cb_urunAdi.Properties.Items[0].ToString();

            //    PItemsList = controlHelper.GetProducts(ref dt);
            //}
            //catch (Exception e)
            //{
            //    string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
            //    excLogger = new ExceptionLogger(e.Message, excSource);// DB ye log yaz
            //    ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
            //    excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
            //    excMail.ErrorSource = excSource + "()";
            //    excMail.Send(); // Mail at
            //}
            //finally
            //{
            //    db.Close();
            //    db = null;
            //    dt.Dispose();
            //}
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

        private void btn_iade_Click(object sender, EventArgs e)
        {
            string product_code = "";
            DataRow dr;
            dr = (detailsView.GetFocusedDataRow() == null) ? (DataRow)null : detailsView.GetFocusedDataRow();
            if (dr == null)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("İade yapmak istediğiniz ürünü seçiniz.", MessageBoxIcon.Information, MessageBoxButtons.OK);
                return;
            }
            if (dr == null)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("İade yapmak istediğiniz ürünü seçiniz.", MessageBoxIcon.Information, MessageBoxButtons.OK);
                return;
            }
            product_code = (dr["product_code"]).ToString();
            Products.frmProductReturn ctrl = new Products.frmProductReturn(product_code);
            ctrl.ProductReturnGridChanged += new frmProductReturn.ProductReturnGridHandler(ctrl_ProductReturnGridChanged);
            ctrl.ShowDialog(this);
        }

        void ctrl_ProductReturnGridChanged(object sender, EventArgs e)
        {
            FillGrid(); //refresh grid on product amounts changed
            onProductTotalChanged(EventArgs.Empty);

        }

        //private void gridControl1_Load(object sender, EventArgs e)
        //{
        //    FillGrid();
        //}

        private bool refresh_view;
        private void FillGrid()
        {
            refresh_view = true;
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "";
            if (goods_id == -1)
            {
                strSQL = "select * from v_goods_stocks where is_deleted=0 order by goods_name asc;";
                gridView1.ShowFindPanel();
                //strSQL = "select * from v_stocks_master order by product_name asc;";
              //  strSQL += " select * from v_stocks  order by product_size asc";
            }
            else
            {
             //   strSQL = "select * from v_stocks_master where product_id=" + this.goods_id + " order by product_name asc;";
                strSQL += " select * from v_goods_stocks  where is_deleted=0  and  goods_id=" + this.goods_id;
                gridView1.HideFindPanel();
            }
            DataSet ds = new DataSet();

            try
            {
                ds = cmd.GetDataSet(strSQL);

                //Set up a master-detail relationship between the DataTables
                //DataColumn keyColumn = ds.Tables[0].Columns["product_id"];
                //DataColumn foreignKeyColumn = ds.Tables[1].Columns["product_id"];
                //ds.Relations.Add("ProductsDetails", keyColumn, foreignKeyColumn);
                gridControl1.DataSource = ds.Tables[0];
                SetConditionalFormatting("[product_count]");
                gridControl1.ForceInitialize();
                //detailsView = new GridView(gridControl1);
                //gridControl1.LevelTree.Nodes.Add("ProductsDetails", detailsView);

                //Specify text to be displayed within detail tabs.
                //SetDetailsView("Ürün Detayları", ref ds);
              //  gridView1.ShowFindPanel();

                if (ds.Tables[0].Rows.Count <= 0)
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Ürün stoklarda mevcut değil", MessageBoxIcon.Information, MessageBoxButtons.OK);
                    return;
                }
                if (goods_id != -1)//ürünler gridinden özel olarak seçilmiş ürün label e ismini yazdıralım
                {
                    lbl_product_name.Text = "Ürün İade (" + ds.Tables[0].Rows[0]["goods_name"].ToString() + ")";
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

        /// <summary>
        /// details gridinin kolonlarını ve özelliklerini set eder
        /// </summary>
        /// <param name="viewHeader"></param>
        /// <param name="ds"></param>
        private void SetDetailsView(string viewHeader, ref DataSet ds)
        {
            //add double click event
            detailsView.DoubleClick += new EventHandler(detailsView_DoubleClick);
            detailsView.ViewCaption = "Ürün Detayları";
            detailsView.PopulateColumns(ds.Tables[1]);
            detailsView.Columns["product_size"].VisibleIndex = -1;
            detailsView.Columns["color_name"].VisibleIndex = -1;
            detailsView.Columns["product_color"].VisibleIndex = -1;
            detailsView.Columns["cat_name"].VisibleIndex = -1;
            detailsView.Columns["product_code"].VisibleIndex = -1;
            detailsView.Columns["product_cat"].VisibleIndex = -1;
            detailsView.Columns[4].Visible = false;//img_path
            detailsView.Columns["product_name"].VisibleIndex = -1;
            detailsView.Columns["product_id"].VisibleIndex = -1;

            //product_code_manual column
            detailsView.Columns["product_code_manual"].Caption = "Ürün Kodu";
            detailsView.Columns["product_code_manual"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_code_manual"].OptionsColumn.ReadOnly = false;

            //barcode column
            detailsView.Columns["barcode"].Caption = "Barkod No.";
            detailsView.Columns["barcode"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["barcode"].OptionsColumn.ReadOnly = false;

            //color_name column
            detailsView.Columns["color_name"].Caption = "Renk";
            detailsView.Columns["color_name"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["color_name"].OptionsColumn.ReadOnly = false;

            //product_size column
            detailsView.Columns["product_size"].Caption = "Numara";
            detailsView.Columns["product_size"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_size"].OptionsColumn.ReadOnly = false;

            //product_count column
            detailsView.Columns["product_count"].Caption = "Miktar";
            detailsView.Columns["product_count"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_count"].OptionsColumn.ReadOnly = false;

            //modified_date column
            detailsView.Columns["modified_date"].Caption = "Son Alım Tarihi";
            detailsView.Columns["modified_date"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["modified_date"].OptionsColumn.ReadOnly = false;

            detailsView.BestFitColumns();
            detailsView.OptionsView.ShowGroupPanel = false;

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

        void detailsView_DoubleClick(object sender, EventArgs e)
        {
            string p_code = "";
            GridView details = sender as GridView;
            DataRow dr = details.GetFocusedDataRow();
            p_code = (dr["product_code"]).ToString();

            using (Products.frmProductReturn ctrl = new Products.frmProductReturn(p_code))
            {
                ctrl.ProductReturnGridChanged += new frmProductReturn.ProductReturnGridHandler(ctrl_ProductReturnGridChanged);
                ctrl.ShowDialog(this);
            }

            //Products.frmProductReturn ctrl = new Products.frmProductReturn(p_code);
            //ctrl.ProductReturnGridChanged += new frmProductReturn.ProductReturnGridHandler(ctrl_ProductReturnGridChanged);
            //ctrl.ShowDialog(this);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            int id =0;
            GridView details = sender as GridView;
            DataRow dr = details.GetFocusedDataRow();
            id = Convert.ToInt32(dr["goods_id"]);

            using (Products.frmProductReturn ctrl = new Products.frmProductReturn(id))
            {
                ctrl.ProductReturnGridChanged += new frmProductReturn.ProductReturnGridHandler(ctrl_ProductReturnGridChanged);
                ctrl.ShowDialog(this);
            }


            //if (firstLoad)
            //{
            //    firstLoad = false;
            //    refresh_view = false;
            //}
            //else
            //    if (refresh_view) // amac grid refresh ise hiç bir row expand etme
            //    {
            //        refresh_view = false;
            //        return;
            //    }

            //GridView View = gridControl1.FocusedView as GridView;
            //int rHandle = View.FocusedRowHandle;
            //CollapseDetailRows(rHandle, View.RowCount);
            //gridView1.SetMasterRowExpanded(rHandle, !gridView1.GetMasterRowExpanded(rHandle));
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
    }
}
