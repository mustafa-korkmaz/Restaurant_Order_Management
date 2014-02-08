using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StockProgram.DBObjects;

namespace StockProgram.Suppliers
{
    public partial class ucShoppingDetails : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void ShoppingHandler(object sender,EventArgs e);
        public event ShoppingHandler ShoppingCompleted;
        private int supplier_id;
        private bool firstLoad;
        private string  supplier_name;
        public ucShoppingDetails(int id,string name)
        {
            this.supplier_id = id;
            this.supplier_name = name;
            InitializeComponent();
        }

        private void btn_AlisverisEkle_Click(object sender, EventArgs e)
        {
            using (Suppliers.frmAddShopping shopping = new Suppliers.frmAddShopping(this.supplier_id,this.supplier_name))
            {
                shopping.SupplierShoppingCompleted += new frmAddShopping.SupplierShoppingHandler(shopping_SupplierShoppingCompleted);
              shopping.ShowDialog(this);
            }
        }

        void shopping_SupplierShoppingCompleted(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void ucShoppingDetails_Load(object sender, EventArgs e)
        {
            firstLoad = true;
            FillGrid();
            this.lbl_header.Text = this.supplier_name + " İşlem Detayları";
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
            string strSQL = "select *,CAST(product_count as DECIMAL (11,2)) as product_count2,if(toplam_borc<0,-toplam_borc,toplam_borc) as net_price from v_supplier_payment_details where suppliers_id=" + this.supplier_id + " order by modified_date desc";

            DataTable dt = new DataTable();
            dt = cmd.GetDataTable(strSQL);
            RetrieveBuyDetails(ref dt);//mal alım detaylarını getir
            try
            {
                gridControl1.DataSource = dt;
                gridView2.BestFitColumns();
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

        void repo_button_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ErrorMessages.Message msg = new ErrorMessages.Message();
            DataRow dr = gridView2.GetFocusedDataRow();
            int type_id = Convert.ToInt32(dr["p_type"]);
            int payment_id = Convert.ToInt32(dr["payment_id"]);

            if ((type_id != 0 && type_id != 3))
            {
                msg.WriteMessage("Bu ilşem grubu, silinmeye ve düzenlemeye karşı korumalıdır.", MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            else
                if ((type_id == 0 || type_id == 3) && e.Button.Caption == "Düzenle")
                {
                    using (Suppliers.frmEditShopping shopping = new Suppliers.frmEditShopping(this.supplier_name, ref dr))
                    {
                        shopping.ShoppingEdited += new frmEditShopping.SupplierShoppingHandler(shopping_ShoppingEdited);
                        shopping.ShowDialog(this);
                    }
                }

                else if (e.Button.Caption == "Sil")
                {
                    if (msg.WriteMessage("İşlemi silmek istediğinize emin misiniz?", MessageBoxIcon.Error, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        DeleteSupplierPayment(payment_id);
                        FillGrid();
                        OnShoppingCompleted(EventArgs.Empty);
                    }
                }

        }

        private void DeleteSupplierPayment(int payment_id)
        {
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "delete from suppliers_payment where payment_id=" + payment_id;

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
        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            int payment_id = 0;
            int payment_type=0;
            int rowHandle = gridView2.FocusedRowHandle;
            DataRow dr;
            dr = gridView2.GetDataRow(rowHandle);
            payment_id = Convert.ToInt32(dr["payment_id"]);
            payment_type = Convert.ToInt32(dr["p_type"]);

            if (payment_type == 0 || payment_type == 3)
            {
                using (Suppliers.frmEditShopping shopping = new Suppliers.frmEditShopping(this.supplier_name,ref dr))
                {
                    shopping.ShoppingEdited += new frmEditShopping.SupplierShoppingHandler(shopping_ShoppingEdited);
                    shopping.ShowDialog(this);
                }
            }
            else
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Yalnızca ödeme ve ilave işlemlerini düzenleyebilirsiniz. ", MessageBoxIcon.Error, MessageBoxButtons.OK);
                return;
            }
                
        }

        void shopping_ShoppingEdited(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void RetrieveBuyDetails(ref DataTable dtable)
        {
          //  dtable.Columns.Add("unit_amount", typeof(Double)); //product_name için bir kolon ekleyelim
            //dtable.Columns.Add("currency_text", typeof(String)); //product_name için bir kolon ekleyelim
            //dtable.Columns.Add("unit_text", typeof(String)); //product_name için bir kolon ekleyelim
            //dtable.Columns.Add("net_price", typeof(Double)); //product_name için bir kolon ekleyelim
            dtable.Columns.Add("kdv", typeof(Double)); //product_name için bir kolon ekleyelim
            MySqlDbHelper cmd;
            DataTable dt;
            string strSQL = string.Empty;
            int payment_id = 0;
            foreach (DataRow row in dtable.Rows)
            {
                if (Convert.ToInt32(row["p_type"])==2)
                {
                       
                    try
                   {
                        payment_id = Convert.ToInt32(row["payment_id"]);
                       cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
                       strSQL = "SELECT *,CAST(product_count as DECIMAL (11,2)) as product_count2 ,(product_count) as total_weight , (sum(product_count)*avg(buy_price)) as net_price ,(payment_price-(sum(product_count)*avg(buy_price))) as kdv2" +
                   " FROM v_supplier_payment_type2_details where type=2 and suppliers_id=" + this.supplier_id + " and payment_id=" + payment_id + "" +
                   " group by payment_id";
                            dt = new DataTable();
                           dt = cmd.GetDataTable(strSQL);
                         //  row["unit_amount"] = Convert.ToDouble(dt.Rows[0]["unit_amount"]);
                           row["unit"] = dt.Rows[0]["unit"].ToString();
                           row["goods_name"] = dt.Rows[0]["goods_name"].ToString();
                       //    row["currency_text"] = dt.Rows[0]["currency_text"].ToString();
                           row["net_price"] = Convert.ToDouble(dt.Rows[0]["net_price"]);
                           row["total_weight"] = Convert.ToDouble(dt.Rows[0]["total_weight"]);
                           row["kdv"] = Convert.ToDouble(dt.Rows[0]["kdv2"]);

                           cmd.Close();
                    }
                      catch (Exception e)
                         {
                             ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                             excMail.Subject = "Stok Programı, InsertProduct() hata hk ";
                             excMail.Send();
                             ErrorMessages.Message message = new ErrorMessages.Message();
                             message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
                         }
                    
                }  
            }
        
        }
    }
}
