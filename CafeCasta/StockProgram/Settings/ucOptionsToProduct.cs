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
    public partial class ucOptionsToProducts : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        private ControlHelper controlHelper;
        private List<Product> pList;
        private List<DBObjects.Options> optionsList;
        public delegate void OptionsGridHandler(object sender, EventArgs e);
        public event OptionsGridHandler OptionGridChanged;
        public ucOptionsToProducts()
        {
            InitializeComponent();
        }

        protected virtual void OnOptionGridChanged(EventArgs e)
        {
            if (OptionGridChanged != null)
                OptionGridChanged(this, e);
        }


        private void FillOptions()
        {
            //fill Suppliers
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select option_id, option_name from option_details where is_deleted=0  order by option_name asc";

            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);
                gridControl1.DataSource = dt;
                gridView1.ShowFindPanel();

                if (dt.Rows.Count == 0)
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("En az 1 seçenek eklemelisiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    Parent.Controls["pnl_master"].Visible = true;
                    this.Dispose();
                    return;
                }
             //   cb_options.Text = "Tedarikçi Seçiniz";
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
        private void AddOptionToDB(string option_name)
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string proc_name = "addOption";
            try
            {
                db.CreateSetParameter("name", MySql.Data.MySqlClient.MySqlDbType.VarChar,option_name);
                db.ExecuteNonQuerySP(proc_name);
              
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
            //Parent.Controls["pnl_master"].Visible = true;
            //this.Dispose();
            Success();
        }


        private bool CheckOptionDuplicate()
        {
            return false;
        }
        private void Success()
        {
            //ErrorMessages.Message m = new ErrorMessages.Message();
            //m.WriteMessage(txt_isim.Text+", başarılı bir şekilde renklere eklendi.", MessageBoxIcon.Information, MessageBoxButtons.OK);
            this.FillOptions();
      
        }

        private void ucAddOption_Load(object sender, EventArgs e)
        {
            this.FillOptions();
            this.FillProducts();
        }

        private void FillProducts()
        { 
            //fill products
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select product_id, product_name from product_details where product_isDeleted=0 and isOnMenu=1 order by product_name asc";
            pList = new List<DBObjects.Product>();
            optionsList = new List<DBObjects.Options>();
            
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);
                controlHelper = new ControlHelper();
                if (dt.Rows.Count == 0)
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Tedarikçiler sayfsından en az 1 tedarikçi eklemelisiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    Parent.Controls["pnl_master"].Visible = true;
                    this.Dispose();
                    return;
                }
                controlHelper.FillControl(cb_products, Enums.RepositoryItemType.ComboBox, ref dt, "product_name");
                pList = controlHelper.GetProducts(ref dt);             
                cb_products.SelectedIndex = 0;
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

        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }

        private void cb_products_SelectedIndexChanged(object sender, EventArgs e)
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            int product_id = pList[cb_products.SelectedIndex].Id;
            string strSQL = "select * from v_options_to_product where is_Deleted=0 and product_id= "+product_id+" order by option_name asc";
            optionsList.Clear();
            lb_options.Items.Clear();
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    return;
                }
                controlHelper.FillControl(lb_options, Enums.RepositoryItemType.ListBox, ref dt, "option_name");
                optionsList = controlHelper.GetOptions(ref dt);
            }
            catch (Exception ex)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(ex, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, ucMigo.FillSuppliers() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(ex.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
                // retValue = 0;
            }
            finally
            {
                db.Close();
                db = null;
                dt.Dispose();
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            int id = Convert.ToInt32(dr["option_id"]);
            string name = dr["option_name"].ToString();
            if (!isItemInOptionList(name))
            {
                optionsList.Add(new Options() { option_id = id, option_name = name });
                lb_options.Items.Add(name);
            }
        
        }

        private bool isItemInOptionList(object value)
        {
            bool retValue = false;
            foreach (object itemValue in lb_options.Items)
            {
                if (itemValue.ToString() == value.ToString())
                {
                    retValue = true;
                    break;
                }
                else retValue = false;
            }
            return retValue;
        }

        private void btn_in_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            int id = Convert.ToInt32(dr["option_id"]);
            string name = dr["option_name"].ToString();
            if (!isItemInOptionList(name))
            {
                optionsList.Add(new Options() { option_id = id, option_name = name });
                lb_options.Items.Add(name);
            }
        
        }

        private void btn_out_Click(object sender, EventArgs e)
        {
            int index = lb_options.SelectedIndex;
            optionsList.RemoveAt(index);
            lb_options.Items.RemoveAt(index);
        }

        private void btn_tamamla_Click(object sender, EventArgs e)
        {
            MySqlCmd cmd = new MySqlCmd(StaticObjects.MySqlConn);
            string sql = string.Empty;
            int  p_id=this.pList[cb_products.SelectedIndex].Id;
           
            #region delete old o2p records
            sql = "delete from options_to_product where product_id=" + p_id;
            cmd.ExecuteNonQuery(sql);
            #endregion

            #region add new options to product
            cmd = new MySqlCmd(StaticObjects.MySqlConn);
            sql = "addOptionToProduct";//proc name
            cmd.CreateSetParameter("product_id", MySql.Data.MySqlClient.MySqlDbType.Int32, p_id);
            cmd.CreateParameter("option_id", MySql.Data.MySqlClient.MySqlDbType.Int32);

            foreach (Options option in optionsList)
            {
                cmd.SetParameterAt("option_id", option.option_id);
                cmd.ExecuteNonQuerySP(sql);
            }
            #endregion

            ErrorMessages.Message message = new ErrorMessages.Message();
            message.WriteMessage("Seçenek ekleme işlemi başarılı bir şekilde gerçekleşti.", MessageBoxIcon.Information, MessageBoxButtons.OK);
            this.OnOptionGridChanged(EventArgs.Empty);
        }

    }
}
