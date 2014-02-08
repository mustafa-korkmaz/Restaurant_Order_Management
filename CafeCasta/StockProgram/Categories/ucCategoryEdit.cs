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
    public partial class ucCategoryEdit : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void CategoryHandler(object sender, EventArgs e);
        public event CategoryHandler CategoryTreeChanged;
        private StockProgram.ControlHelper controlHelper;
        private ExceptionLogger excLogger;
        int tree_type;
        private DevExpress.XtraTreeList.TreeList c_tree;
        private CategoryItem CItem;  // my category items in cbox
        private List<CategoryItem> CItemList; // my category item list
        int cat_id;

        public ucCategoryEdit(ref DevExpress.XtraTreeList.TreeList category_tree,int cat_id,int type)
        {
            this.c_tree = category_tree;
            this.cat_id = cat_id;
            this.tree_type = type;
            c_tree.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(c_tree_FocusedNodeChanged);
            InitializeComponent();
        }

        void c_tree_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            string cat_name = ((System.Data.DataRowView)(c_tree.GetDataRecordByNode(c_tree.FocusedNode))).Row.ItemArray[2].ToString();
            //txt_bagli_kategori.Text = cat_name;
            cb_bagli_kategori.Properties.Items.Clear();
            cb_bagli_kategori.Properties.Items.Add("En Üst Kategori");
            cb_bagli_kategori.Properties.Items.Add(cat_name);
            cb_bagli_kategori.Text = cat_name;
        }
        private void btn_back_Click(object sender, EventArgs e)
        {
            if (this.tree_type == 1)
            {
                Parent.Controls["pnl_urun_category"].Visible = true;
            }
            else if (this.tree_type == 2)
            {
                Parent.Controls["pnl_gider_category"].Visible = true;
            }

            this.Dispose();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            ErrorMessages.Message m;
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string strSQL = "";
            string Name = txt_kategoriAdi.Text.Trim().ToUpper();

            if (Name == "")
            {
                m = new ErrorMessages.Message();
                if (m.WriteMessage("Kategori adını boş bırakmayınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                    return;
            }
            DataRow dr = ((System.Data.DataRowView)(c_tree.GetDataRecordByNode(c_tree.FocusedNode))).Row;
          //  int cat_id;
            int ParentId=0;

            if (cb_bagli_kategori.SelectedIndex != 0)
            {
                if (cb_bagli_kategori.SelectedIndex ==-1)
                {
                      m = new ErrorMessages.Message();
                     if (m.WriteMessage("Bağlı kategori adını boş bırakmayınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                    return;
                }
                if (this.tree_type == 1)//ürün
                {
                    ParentId = Convert.ToInt32(dr["cat_id"]);
                    strSQL = "update category_details set cat_name=@cat_name, parent_id=@parent_id, display_order=@display_order,modified_date=@modified_date where cat_id=@cat_id;";
                }
                else if (this.tree_type == 2) //gider
                {
                    ParentId = Convert.ToInt32(dr["process_id"]);
                    strSQL = "update  process_details set process_name=@cat_name,parent_id=@parent_id, display_order=@display_order, modified_date=@modified_date where process_id=@cat_id;";

                }

            }
            else if (this.tree_type == 1)//ürün
            {
                ParentId = 0;
                strSQL = "update category_details set cat_name=@cat_name, parent_id=@parent_id, display_order=@display_order, modified_date=@modified_date where cat_id=@cat_id;";
            }
            else if (this.tree_type == 2) //gider
            {
                ParentId = 0;
                strSQL = "update  process_details set process_name=@cat_name,parent_id=@parent_id,modified_date=@modified_date where process_id=@cat_id;";

            }

            try
            {
                db.CreateSetParameter("modified_date", MySql.Data.MySqlClient.MySqlDbType.DateTime, DateTime.Now);
                db.CreateSetParameter("cat_name", MySql.Data.MySqlClient.MySqlDbType.VarChar,Name);
                db.CreateSetParameter("cat_id", MySql.Data.MySqlClient.MySqlDbType.Int32, this.cat_id);
                db.CreateSetParameter("parent_id", MySql.Data.MySqlClient.MySqlDbType.Int32, ParentId);
                db.CreateSetParameter("display_order", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt16(txt_display_order.Text));
                
                db.ExecuteNonQuery(strSQL);
                m = new ErrorMessages.Message();
                m.WriteMessage("Kategori, başarılı bir şekilde eklendi.", MessageBoxIcon.Information, MessageBoxButtons.OK);
                txt_kategoriAdi.Text = "";
                //FillCategory();
                OnCategoryTreeChanged(EventArgs.Empty); //fire the event

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

             btn_back_Click("" ,EventArgs.Empty);
            return;
        }
        /// <summary>
        /// fires when category tree successfully changed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCategoryTreeChanged(EventArgs e)
        {
            if (CategoryTreeChanged != null)
                CategoryTreeChanged(this, e);
        }

        private void ucCategoryEdit_Load(object sender, EventArgs e)
        {
            object[] array= ((System.Data.DataRowView)(c_tree.GetDataRecordByNode(c_tree.FocusedNode))).Row.ItemArray;
            txt_kategoriAdi.Text = array[2].ToString();
            txt_display_order.Text = array[3].ToString();
            int parentId = Convert.ToInt32(array[1]);
            txt_bagli_kategori.Text = GetParentCatName(parentId);
        }

        private string GetParentCatName(int parentId)
        {
            string cat_name=string .Empty;
            if (parentId == 0)
            {
                cat_name = "EN ÜST KATEGORİ";
            }
            else
            {
                MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
                cat_name = (this.tree_type == 1) ? db.ExecuteScalar("select cat_name from category_details where cat_id=" + parentId).ToString() : db.ExecuteScalar("select process_name from process_details where process_id=" + parentId).ToString();
                db.Close();
                db = null;
            }
         
            return cat_name;
        }
    }
}
