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
    public partial class ucEditTableCat : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        private StockProgram.ControlHelper controlHelper;
        private List<DBObjects.TableCategory> tCatList;
        private List<DBObjects.Status> statusList;
        public delegate void SupplierGridHandler(object sender, EventArgs e);
        public event SupplierGridHandler SupplierGridChanged;
        private DBObjects.TableCategory tableCat;
        private int table_id;

        public ucEditTableCat()
        {
            InitializeComponent();
        }
        public ucEditTableCat(int id)
        {
            this.tableCat = new TableCategory(id);
            InitializeComponent();
          //  FillTableCategories();
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
            controlHelper = new ControlHelper();
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
            string strSQL = "select * from v_table_categories where tcategory_id="+this.tableCat.Id;
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);

                tableCat.display_order = Convert.ToInt16(dt.Rows[0]["display_order"]);
                tableCat.Name = (dt.Rows[0]["tcategory_name"]).ToString();
                tableCat.status = new Status { id = Convert.ToInt32(dt.Rows[0]["tcategory_status"]), name = (dt.Rows[0]["status_name"]).ToString() };
                
                txt_name.Text = tableCat.Name;
                cb_status.Text = tableCat.status.name;
                txt_display_order.Text = tableCat.display_order.ToString();

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

        private void btn_addCustomer_Click(object sender, EventArgs e)
        {
            if (txt_name.Text=="")
            {
                ErrorMessages.Message m = new ErrorMessages.Message();
                if (m.WriteMessage("Yerleşim alanını boş bırakmayınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                    return;
            }

            tableCat.display_order = Convert.ToInt16(txt_display_order.Text);
            tableCat.Name = txt_name.Text.Trim().ToUpper();
            tableCat.status = statusList[cb_status.SelectedIndex];
            EditTable();
        }

        private void EditTable()
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string procName = "editTableCategory";
          
            try
            {
                db.CreateSetParameter("id", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt32(tableCat.Id));
                db.CreateSetParameter("name", MySql.Data.MySqlClient.MySqlDbType.Text,tableCat.Name);
                db.CreateSetParameter("status", MySql.Data.MySqlClient.MySqlDbType.Int16,tableCat.status.id);
                db.CreateSetParameter("display_order", MySql.Data.MySqlClient.MySqlDbType.Int16, tableCat.display_order);
                db.CreateSetParameter("is_deleted", MySql.Data.MySqlClient.MySqlDbType.Int16, 0);
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
