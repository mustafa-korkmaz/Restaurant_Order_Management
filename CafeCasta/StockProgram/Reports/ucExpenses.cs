using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;

namespace StockProgram.Reports
{
    public partial class ucExpenses : DevExpress.XtraEditors.XtraUserControl
    {
        public ucExpenses()
        {
            InitializeComponent();
        }
        private DBObjects.ExceptionLogger excLogger;
        private void btn_depo_gir_Click(object sender, EventArgs e)
        {

        }

        private void ucSuppliersMainPage_Load(object sender, EventArgs e)
        {
            FillGrid();
            date_to.DateTime = DateTime.Now;
        }

        /// <summary>
        /// tedarikçi gridview i doldurur
        /// </summary>
        private void FillGrid()
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            string sql = "select *  from v_expense_details ";
            try
            {
                dt = db.GetDataTable(sql);
                gridControl1.DataSource = dt;
                gridView1.RowHeight = 35;
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new DBObjects.ExceptionLogger(e.Message, excSource);// DB ye log yaz
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
  
        }

        /// <summary>
        /// tedarikçi gridview i doldurur
        /// </summary>
        private void FillGrid(string d1,string d2)
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            string sql = "select *  from v_expense_details where  modified_date between '" + d1 + "' and  '" + d2 + "'";
            try
            {
                dt = db.GetDataTable(sql);
                gridControl1.DataSource = dt;
                gridView1.RowHeight = 35;
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new DBObjects.ExceptionLogger(e.Message, excSource);// DB ye log yaz
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

        }

        void ctrl_SupplierGridChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {

         //   int payment_cat = 0;
         ////   int type = 0;
         //   int rowHandle = gridView1.FocusedRowHandle;
         //   DataRow dr;
         //   dr = gridView1.GetDataRow(rowHandle);
         //   payment_cat = Convert.ToInt32(dr["payment_cat"]);
            //type = Convert.ToInt32(dr["type"]);
            //if (type!=4)
            //{
            //        ErrorMessages.Message msg = new ErrorMessages.Message();
            //        msg.WriteMessage("Sadece elden girilen masrafları düzenleyebilirsiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
            //        return;
            //}

        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Reports.ucReportMainPage ctrl = new Reports.ucReportMainPage();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            #region validations
            //Tarih kontrol
            if (date_from.Text == "" || date_to.Text == "")
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Tarih aralığını boş bırakmayınız.", MessageBoxIcon.Error, MessageBoxButtons.OK);
                return;
            }
            else if (date_from.DateTime.Subtract(date_to.DateTime).Days >= 0)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Soldaki tarih kutusuna eski tarihi, sağdakine yeni tarihi giriniz.", MessageBoxIcon.Error, MessageBoxButtons.OK);
                return;
            }
            #endregion
            string _d1= date_from.DateTime.ToString("yyyy-MM-dd");
            string _d2 = date_to.DateTime.AddDays(+1).ToString("yyyy-MM-dd");
            FillGrid(_d1, _d2);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

     

    }
}
