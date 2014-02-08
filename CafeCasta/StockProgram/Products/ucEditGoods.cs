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
    public partial class ucEditGoods : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        public delegate void SupplierGridHandler(object sender, EventArgs e);
        public event SupplierGridHandler GoodsGridChanged;
        private DBObjects.Goods goods;

        public ucEditGoods(int id)
        {
            this.goods = new Goods();
            this.goods.id = id;
            InitializeComponent();
            FillProductProperties();
        }

        private void FillProductProperties()
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            string sql = "select * from goods_details where goods_id=" + this.goods.id;
            try
            {
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    this.goods.unit = dt.Rows[0]["unit"].ToString();
                    this.goods.name = dt.Rows[0]["goods_name"].ToString();
                 
                    //fill textboxes
                    txt_name.Text = goods.name;
                    cb_birim.Text = goods.unit;
                }

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

            //DBObjects.Goods goods = new DBObjects.Goods();
            goods.name = txt_name.Text;
            goods.unit = cb_birim.Text.Trim() ;
        

            EditGoods();
        }

        private void EditGoods()
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string procName = "editGoods";
          
            try
            {
                db.CreateSetParameter("name", MySql.Data.MySqlClient.MySqlDbType.VarChar, goods.name.ToUpper());
                db.CreateSetParameter("unit", MySql.Data.MySqlClient.MySqlDbType.VarChar,goods.unit);
                db.CreateSetParameter("id", MySql.Data.MySqlClient.MySqlDbType.Int32, goods.id);             
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
