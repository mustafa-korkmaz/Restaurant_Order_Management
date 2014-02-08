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

namespace StockProgram.Categories
{
    public partial class ucCategoriesMasterPage : DevExpress.XtraEditors.XtraUserControl
    {
        public ucCategoriesMasterPage()
        {
            InitializeComponent();
            FillCategory();
            FillCategoryGider();
        }
        private ExceptionLogger excLogger;
        private StockProgram.ControlHelper controlHelper;
        private CategoryItem CItem;  // my category items in cbox
        private List<CategoryItem> CItemList; // my category item list
        private List<CategoryItem> GiderItemList; // my gider item list

        private int FillCategory()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            int retValue = 0;
            controlHelper = new ControlHelper();
            try
            {
              //  cb_bagliKategori.Properties.Items.Clear();
                string strSQL = "select cat_id,parent_id,cat_name,display_order from category_details where is_deleted=0 order by display_order,cat_name asc";
                dt = db.GetDataTable(strSQL);
                InitializeCategoryItems(ref dt);
                FillCategoryTree(1,ref dt);
                dt.Dispose();
                retValue = 1;
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
              //  excLogger = new ExceptionLogger(e.Message, excSource);// DB ye log yaz
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

        private int FillCategoryGider()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            int retValue = 0;
            controlHelper = new ControlHelper();
            try
            {
                //  cb_bagliKategori.Properties.Items.Clear();
                string strSQL = "select process_id,parent_id,process_name,display_order from process_details where display_order<>-1 order by display_order,process_name asc";
                dt = db.GetDataTable(strSQL);
                InitializeGiderCategoryItems(ref dt);
                FillCategoryTree(2,ref dt);
                dt.Dispose();
                retValue = 1;
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                //  excLogger = new ExceptionLogger(e.Message, excSource);// DB ye log yaz
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

        private void FillCategoryTree(int tabIndex,ref DataTable dt)
        {
            if (tabIndex==1)
            {
                tree_category.DataSource = dt;              
            }
            else if  (tabIndex==2)
            {
                tree_gider.DataSource = dt;
            }
        }

        private void InitializeCategoryItems(ref DataTable dt)
        {
            CItemList = new List<CategoryItem>();
            CItemList.Add(new CategoryItem { ParentId = 0, Id = 0, Name = "En Üst Kategori" });
            foreach (DataRow row in dt.Rows)
            {
                CItem = new CategoryItem();
                CItem.Id = Convert.ToInt32(row["cat_id"].ToString());
                CItem.ParentId = Convert.ToInt32(row["parent_id"].ToString());
                CItem.Name = row["cat_name"].ToString();
                CItemList.Add(CItem);
            }
        }

        private void InitializeGiderCategoryItems(ref DataTable dt)
        {
            GiderItemList = new List<CategoryItem>();
            GiderItemList.Add(new CategoryItem { ParentId = 0, Id = 0, Name = "En Üst Kategori" });
            foreach (DataRow row in dt.Rows)
            {
                CItem = new CategoryItem();
                CItem.Id = Convert.ToInt32(row["process_id"].ToString());
                CItem.ParentId = Convert.ToInt32(row["parent_id"].ToString());
                CItem.Name = row["process_name"].ToString();
                GiderItemList.Add(CItem);
            }
        }

        private void btn_kategori_gir_Click_1(object sender, EventArgs e)
        {
            pnl_urun_category.Visible = false;
            Categories.ucAddCategory ctrl = new Categories.ucAddCategory(1,ref tree_category);
            ctrl.Dock = DockStyle.Fill;
            ctrl.CategoryTreeChanged += new ucAddCategory.CategoryHandler(ctrl_CategoryTreeChanged);
            this.spliter.Panel2.Controls.Add(ctrl);
        }

        protected void ctrl_CategoryTreeChanged(object sender, EventArgs e)
        {
            FillCategory();
            FillCategoryGider();
        }

        private void btn_kategori_sil_Click(object sender, EventArgs e)
        {
            ErrorMessages.Message msg = new ErrorMessages.Message();
            if (msg.WriteMessage("Kategori sistemden tamamen silinecektir.\nDevam etmek istiyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int cat_id = Convert.ToInt32(((System.Data.DataRowView)(tree_category.GetDataRecordByNode(tree_category.FocusedNode))).Row.ItemArray[0]);
                DeleteCat(cat_id,1);
            }
        }
        private void DeleteCat(int id,int type)
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string sql = "";
            try
            {
                if (type==1)
                {
                    sql = "update category_details set is_deleted=1 where cat_id=" + id;
                    db.ExecuteNonQuery(sql);
                    FillCategory();
                }
                else
                    if (type == 2)
                    {
                        sql = "update process_details set display_order=-1 where process_id=" + id;
                        db.ExecuteNonQuery(sql);
                        FillCategoryGider();         
                    }       
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new DBObjects.ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "CasTa Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at
            }
            finally
            {
                db.Close();
                db = null;
            }
        }

        private void btn_kategori_duzelt_Click(object sender, EventArgs e)
        {
            pnl_urun_category.Visible = false;
            int cat_id = Convert.ToInt32(((System.Data.DataRowView)(tree_category.GetDataRecordByNode(tree_category.FocusedNode))).Row.ItemArray[0]);
            Categories.ucCategoryEdit ctrl = new Categories.ucCategoryEdit(ref tree_category,cat_id,1);
            ctrl.CategoryTreeChanged += new ucCategoryEdit.CategoryHandler(ctrl_CategoryTreeChanged);
            ctrl.Dock = DockStyle.Fill;
            this.spliter.Panel2.Controls.Add(ctrl);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            pnl_gider_category.Visible = false;
            Categories.ucAddCategory ctrl = new Categories.ucAddCategory(2,ref tree_gider);
            ctrl.Dock = DockStyle.Fill;
            ctrl.CategoryTreeChanged += new ucAddCategory.CategoryHandler(ctrl_CategoryTreeChanged);
            this.splitGider.Panel2.Controls.Add(ctrl);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            pnl_urun_category.Visible = false;
            int cat_id = Convert.ToInt32(((System.Data.DataRowView)(tree_gider.GetDataRecordByNode(tree_gider.FocusedNode))).Row.ItemArray[0]);
            Categories.ucCategoryEdit ctrl = new Categories.ucCategoryEdit(ref tree_gider, cat_id, 2);
            ctrl.CategoryTreeChanged += new ucCategoryEdit.CategoryHandler(ctrl_CategoryTreeChanged);
            ctrl.Dock = DockStyle.Fill;
            this.splitGider.Panel2.Controls.Add(ctrl);
        }

        private void btn_gider_sil_Click(object sender, EventArgs e)
        {
            ErrorMessages.Message msg = new ErrorMessages.Message();
            if (msg.WriteMessage("Kategori sistemden tamamen silinecektir.\nDevam etmek istiyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int cat_id = Convert.ToInt32(((System.Data.DataRowView)(tree_gider.GetDataRecordByNode(tree_gider.FocusedNode))).Row.ItemArray[0]);
                DeleteCat(cat_id, 2);
            }
        }
 
    }

}

