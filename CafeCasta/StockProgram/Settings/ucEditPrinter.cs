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
    public partial class ucEditPrinter : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        private StockProgram.ControlHelper controlHelper;
        public delegate void SupplierGridHandler(object sender, EventArgs e);
        public event SupplierGridHandler SupplierGridChanged;
        private DBObjects.Printer  printer;

        public ucEditPrinter()
        {
            InitializeComponent();
        }
        public ucEditPrinter(int id)
        {
            this.printer = new Printer(id);
            InitializeComponent();
            //FillStatus();
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

        //public void FillStatus()
        //{
         
        //    DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
        //    string strSQL = "select status_id, status_name from status_details where is_deleted=0  order by status_name asc";
        //    statusList = new List<DBObjects.Status>();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dt = db.GetDataTable(strSQL);
        //        if (dt.Rows.Count == 0)
        //        {
        //            ErrorMessages.Message message = new ErrorMessages.Message();
        //            message.WriteMessage("Tedarikçiler sayfsından en az 1 tedarikçi eklemelisiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
        //            Parent.Controls["pnl_master"].Visible = true;
        //            this.Dispose();
        //            return;
        //        }
        //        controlHelper.FillControl(cb_status, Enums.RepositoryItemType.ComboBox, ref dt, "status_name");
        //        statusList = controlHelper.GetStatusDetails(ref dt);
        //    }
        //    catch (Exception e)
        //    {
        //        ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
        //        excMail.Subject = "Stok Programı, ucMigo.FillSuppliers() hata hk ";
        //        excMail.Send();
        //        ErrorMessages.Message message = new ErrorMessages.Message();
        //        message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
        //        // retValue = 0;
        //    }
        //    finally
        //    {
        //        db.Close();
        //        db = null;
        //        dt.Dispose();
        //    }      
        //}

        public void FillProductProperties()
        {
            //fill Suppliers
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select * from printer_details  where printer_id=" + this.printer.id;
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);

                printer.ip = (dt.Rows[0]["printer_ip"]).ToString();
                printer.desc = (dt.Rows[0]["printer_desc"]).ToString();
                printer.type = (Enums.PrinterType)Convert.ToInt16(dt.Rows[0]["type"]);
           
                txt_name.Text = printer.ip;
                txt_desc.Text = printer.desc;
                cb_type.SelectedIndex = Convert.ToInt16(printer.type);

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
                if (m.WriteMessage("Masa ismi alanını boş bırakmayınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                    return;
            }
       

            printer.ip = txt_name.Text.Trim().ToUpper();
            printer.desc = txt_desc.Text.Trim().ToUpper();
            printer.type = (Enums.PrinterType)this.cb_type.SelectedIndex;
            //table.category = tCatList[cb_category.SelectedIndex];
            //table.status = statusList[cb_status.SelectedIndex];
            EditPrinter();
        }

        private void EditPrinter()
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string procName = "editPrinter";
          
            try
            {
                db.CreateSetParameter("id", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt32(printer.id));
                db.CreateSetParameter("ip", MySql.Data.MySqlClient.MySqlDbType.VarChar,printer.ip);
                db.CreateSetParameter("_desc", MySql.Data.MySqlClient.MySqlDbType.Text,printer.desc);
                db.CreateSetParameter("pType", MySql.Data.MySqlClient.MySqlDbType.Int16, Convert.ToInt16(printer.type));      
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
    }
}
