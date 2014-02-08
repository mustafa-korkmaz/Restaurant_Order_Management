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
    public partial class ucAddOption : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        private ControlHelper controlHelper;
        private List<DBObjects.Options> OptionsList;
        public delegate void ColorGridHandler(object sender, EventArgs e);
        public event ColorGridHandler ColorGridChanged;
        public ucAddOption()
        {
            InitializeComponent();
        }

        protected virtual void OnColorGridChanged(EventArgs e)
        {
            if (ColorGridChanged != null)
                ColorGridChanged(this, e);
        }

        private void btn_renkEkle_Click(object sender, EventArgs e)
        {
            if (cb_options.Text != "")
            {// create  bank object
                string option = string.Empty;
                option = cb_options.Text.ToUpper();

                //add option
                if (CheckOptionDuplicate())
                {
                    AddOptionToDB(option);
                }
                else
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Bu seçenek sistemde mevcut.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                }
            }
        }

        private void FillOptions()
        {
            //fill Suppliers
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select option_id, option_name from option_details where is_deleted=0  order by option_name asc";
            OptionsList = new List<DBObjects.Options>();
            controlHelper = new ControlHelper();

            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);
                //if (dt.Rows.Count == 0)
                //{
                //    ErrorMessages.Message message = new ErrorMessages.Message();
                //    message.WriteMessage("Seçenekler sayfsından en az 1 seçenek eklemelisiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                //    Parent.Controls["pnl_master"].Visible = true;
                //    this.Dispose();
                //    return;
                //}
                controlHelper.FillControl(cb_options, Enums.RepositoryItemType.ComboBox, ref dt, "option_name");
             //   cb_options.Text = "Tedarikçi Seçiniz";
                OptionsList = controlHelper.GetOptions(ref dt);
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
            if (cb_options.SelectedIndex >= 0)
            {
                return false;
            }
            else
                return true;
        }
        private void Success()
        {
            //ErrorMessages.Message m = new ErrorMessages.Message();
            //m.WriteMessage(txt_isim.Text+", başarılı bir şekilde renklere eklendi.", MessageBoxIcon.Information, MessageBoxButtons.OK);
            this.FillOptions();
            cb_options.Text = "";
            this.cb_options.Focus();
        }

        private void ucAddOption_Load(object sender, EventArgs e)
        {
            this.FillOptions();
        }

        private void cb_options_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btn_renkEkle_Click(sender, e);
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }


    }
}
