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
    public partial class ucOptions : DevExpress.XtraEditors.XtraUserControl
    {
        private bool firstLoad;
        private ExceptionLogger excLogger;
        public ucOptions()
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
            string sql = "select * from v_options_to_product where is_deleted=0 order by product_name asc";
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
                    //repo_button.Buttons[0].Image = global::StockProgram.Properties.Resources.edit_small;
                    //repo_button.Buttons[0].Caption = "Düzenle";
                    //repo_button.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;

                    //erase button
                    repo_button.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    repo_button.Buttons[0].Image = global::StockProgram.Properties.Resources.delete;
                    repo_button.Buttons[0].Caption = "Sil";
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
            int option_id = Convert.ToInt32(dr["option_id"]);
            int  product_id = Convert.ToInt32(dr["product_id"]);
            int count = split.Panel2.Controls.Count;
            string ucName = split.Panel2.Controls[count - 1].GetType().Name;

            ErrorMessages.Message msg = new ErrorMessages.Message();
            if (msg.WriteMessage("Ürün ve seçenek arasındaki bağlantı silinecektir.\nDevam etmek istiyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DeleteOption(product_id,option_id);
            }
        }


        private void DeleteOption(int p_id,int o_id)
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string sql = "delete from options_to_product where product_id=" + p_id +" and option_id="+o_id;
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
            pnl_master.Visible = false;
            int count = split.Panel2.Controls.Count;
            string ucName = split.Panel2.Controls[count - 1].GetType().Name;
            if (ucName == "ucAddOption" || ucName == "ucOptionsToProduct")
            {
                split.Panel2.Controls[count - 1].Dispose();
            }
            ucAddOption ctrl = new ucAddOption();
            ctrl.Dock = DockStyle.Fill;
            ctrl.ColorGridChanged += new ucAddOption.ColorGridHandler(ctrl_OptionGridChanged);
            this.pnl_master.Visible = false;
            this.split.Panel2.Controls.Add(ctrl);
        }

        private void EditColor(int id,string name)
        {
            ucEditOption ctrl = new ucEditOption(id,name);
            ctrl.Dock = DockStyle.Fill;
        //    ctrl.ColorGridChanged += new ucEditOption.ColorGridHandler(ctrl_ColorGridChanged);
            this.pnl_master.Visible = false;
            this.split.Panel2.Controls.Add(ctrl);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            pnl_master.Visible = false;
            int count = split.Panel2.Controls.Count;
            string ucName = split.Panel2.Controls[count - 1].GetType().Name;
            if (ucName == "ucAddOption" || ucName == "ucOptionsToProduct")
            {
                split.Panel2.Controls[count - 1].Dispose();
            }
            ucOptionsToProducts ctrl = new ucOptionsToProducts();
            ctrl.Dock = DockStyle.Fill;
            ctrl.OptionGridChanged += new ucOptionsToProducts.OptionsGridHandler(ctrl_OptionGridChanged);
            this.pnl_master.Visible = false;
            this.split.Panel2.Controls.Add(ctrl);
        }

        void ctrl_OptionGridChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

    }
}
