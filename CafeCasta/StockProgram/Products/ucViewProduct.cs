using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StockProgram.DBObjects;
using System.Diagnostics;

namespace StockProgram.Products
{
    public partial class ucViewProduct : DevExpress.XtraEditors.XtraUserControl
    {
        private int product_id;
        string header_text;
        //List<ProductAttributes> ProductAttributeList;
        public delegate void ProductTotalHandler(object sender, EventArgs e);
        //public event ProductTotalHandler ProductTotalChanged;
        List<ucShoeSize> shoeSizeList;
        private ExceptionLogger excLogger;

        public ucViewProduct(int product_id)
        {
            this.product_id = product_id;
            InitializeComponent();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        /// <summary>
        /// ekrana ayakkabı numaralarının seçilebileceği user controlleri getirir.
        /// </summary>
        private void SetShoeSizeButtons()
        {
            shoeSizeList = new List<ucShoeSize>();
            DBObjects.MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            string sql = "SELECT * FROM product_size_limits  WHERE  product_id=" + this.product_id;
            int minSize = 0;
            int maxSize = 0;
            try
            {
                DataTable dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    db.Close();
                    dt.Dispose();
                    return;
                }
                minSize = Convert.ToInt32(dt.Rows[0]["min_size"]);
                maxSize = Convert.ToInt32(dt.Rows[0]["max_size"]);
                ShoeSize sz = new ShoeSize();
                sz.MaxSize = maxSize;
                sz.MinSize = minSize;
                sz.ProductId = this.product_id;
                ShowSizeButtons(ref sz);
                LoadSizeButtons();
            }
            catch (Exception ex)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(ex.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(ex, ErrorMessages.MailServerNames.Gmail);
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

        /// <summary>
        /// hangi numaradan kaç tane olduğunu doldurur
        /// </summary>
        private void LoadSizeButtons()
        {
            DBObjects.MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            string sql = "SELECT * ,sum(product_count) as total_product_count FROM v_stocks  WHERE  product_id=" + this.product_id+"  group by product_size";
            try
            {
                DataTable dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    db.Close();
                    dt.Dispose();
                    return;
                }
                lbl_product_view.Text = "Ürün İzleme (" + dt.Rows[0]["product_name"].ToString()+" - "+ dt.Rows[0]["product_code_manual"].ToString().Trim()+")";
                this.header_text =dt.Rows[0]["product_name"].ToString() + " - " + dt.Rows[0]["product_code_manual"].ToString().Trim();
                int toplam=0;
        
                 foreach (DataRow dr in dt.Rows)
                 {
                        toplam += Convert.ToInt32(dr["total_product_count"]);
                 }
                 lbl_adet.Text += toplam.ToString() + " adet ürün";

                //foreach (ucShoeSize item in shoeSizeList)
                //{
                //    foreach (DataRow dr in dt.Rows)
                //    {
                //        string  shoe_size=(dr["product_size"]).ToString().Trim();
                //        if (shoe_size == item.btn_numara.Text)
                //        {
                //            item.spin_adet.Value = Convert.ToDecimal(dr["total_product_count"]);
                //        }
                //    }
                //}

            }
            catch (Exception ex)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(ex.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(ex, ErrorMessages.MailServerNames.Gmail);
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
        private void ShowSizeButtons(ref ShoeSize size)
        {
            shoeSizeList = new List<ucShoeSize>();
            int tab_index = 0;
            int index = size.MaxSize - size.MinSize;
            ucShoeSize ucSize;
            int x = 15;
            int y = 35;
            for (int i = 0; i <= index; i++)
            {
                ucSize = new ucShoeSize();
              //  ucSize.ProductAmountChanged += new ucShoeSize.ProductAmountHandler(ucSize_ProductAmountChanged);
                ucSize.TabIndex = ++tab_index;
                ucSize.Width = 73;
                ucSize.Height = 48;
                ucSize.Location = new System.Drawing.Point(x, y);
                ucSize.btn_numara.Text = (size.MinSize + i).ToString();
                ucSize.SetControlsDisabled();
                grpCtrl_numara.Controls.Add(ucSize);
                x += ucSize.Width + 5;
                shoeSizeList.Add(ucSize);
                //ProductAttributeList.Add(ucSize.GetProductAttributeItem()); //adet ve numara bilgisini listemize ekleyelim.
            }
          
        }

        private void ucViewProduct_Load(object sender, EventArgs e)
        {
            //SetShoeSizeButtons(); takı için numaralar yok
           // LoadSizeButtons();
            FillGrid();
        }

        /// <summary>
        ///  gridview i doldurur
        /// </summary>
        private void FillGrid()
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
         //   string sql = "select pv.*,(pv.product_buy_amount-pv.product_return_count) as product_net_buy_amount,((pv.product_buy_amount-pv.product_return_count)*pv.buy_price) as total_buy_price,pd.product_price from v_product_view pv inner join product_details pd on(pv.product_id=pd.product_id) where pv.product_id=" + this.product_id;
            string sql = "select pv.*,(total_sale_price-total_return_price) as total_income from v_product_view pv where pv.product_id=" + this.product_id;
            try
            {
                dt = db.GetDataTable(sql);
                gridControl1.DataSource = dt;
                gridView1.RowHeight = 40;
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

        public void SetLabelsForSaleView() //satış ekranından çagrıldıgında controller değişsin
        {
            this.btn_back.Visible = false;
            this.lbl_product_view.Text = this.header_text;
        }

        public void SetProductPreviewEvent()
        {
            ((Sales.frmProductView)this.ParentForm).KeyPreview = true;// ekran üzerinden basılan her hangi bir tuşu algılaması ve keyPressed eventi içine girmesi için
            ((Sales.frmProductView)this.ParentForm).KeyUp += new KeyEventHandler(ParentForm_KeyUp);
        }

        void ParentForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)  //ürün izleme ekranına getirelecek
            {
                this.KeyUp -= new KeyEventHandler(ParentForm_KeyUp);
           //     this.Dispose();
                ((Sales.frmProductView)this.ParentForm).Close();
            }
        }
    }
}
