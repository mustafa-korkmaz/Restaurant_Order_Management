using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StockProgram.DBObjects;
using DevExpress.XtraGrid;
using System.Diagnostics;
using StockProgram.Categories;

namespace StockProgram.Reports
{
    public partial class ucTimeBased : DevExpress.XtraEditors.XtraUserControl
    {
        private bool categoryFirstLoad;
        private ExceptionLogger excLogger;
        private CategoryItem CItem;  // my category items in cbox
        private List<CategoryItem> CItemList; // my category item list

        public ucTimeBased()
        {
            InitializeComponent();
            this.categoryFirstLoad = true;
            FillCategory();
            this.date.DateTime = DateTime.Now;
            FillGrid(SetSqlQuery());
        }

        private void InitializeCategoryItems(ref DataTable dt)
        {
            CItemList = new List<CategoryItem>();
            // CItemList.Add(new CategoryItem { ParentId = 0, Id = 0, Name = "En Üst Kategori" });
            foreach (DataRow row in dt.Rows)
            {
                CItem = new CategoryItem();
                CItem.Id = Convert.ToInt32(row["cat_id"].ToString());
                CItem.ParentId = Convert.ToInt32(row["parent_id"].ToString());
                CItem.Name = row["cat_name"].ToString();
                CItemList.Add(CItem);
            }
        }

        private int FillCategory()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            int retValue = 0;
            //controlHelper = new ControlHelper();
            try
            {
                string strSQL = "select cat_id,parent_id,cat_name from category_details order by display_order ,cat_name asc";
                dt = db.GetDataTable(strSQL);
                InitializeCategoryItems(ref dt);
                FillCategoryTree(ref dt);
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

        private void FillCategoryTree(ref DataTable dt)
        {
            tree_category.DataSource = dt;
        }

        private void btn_urunIzle_Click(object sender, EventArgs e)
        {
            if (date.Text == "")
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Tarih seçiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
            }
            else
            {
               FillGrid( SetSqlQuery());
            }
        }

        /// <summary>
        /// sql sorgusunu hazırlar
        /// </summary>
        /// <returns></returns>
        private string SetSqlQuery()
        {

          string date = this.date.DateTime.AddDays(1).ToString("yyyy-MM-dd");
          string sql = "select vp.product_name, vp.product_img_path,vp.product_code_manual,vp.product_cat,vp.cat_name,vp.color_name,T1.*,T3.product_return_count,T2.sell_amount,T2.avg_sell_price,T2.total_sell_price from"+
        " (select stocks.*,lbp.last_buy_price from v_last_buy_price lbp inner join "+
        " (select t1.product_id as product_id,sum(t1.product_count)as total_buy_amount "+
        " from (SELECT * from buy_list where modified_date<'" + date + "') t1 group by t1.product_id) stocks " +
        " on(stocks.product_id=lbp.product_id)) T1 "+
        " left join (select t1.product_id,sum(t1.product_amount)as sell_amount,sum(t1.product_price*t1.product_amount) as total_sell_price "+
        " ,(sum(t1.product_price*t1.product_amount)/sum(t1.product_amount)) as avg_sell_price "+
        " from (SELECT * FROM `sell_list` WHERE  modified_date<'"+date+"') t1 group by t1.product_id) T2 on(T1.product_id=T2.product_id) "+
        " left join (SELECT `product_id`,sum(`product_count`) AS product_return_count FROM `product_return` WHERE modified_date<'" + date + "' group by product_id) T3 " +
        " on (T1.product_id=T3.product_id) inner join v_products vp on(vp.product_id=T1.product_id)";
          return sql;
        }
        private void FillGrid()
        {
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select vpa.*,pv.buy_price,(pv.product_buy_amount-pv.product_return_count) as product_net_buy_amount,((pv.product_buy_amount-pv.product_return_count)*pv.buy_price) as total_buy_price from v_product_view pv inner join product_details pd on(pv.product_id=pd.product_id) inner join v_product_amount vpa on (vpa.product_id=pv.product_id) order by vpa.modified_date desc";

            DataTable dt = new DataTable();
            dt = cmd.GetDataTable(strSQL);
            CategoryFamilyTree cft = new CategoryFamilyTree(ref this.CItemList);

            try
            {
                dt.Columns.Add("top_cat_name", typeof(String));// ana kategori adı için
                for (int i = 0; i < dt.Rows.Count; i++) // fill  top category names
                {
                    dt.Rows[i]["top_cat_name"] = cft.GetTopCategoryItem(Convert.ToInt32(dt.Rows[i]["product_cat"])).Name;
                }

                dt.Columns.Add("Image", typeof(Image));
                Image myImage;

                string file_name;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    file_name = Application.StartupPath + StaticObjects.MainImagePath + dt.Rows[i]["product_img_path"].ToString();
                    if (System.IO.File.Exists(file_name))
                    {
                        myImage = Image.FromFile(file_name);
                        myImage = StaticObjects.ResizeImage(myImage, 150, 70);
                        dt.Rows[i]["Image"] = myImage;
                    }
                    else continue;
                }

                gridControl1.DataSource = dt;
                SetConditionalFormatting("[count]");
                gridView1.ShowFindPanel();
                gridView1.Columns["Image"].ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
                gridView1.RowHeight = 70;
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, InsertProduct() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
        }

        private void FillGrid(string sql)
        {
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();       
            CategoryFamilyTree cft = new CategoryFamilyTree(ref this.CItemList);

            try
            {
                dt = cmd.GetDataTable(sql);
                dt.Columns.Add("top_cat_name", typeof(String));// ana kategori adı için
                dt.Columns.Add("product_net_amount",typeof(Int32));
                dt.Columns.Add("total_cost", typeof(Double));
                for (int i = 0; i < dt.Rows.Count; i++) // fill  top category names
                {
                    dt.Rows[i]["top_cat_name"] = cft.GetTopCategoryItem(Convert.ToInt32(dt.Rows[i]["product_cat"])).Name;
                    if (dt.Rows[i]["total_buy_amount"].ToString() == "")
                    {
                        dt.Rows[i]["total_buy_amount"] = 0;
                    }
                    if (dt.Rows[i]["last_buy_price"].ToString() == "")
                    {
                        dt.Rows[i]["last_buy_price"] = 0;
                    }
                    if (dt.Rows[i]["product_return_count"].ToString()=="")
                    {
                        dt.Rows[i]["product_return_count"] = 0;
                    }
                    if (dt.Rows[i]["sell_amount"].ToString() == "")
                    {
                        dt.Rows[i]["sell_amount"] = 0;
                    }
                    if (dt.Rows[i]["avg_sell_price"].ToString() == "")
                    {
                        dt.Rows[i]["avg_sell_price"] = 0;
                    }
                    if (dt.Rows[i]["total_sell_price"].ToString() == "")
                    {
                        dt.Rows[i]["total_sell_price"] = 0;
                    }
                    dt.Rows[i]["product_net_amount"] =Convert.ToInt32(dt.Rows[i]["total_buy_amount"])-Convert.ToInt32( dt.Rows[i]["sell_amount"]) -Convert.ToInt32(dt.Rows[i]["product_return_count"]);
                    dt.Rows[i]["total_cost"] = Convert.ToInt32(dt.Rows[i]["product_net_amount"]) * Convert.ToInt32(dt.Rows[i]["last_buy_price"]);                
                    
                }

                dt.Columns.Add("Image", typeof(Image)); //resim için
                Image myImage;

                string file_name;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    file_name = Application.StartupPath + StaticObjects.MainImagePath + dt.Rows[i]["product_img_path"].ToString();
                    if (System.IO.File.Exists(file_name))
                    {
                        myImage = Image.FromFile(file_name);
                        myImage = StaticObjects.ResizeImage(myImage, 150, 70);
                        dt.Rows[i]["Image"] = myImage;
                    }
                    else continue;
                }

                gridControl1.DataSource = dt;
                SetConditionalFormatting("[count]");
                gridView1.ShowFindPanel();
                gridView1.Columns["Image"].ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
                gridView1.RowHeight = 70;
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
                cmd.Close();
            }
        }

        private void SetConditionalFormatting(string columnName)
        {
            StyleFormatCondition condition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            condition1.Appearance.ForeColor = System.Drawing.Color.Red;
            condition1.Appearance.Options.UseForeColor = true;
            condition1.Condition = FormatConditionEnum.Expression;
            condition1.Expression = columnName + "<=0";
            gridView1.FormatConditions.Add(condition1);
        }

        private void tree_category_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (!categoryFirstLoad)
            {
                if (date.Text=="")
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Tarih seçiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    return;
                }
                DataRow dr = ((System.Data.DataRowView)(tree_category.GetDataRecordByNode(tree_category.FocusedNode))).Row;
                int cat_id = Convert.ToInt32(dr["cat_id"]);

                List<int> familyIdList = new List<int>();
                CategoryFamilyTree cft = new CategoryFamilyTree(cat_id, ref CItemList);
                familyIdList = cft.GetFamilyMemberIDs();
                string strSQL = this.SetSqlQuery()+" where ";
                
                for (int i = 0; i < familyIdList.Count; i++)
                {
                    if (i == familyIdList.Count - 1)
                    {
                        strSQL += " vp.product_cat=" + familyIdList[i];
                    }
                    else
                        strSQL += " vp.product_cat=" + familyIdList[i] + " or ";
                }
                strSQL += " order by vp.product_id asc";

                FillGrid(strSQL);
            }
            else categoryFirstLoad = !categoryFirstLoad;
        }

        private void ucTimeBasedStocksReport_Load(object sender, EventArgs e)
        {
            gridView1.GroupPanelText = StaticObjects.GroupPanelText;
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Reports.ucReportMainPage ctrl = new Reports.ucReportMainPage();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }
    }
}
