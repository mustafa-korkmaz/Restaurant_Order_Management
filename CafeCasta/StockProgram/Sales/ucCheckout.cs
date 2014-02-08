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

namespace StockProgram.Sales
{
    public partial class ucCheckout : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        private StockProgram.ControlHelper controlHelper;
        private List<DBObjects.TableCategory> tCatList;
        public delegate void SupplierGridHandler(object sender, EventArgs e);
        public event SupplierGridHandler SupplierGridChanged;

        public ucCheckout()
        {
            InitializeComponent();
            FillTableCategories();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            ucSaleOptions ctrl = new ucSaleOptions();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
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
            string strSQL = "select tcategory_id, tcategory_name from table_categories where is_deleted=0  order by tcategory_name asc";
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
        
        }

        private void AddTable(ref DBObjects.Table t)
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string procName = "addTable";
          
            try
            {
                db.CreateSetParameter("name", MySql.Data.MySqlClient.MySqlDbType.Text,t.name);
                db.CreateSetParameter("status", MySql.Data.MySqlClient.MySqlDbType.Int16,t.status.id);
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
        }
    }
}
