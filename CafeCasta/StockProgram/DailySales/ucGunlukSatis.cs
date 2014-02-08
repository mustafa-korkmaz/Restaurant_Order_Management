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

namespace StockProgram.DailySales
{
    public partial class ucGunlukSatis : DevExpress.XtraEditors.XtraUserControl
    {
        private bool firstLoad;
        private GridView detailsView;
        private ExceptionLogger excLogger;
        private DateTime current_date;
        public double totalReturnPrice;
        public double furtherReturnPrice;// ileride bir iade olursa bu sayfaya geri döndüğünde o günkü kasa tutarını denkleştirmek için iadeleri tekrar hesapla
        public ucGunlukSatis()
        {
            InitializeComponent();
        }

        private void ucGunlukSatis_Load(object sender, EventArgs e)
        {
            firstLoad = true;
            lbl_date.Text = DateTime.Now.ToString("d.MM.yyyy");
            current_date = DateTime.Now;
            date.DateTime = current_date;
        
           // FillGrid();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            System.TimeSpan day = new System.TimeSpan(1, 0, 0, 0);
            current_date = current_date.Add(day);
            date.DateTime = current_date;
            lbl_date.Text = current_date.ToString("d.MM.yyyy");
           // FillGrid();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            System.TimeSpan day = new System.TimeSpan(1, 0, 0, 0);
            current_date = current_date.Subtract(day);
            date.DateTime = current_date;
            lbl_date.Text = current_date.ToString("d.MM.yyyy");
           // FillGrid();
        }

        /// <summary>
        /// fills the grid between the given date time
        /// </summary>
        /// <param name="dateTime"></param>
        private void FillGrid()
        {
            string dayOne=current_date.ToString("yyyy-MM-d");
            string dayTwo=current_date.AddDays(1).ToString("yyyy-MM-d");
            string strSQL = "SELECT v_slm.* , ifnull(pd.pesin,0)as pesin,ifnull(pd.pos,0) as pos, ifnull(pd.veresiye,0) as veresiye, if(pd.hediye>0,pd.hediye,0) as hediye FROM  `v_sell_lists_master` v_slm LEFT JOIN v_payment_details pd ON (pd.sell_id = v_slm.sell_id)  where modified_date between '" + dayOne + "' and  '" + dayTwo + "';";
            strSQL += " SELECT *, cast((product_amount*product_price) as decimal(11,2)) as sale_formatted ,cast(product_price as decimal(11,2)) as product_price_formatted FROM `v_sell_lists`   where modified_date between '" + dayOne + "' and  '" + dayTwo + "'";
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataSet ds = new DataSet();
            try
            {
                ds = db.GetDataSet(strSQL);
                SetReturnColumn(ref ds); //iade kolonu verilerini hazırla
           //     SetProductCodesColumn(ref ds);// ürün kodlarını barındıran kolonu hazırla
                SetBankNameColumn(ref ds);// banka adlarını barındıran kolonu hazırla
                gridView1.RowHeight = 35;
                //set relations
                DataColumn keyColumn = ds.Tables[0].Columns["sell_id"];
                DataColumn foreignKeyColumn = ds.Tables[1].Columns["sell_id"];
                ds.Relations.Add("PurchaseDetails", keyColumn, foreignKeyColumn);

                gridControl1.DataSource = ds.Tables[0];
             //   SetConditionalFormatting("[total]");
                gridControl1.ForceInitialize();
                gridView1.BestFitColumns();
                detailsView = new GridView(gridControl1);
                gridControl1.LevelTree.Nodes.Add("PurchaseDetails", detailsView);
                //Specify text to be displayed within detail tabs.
                SetDetailsView("Satış Kalemleri", ref ds);
                SetOldReturnsPrice();//Eski tarihli iadeler varsa ekrana bildirim olarak göster.
                SetFurtherReturnsPrice();//ileri tarihli iade alımı yapılmışsa ekrana bildirim olarak göster
                CalculateKASA(ref ds);
                SetIncomeRatio();//Karlılık yüzdesi hesapla
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
                ds.Dispose();
                db.Close();
                db = null;
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
            try
            {
                detailsView.Columns["sell_id"].VisibleIndex = -1;
                detailsView.Columns["sell_staff_id"].VisibleIndex = -1;
                detailsView.Columns["account_type"].VisibleIndex = -1;
                detailsView.Columns["account_staff_name"].VisibleIndex = -1;
                detailsView.Columns["sell_staff_name"].VisibleIndex = -1;
                detailsView.Columns["account_status"].VisibleIndex = -1;
           //     detailsView.Columns["product_desc"].VisibleIndex = -1;
                detailsView.Columns["account_id"].VisibleIndex = -1;
                detailsView.Columns["account_owner_name"].VisibleIndex = -1;
                detailsView.Columns["product_price"].VisibleIndex = -1;
                detailsView.Columns["account_owner"].VisibleIndex = -1;
                detailsView.Columns["account_staff_id"].VisibleIndex = -1;
            //    detailsView.Columns["product_cat"].VisibleIndex = -1;
                detailsView.Columns["modified_date"].VisibleIndex = -1;
               // detailsView.Columns["unit_amount"].VisibleIndex = -1;
                detailsView.Columns["product_id"].VisibleIndex = -1;
                detailsView.Columns["sell_desc"].VisibleIndex = -1;
                //   detailsView.Columns["currency_text"].VisibleIndex = -1;
             //   detailsView.Columns["total_weight"].VisibleIndex = -1;
            }
            catch (Exception)
            {
                
                throw;
            }
         
            //product_name column
            detailsView.Columns["product_name"].Caption = "Ürün Adı";
            detailsView.Columns["product_name"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_name"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["product_name"].VisibleIndex = 1;

            //product_price column
            detailsView.Columns["product_price_formatted"].Caption = "Birim Satış Fiyatı";
            detailsView.Columns["product_price_formatted"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_price_formatted"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["product_price_formatted"].VisibleIndex = 8;

   
            //sell_id column
            //detailsView.Columns["sell_id"].Caption = "Satış Numarası";
            //detailsView.Columns["sell_id"].OptionsColumn.AllowEdit = false;
            //detailsView.Columns["sell_id"].OptionsColumn.ReadOnly = false;
            //detailsView.Columns["sell_id"].VisibleIndex = 0;

            //cat_name column
            //detailsView.Columns["cat_name"].Caption = "Kategori";
            //detailsView.Columns["cat_name"].OptionsColumn.AllowEdit = false;
            //detailsView.Columns["cat_name"].OptionsColumn.ReadOnly = false;
            //detailsView.Columns["cat_name"].VisibleIndex = 3;


            //product_amount column
            detailsView.Columns["product_amount"].Caption = "Miktar";
            detailsView.Columns["product_amount"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_amount"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["product_amount"].VisibleIndex = 7;

            //sale column
            detailsView.Columns["sale_formatted"].Caption = "Satış Tutarı";
            detailsView.Columns["sale_formatted"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["sale_formatted"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["sale_formatted"].VisibleIndex = 10;

            detailsView.BestFitColumns();
            detailsView.OptionsView.ShowGroupPanel = false;

        }

        void detailsView_DoubleClick(object sender, EventArgs e)
        {
            //GridView details = sender as GridView;
            //DataRow dr = details.GetFocusedDataRow();
            //Products.frmSellReturn ctrl = new Products.frmSellReturn(ref dr);
            //ctrl.ProductReturnGridChanged += new frmSellReturn.ProductReturnGridHandler(ctrl_ProductReturnGridChanged);
            //ctrl.ShowDialog(this);
        }

        private bool refresh_view;
        private void gridView1_DoubleClick_1(object sender, EventArgs e)
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

        private void SetFurtherReturnsPrice()
        { 
            string dayOne=current_date.ToString("yyyy-MM-d");
            string dayTwo=current_date.AddDays(1).ToString("yyyy-MM-d");
            string strSQL="";
            double totalPrice = 0;
            this.furtherReturnPrice = 0;
    
            strSQL += "SELECT distinct sr.*,sl.modified_date as sell_date FROM `sell_return` sr" +
            " left join sell_list sl on(sr.sell_id=sl.sell_id) where (sr.modified_date >= '" + dayTwo+ "' )  " +
            " and  (sl.modified_date between  '" + dayOne + "' and   '" + dayTwo + "')  ";
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt=db.GetDataTable(strSQL);
            if (dt.Rows.Count == 0)
            {
                lbl_furtherReturnsPrice.Text ="0.0 TL";
                pnl_further_returns.Visible = false;
                return;
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    totalPrice += Convert.ToDouble(row["product_price"]);//* Convert.ToDouble(row["product_amount"]);
                }
                lbl_furtherReturnsPrice.Text = totalPrice.ToString("#0.00") + " TL";
            }
            this.furtherReturnPrice= totalPrice;
            if (this.furtherReturnPrice == 0)
            {
                pnl_further_returns.Visible = false;
            }
            else pnl_further_returns.Visible = true;
        }

        private void SetReturnColumn(ref DataSet ds)
        {
            ds.Tables[0].Columns.Add("returns", typeof(Double)); //iadeler için bir kolon ekleyelim

            foreach (DataRow row in ds.Tables[0].Rows) //değerleri hesapla
            {
                row["returns"] = -(Convert.ToDouble(row["pesin"]) + Convert.ToDouble(row["pos"]) + Convert.ToDouble(row["veresiye"])-Convert.ToDouble(  row["sell_income"]));
            }
        }

        private void SetBankNameColumn(ref DataSet ds)
        {
            ds.Tables[0].Columns.Add("bank_name", typeof(String)); //POS satış varsa banka adı için bir kolon ekleyelim
            int sell_id = 0;

            foreach (DataRow row in ds.Tables[0].Rows) //değerleri hesapla
            {
                if (Convert.ToDouble(row["pos"])>0) //pos satış var banka adını bul
                {
                    sell_id = Convert.ToInt32(row["sell_id"]);
                    row["bank_name"]= GetBankName(sell_id);
                }
             
            }
        }

        private string  GetBankName(int sell_id)
        {
            string bank_name=string.Empty;
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = string.Empty;
            strSQL = "select * from v_sell_via_pos_details where sell_id=" + sell_id;
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);
                bank_name = dt.Rows[0]["bank_name"].ToString();
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
                dt.Dispose();
                db.Close();
                db = null;
            }
            return bank_name;
        }


        private void CalculateKASA(ref DataSet ds)
        {
            double KASA = CalculateCash_PosSales(ref ds) +CalculateVeresiye()- this.totalReturnPrice-CalculateToday_sReturns(ref ds)+this.furtherReturnPrice;
            lbl_kasa.Text = KASA.ToString("#0.00")+" TL" ;
        }

        private double CalculateVeresiye()
        {
            double retValue = 0;
            string dayOne = current_date.ToString("yyyy-MM-d");
            string dayTwo = current_date.AddDays(1).ToString("yyyy-MM-d");
            string strSQL = "";
            strSQL += "SELECT * from customer_payments where type=0 and  (payment_date  between  '" + dayOne + "' and   '" + dayTwo + "')  ";
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = db.GetDataTable(strSQL);
            if (dt.Rows.Count == 0)
            {
               retValue= 0;
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    retValue += Convert.ToDouble(row["payment"]);
                }
            }
            lbl_veresiye.Text = retValue.ToString("#0.00")+" TL";
            return retValue;
        }

        private double CalculateToday_sReturns(ref DataSet ds)
        {
            double returns = 0;

            foreach (DataRow row in ds.Tables[0].Rows) //değerleri hesapla
            {
                if (Convert.ToDouble(row["returns"]) < 0) //bu gün yapılan iadeler var
                {
                    returns += Convert.ToDouble(row["returns"]);
                }

            }
            return -returns;
        }
        private double CalculateCash_PosSales(ref DataSet ds)
        {
            double cash_POS = 0;

            foreach (DataRow row in ds.Tables[0].Rows) //değerleri hesapla
            {
                //if (Convert.ToDouble(row["pos"]) > 0 || Convert.ToDouble(row["pesin"]) > 0) //pos satış var banka adını bul
                //{
                //    cash_POS += Convert.ToDouble(row["pos"])+Convert.ToDouble(row["pesin"]);
                //}
                //if ( Convert.ToDouble(row["pesin"]) > 0) //pos satış var banka adını bul
                //{
                //    cash_POS += Convert.ToDouble(row["pos"]) + Convert.ToDouble(row["pesin"]);
                //}
                if (Convert.ToDouble(row["pesin"]) > 0) //pos satış var banka adını bul
                {
                    cash_POS += Convert.ToDouble(row["pesin"]);
                }

            }
            return cash_POS;
        }
         private void SetProductCodesColumn(ref DataSet ds)
         {
             ds.Tables[0].Columns.Add("p_codes", typeof(String)); //satılan ürün kodları için bir kolon ekleyelim
             int sell_id=0;
             List<string > product_codes=new List<string>();

             foreach (DataRow row in ds.Tables[0].Rows) //değerleri hesapla
             {
                 sell_id = Convert.ToInt32(row["sell_id"]);
                 foreach (DataRow row1 in ds.Tables[1].Rows)
                 {
                     if (sell_id==Convert.ToInt32( row1["sell_id"]))
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

         private void SetOldReturnsPrice()
         { 
            string dayOne=current_date.ToString("yyyy-MM-d");
            string dayTwo=current_date.AddDays(1).ToString("yyyy-MM-d");
            string strSQL="";
            double totalPrice = 0;
            this.totalReturnPrice = 0;
            /* 
SELECT distinct sr.*,sl.modified_date as sell_date FROM `sell_return` sr
left join sell_list sl on(sr.sell_id=sl.sell_id) where (sr.modified_date between '2013-02-5' and  '2013-02-6')
and (sl.modified_date not between  '2013-02-5' and  '2013-02-6')*/
            strSQL += "SELECT distinct sr.*,sl.modified_date as sell_date FROM `sell_return` sr" +
            " left join sell_list sl on(sr.sell_id=sl.sell_id) where (sr.modified_date between  '" + dayOne + "' and   '" + dayTwo + "')  " +
            " and  (sl.modified_date not between  '" + dayOne + "' and   '" + dayTwo + "')  ";
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt=db.GetDataTable(strSQL);
            if (dt.Rows.Count == 0)
            {
                lbl_oldReturnsPrice.Text ="0.0 TL";
                return;
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    totalPrice += Convert.ToDouble(row["product_price"]);//* Convert.ToDouble(row["product_amount"]);
                }
                lbl_oldReturnsPrice.Text = totalPrice.ToString("#0.00") + " TL";
            }
            this.totalReturnPrice= totalPrice;
         }
         private void SetIncomeRatio()
         {
             double total_cost = Convert.ToDouble(this.gridColumn8.SummaryItem.SummaryValue);
             double total_income = Convert.ToDouble(this.gridColumn5.SummaryItem.SummaryValue);

             if (total_cost == 0)
             {
                 this.lbl_yuzdelik.Text = "% 0";
             }
             else
             {
                 double ratio = (total_income - total_cost) / total_cost * 100;
                 this.lbl_yuzdelik.Text = "% " + ratio.ToString("#0.0");
             }
         }
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            ((MainForm)this.ParentForm).SettingStatus();
            this.Dispose();
        }

        private void date_EditValueChanged(object sender, EventArgs e)
        {
            current_date = date.DateTime;
            lbl_date.Text = current_date.ToString("d.MM.yyyy");
                
            FillGrid();
        }
    }
}
