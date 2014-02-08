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

namespace StockProgram.Settings
{
    public partial class ucAddTable : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        private StockProgram.ControlHelper controlHelper;
        private List<DBObjects.TableCategory> tCatList;
        public delegate void SupplierGridHandler(object sender, EventArgs e);
        public event SupplierGridHandler SupplierGridChanged;

        public ucAddTable()
        {
            InitializeComponent();
            FillTableCategories();
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

        private void FillTableCategories()
        {
            //fill Suppliers
            controlHelper = new ControlHelper();
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select tcategory_id, tcategory_name from table_categories where (is_deleted=0 and tcategory_status<>2) order by tcategory_name asc";
            tCatList = new List<DBObjects.TableCategory>();
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
                controlHelper.FillControl(cb_category, Enums.RepositoryItemType.ComboBox, ref dt, "tcategory_name");
                cb_category.Text = "Seçiniz";
                tCatList = controlHelper.GetTableCategories(ref dt);
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, ucMigo.FillSuppliers() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
                // retValue = 0;
            }
            finally
            {
                db.Close();
                db = null;
                dt.Dispose();
            }
        }

        private void btn_addCustomer_Click(object sender, EventArgs e)
        {
            if (txt_name.Text=="")
            {
                ErrorMessages.Message m = new ErrorMessages.Message();
                if (m.WriteMessage("Masa ismi alanını boş bırakmayınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                    return;
            }
            if (cb_category.Text == "Seçiniz")
            {
                ErrorMessages.Message m = new ErrorMessages.Message();
                if (m.WriteMessage("Yerleşim alanını boş bırakmayınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                    return;
            }
            DBObjects.Table t= new Table();
            t.is_deleted = 0;
            t.category = tCatList[cb_category.SelectedIndex];
            t.name = txt_name.Text.Trim().ToUpper();
            t.display_order = Convert.ToInt32(txt_display_order.Text);
            t.status = new Status { id = Convert.ToInt32(Enums.TableStatus.Uygun), name = "UYGUN" };
            AddTable(ref t);
        }

        private void AddTable(ref DBObjects.Table t)
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string procName = "addTable";
          
            try
            {
                db.CreateSetParameter("name", MySql.Data.MySqlClient.MySqlDbType.Text,t.name);
                db.CreateSetParameter("status", MySql.Data.MySqlClient.MySqlDbType.Int16,t.status.id);
                db.CreateSetParameter("display_order", MySql.Data.MySqlClient.MySqlDbType.Int16, t.display_order);
                db.CreateSetParameter("category", MySql.Data.MySqlClient.MySqlDbType.Int32,t.category.Id);
                db.ExecuteNonQuerySP(procName);
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

        private void ucAddCustomer_Load(object sender, EventArgs e)
        {
            txt_name.Focus();
        }

        private void txt_display_order_Click(object sender, EventArgs e)
        {
            this.txt_display_order.SelectAll();
        }
    }
}
