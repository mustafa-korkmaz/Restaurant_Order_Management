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

namespace StockProgram.Customers
{
    public partial class ucCustomersMainPage : DevExpress.XtraEditors.XtraUserControl
    {
        public ucCustomersMainPage()
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
            gridView1.ShowFindPanel();
        }

        /// <summary>
        /// tedarikçi gridview i doldurur
        /// </summary>
        private void FillSuppliers()
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            string sql = "select * from v_customers_master where is_deleted=0 order by name asc";
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
            int customer_id =Convert.ToInt32( dr["customer_id"]);
          
            switch (e.Button.Caption)
            {
                case "ÖdemeYap":

                    //ödeme yap
                    break;
                case "Düzenle":
                    ucEditCustomer ctrl = new ucEditCustomer(customer_id);                  
                    ctrl.Dock = DockStyle.Fill;
                    ctrl.SupplierGridChanged += new ucEditCustomer.SupplierGridHandler(ctrl_SupplierGridChanged);
                    this.pnl_master.Visible = false;
                    int count=spliter.Panel2.Controls.Count;
                    string ucName = spliter.Panel2.Controls[count - 1].GetType().Name;
                    if (ucName == "ucAddCustomer" || ucName == "ucEditCustomer" || ucName == "ucAddPayment" || ucName == "ucCustomerDetails") 
                    {
                        spliter.Panel2.Controls[count-1].Dispose();
                    }
                    this.spliter.Panel2.Controls.Add(ctrl);
                    break;
                case "Sil":
                    ErrorMessages.Message msg = new ErrorMessages.Message();
                    if (msg.WriteMessage("Müşteri silme işleminden sonra o müşteri ile yapılan tüm alışverişler silinecektir. \nDevam etmek istiyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        DeleteCustomer(customer_id);
                    }
                    break;
                default:
                    break;
            }
        }

        private void DeleteCustomer(int customer_id)
        {
            string sql = "update customer_details set is_deleted=1 where customer_id=" + customer_id;
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);

            try
            {
                cmd.ExecuteNonQuery(sql);
                FillSuppliers();
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, DeleteCustomer() hata hk ";
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
            ucAddPayment ctrl = new ucAddPayment ();
            ctrl.Dock = DockStyle.Fill;
            ctrl.VeresiyeGridChanged += new ucAddPayment.VeresiyeGridHandler(ctrl_VeresiyeGridChanged);
            this.pnl_master.Visible = false;
            this.spliter.Panel2.Controls.Add(ctrl);
        }

        void ctrl_VeresiyeGridChanged(object sender, EventArgs e)
        {
            FillSuppliers();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {

            int customer_id = 0;
         //   int type = 0;
            int rowHandle = gridView1.FocusedRowHandle;
            DataRow dr;
            dr = gridView1.GetDataRow(rowHandle);
            customer_id = Convert.ToInt32(dr["customer_id"]);
             string  customer_name = dr["name"].ToString();
         
            int count = spliter.Panel2.Controls.Count;
            string ucName = spliter.Panel2.Controls[count - 1].GetType().Name;
         
            if (ucName == "ucAddCostumer" || ucName == "ucEditCustomer" ||ucName=="ucCustomerDetails")
            {
                spliter.Panel2.Controls[count - 1].Dispose();
            }
            ucCustomerDetails ctrl = new ucCustomerDetails(customer_id, customer_name);
            ctrl.ShoppingCompleted += new ucCustomerDetails.ShoppingHandler(ctrl_ShoppingCompleted);

            ctrl.Dock = DockStyle.Fill;
            this.pnl_master.Visible = false;
            this.spliter.Panel2.Controls.Add(ctrl);
        }

        void ctrl_ShoppingCompleted(object sender, EventArgs e)
        {
            FillSuppliers();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            ((MainForm)this.ParentForm).SettingStatus();
            this.Dispose();
        }

        private void btn_addCustomer_Click(object sender, EventArgs e)
        {
            ucAddCustomer ctrl = new ucAddCustomer();
            ctrl.Dock = DockStyle.Fill;
            ctrl.SupplierGridChanged += new ucAddCustomer.SupplierGridHandler(ctrl_SupplierGridChanged);      
            this.pnl_master.Visible = false;
            this.spliter.Panel2.Controls.Add(ctrl);
        }

        void ctrl_SupplierGridChanged(object sender, EventArgs e)
        {
            FillSuppliers();
        }

     

    }
}
