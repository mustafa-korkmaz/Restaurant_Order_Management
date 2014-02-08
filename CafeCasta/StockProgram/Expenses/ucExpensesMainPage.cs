using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;

namespace StockProgram.Expenses
{
    public partial class ucExpensesMainPage : DevExpress.XtraEditors.XtraUserControl
    {
        public ucExpensesMainPage()
        {
            InitializeComponent();
        }
        private DBObjects.ExceptionLogger excLogger;
        private void btn_depo_gir_Click(object sender, EventArgs e)
        {

        }

        bool firstLoad;
        private void ucSuppliersMainPage_Load(object sender, EventArgs e)
        {
            firstLoad = true;
            FillSuppliers();
        }

        /// <summary>
        /// tedarikçi gridview i doldurur
        /// </summary>
        private void FillSuppliers()
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            string sql = "select sum(payment_price)as payment_price,payment_cat,process_name from v_expense_details where display_order>-1 group by process_name";
            try
            {
                dt = db.GetDataTable(sql);
                gridControl1.DataSource = dt;

                if (firstLoad)
                {
                    repo_button.Buttons[0].Image = global::StockProgram.Properties.Resources.edit_small;
                    repo_button.Buttons[0].Caption = "Düzenle";
                    repo_button.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    repo_button.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());
                    repo_button.Buttons[1].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    repo_button.Buttons[1].Image = global::StockProgram.Properties.Resources.delete;
                    repo_button.Buttons[1].Caption = "Sil";
                    repo_button.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repo_button_ButtonClick);
                }
                firstLoad = false;
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

        private void repo_button_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            int payment_id =Convert.ToInt32( dr["payment_id"]);
            int type= Convert.ToInt32(dr["type"]);
            if (type != 4)
            {
                ErrorMessages.Message msg = new ErrorMessages.Message();
                msg.WriteMessage("Sadece elden girilen masrafları düzenleyebilirsiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                return;
            }
            switch (e.Button.Caption)
            {
                case "Düzenle":
                    ucEditExpense ctrl = new ucEditExpense(payment_id,type);                  
                    ctrl.Dock = DockStyle.Fill;
                    ctrl.SupplierGridChanged+=new ucEditExpense.SupplierGridHandler(ctrl_SupplierGridChanged);
                    this.pnl_master.Visible = false;
                    int count=spliter.Panel2.Controls.Count;
                    string ucName = spliter.Panel2.Controls[count - 1].GetType().Name;
                    if (ucName=="ucAddExpense" ||ucName=="ucEditExpense"||ucName=="ucExpenseDetails") 
                    {
                        spliter.Panel2.Controls[count-1].Dispose();
                    }
                    this.spliter.Panel2.Controls.Add(ctrl);
                    break;
                case "Sil":
                    ErrorMessages.Message msg = new ErrorMessages.Message();
                    if (msg.WriteMessage("Tedarikçi silme işleminden sonra o tedarikçiden alınan ürünler 'Tedarikçi Yok' olarak görünecektir.\nDevam etmek istiyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    { 
                    //sil
                    }
                    break;
                default:
                    break;
            }
        }

        private void btn_urun_gir_Click_1(object sender, EventArgs e)
        {
            ucAddExpense ctrl = new ucAddExpense();
            ctrl.Dock = DockStyle.Fill;
            ctrl.SupplierGridChanged += new ucAddExpense.SupplierGridHandler(ctrl_SupplierGridChanged);
            this.pnl_master.Visible = false;
            this.spliter.Panel2.Controls.Add(ctrl);
        }

        void ctrl_SupplierGridChanged(object sender, EventArgs e)
        {
            FillSuppliers();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {

            int payment_cat = 0;
         //   int type = 0;
            int rowHandle = gridView1.FocusedRowHandle;
            DataRow dr;
            dr = gridView1.GetDataRow(rowHandle);
            payment_cat = Convert.ToInt32(dr["payment_cat"]);
            //type = Convert.ToInt32(dr["type"]);
            //if (type!=4)
            //{
            //        ErrorMessages.Message msg = new ErrorMessages.Message();
            //        msg.WriteMessage("Sadece elden girilen masrafları düzenleyebilirsiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
            //        return;
            //}

            int count = spliter.Panel2.Controls.Count;
            string ucName = spliter.Panel2.Controls[count - 1].GetType().Name;
            if (ucName == "ucAddExpense" || ucName == "ucEditExpense" || ucName =="ucExpenseDetails")
            {
                spliter.Panel2.Controls[count - 1].Dispose();
            }
       //     ucEditExpense ctrl = new ucEditExpense(supplier_id, supplier_name);
            ucExpenseDetails ctrl = new ucExpenseDetails(payment_cat);
       //     ctrl.SupplierGridChanged += new ucEditExpense.SupplierGridHandler(ctrl_SupplierGridChanged);

            ctrl.Dock = DockStyle.Fill;
            ctrl.ExpenseGridChanged += new ucExpenseDetails.ExpenseGridHandler(ctrl_ExpenseGridChanged);
            this.pnl_master.Visible = false;
            this.spliter.Panel2.Controls.Add(ctrl);
        }

        void ctrl_ExpenseGridChanged(object sender, EventArgs e)
        {
            FillSuppliers();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            ((MainForm)this.ParentForm).SettingStatus();
            this.Dispose();
        }

     

    }
}
