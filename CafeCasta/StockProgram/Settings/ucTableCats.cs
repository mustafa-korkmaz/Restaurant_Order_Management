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
    public partial class ucTableCats : DevExpress.XtraEditors.XtraUserControl
    {
        private bool firstLoad;
        private ExceptionLogger excLogger;
        public ucTableCats()
        {
            InitializeComponent();
            gridView1.OptionsView.ShowGroupPanel = false;
            firstLoad = true;
            FillGrid();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           ucSettings ctrl = new ucSettings();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }

        private void FillGrid()
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            string sql = "select * from v_table_categories where is_deleted=0 order by display_order, tcategory_name asc";
            try
            {
                dt = db.GetDataTable(sql);
                gridControl1.DataSource = dt;

                if (firstLoad)
                {
                    //money button
                    //repo_button.Buttons[0].Image = global::StockProgram.Properties.Resources._case;
                    //repo_button.Buttons[0].Caption = "Alışveriş";
                    //repo_button.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;

                    //edit button
                    //     repo_button.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());
                    repo_button.Buttons[0].Image = global::StockProgram.Properties.Resources.edit_small;
                    repo_button.Buttons[0].Caption = "Düzenle";
                    repo_button.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;

                    //erase button
                    repo_button.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());
                    repo_button.Buttons[1].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    repo_button.Buttons[1].Image = global::StockProgram.Properties.Resources.delete;
                    repo_button.Buttons[1].Caption = "Sil";
                    repo_button.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
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
            int tc_id = Convert.ToInt32(dr["tcategory_id"]);
        //    string  table_name = (dr["table_name"]).ToString();
            int count = split.Panel2.Controls.Count;
            string ucName = split.Panel2.Controls[count - 1].GetType().Name;
            switch (e.Button.Caption)
            {
                case "Alışveriş":
                    //ucMoneyTransfer ctrl = new ucMoneyTransfer(bank_id);
                    //ctrl.Dock = DockStyle.Fill;
                    //ctrl.MoneyTransferChanged += new ucMoneyTransfer.MoneyTransferHandler(ctrl_BankGridChanged);
                    //this.pnl_master.Visible = false;
                    //int count=spliter.Panel2.Controls.Count;
                    //if (spliter.Panel2.Controls[count-1].GetType().Name=="ucAddBank") 
                    //{
                    //    spliter.Panel2.Controls[count-1].Dispose();
                    //}
                    //this.spliter.Panel2.Controls.Add(ctrl);
                    break;
                case "Düzenle":
                    //todo: edit operations
                    if (ucName == "ucAddTable" || ucName == "ucEditTable" || ucName == "ucAddTableCat" || ucName == "ucEditTableCat")
                    {
                        split.Panel2.Controls[count - 1].Dispose();
                    }
                    EditTable(tc_id);
                    break;
                case "Sil":
                    ErrorMessages.Message msg = new ErrorMessages.Message();
                    if (msg.WriteMessage("Yerleşim alanı sistemden tamamen silinecektir.\nDevam etmek istiyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        DeleteTableCat(tc_id);
                    }
                    break;
                default:
                    break;
            }
        }


        private void DeleteTableCat(int id)
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string sql = "update table_categories set is_deleted=1 where tcategory_id=" + id;
            try
            {
                db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new DBObjects.ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "CastaStok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at
            }
            finally
            {
                db.Close();
                db = null;
            }
            FillGrid();
        }
        private void btn_urun_gir_Click(object sender, EventArgs e)
        {
            ucAddTable ctrl = new ucAddTable();
            ctrl.Dock = DockStyle.Fill;
            ctrl.SupplierGridChanged += new ucAddTable.SupplierGridHandler(ctrl_ColorGridChanged);
            this.pnl_master.Visible = false;
            this.split.Panel2.Controls.Add(ctrl);
        }

        void ctrl_ColorGridChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void EditTable(int id)
        {
            ucEditTableCat ctrl = new ucEditTableCat(id);
            ctrl.Dock = DockStyle.Fill;
            ctrl.SupplierGridChanged += new ucEditTableCat.SupplierGridHandler(ctrl_ColorGridChanged);
            this.pnl_master.Visible = false;
            this.split.Panel2.Controls.Add(ctrl);
        }

        private void btn_category_add_Click(object sender, EventArgs e)
        {
            ucAddTableCat ctrl = new ucAddTableCat();
            ctrl.Dock = DockStyle.Fill;
            ctrl.SupplierGridChanged += new ucAddTableCat.SupplierGridHandler(ctrl_ColorGridChanged);
            this.pnl_master.Visible = false;
            this.split.Panel2.Controls.Add(ctrl);
        }

    }
}
