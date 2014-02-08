using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StockProgram.DBObjects;
using StockProgram.Categories;
using System.Diagnostics;
using DevExpress.XtraGrid;

namespace StockProgram.Reports
{
    public partial class ucProductTracking : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        private  ControlHelper controlHelper;
        private bool categoryFirstLoad;
        private CategoryItem CItem;  // my category items in cbox
        private List<CategoryItem> CItemList; // my category item list
  
        public ucProductTracking()
        {
            InitializeComponent();
            this.categoryFirstLoad = true;             
            controlHelper = new ControlHelper();
            FillCategory();
            FillGrid();
        }

        private int FillCategory()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            int retValue = 0;
            controlHelper = new ControlHelper();
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

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

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

        private void FillCategoryTree(ref DataTable dt)
        {
            tree_category.DataSource = dt;
        }

        private void ucProductTracking_Load(object sender, EventArgs e)
        {
           // FillProducts();
            gridView1.GroupPanelText = StaticObjects.GroupPanelText;
        }

        private void cb_urunAdi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_urunIzle_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void btn_yazdir_Click(object sender, EventArgs e)
        {
            DevExpress.XtraPrinting.PrintableComponentLink a = new DevExpress.XtraPrinting.PrintableComponentLink();
            a.Component = gridControl1;
            printingSystem1.Links.Clear();
            printingSystem1.Links.Add(a);
            a.ShowPreview();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
           // ClearControl(pnl_main);
            Reports.ucReportMainPage ctrl = new Reports.ucReportMainPage();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();

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
            dt = cmd.GetDataTable(sql);
            CategoryFamilyTree cft=new CategoryFamilyTree(ref this.CItemList);

            try
            {
                dt.Columns.Add("top_cat_name", typeof(String));// ana kategori adı için
                for (int i = 0; i < dt.Rows.Count; i++) // fill  top category names
                {
                    dt.Rows[i]["top_cat_name"] = cft.GetTopCategoryItem(Convert.ToInt32(dt.Rows[i]["product_cat"])).Name;
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

        private void tree_category_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (!categoryFirstLoad)
            {
                DataRow dr = ((System.Data.DataRowView)(tree_category.GetDataRecordByNode(tree_category.FocusedNode))).Row;
                int cat_id = Convert.ToInt32(dr["cat_id"]);

                List<int> familyIdList = new List<int>();
                CategoryFamilyTree cft = new CategoryFamilyTree(cat_id,ref CItemList);
                familyIdList = cft.GetFamilyMemberIDs();
                string strSQL = "select vpa.*,pv.buy_price,(pv.product_buy_amount-pv.product_return_count) as product_net_buy_amount,((pv.product_buy_amount-pv.product_return_count)*pv.buy_price) as total_buy_price from v_product_view pv inner join product_details pd on(pv.product_id=pd.product_id) inner join v_product_amount vpa on (vpa.product_id=pv.product_id) where ";
                for (int i = 0; i < familyIdList.Count; i++)
                {
                    if (i==familyIdList.Count-1)
                    {
                          strSQL += " vpa.product_cat=" + familyIdList[i];         
                    }
                    else
                    strSQL += " vpa.product_cat=" + familyIdList[i]+" or ";                
                }
                strSQL += " order by vpa.modified_date desc";

                FillGrid(strSQL);
            }
            else categoryFirstLoad = !categoryFirstLoad;
        }
    
    }
}
