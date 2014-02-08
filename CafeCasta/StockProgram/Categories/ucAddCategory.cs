using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StockProgram.DBObjects;
using StockProgram.ErrorMessages;
using System.Diagnostics;

namespace StockProgram.Categories
{
    public partial class ucAddCategory : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void CategoryHandler(object sender, EventArgs e);
        public event CategoryHandler CategoryTreeChanged;
        private StockProgram.ControlHelper controlHelper;
        private ExceptionLogger excLogger;
        private DevExpress.XtraTreeList.TreeList c_tree;
        private int tree_type;
        public ucAddCategory(int type,ref DevExpress.XtraTreeList.TreeList category_tree)
        {
            this.tree_type = type;// gider mi ürün mü? 1: Ürün; 2:Gider
            this.c_tree = category_tree;
            c_tree.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(c_tree_FocusedNodeChanged);
            InitializeComponent();
        }

        void c_tree_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            string cat_name=((System.Data.DataRowView)(c_tree.GetDataRecordByNode(c_tree.FocusedNode))).Row.ItemArray[2].ToString();
            //txt_bagli_kategori.Text = cat_name;
            cb_bagli_kategori.Properties.Items.Clear();
            cb_bagli_kategori.Properties.Items.Add("En Üst Kategori");
            cb_bagli_kategori.Properties.Items.Add(cat_name);
            cb_bagli_kategori.Text = cat_name;
        }
      
        private CategoryItem CItem;  // my category items in cbox
        private List<CategoryItem> CItemList; // my category item list

        private void btn_ekle_Click(object sender, EventArgs e)
        {
              ErrorMessages.Message m;
              MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn); 
              string strSQL = "";
              string Name = txt_kategoriAdi.Text.Trim().ToUpper() ;
            
            if (Name=="")
              {
                  m = new ErrorMessages.Message();
                  if (m.WriteMessage("Kategori adını boş bırakmayınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                      return;
              }
            DataRow dr = (c_tree.FocusedNode == null) ? null: ((System.Data.DataRowView)(c_tree.GetDataRecordByNode(c_tree.FocusedNode))).Row ; 
            int ParentId=0;
     
            if (cb_bagli_kategori.SelectedIndex != 0)
            {
                if (this.tree_type==1)//ürün
                {
                    ParentId = Convert.ToInt32(dr["cat_id"]);
                    strSQL = "insert into category_details (cat_name,parent_id,display_order,modified_date) values (@name," + ParentId + "," + Convert.ToInt16(txt_display_order.Text) + ",@modified_date)";                  
                }
                else if (this.tree_type == 2) //gider
                {
                    ParentId = Convert.ToInt32(dr["process_id"]);
                    strSQL = "insert into process_details (process_name,parent_id,modified_date) values (@name," + ParentId + "," + Convert.ToInt16(txt_display_order.Text) + ", @modified_date)";
                               
                }
            }
            else if (this.tree_type == 1)//ürün
            {
                ParentId = 0;
                strSQL = "insert into category_details (cat_name,parent_id, display_order, modified_date) values (@name," + ParentId + ","+ Convert.ToInt16(txt_display_order.Text) +", @modified_date)";
            }
            else if (this.tree_type == 2) //gider
            {
                ParentId = 0;
                strSQL = "insert into process_details (process_name,parent_id, display_order, modified_date) values (@name," + ParentId + ","+ Convert.ToInt16(txt_display_order.Text) + ", @modified_date)";

            }

            //  ParentId = CItemList[index].Id;
              try
              {
                  db.CreateSetParameter("name", MySql.Data.MySqlClient.MySqlDbType.VarChar, Name);
                  db.CreateSetParameter("modified_date", MySql.Data.MySqlClient.MySqlDbType.DateTime, DateTime.Now);
                  db.ExecuteNonQuery(strSQL);
                  m = new ErrorMessages.Message();
                  m.WriteMessage("Kategori, başarılı bir şekilde eklendi.", MessageBoxIcon.Information, MessageBoxButtons.OK);
                  txt_kategoriAdi.Text = "";
                  txt_display_order.Text = "0";
                  FillCategory();
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
          
              return;
        }

        private int FillCategory()
        {
              MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn); 
              DataTable dt = new DataTable();
              int retValue = 0;
              controlHelper = new ControlHelper();
            try
            {
                //cb_bagliKategori.Properties.Items.Clear();
                string strSQL = string.Empty;
                if (this.tree_type==1)
                {
                    strSQL = "select cat_id,parent_id,cat_name from category_details order by display_order,cat_name asc";
                }
                else
                    strSQL = "select process_id,parent_id,process_name from process_details order by display_order,process_name asc";

                dt = db.GetDataTable(strSQL);
                InitializeCategoryItems(ref dt);
                dt.Dispose();
                retValue = 1;
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
            //    excLogger = new ExceptionLogger(e.Message, excSource);// DB ye log yaz
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

        private bool IsErrorMailSent=false;
        public void excMail_MailSent(object sender, EventArgs e)
        {  // mail sent event handler
            IsErrorMailSent = true;
        }

        private void InitializeCategoryItems(ref DataTable dt)
        {
            CItemList = new List<CategoryItem>();
            CItemList.Add(new CategoryItem{ParentId=0,Id=0,Name="En Üst Kategori"});
            cb_bagli_kategori.Properties.Items.Add("En Üst Kategori");
            cb_bagli_kategori.Text = "En Üst Kategori";

            foreach (DataRow row in dt.Rows)
            {
                CItem = new CategoryItem();
                CItem.ParentId = Convert.ToInt32(row["parent_id"].ToString());
                if (this.tree_type == 1)
                {
                    CItem.Id = Convert.ToInt32(row["cat_id"].ToString());
                    CItem.Name = row["cat_name"].ToString();
                }
                else
                {
                    CItem.Id = Convert.ToInt32(row["process_id"].ToString());
                    CItem.Name = row["process_name"].ToString();
                }         
                CItemList.Add(CItem);
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            if (this.tree_type==1)
            {
                Parent.Controls["pnl_urun_category"].Visible = true;
            }
            else if (this.tree_type==2)
            {
                Parent.Controls["pnl_gider_category"].Visible = true;
            }
           
            this.Dispose();
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
        private void ucAddCategory_Load(object sender, EventArgs e)
        {
            FillCategory();
        }

        private void txt_kategoriAdi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btn_ekle_Click(sender, e);
            }
        }

        private void cb_bagli_kategori_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btn_ekle_Click(sender, e);
            }
        }

    }
}
