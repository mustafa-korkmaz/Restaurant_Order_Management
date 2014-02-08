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

namespace StockProgram.Suppliers
{
    public partial class ucSuppliersMainPage : DevExpress.XtraEditors.XtraUserControl
    {
        public ucSuppliersMainPage()
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
            string sql = "select * from suppliers_details where suppliers_isDeleted=0;";
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
            int supplier_id =Convert.ToInt32( dr["suppliers_id"]);
            switch (e.Button.Caption)
            {
                case "Düzenle":
                 //   Type t= spliter.Panel2.Controls[0].GetType();
                    ucEditSupplier ctrl = new ucEditSupplier(supplier_id);                  
                    ctrl.Dock = DockStyle.Fill;
                    ctrl.SupplierGridChanged+=new ucEditSupplier.SupplierGridHandler(ctrl_SupplierGridChanged);
                    this.pnl_master.Visible = false;
                    int count=spliter.Panel2.Controls.Count;
                    string ucName = spliter.Panel2.Controls[count - 1].GetType().Name;
                    if (ucName=="ucAddSupplier" ||ucName=="ucEditSupplier"||ucName=="ucShoppingDetails") 
                    {
                        spliter.Panel2.Controls[count-1].Dispose();
                    }
                    this.spliter.Panel2.Controls.Add(ctrl);
                    break;
                case "Sil":
                    ErrorMessages.Message msg = new ErrorMessages.Message();
                    if (msg.WriteMessage("Tedarikçi silme işleminden sonra o tedarikçiden alınan ürünler 'Tedarikçi Yok' olarak görünecektir.\nDevam etmek istiyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        DeleteSupplier(supplier_id);
                    }
                    break;
                default:
                    break;
            }
        }

        private void DeleteSupplier(int supplier_id)
        {
            string sql = "update suppliers_details set suppliers_isDeleted=1 where suppliers_id=" + supplier_id;
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            
            try
            {
                cmd.ExecuteNonQuery(sql);
                FillSuppliers();
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, DeleteSupplier() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                cmd.Close();
                cmd = null;
            }
        }

        private void btn_urun_gir_Click_1(object sender, EventArgs e)
        {
            ucAddSupplier  ctrl= new ucAddSupplier();
            ctrl.Dock = DockStyle.Fill;
            ctrl.SupplierGridChanged += new ucAddSupplier.SupplierGridHandler(ctrl_SupplierGridChanged);
            this.pnl_master.Visible = false;
            this.spliter.Panel2.Controls.Add(ctrl);
        }

        void ctrl_SupplierGridChanged(object sender, EventArgs e)
        {
            FillSuppliers();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
           
            int supplier_id = 0;
            string supplier_name = "";
            int rowHandle = gridView1.FocusedRowHandle;
            DataRow dr;
            dr = gridView1.GetDataRow(rowHandle);
            supplier_id = Convert.ToInt32(dr["suppliers_id"]);
            supplier_name = dr["suppliers_name"].ToString();

            int count = spliter.Panel2.Controls.Count;
            string ucName = spliter.Panel2.Controls[count - 1].GetType().Name;
            if (ucName == "ucAddSupplier" || ucName == "ucEditSupplier" || ucName =="ucShoppingDetails")
            {
                spliter.Panel2.Controls[count - 1].Dispose();
            }
            ucShoppingDetails ctrl = new ucShoppingDetails(supplier_id,supplier_name);
            ctrl.Dock = DockStyle.Fill;
            this.pnl_master.Visible = false;
            this.spliter.Panel2.Controls.Add(ctrl);
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            ((MainForm)this.ParentForm).SettingStatus();
            this.Dispose();
        }

     

    }
}
