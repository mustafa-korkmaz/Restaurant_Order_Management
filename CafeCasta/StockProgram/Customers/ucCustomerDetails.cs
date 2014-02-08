using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StockProgram.DBObjects;

namespace StockProgram.Customers
{
    public partial class ucCustomerDetails : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void ShoppingHandler(object sender,EventArgs e);
        public event ShoppingHandler ShoppingCompleted;
        private int id;
        private string customer_name;
        private bool firstLoad;

        public ucCustomerDetails(int id, string customer_name)
        {
            this.customer_name = customer_name;
            this.id = id;
            InitializeComponent();
        }

        private void btn_AlisverisEkle_Click(object sender, EventArgs e)
        {
            using (Customers.frmMoneyTransfer transfer = new frmMoneyTransfer(this.id,this.customer_name))
            {
                transfer.MoneyTransferChanged += new frmMoneyTransfer.MoneyTransferHandler(transfer_MoneyTransferChanged);
                transfer.ShowDialog(this);
            }
        }

        void transfer_MoneyTransferChanged(object sender, EventArgs e)
        {
            FillGrid();
            OnShoppingCompleted(EventArgs.Empty);
        }

        private void ucShoppingDetails_Load(object sender, EventArgs e)
        {
            firstLoad = true;
            FillGrid();
        }

        private void FillGrid()
        {
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

            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select * from v_customer_payment_details where customer_id="+this.id+" order by payment_date desc";

            DataTable dt = new DataTable();
            dt = cmd.GetDataTable(strSQL);
            this.lbl_header.Text = this.customer_name+ " Alışveriş Detayları";
            try
            {
                gridControl1.DataSource = dt;
        //        gridView1.ShowFindPanel();
           
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, InsertProduct() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);          
            }
            finally
            {
                cmd.Close();
                cmd = null;
                dt.Dispose();
            }
        }

        void repo_button_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ErrorMessages.Message msg = new ErrorMessages.Message();
            DataRow dr = gridView1.GetFocusedDataRow();
            int type_id = Convert.ToInt32(dr["type_id"]);
            int payment_id = Convert.ToInt32(dr["payment_id"]);
      
            if ((type_id==1||type_id==2||type_id==3) && (e.Button.Caption=="Düzenle"))
            {
                using (Customers.frmMoneyTransfer transfer = new frmMoneyTransfer(dr,true))
                {
                    transfer.MoneyTransferChanged += new frmMoneyTransfer.MoneyTransferHandler(transfer_MoneyTransferChanged);
                    transfer.ShowDialog(this);
                }
            }
            else 
            if ((type_id == 1 || type_id == 2 || type_id == 3) && (e.Button.Caption == "Sil"))
            {
                msg.WriteMessage("Bu ilşem grubu, silmeye karşı korumalıdır.", MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            else if (e.Button.Caption == "Düzenle")
            {
                using (Customers.frmMoneyTransfer transfer = new frmMoneyTransfer(dr))
                {
                    transfer.MoneyTransferChanged += new frmMoneyTransfer.MoneyTransferHandler(transfer_MoneyTransferChanged);
                    transfer.ShowDialog(this);
                }
            }
            else if (e.Button.Caption == "Sil")
            {
                if (msg.WriteMessage("İşlemi silmek istediğinize emin misiniz?", MessageBoxIcon.Error, MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DeleteCustomerPayment(payment_id);
                    FillGrid();
                    OnShoppingCompleted(EventArgs.Empty);
                }
            }
          
        }

        private void DeleteCustomerPayment(int payment_id)
        {
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "delete from customer_payments where id=" + payment_id;

            try
            {
                cmd.ExecuteNonQuery(strSQL);
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, InsertProduct() hata hk ";
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
       

        /// <summary>
        /// fires when amount  successfully changed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnShoppingCompleted(EventArgs e)
        {
            if (ShoppingCompleted != null)
                ShoppingCompleted(this, e);
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }
    }
}
