using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StockProgram.DBObjects;
using DevExpress.XtraGrid;
using System.Diagnostics;

namespace StockProgram.Products
{
    public partial class ucProductsMasterPage : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        private List<CategoryItem> CItemList;

        public ucProductsMasterPage()
        {
            InitializeComponent();
            FillCategory();
        }

        private int FillCategory()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            int retValue = 0;
            //   controlHelper = new ControlHelper();
            try
            {
                string strSQL = "select cat_id,parent_id,cat_name from category_details order by display_order ,cat_name asc";
                dt = db.GetDataTable(strSQL);
                InitializeCategoryItems(ref dt);
                // FillCategoryTree(ref dt);
                dt.Dispose();
                retValue = 1;
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at

                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                db.Close();
                db = null;
            }
            return retValue;
        }
        private void btn_depo_giris_Click(object sender, EventArgs e)
        {
            pnl_master.Visible = false;
            Products.ucMigo ctrl = new Products.ucMigo();
            ctrl.ProductTotalChanged += new ucMigo.ProductTotalHandler(ctrl_ProductGridChanged);
            ctrl.Dock = DockStyle.Fill;
            this.spliter.Panel2.Controls.Add(ctrl);

        }

        private void InitializeCategoryItems(ref DataTable dt)
        {
            CItemList = new List<CategoryItem>();
            // CItemList.Add(new CategoryItem { ParentId = 0, Id = 0, Name = "En Üst Kategori" });
            CategoryItem CItem;
            foreach (DataRow row in dt.Rows)
            {
                CItem = new CategoryItem();
                CItem.Id = Convert.ToInt32(row["cat_id"].ToString());
                CItem.ParentId = Convert.ToInt32(row["parent_id"].ToString());
                CItem.Name = row["cat_name"].ToString();
                CItemList.Add(CItem);
            }
        }

        private void btn_depo_cikis_Click(object sender, EventArgs e)
        {
            pnl_master.Visible = false;
            Products.ucDepoCikis ctrl = new Products.ucDepoCikis();
            ctrl.Dock = DockStyle.Fill;
            this.spliter.Panel2.Controls.Add(ctrl);

        }

        private void btn_urun_gir_Click(object sender, EventArgs e)
        {
            pnl_master.Visible = false;
            Products.ucAddProduct ctrl = new Products.ucAddProduct();
            ctrl.Dock = DockStyle.Fill;
            ctrl.ProductGridChanged += new ucAddProduct.ProductGridHandler(ctrl_ProductGridChanged);
            this.spliter.Panel2.Controls.Add(ctrl);
        }

        void ctrl_ProductGridChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void ucProductsMasterPage_Load(object sender, EventArgs e)
        {
            firstLoad = true;
            FillGrid();
            FillGoodsGrid();
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.ActiveControl = txt_barkod;
        }

        private bool firstLoad;
        private void FillGrid()
        {
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "SELECT * FROM v_products WHERE ( product_isDeleted = 'Evet') " +
            " order by modified_date desc";

            DataTable dt = new DataTable();
            StockProgram.Categories.CategoryFamilyTree cft = new StockProgram.Categories.CategoryFamilyTree(ref this.CItemList);
            dt = cmd.GetDataTable(strSQL);
            dt.Columns.Add("top_cat_name", typeof(String));// ana kategori adı için

            for (int i = 0; i < dt.Rows.Count; i++) // fill  top category names
            {
                dt.Rows[i]["top_cat_name"] = cft.GetTopCategoryItem(Convert.ToInt32(dt.Rows[i]["product_cat"])).Name;
            }

            try
            {
                dt.Columns.Add("Image", typeof(Image));
                Image myImage;

                string file_name;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    file_name = Application.StartupPath + StaticObjects.MainImagePath + dt.Rows[i]["product_img_path"].ToString();
                    if (System.IO.File.Exists(file_name))
                    {
                        myImage = Image.FromFile(file_name);
                        myImage = StaticObjects.ResizeImage(myImage, 150, 70);
                        dt.Rows[i]["Image"] = myImage;
                    }
                    else continue;
                }

                gridControl1.DataSource = dt;
                SetConditionalFormatting("[count]");
                SetConditionalFormatting("[count]=0", System.Drawing.Color.Green);
                gridView1.ShowFindPanel();
                gridView1.Columns["Image"].ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
                gridView1.RowHeight = 70;

                if (firstLoad)
                {
                    //malzeme tabı için
                    repo_button_malzeme.Buttons[0].Image = global::StockProgram.Properties.Resources.add_product_small;
                    repo_button_malzeme.Buttons[0].Caption = "Mal Girişi";
                    repo_button_malzeme.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    repo_button_malzeme.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());               
                    repo_button_malzeme.Buttons[1].Image = global::StockProgram.Properties.Resources.return_small;
                    repo_button_malzeme.Buttons[1].Caption = "İade";
                    repo_button_malzeme.Buttons[1].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    repo_button_malzeme.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());
                    repo_button_malzeme.Buttons[2].Image = global::StockProgram.Properties.Resources.edit_small;
                    repo_button_malzeme.Buttons[2].Caption = "Düzenle";
                    repo_button_malzeme.Buttons[2].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    repo_button_malzeme.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());
                    repo_button_malzeme.Buttons[3].Image = global::StockProgram.Properties.Resources.delete;
                    repo_button_malzeme.Buttons[3].Caption = "Sil";
                    repo_button_malzeme.Buttons[3].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    //ürün tabı için
                    repo_button.Buttons[0].Image = global::StockProgram.Properties.Resources.edit_small;
                    repo_button.Buttons[0].Caption = "Düzenle";
                    repo_button.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    repo_button.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());
                    repo_button.Buttons[1].Image = global::StockProgram.Properties.Resources.delete;
                    repo_button.Buttons[1].Caption = "Sil";
                    repo_button.Buttons[1].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
               
                    repo_button.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repo_button_ButtonClick);
                    repo_button_malzeme.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repo_button_malzeme_ButtonClick);
                }
                firstLoad = false;
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, InsertProduct() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
          //  txt_barkod.Focus();
        }

        private void FillGoodsGrid()
        {
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "SELECT * FROM v_goods_amount WHERE ( is_deleted =  0) " +
            " OR (count =0 AND is_deleted=0 AND last_modified_date IS NULL) " +
            " order by modified_date desc";

            DataTable dt = new DataTable();
       //     StockProgram.Categories.CategoryFamilyTree cft = new StockProgram.Categories.CategoryFamilyTree(ref this.CItemList);
            dt = cmd.GetDataTable(strSQL);
         

            try
            {
                gridControl2.DataSource = dt;
                SetConditionalFormatting("[count]");
                SetConditionalFormatting("[count]=0", System.Drawing.Color.Green);
                gridView2.ShowFindPanel();
                gridView2.RowHeight = 40;
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

        void repo_button_malzeme_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow dr = gridView2.GetFocusedDataRow();
            int goods_id = 0;
            goods_id = Convert.ToInt32(dr["goods_id"]);
            int count = spliter_malzeme.Panel2.Controls.Count;
            string ucName = spliter_malzeme.Panel2.Controls[count - 1].GetType().Name;

            switch (e.Button.Caption)
            {
                case "Mal Girişi":
                    pnl_malzeme.Visible = false;
                    count = spliter_malzeme.Panel2.Controls.Count;
                    ucName = spliter_malzeme.Panel2.Controls[count - 1].GetType().Name;
                    if (ucName == "ucMigo" || ucName == "ucAddGoods" || ucName == "ucProductReturn" || ucName == "ucCustomerReturn" || ucName == "ucEditGoods" || ucName == "ucViewProduct" || ucName == "ucPrintLabel" || ucName == "ucViewGoods")
                    {
                        spliter_malzeme.Panel2.Controls[count - 1].Dispose();
                    }
                    Products.ucMigo ctrlView = new Products.ucMigo(goods_id);
                    ctrlView.ProductTotalChanged += new ucMigo.ProductTotalHandler(ctrlEdit_GoodsGridChanged);
                    ctrlView.Dock = DockStyle.Fill;
                    this.spliter_malzeme.Panel2.Controls.Add(ctrlView);
                    break;
                case "İade":
                    pnl_malzeme.Visible = false;
                    if (ucName == "ucMigo" || ucName == "ucAddGoods" || ucName == "ucProductReturn" || ucName == "ucCustomerReturn" || ucName == "ucEditGoods" || ucName == "ucViewProduct" || ucName == "ucPrintLabel" || ucName == "ucViewGoods")
                    {
                        spliter_malzeme.Panel2.Controls[count - 1].Dispose();
                    }
                    Products.ucProductReturn ctrl = new Products.ucProductReturn(goods_id);
                    ctrl.ProductTotalChanged += new ucProductReturn.ProductTotalHandler(ctrlEdit_GoodsGridChanged);
                    ctrl.Dock = DockStyle.Fill;
                    this.spliter_malzeme.Panel2.Controls.Add(ctrl);
                    break;
                case "Düzenle":
                    pnl_malzeme.Visible = false;
                    if (ucName == "ucMigo" || ucName == "ucAddGoods" || ucName == "ucProductReturn" || ucName == "ucCustomerReturn" || ucName == "ucEditGoods" || ucName == "ucViewProduct" || ucName == "ucPrintLabel" || ucName == "ucViewGoods")
                    {
                        spliter_malzeme.Panel2.Controls[count - 1].Dispose();
                    }
                    Products.ucEditGoods ctrlEdit = new Products.ucEditGoods(goods_id);
                    ctrlEdit.GoodsGridChanged += new ucEditGoods.SupplierGridHandler(ctrlEdit_GoodsGridChanged);
                    ctrlEdit.Dock = DockStyle.Fill;
                    this.spliter_malzeme.Panel2.Controls.Add(ctrlEdit);
                    break;
                case "Barkod":
                    pnl_master.Visible = false;
                    if (ucName == "ucMigo" || ucName == "ucAddGoods" || ucName == "ucProductReturn" || ucName == "ucCustomerReturn" || ucName == "ucEditGoods" || ucName == "ucViewProduct" || ucName == "ucPrintLabel" || ucName == "ucViewGoods")
                    {
                        spliter.Panel2.Controls[count - 1].Dispose();
                    }
                    Products.ucPrintLabel ctrPrintLabel = new ucPrintLabel(goods_id);
                    ctrPrintLabel.Dock = DockStyle.Fill;
                    this.spliter.Panel2.Controls.Add(ctrPrintLabel);
                    break;
                case "Sil":
                    ErrorMessages.Message msg = new ErrorMessages.Message();
                    if (msg.WriteMessage("Malzeme silme işlemini onaylıyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        //DeleteProduct(goods_id);
                        DeleteGoods(goods_id);
                    }
                    break;
                default:
                    break;
            }
        }

        void ctrlEdit_GoodsGridChanged(object sender, EventArgs e)
        {
            FillGoodsGrid();
        }

        /// <summary>
        /// işlemler butonlarından birine basıldıgında çalışır
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repo_button_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            int product_id = 0;
            product_id = Convert.ToInt32(dr["product_id"]);
            int count = spliter.Panel2.Controls.Count;
            string ucName = spliter.Panel2.Controls[count - 1].GetType().Name;

            switch (e.Button.Caption)
            {
                case "Mal Girişi":
                    pnl_master.Visible = false;
                    count = spliter.Panel2.Controls.Count;
                    ucName = spliter.Panel2.Controls[count - 1].GetType().Name;
                    if (ucName == "ucMigo" || ucName == "ucAddProduct" || ucName == "ucProductReturn" || ucName == "ucCustomerReturn" || ucName == "ucEditProduct" || ucName == "ucViewProduct" || ucName == "ucPrintLabel")
                    {
                        spliter.Panel2.Controls[count - 1].Dispose();
                    }
                    Products.ucMigo ctrlView = new Products.ucMigo(product_id);
                    ctrlView.ProductTotalChanged += new ucMigo.ProductTotalHandler(ctrl_ProductGridChanged);
                    ctrlView.Dock = DockStyle.Fill;
                    this.spliter.Panel2.Controls.Add(ctrlView);
                    break;
                case "İade":
                    product_id = Convert.ToInt32(dr["product_id"]);
                    pnl_master.Visible = false;
                    if (ucName == "ucMigo" || ucName == "ucAddProduct" || ucName == "ucProductReturn" || ucName == "ucCustomerReturn" || ucName == "ucEditProduct" || ucName == "ucViewProduct" || ucName == "ucPrintLabel")
                    {
                        spliter.Panel2.Controls[count - 1].Dispose();
                    }
                    Products.ucProductReturn ctrl = new Products.ucProductReturn(product_id);
                    ctrl.ProductTotalChanged += new ucProductReturn.ProductTotalHandler(ctrl_ProductTotalChanged);
                    ctrl.Dock = DockStyle.Fill;
                    this.spliter.Panel2.Controls.Add(ctrl);
                    break;
                case "Düzenle":
                    pnl_master.Visible = false;
                    if (ucName == "ucMigo" || ucName == "ucAddProduct" || ucName == "ucProductReturn" || ucName == "ucCustomerReturn" || ucName == "ucEditProduct" || ucName == "ucViewProduct" || ucName == "ucPrintLabel")
                    {
                        spliter.Panel2.Controls[count - 1].Dispose();
                    }
                    Products.ucEditProduct ctrlEdit = new Products.ucEditProduct(product_id);
                    ctrlEdit.ProductGridChanged += new ucEditProduct.ProductGridHandler(ctrl_ProductGridChanged);
                    ctrlEdit.Dock = DockStyle.Fill;
                    this.spliter.Panel2.Controls.Add(ctrlEdit);
                    break;
                case "Barkod":
                    pnl_master.Visible = false;
                    if (ucName == "ucMigo" || ucName == "ucAddProduct" || ucName == "ucProductReturn" || ucName == "ucCustomerReturn" || ucName == "ucEditProduct" || ucName == "ucViewProduct" || ucName == "ucPrintLabel")
                    {
                        spliter.Panel2.Controls[count - 1].Dispose();
                    }
                    Products.ucPrintLabel ctrPrintLabel = new ucPrintLabel(product_id);
                    ctrPrintLabel.Dock = DockStyle.Fill;
                    this.spliter.Panel2.Controls.Add(ctrPrintLabel);
                    break;
                case "Sil":
                     ErrorMessages.Message msg = new ErrorMessages.Message();
                    if (msg.WriteMessage("Ürün silme işlemini onaylıyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        DeleteProduct(product_id);
                    }
                    break;
                default:
                    break;
            }
        }

        private void DeleteGoods(int goods_id)
        {
            string sql = "update goods_details set is_deleted=1 where goods_id=" + goods_id;
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);

            try
            {
                cmd.ExecuteNonQuery(sql);
                FillGoodsGrid();
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
        private void DeleteProduct(int product_id)
        {
            string sql = "update product_details set product_isDeleted=1 where product_id=" + product_id;
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);

            try
            {
                cmd.ExecuteNonQuery(sql);
                FillGrid();
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

        void ctrlEdit_ProductGridChanged(object sender, EventArgs e)
        {
            FillGrid();
            txt_barkod.Focus();
        }

        private void btn_return_Click(object sender, EventArgs e)
        {
            pnl_master.Visible = false;
            int count = spliter.Panel2.Controls.Count;
            string ucName = spliter.Panel2.Controls[count - 1].GetType().Name;
            if (ucName == "ucMigo" || ucName == "ucAddProduct" || ucName == "ucProductReturn" || ucName == "ucCustomerReturn" || ucName == "ucEditProduct" || ucName == "ucViewProduct" || ucName == "ucPrintLabel")
            {
                spliter.Panel2.Controls[count - 1].Dispose();
            }
            Products.ucProductReturn ctrl = new ucProductReturn();
            ctrl.ProductTotalChanged += new ucProductReturn.ProductTotalHandler(ctrl_ProductTotalChanged);
            ctrl.Dock = DockStyle.Fill;
            this.spliter.Panel2.Controls.Add(ctrl);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            int product_id = 0;
            int rowHandle = gridView1.FocusedRowHandle;
            DataRow dr;
            dr = gridView1.GetDataRow(rowHandle);
            product_id = Convert.ToInt32(dr["product_id"]);
            pnl_master.Visible = false;
            int count = spliter.Panel2.Controls.Count;
            string ucName = spliter.Panel2.Controls[count - 1].GetType().Name;
            if (ucName == "ucMigo" || ucName == "ucAddProduct" || ucName == "ucProductReturn" || ucName == "ucCustomerReturn" || ucName == "ucEditProduct" || ucName == "ucViewProduct" || ucName == "ucPrintLabel")
            {
                spliter.Panel2.Controls[count - 1].Dispose();
            }
            Products.ucViewProduct ctrl = new Products.ucViewProduct(product_id);
            //  ctrl.ProductTotalChanged += new ucMigo.ProductTotalHandler(ctrl_ProductGridChanged);
            ctrl.Dock = DockStyle.Fill;
            this.spliter.Panel2.Controls.Add(ctrl);
        }

        void ctrl_ProductTotalChanged(object sender, EventArgs e)
        {
            FillGrid();
            txt_barkod.Focus();
        }

        private void btn_customer_return_Click(object sender, EventArgs e)
        {
            pnl_master.Visible = false;
            int count = spliter.Panel2.Controls.Count;
            string ucName = spliter.Panel2.Controls[count - 1].GetType().Name;
            if (ucName == "ucMigo" || ucName == "ucAddProduct" || ucName == "ucProductReturn" || ucName == "ucCustomerReturn" || ucName == "ucEditProduct" || ucName == "ucViewProduct" || ucName == "ucPrintLabel")
            {
                spliter.Panel2.Controls[count - 1].Dispose();
            }
            Products.ucCustomerReturn ctrl = new ucCustomerReturn();
            ctrl.ProductTotalChanged += new ucCustomerReturn.ProductTotalHandler(ctrl_ProductTotalChanged);
            ctrl.Dock = DockStyle.Fill;
            this.spliter.Panel2.Controls.Add(ctrl);
        }

        /// <summary>
        /// colon toplamı 0 ise satırı kırmızı boyar
        /// </summary>
        /// <param name="columnName"></param>
        private void SetConditionalFormatting(string columnName)
        {
            StyleFormatCondition condition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            condition1.Appearance.ForeColor = System.Drawing.Color.Red;
            condition1.Appearance.Options.UseForeColor = true;
            condition1.Condition = FormatConditionEnum.Expression;
            condition1.Expression = columnName + "<=1";
            gridView1.FormatConditions.Add(condition1);

            //StyleFormatCondition condition2 = new DevExpress.XtraGrid.StyleFormatCondition();
            //condition2.Appearance.ForeColor = System.Drawing.Color.Red;
            //condition2.Appearance.Options.UseForeColor = true;
            //condition2.Condition = FormatConditionEnum.Expression;
            //condition2.Expression = columnName + "<=1";
            //gridView1.FormatConditions.Add(condition1);
        }

        private void SetConditionalFormatting(string expression, System.Drawing.Color color)
        {
            StyleFormatCondition condition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            condition1.Appearance.ForeColor = color;
            condition1.Appearance.Options.UseForeColor = true;
            condition1.Condition = FormatConditionEnum.Expression;
            condition1.Expression = expression;
            gridView1.FormatConditions.Add(condition1);
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            ((MainForm)this.ParentForm).SettingStatus();
            this.Dispose();
        }

        private void txt_barkod_KeyPress(object sender, KeyPressEventArgs e)
        {
            string product_code_manual = string.Empty;

            if (e.KeyChar == (char)13)
            {
                product_code_manual = GetProductManualCode(txt_barkod.Text);
                gridView1.ApplyFindFilter(product_code_manual);
                txt_barkod.Focus();
                txt_barkod.SelectAll();
            }
        }

        /// <summary>
        /// girilen barcode numarasından ürün kodu döndürür
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private string GetProductManualCode(string barcode)
        {
            string p_code = string.Empty ;
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable("v_stocs");
            MySqlCmd cmd = db.CreateCommand();
            try
            {
                cmd.CreateSetParameter("barcode", MySql.Data.MySqlClient.MySqlDbType.Text, barcode);
                string SQL = "select product_code_manual from v_stocks where  barcode=@barcode";
                dt = cmd.GetDataTable(SQL);
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new DBObjects.ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at

                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);

            }
            finally
            {
                cmd.Close();
            }
            if (dt.Rows.Count <= 0)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Bu ürün stoklarda mevcut değil.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                txt_barkod.Focus();
                txt_barkod.SelectAll();
            }
            else
            {
                p_code = dt.Rows[0]["product_code_manual"].ToString();
            }
            return p_code;
        }

        void ucSalesMasterPage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)  //ürün izleme ekranına getirelecek
            {
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                int product_id = Convert.ToInt32(dr["product_id"]);
                //   this.isPreviewFormOpen = true;                            
            }
            if (e.KeyCode == Keys.F5)  //ürün izleme ekranına getirelecek
            {
                txt_barkod.Focus();
                txt_barkod.SelectAll();
            }
            if (e.KeyCode == Keys.F2)  //ürün izleme ekranına getirelecek
            {
        
            }
        }

        private void btn_satis_aktar_Click(object sender, EventArgs e)
        {
            pnl_master.Visible = false;
            int count = spliter.Panel2.Controls.Count;
            string ucName = spliter.Panel2.Controls[count - 1].GetType().Name;
            if (ucName == "ucMigo" || ucName == "ucAddProduct" || ucName == "ucProductReturn" || ucName == "ucCustomerReturn" || ucName == "ucEditProduct" || ucName == "ucViewProduct" || ucName == "ucPrintLabel")
            {
                spliter.Panel2.Controls[count - 1].Dispose();
            }
            Products.ucCustomerReturn ctrl = new ucCustomerReturn("aktar");
            ctrl.ProductTotalChanged += new ucCustomerReturn.ProductTotalHandler(ctrl_ProductTotalChanged);
            ctrl.Dock = DockStyle.Fill;
            this.spliter.Panel2.Controls.Add(ctrl);
        }

        private void btn_addGoods_Click(object sender, EventArgs e)
        {
            pnl_malzeme.Visible = false;
            Products.ucAddGoods ctrl = new Products.ucAddGoods();
            ctrl.Dock = DockStyle.Fill;
            ctrl.GoodsGridChanged += new ucAddGoods.SupplierGridHandler(ctrl_GoodsGridChanged);
            this.spliter_malzeme.Panel2.Controls.Add(ctrl);
        }

        void ctrl_GoodsGridChanged(object sender, EventArgs e)
        {
            FillGoodsGrid();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            pnl_malzeme.Visible = false;
            int count = spliter_malzeme.Panel2.Controls.Count;
            string ucName = spliter_malzeme.Panel2.Controls[count - 1].GetType().Name;
            if (ucName == "ucMigo" || ucName == "ucAddProduct" || ucName == "ucProductReturn" || ucName == "ucCustomerReturn" || ucName == "ucEditProduct" || ucName == "ucViewProduct" || ucName == "ucPrintLabel")
            {
                spliter_malzeme.Panel2.Controls[count - 1].Dispose();
            }
            Products.ucProductReturn ctrl = new ucProductReturn();
            ctrl.ProductTotalChanged += new ucProductReturn.ProductTotalHandler(ctrl_GoodsGridChanged);
            ctrl.Dock = DockStyle.Fill;
            this.spliter_malzeme.Panel2.Controls.Add(ctrl);
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            int goods_id = 0;
            int rowHandle = gridView2.FocusedRowHandle;
            DataRow dr;
            dr = gridView2.GetDataRow(rowHandle);
            goods_id = Convert.ToInt32(dr["goods_id"]);
            pnl_malzeme.Visible = false;
            int count = spliter_malzeme.Panel2.Controls.Count;
            string ucName = spliter_malzeme.Panel2.Controls[count - 1].GetType().Name;
            if (ucName == "ucMigo" || ucName == "ucAddGoods" || ucName == "ucProductReturn" || ucName == "ucCustomerReturn" || ucName == "ucEditGoods" || ucName == "ucViewProduct" || ucName == "ucPrintLabel" || ucName == "ucViewGoods")
            {
                spliter_malzeme.Panel2.Controls[count - 1].Dispose();
            }
            Products.ucViewGoods ctrl = new Products.ucViewGoods(goods_id);
            //  ctrl.ProductTotalChanged += new ucMigo.ProductTotalHandler(ctrl_ProductGridChanged);
            ctrl.Dock = DockStyle.Fill;
            this.spliter_malzeme.Panel2.Controls.Add(ctrl);
        }
    }
}
