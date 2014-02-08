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
    public partial class ucEditTable : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        private StockProgram.ControlHelper controlHelper;
        private List<DBObjects.TableCategory> tCatList;
        private List<DBObjects.Status> statusList;
        public delegate void SupplierGridHandler(object sender, EventArgs e);
        public event SupplierGridHandler SupplierGridChanged;
        private DBObjects.Table table;
        private int table_id;

        public ucEditTable()
        {
            InitializeComponent();
            FillTableCategories();
        }
        public ucEditTable(int id)
        {
            this.table = new Table(id);
            InitializeComponent();
            FillTableCategories();
            FillStatus();
            FillProductProperties();
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

        public void FillStatus()
        {
         
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select status_id, status_name from status_details where is_deleted=0  order by status_name asc";
            statusList = new List<DBObjects.Status>();
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
                controlHelper.FillControl(cb_status, Enums.RepositoryItemType.ComboBox, ref dt, "status_name");
                statusList = controlHelper.GetStatusDetails(ref dt);
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

        public void FillProductProperties()
        {
            //fill Suppliers
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select * from v_table_details where table_id="+this.table.id;
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);

                table.is_deleted = 0;
                table.category.Id = Convert.ToInt32(dt.Rows[0]["table_category"]);
                table.category.Name = (dt.Rows[0]["table_category_name"]).ToString();
                table.name = (dt.Rows[0]["table_name"]).ToString();
                table.display_order = Convert.ToInt16(dt.Rows[0]["display_order"]);
                table.status = new Status { id = Convert.ToInt32(dt.Rows[0]["table_category"]), name = (dt.Rows[0]["status_name"]).ToString() };
                
                txt_name.Text = table.name;
                cb_category.Text = table.category.Name;
                cb_status.Text = table.status.name;
                txt_display_order.Text = table.display_order.ToString();

                if (dt.Rows.Count == 0)
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Ayarlar sayfasından en az 1 masa eklemelisiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    Parent.Controls["pnl_master"].Visible = true;
                    this.Dispose();
                    return;
                }
            
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

        private void FillTableCategories()
        {
            //fill Suppliers
            controlHelper = new ControlHelper();
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select tcategory_id, tcategory_name from table_categories where (is_deleted=0  and tcategory_status<>2 )order by tcategory_name asc";
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

            table.name = txt_name.Text.Trim().ToUpper();
            table.category = tCatList[cb_category.SelectedIndex];
            table.status = statusList[cb_status.SelectedIndex];
            table.display_order = Convert.ToInt32(txt_display_order.Text);

            EditTable();
        }

        private void EditTable()
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string procName = "editTable";
          
            try
            {
                db.CreateSetParameter("id", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt32(table.id));
                db.CreateSetParameter("name", MySql.Data.MySqlClient.MySqlDbType.Text,table.name);
                db.CreateSetParameter("status", MySql.Data.MySqlClient.MySqlDbType.Int16,table.status.id);
                db.CreateSetParameter("display_order", MySql.Data.MySqlClient.MySqlDbType.Int16, table.display_order);
                db.CreateSetParameter("category", MySql.Data.MySqlClient.MySqlDbType.Int32,table.category.Id);
                db.CreateSetParameter("is_deleted", MySql.Data.MySqlClient.MySqlDbType.Int16, Convert.ToInt16(table.is_deleted));
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
