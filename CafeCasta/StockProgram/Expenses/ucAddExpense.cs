using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using StockProgram.DBObjects;

namespace StockProgram.Expenses
{
    public partial class ucAddExpense : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        private List<DBObjects.CategoryItem> CItemList;
        private CategoryItem CItem;  // my category items in cbox
        StockProgram.ControlHelper controlHelper;
        public delegate void SupplierGridHandler(object sender,EventArgs e);
        public event SupplierGridHandler SupplierGridChanged;
        public ucAddExpense()
        {
            InitializeComponent();
            FillCategory();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }

        protected virtual void OnSupplierGridChanged(EventArgs e)
        {
            if (SupplierGridChanged != null)
                SupplierGridChanged(this, e);
        }

        private void btn_tedarikciEkle_Click(object sender, EventArgs e)
        {         
            if (spin_fiyat.Value<=0)
            {
                ErrorMessages.Message m = new ErrorMessages.Message();
                if (m.WriteMessage("Masraf tutarını boş bırakmayınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                    return;
            }
            Gider gider = new Gider();
            gider.Desc = txt_tanim.Text.ToUpper();
            gider.Price = Convert.ToDouble(spin_fiyat.Value);
            gider.ProcessId = CItemList[tree_category.FocusedNode.Id].Id;
            AddExpenseToDB(ref gider);
          
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

        private void AddExpenseToDB(ref Gider g)
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string strSQL = "";
            strSQL = "insert into expense_details (payment_cat,payment_desc,payment_price,modified_date)" +
               " values ('" + g.ProcessId + "',@payment_desc,@price,@modified_date)";
            try
            {
                db.CreateSetParameter("payment_desc", MySql.Data.MySqlClient.MySqlDbType.Text, g.Desc);
                db.CreateSetParameter("modified_date", MySql.Data.MySqlClient.MySqlDbType.DateTime, DateTime.Now);
                db.CreateSetParameter("price", MySql.Data.MySqlClient.MySqlDbType.Double, g.Price);
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
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }

        private int FillCategory()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            int retValue = 0;
            controlHelper = new ControlHelper();
            try
            {
                string strSQL = "select process_id as cat_id,parent_id,process_name as cat_name from process_details where display_order <>-1 order by display_order ,process_name asc";
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

        private void spin_fiyat_Click(object sender, EventArgs e)
        {
            this.spin_fiyat.SelectAll();
        }
    }
}
