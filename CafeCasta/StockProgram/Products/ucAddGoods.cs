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

namespace StockProgram.Products
{
    public partial class ucAddGoods : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        public delegate void SupplierGridHandler(object sender, EventArgs e);
        public event SupplierGridHandler GoodsGridChanged;

        public ucAddGoods()
        {
            InitializeComponent();
            cb_birim.SelectedIndex = 0;
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_malzeme"].Visible = true;
            this.Dispose();
        }

        protected virtual void OnGoodsGridChanged(EventArgs e)
        {
            if (GoodsGridChanged != null)
                GoodsGridChanged(this, e);
        }

        private void btn_addCustomer_Click(object sender, EventArgs e)
        {
            if (txt_name.Text=="")
            {
                ErrorMessages.Message m = new ErrorMessages.Message();
                if (m.WriteMessage("İsim alanını boş bırakmayınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                    return;
            }

            DBObjects.Goods goods = new DBObjects.Goods();
            goods.name = txt_name.Text;
            goods.unit = cb_birim.Text.Trim() ;
        

            AddGoods(ref goods);
        }

        private void AddGoods(ref DBObjects.Goods g)
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string procName = "addGoods";
          
            try
            {
                db.CreateSetParameter("name", MySql.Data.MySqlClient.MySqlDbType.VarChar, g.name.ToUpper());
                db.CreateSetParameter("unit", MySql.Data.MySqlClient.MySqlDbType.VarChar,g.unit);        
                db.ExecuteNonQuerySP(procName);
                OnGoodsGridChanged(EventArgs.Empty);// fire the gridChanged event
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
         //   Parent.Controls["pnl_malzeme"].Visible = true;
            //this.Dispose();
            this.txt_name.Text = "";
        }

        private void ucAddCustomer_Load(object sender, EventArgs e)
        {
            txt_name.Focus();
        }

        private void cb_birim_EditValueChanged(object sender, EventArgs e)
        {
            string val = (cb_birim.EditValue).ToString();
            bool result = true;

                if (val!="ADET" && val!="GR" && val!="L" && val!="ML" && val!="KG")
                {
                    result = false ;
                }
                else
                    result = true;
            

            if (result == false)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Tanımlanmamış birim giremezsiniz.", MessageBoxIcon.Error, MessageBoxButtons.OK);
                cb_birim.SelectedIndex = 0;
            }
        }
    }
}
