using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StockProgram.DBObjects;
using System.Diagnostics;

namespace StockProgram.Expenses
{
    public partial class frmEditExpense : DevExpress.XtraEditors.XtraForm
    {
        private ExceptionLogger excLogger;
        private bool categoryFirstLoad;
        private List<DBObjects.CategoryItem> CItemList;
        private CategoryItem CItem;  // my category items in cbox
        StockProgram.ControlHelper controlHelper;
        public delegate void SupplierGridHandler(object sender, EventArgs e);
        public event SupplierGridHandler SupplierGridChanged;
        private int p_id;
        private int p_type;
        private Gider g;

        public frmEditExpense(int p_id, int p_type)
        {
            InitializeComponent();
            this.p_id = p_id;
            this.p_type = p_type;
            this.categoryFirstLoad = true;
            frmSettings();
        }

        private void frmSettings()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmEditExpense_Load(object sender, EventArgs e)
        {
            FillGiderProperties();
            FillCategory();
        }

        private void FillGiderProperties()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = db.GetDataTable("select * from v_expense_details where payment_id= " + this.p_id + " and type=" + this.p_type);
            g = new Gider();
            g.Id = this.p_id;
            g.type = this.p_type;
            g.Price = Convert.ToDouble(dt.Rows[0]["payment_price"]);
            g.ProcessName = dt.Rows[0]["process_name"].ToString();
            g.Desc = dt.Rows[0]["payment_desc"].ToString();

            txt_tanim.Text = g.Desc;
            txt_kategori.Text = g.ProcessName;
            spin_fiyat.Value = Convert.ToDecimal(g.Price);

            InitializeCategoryItems(ref dt);

        }

        private void InitializeCategoryItems(ref DataTable dt)
        {
            CItemList = new List<CategoryItem>();
            // CItemList.Add(new CategoryItem { ParentId = 0, Id = 0, Name = "En Üst Kategori" });
            foreach (DataRow row in dt.Rows)
            {
                CItem = new CategoryItem();
                CItem.Id = Convert.ToInt32(row["payment_cat"].ToString());
                //  CItem.ParentId = Convert.ToInt32(row["parent_id"].ToString());
                CItem.Name = row["process_name"].ToString();
                CItemList.Add(CItem);
            }
        }

        private void UpdateExpense(ref Gider g)
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string strSQL = "update expense_details set payment_price=" + g.Price + ",payment_desc='" + g.Desc + "',payment_cat=" + g.ProcessId + " where  payment_id=" + this.p_id;
            try
            {
                // db.CreateSetParameter("modified_date", MySql.Data.MySqlClient.MySqlDbType.DateTime, DateTime.Now);
                db.ExecuteNonQuery(strSQL);
                OnSupplierGridChanged(EventArgs.Empty);// fire the gridChanged event
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at
            }
            finally
            {
                db.Close();
                db = null;
            }

           // Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }

        protected virtual void OnSupplierGridChanged(EventArgs e)
        {
            if (SupplierGridChanged != null)
                SupplierGridChanged(this, e);
        }

        private int FillCategory()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            int retValue = 0;
            controlHelper = new ControlHelper();
            try
            {
                string strSQL = "select process_id as cat_id,parent_id,process_name as cat_name from process_details where display_order<>-1 order by display_order ,process_name asc";
                dt = db.GetDataTable(strSQL);
                //InitializeCategoryItems(ref dt);
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

        private void tree_category_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (!categoryFirstLoad)
            {
                DataRow dr = ((System.Data.DataRowView)(tree_category.GetDataRecordByNode(tree_category.FocusedNode))).Row;
                string cat_name = dr["cat_name"].ToString();
                txt_kategori.Text = cat_name;
                CItemList[0].Id = Convert.ToInt32(dr["cat_id"]);
                CItemList[0].Name = cat_name;
            }
            else categoryFirstLoad = !categoryFirstLoad;
        }

        private void btn_tamamla_Click(object sender, EventArgs e)
        {
            if (spin_fiyat.Value <= 0)
            {
                ErrorMessages.Message m = new ErrorMessages.Message();
                if (m.WriteMessage("Masraf tutarını boş bırakmayınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                    return;
            }
            Gider gider = new Gider();
            gider.Desc = txt_tanim.Text.ToUpper();
            gider.Price = Convert.ToDouble(spin_fiyat.Value);
            gider.ProcessId = CItemList[0].Id;
            UpdateExpense(ref gider);
        }

        private void spin_fiyat_Click(object sender, EventArgs e)
        {
            this.spin_fiyat.SelectAll();
        }
    }
}