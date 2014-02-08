using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StockProgram.DBObjects;
using System.Diagnostics;

namespace StockProgram.Expenses
{
    public partial class ucExpenseDetails : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void ExpenseGridHandler(object sender, EventArgs e);
        public event ExpenseGridHandler ExpenseGridChanged;
        private ExceptionLogger excLogger;
        private int cat_id;
        private bool isEditable;
        private bool firstLoad;
        public ucExpenseDetails(int cat_id)
        {
            this.cat_id = cat_id;
            if (cat_id == -1)
            {
                this.isEditable = false;
            }
            else this.isEditable = true;

            InitializeComponent();
        }


        /// <summary>
        ///  gridview i doldurur
        /// </summary>
        private void FillGrid()
        {
            if (firstLoad)
            {
                repo_button.Buttons[0].Image = global::StockProgram.Properties.Resources.edit_small;
                repo_button.Buttons[0].Caption = "Düzenle";
                repo_button.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                //erase button
                repo_button.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());
                repo_button.Buttons[1].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                repo_button.Buttons[1].Image = global::StockProgram.Properties.Resources.delete;
                repo_button.Buttons[1].Caption = "Sil";

                repo_button.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repo_button_ButtonClick);
            }
            firstLoad = false;
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            string sql = "select * from v_expense_details where payment_cat="+this.cat_id;
            try
            {
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count>0)
                {
                    this.lbl_header.Text = "Gider Detayları (" + dt.Rows[0]["process_name"].ToString().Trim() + ")";
                }
                gridControl1.DataSource = dt;
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

        protected virtual void OnExpenseGridChanged(EventArgs e)
        {
            if (ExpenseGridChanged != null)
                ExpenseGridChanged(this, e);
        }

        void repo_button_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (isEditable)
            {
                DataRow dr = gridView1.GetFocusedDataRow();
                int payment_id = Convert.ToInt32(dr["payment_id"]);
                int type = Convert.ToInt32(dr["type"]);

                if (e.Button.Caption=="Düzenle")
                {   
                    frmEditExpense edit = new frmEditExpense(payment_id, type);
                    edit.SupplierGridChanged += new frmEditExpense.SupplierGridHandler(edit_SupplierGridChanged);
                    edit.ShowDialog();
                }
                else if (e.Button.Caption == "Sil")
                {
                    ErrorMessages.Message msg = new ErrorMessages.Message();
                    if (msg.WriteMessage("Gider işlemi sistemden tamamen silinecektir.\nDevam etmek istiyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        DeleteExpense(payment_id);
                    }
                }
            }
            else
            {
                ErrorMessages.Message msg = new ErrorMessages.Message();
                msg.WriteMessage("Bu gider grubu, düzenlemeye karşı korumalıdır.", MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
        }

        void edit_SupplierGridChanged(object sender, EventArgs e)
        {
            this.FillGrid();
            OnExpenseGridChanged(EventArgs.Empty);
        }

        private void DeleteExpense(int id)
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string sql = "delete from expense_details where payment_id=" + id;
            try
            {
                db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new DBObjects.ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Casta Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at
            }
            finally
            {
                db.Close();
                db = null;
            }
            FillGrid();
            OnExpenseGridChanged(EventArgs.Empty);
        }
        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }

        private void ucExpenseDetails_Load(object sender, EventArgs e)
        {
            firstLoad = true;
            FillGrid();
        }

    }
}
