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

namespace StockProgram.Reports
{
    public partial class ucKarYuzdelik : DevExpress.XtraEditors.XtraUserControl
    {
        private bool firstLoad;
        private DataTable reportTable;
        private ExceptionLogger excLogger;
        private DateTime current_date;
        public ucKarYuzdelik()
        {
            InitializeComponent();
          //  SetReportTable();
        }

        private void SetReportTable()
        { //tablo yapısını hazırlayalım
            this.reportTable = new DataTable("ReportTable");
            reportTable.Columns.Add("date_counter", typeof(String));
            reportTable.Columns.Add("date_period", typeof(String));
            reportTable.Columns.Add("total_sale_amount", typeof(Int32));
            reportTable.Columns.Add("sell_outgoing", typeof(Double));
            reportTable.Columns.Add("sell_income", typeof(Double));
            reportTable.Columns.Add("pesin", typeof(Double));
            reportTable.Columns.Add("pos", typeof(Double));
            reportTable.Columns.Add("veresiye", typeof(Double));
            reportTable.Columns.Add("returns", typeof(Double)); //iadeler için bir kolon ekleyelim
            reportTable.Columns.Add("hediye", typeof(Double));
            reportTable.Columns.Add("profit_ratio", typeof(String));
        }

        private void ucGunlukSatis_Load(object sender, EventArgs e)
        {
            firstLoad = true;
           // lbl_date.Text = DateTime.Now.ToString("d.MM.yyyy");
            date_to.DateTime = DateTime.Now;
            current_date = DateTime.Now;
           // FillGrid();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            System.TimeSpan day = new System.TimeSpan(1, 0, 0, 0);
            current_date =  current_date.Add(day);
         //   lbl_date.Text = current_date.ToString("d.MM.yyyy");
         //   FillGrid();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            System.TimeSpan day = new System.TimeSpan(1, 0, 0, 0);
            current_date = current_date.Subtract(day);
        //    lbl_date.Text = current_date.ToString("d.MM.yyyy");
        //    FillGrid();
        }

        /// <summary>
        /// fills the grid between the given date time
        /// </summary>
        /// <param name="dateTime"></param>
        private void AddRow(int rowCounter, string time,DateTime d1,DateTime d2)
        {
            string _d1= d1.ToString("yyyy-MM-dd");
            string _d2 = d2.AddDays(+1).ToString("yyyy-MM-dd");

            string strSQL = "select sum(`bsr`.`total_sale_amount`) AS `total_sale_amount`,sum(`bsr`.`sell_outgoing`) AS `sell_outgoing`,sum(`bsr`.`sell_income`) AS `sell_income`,sum(ifnull(`pd`.`pesin`,0)) AS `pesin`,sum(ifnull(`pd`.`pos`,0)) AS `pos`,sum(ifnull(`pd`.`veresiye`,0)) AS `veresiye`, sum(if(pd.hediye>0,bsr.sell_outgoing,0)) as hediye from (`v_buy_sell_report_master` `bsr` left join `v_payment_details` `pd` on((`pd`.`sell_id` = `bsr`.`sell_id`)))" +
          " where modified_date between '"+_d1+"' and  '"+_d2+"'";    
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            DataRow dr = reportTable.NewRow();
            try
            {
                dt = db.GetDataTable(strSQL);
             //   SetReturnColumn(ref dt); //iade kolonu verilerini hazırla

                if (dt.Rows.Count <= 0)
                {
                    dr = (DataRow)null;
                }
                else
                {
                    dr["date_counter"] = rowCounter+". "+time;
                    dr["date_period"] = d1.ToString("d.MM.yyyy")+ " - "+d2.ToString("d.MM.yyyy");
                    dr["sell_outgoing"] = (dt.Rows[0]["sell_outgoing"].ToString()!="" )? dt.Rows[0]["sell_outgoing"]: 0;
                    dr["sell_income"] = (dt.Rows[0]["sell_income"].ToString()!="")? dt.Rows[0]["sell_income"]:0;
                    dr["total_sale_amount"] =(dt.Rows[0]["total_sale_amount"].ToString()!="")? dt.Rows[0]["total_sale_amount"]:0;
                    dr["pesin"] = (dt.Rows[0]["pesin"].ToString()!="")? dt.Rows[0]["pesin"]: 0;
                    dr["pos"] = (dt.Rows[0]["pos"].ToString() != "") ? dt.Rows[0]["pos"] : 0;
                    dr["veresiye"] = (dt.Rows[0]["veresiye"].ToString()!="")?dt.Rows[0]["veresiye"]:0;
                    dr["returns"] = -(Convert.ToDouble(dr["pesin"]) + Convert.ToDouble(dr["pos"]) + Convert.ToDouble(dr["veresiye"]) - Convert.ToDouble(dr["sell_income"]));
                    dr["hediye"] = (dt.Rows[0]["hediye"].ToString() != "") ? dt.Rows[0]["hediye"] : 0;
                    dr["profit_ratio"]=SetIncomeRatioInRow(Convert.ToDouble(  dr["sell_outgoing"]),Convert.ToDouble(  dr["sell_income"]));// her satırdaki karlılık yüzdesini hesapla
                    reportTable.Rows.Add(dr);
                }
          //      SetProductCodesColumn(ref ds);// ürün kodlarını barındıran kolonu hazırla
                //gridView1.RowHeight = 35;
       
                //SetOldReturnsPrice();//Eski tarihli iadeler varsa ekrana bildirim olarak göster.
                //SetIncomeRatio();//Karlılık yüzdesi hesapla
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
            return;

        }

        private string SetIncomeRatioInRow(double total_cost, double total_income)
        {
            string _ratio = string.Empty;
            if (total_cost == 0)
            {
               _ratio = "% 0";
            }
            else
            {
                double ratio = (total_income - total_cost) / total_cost * 100;
                _ratio= "% " + ratio.ToString("#0.0");
            }
            return _ratio;
        }

        /// <summary>
        /// details gridinin kolonlarını ve özelliklerini set eder
        /// </summary>
        /// <param name="viewHeader"></param>
        /// <param name="ds"></param>
        /*private void SetDetailsView(string viewHeader, ref DataSet ds)
        {
            //add double click event
            detailsView.DoubleClick += new EventHandler(detailsView_DoubleClick);
            detailsView.ViewCaption = viewHeader;
            detailsView.PopulateColumns(ds.Tables[1]);
            detailsView.Columns["product_color"].VisibleIndex = -1;
            // detailsView.Columns["cat_name"].VisibleIndex = -1;
            //   detailsView.Columns["product_code"].VisibleIndex = -1;
           // detailsView.Columns["product_code_manual"].VisibleIndex = -1;
            detailsView.Columns["product_cat"].VisibleIndex = -1;
            detailsView.Columns["modified_date"].VisibleIndex = -1;
            //   detailsView.Columns["product_name"].VisibleIndex = -1;
            detailsView.Columns["product_id"].VisibleIndex = -1;
            detailsView.Columns["sell_desc"].VisibleIndex = -1;

            //product_name column
            detailsView.Columns["product_name"].Caption = "Ürün Adı";
            detailsView.Columns["product_name"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_name"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["product_name"].VisibleIndex = 1;

            //product_price column
            detailsView.Columns["product_price"].Caption = "Birim Satış Fiyatı";
            detailsView.Columns["product_price"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_price"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["product_price"].VisibleIndex = 8;

            //buy_price column
            detailsView.Columns["buy_price"].Caption = "Birim Alış Fiyatı";
            detailsView.Columns["buy_price"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["buy_price"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["buy_price"].VisibleIndex = 6;

            //product_code_manual column
            detailsView.Columns["product_code_manual"].Caption = "Ürün Kodu";
            detailsView.Columns["product_code_manual"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_code_manual"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["product_code_manual"].VisibleIndex = 2;

            //sell_id column
            detailsView.Columns["sell_id"].Caption = "Satış Numarası";
            detailsView.Columns["sell_id"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["sell_id"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["sell_id"].VisibleIndex = 0;

            //color_name column
            detailsView.Columns["color_name"].Caption = "Renk";
            detailsView.Columns["color_name"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["color_name"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["color_name"].VisibleIndex = 4;

            //cat_name column
            detailsView.Columns["cat_name"].Caption = "Kategori";
            detailsView.Columns["cat_name"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["cat_name"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["cat_name"].VisibleIndex = 3;

            //product_size column
            detailsView.Columns["product_size"].Caption = "Numara";
            detailsView.Columns["product_size"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_size"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["product_size"].VisibleIndex = 5;

            //product_amount column
            detailsView.Columns["product_amount"].Caption = "Miktar";
            detailsView.Columns["product_amount"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_amount"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["product_amount"].VisibleIndex = 7;

            //cost column
            detailsView.Columns["cost"].Caption = "Satış Maliyeti";
            detailsView.Columns["cost"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["cost"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["cost"].VisibleIndex = 9;

            //sale column
            detailsView.Columns["sale"].Caption = "Satış Tutarı";
            detailsView.Columns["sale"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["sale"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["sale"].VisibleIndex = 10;

            detailsView.BestFitColumns();
            detailsView.OptionsView.ShowGroupPanel = false;

        }*/

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

        private void SetReturnColumn(ref DataTable dt)
        {
            dt.Columns.Add("returns", typeof(Double)); //iadeler için bir kolon ekleyelim

            foreach (DataRow row in dt.Rows) //değerleri hesapla
            {
                row["returns"] = -(Convert.ToDouble(row["pesin"]) + Convert.ToDouble(row["pos"]) + Convert.ToDouble(row["veresiye"])-Convert.ToDouble(  row["sell_income"]));
            }
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
            strSQL += " SELECT * FROM `sell_return`   where modified_date between '" + dayOne + "' and  '" + dayTwo + "'";
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt=db.GetDataTable(strSQL);
            if (dt.Rows.Count == 0)
            {
                lbl_oldReturnsPrice.Text ="0.0 TL";
                return;
            }
            else
            {
                double returnPrice = 0;
                foreach (DataRow row in dt.Rows)
                {
                    returnPrice += Convert.ToDouble(row["product_price"]) * Convert.ToDouble(row["product_amount"]);
                }
                lbl_oldReturnsPrice.Text = returnPrice.ToString("#0.0")+" TL";
            }
            
         }
         private double SetIncomeRatio()
         {
             double total_cost = Convert.ToDouble(this.gridColumn8.SummaryItem.SummaryValue);
             double total_income = Convert.ToDouble(this.gridColumn5.SummaryItem.SummaryValue);

             if (total_cost == 0)
             {
                 return 0;
             }
             else
             {
                 return  (total_income - total_cost) / total_cost * 100;
             //    this.lbl_yuzdelik.Text = "% " + ratio.ToString("#0.0");
             }
         }
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            Reports.ucReportMainPage ctrl = new Reports.ucReportMainPage();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }

        private void chk_hafta_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_hafta.CheckState==CheckState.Checked)
            {
                chk_ay.CheckState = CheckState.Unchecked;
                chk_yil.CheckState = CheckState.Unchecked;
                chk_gun.CheckState = CheckState.Unchecked;
            }
        }

        private void chk_ay_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ay.CheckState == CheckState.Checked)
            {
                chk_gun.CheckState = CheckState.Unchecked;
                chk_hafta.CheckState = CheckState.Unchecked;
                chk_yil.CheckState = CheckState.Unchecked;
            }
        }

        private void chk_yil_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_yil.CheckState == CheckState.Checked)
            {
                chk_gun.CheckState = CheckState.Unchecked;
                chk_ay.CheckState = CheckState.Unchecked;
                chk_hafta.CheckState = CheckState.Unchecked;
            }
        }

        private void btn_izle_Click(object sender, EventArgs e)
        {
            #region validations
            //Tarih kontrol
            if (date_to.Text == "" || date_from.Text == "")
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Tarih aralığını boş bırakmayınız.", MessageBoxIcon.Error, MessageBoxButtons.OK);
                return;
            }
            else if (date_from.DateTime.Subtract(date_to.DateTime).Days>=0)
            {
                 ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Soldaki tarih kutusuna eski tarihi, sağdakine yeni tarihi giriniz.", MessageBoxIcon.Error, MessageBoxButtons.OK);
                return;
            }
            //ChkBox kontrol
            if (chk_gun.CheckState == CheckState.Unchecked && chk_hafta.CheckState == CheckState.Unchecked && chk_ay.CheckState == CheckState.Unchecked && chk_yil.CheckState == CheckState.Unchecked)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Zaman dilimi seçiniz.", MessageBoxIcon.Error, MessageBoxButtons.OK);
                return;
            }
            #endregion
            SetReportTable();
            List<Period> periodList = new List<Period>();
            TimePeriod tp = new TimePeriod();
        
            if (chk_gun.CheckState == CheckState.Checked)
            {
                tp = TimePeriod.Gunluk;
            }

            if (chk_ay.CheckState==CheckState.Checked)
            {
                tp = TimePeriod.Aylik;
            }
            else if (chk_hafta.CheckState == CheckState.Checked)
            {
                tp = TimePeriod.Haftalik;
            }
            else if (chk_yil.CheckState == CheckState.Checked)
            {
                tp = TimePeriod.Yillik;
            }
            Periods p = new Periods(date_from.DateTime,date_to.DateTime, tp);
            periodList= p.GetPeriodList();
       
            int counter = 1;
            foreach (Period period in periodList)
            {         
                 AddRow(counter, p.Time, period.FromDate, period.ToDate);
                 counter++;
            }

            gridView1.RowHeight = 35;
            gridView1.BestFitColumns();
            gridControl1.DataSource = reportTable ;
         //   SetOldReturnsPrice();//Eski tarihli iadeler varsa ekrana bildirim olarak göster.
            SetIncomeRatio();//Karlılık yüzdesi hesapla
        }

        double total_profit_ratio = 0;
        private void gridView1_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            GridColumnSummaryItem item = e.Item as GridColumnSummaryItem;
            GridView view = sender as GridView;
            if (Equals("total_profit_ratio", item.Tag))
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                    total_profit_ratio = 0;
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                {
                    total_profit_ratio = SetIncomeRatio();
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = total_profit_ratio.ToString("#0.0");
            }
        }

        private void chk_gun_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_gun.CheckState == CheckState.Checked)
            {
                chk_ay.CheckState = CheckState.Unchecked;
                chk_hafta.CheckState = CheckState.Unchecked;
                chk_yil.CheckState = CheckState.Unchecked;
            }
        }
    }
}
