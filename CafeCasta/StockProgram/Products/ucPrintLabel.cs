using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StockProgram.DBObjects;
using System.Drawing.Printing;
using System.Diagnostics;

namespace StockProgram.Products
{
    public partial class ucPrintLabel : DevExpress.XtraEditors.XtraUserControl
    {
        private int product_id;
        private Product product;
        string header_text;
        List<ProductAttributes> ProductAttributeList;
        public delegate void ProductTotalHandler(object sender, EventArgs e);
     //   public event ProductTotalHandler ProductTotalChanged;
        List<ucShoeSize> shoeSizeList;
        private ExceptionLogger excLogger;

        public ucPrintLabel(int product_id)
        {
            this.product_id = product_id;
            this.ProductAttributeList = new List<ProductAttributes>();
            InitializeComponent();
            FillProductProperties();
        }

        private void FillProductProperties()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            try
            {
                DataTable dt = db.GetDataTable("select * from v_products where product_id= " + this.product_id);
                product = new Product();
                product.Id = this.product_id;
                product.SalePrice = Convert.ToDouble(dt.Rows[0]["product_price"]);
                product.Name = dt.Rows[0]["product_name"].ToString();
                product.Code = dt.Rows[0]["product_code_manual"].ToString();
                product.Desc = dt.Rows[0]["product_desc"].ToString();
                product.ColorName = dt.Rows[0]["color_name"].ToString();
                product.ColorId = Convert.ToInt32(dt.Rows[0]["product_color"].ToString());
          //      cb_color.Text = dt.Rows[0]["color_name"].ToString();
             //   lbl_header.Text = "Ürün Depo Girişi (" + product.Name + ")";
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Close();
                db = null;
            }
       
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
            string sql = "SELECT * FROM v_stocks  WHERE  product_id=" + this.product_id;
            try
            {
                DataTable dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    db.Close();
                    dt.Dispose();
                    //o üründen hiç mal girişi yapılmamış, geri dön
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    if (message.WriteMessage("Bu üründen henüz mal girişi yapılmamış, barkod çıkarılamaz.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        btn_back_Click(this,EventArgs.Empty);
                    }
                    return;
                }
                lbl_product_view.Text = "Etiket Basım (" + dt.Rows[0]["product_name"].ToString() + " - " + dt.Rows[0]["product_code_manual"].ToString().Trim() + " - " + dt.Rows[0]["color_name"].ToString().Trim() + ")";
                this.header_text = dt.Rows[0]["product_name"].ToString() + " - " + dt.Rows[0]["product_code_manual"].ToString().Trim();
        
                foreach (ucShoeSize item in shoeSizeList)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string  shoe_size=(dr["product_size"]).ToString().Trim();
                        if (shoe_size == item.btn_numara.Text)
                        {
                            item.spin_adet.Value = Convert.ToDecimal(dr["product_count"]);
                        }
                    }
                }

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
                ucSize.ProductAmountChanged += new ucShoeSize.ProductAmountHandler(ucSize_ProductAmountChanged);
                ucSize.TabIndex = ++tab_index;
                ucSize.Width = 73;
                ucSize.Height = 48;
                ucSize.Location = new System.Drawing.Point(x, y);
                ucSize.btn_numara.Text = (size.MinSize + i).ToString();
               // ucSize.SetControlsDisabled();
                grpCtrl_numara.Controls.Add(ucSize);
                x += ucSize.Width + 5;
                shoeSizeList.Add(ucSize);
                ProductAttributeList.Add(ucSize.GetProductAttributeItem()); //adet ve numara bilgisini listemize ekleyelim.
            }
          
        }
        void ucSize_ProductAmountChanged(object sender, EventArgs e)
        {
            int toplam = 0;
            foreach (ProductAttributes item in ProductAttributeList)
            {
                toplam += item.Amount;
            }
            lbl_counter.Text = toplam.ToString();
        }

        private void ucViewProduct_Load(object sender, EventArgs e)
        {
            SetShoeSizeButtons();
           // FillGrid();
        }

        /// <summary>
        ///  gridview i doldurur
        /// </summary>
        private void FillGrid()
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            string sql = "select pv.*,(pv.product_buy_amount-pv.product_return_count) as product_net_buy_amount,((pv.product_buy_amount-pv.product_return_count)*pv.buy_price) as total_buy_price,pd.product_price from v_product_view pv inner join product_details pd on(pv.product_id=pd.product_id) where pv.product_id=" + this.product_id;
            try
            {
                dt = db.GetDataTable(sql);
               // gridControl1.DataSource = dt;
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //PrintLabels(); şu an barkod basımı yok
            ErrorMessages.Message message = new ErrorMessages.Message();
            if (message.WriteMessage("Barkod basım modülü henüz entegre edilmemiş.", MessageBoxIcon.Warning, MessageBoxButtons.OK) == DialogResult.OK)
            {
                return;
            }
        }

        /// <summary>
        /// ürüne ait 2 adet etiket bastırır.
        /// </summary>
        private void PrintLabels()
        {
            PrintDialog pd = new PrintDialog();
            pd.PrinterSettings = new PrinterSettings();
            if (DialogResult.OK == pd.ShowDialog(this))
            {
                Label lbl1 = new Label();
                LabelPattern lblPattern;

                foreach (ProductAttributes item in ProductAttributeList)
                {
                    if (item.Amount == 0) // adedi 0 olan ürünler için migo yapma
                    {
                        continue;
                    }
                    item.Barcode = GetBarcode(this.product.Id+":"+ Convert.ToInt32(item.Size)+":"+this.product.ColorId);
                    lbl1.barcode = item.Barcode;
                    lbl1.product_name = this.product.Name;
                    lbl1.color = this.product.ColorName;
                    lbl1.product_code = this.product.Code;
                    lbl1.price = this.product.SalePrice;
                    lbl1.size = item.Size.ToString();

                    int index;
                    if (item.Amount % 2 == 0)
                    {
                        index = item.Amount / 2;  // miktarın yarısı kadar bastırıyoruz çünkü 2 şer tane çıkıyor
                    }
                    else index = item.Amount / 2 + 1; // miktarın yarısı+1 kadar bastırıyoruz 1 adet fazla basmış oluyoruz.

                    for (int i = 0; i < index; i++)
                    {
                        lblPattern = new LabelPattern(ref lbl1, StaticObjects.LabelPatternPath, pd);
                        lblPattern.PrintLabel();
                    }

                }

            }
        }

        private string GetBarcode(string product_code)
        {
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "SELECT * FROM stock_details WHERE product_code='" + product_code + "'";
            string  barcode = string.Empty;

            DataTable dt = cmd.GetDataTable(strSQL);

            if (dt.Rows.Count > 0)
            {
                barcode = dt.Rows[0]["barcode"].ToString();
            }
            else
                barcode = "none";

            return barcode;
        }
        private string GenerateBarcode(int id, int size, int color)
        {
            BarcodeGenerator bg = new BarcodeGenerator(id, size, color);
            return bg.GetBarcode();
        }
    }
}
