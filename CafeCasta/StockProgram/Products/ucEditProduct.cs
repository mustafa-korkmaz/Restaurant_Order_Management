using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using StockProgram.DBObjects;
using System.Diagnostics;

namespace StockProgram.Products
{
    public partial class ucEditProduct : DevExpress.XtraEditors.XtraUserControl
    {
        private List<DBObjects.Color> CItemsList;
        private List<DBObjects.Color> SelectedColorsList;
        private bool isColorChanged=false;
        private int unChangedColorId;
        bool   isProductOnMenu = false;//satış listesinde görünme özelliği
        private bool productFirstLoad;
        private bool categoryFirstLoad;
        private CategoryItem CItem;  // my category items in cbox
        private List<CategoryItem> CItemList; // my category item list
        public delegate void ProductGridHandler(object sender,EventArgs e);
        public event ProductGridHandler ProductGridChanged;
        private Bitmap productImage;
        private string imageName;
        private bool IsImageSelected;
        private ExceptionLogger excLogger;
        StockProgram.ControlHelper controlHelper;
        private int productId;
        string root = Application.StartupPath + StaticObjects.MainImagePath;

        public ucEditProduct(int productId)
        {
            this.imageName = "";
            this.productId = productId;
            this.productFirstLoad = true;
            this.categoryFirstLoad = true;
            this.controlHelper = new ControlHelper();
            InitializeComponent();
            FillColors();
            FillProductProperties();
            FillCategory();
          //  FillSuppliers();

        }

        #region FillControls
        //private void FillWarehouses()
        //{
        //    StockProgram.DBObjects.CDB db= new DBObjects.CDB(StaticObjects.AccessConnStr);
        //    DataTable dt = new DataTable();
        //    WItemslist = new List<DBObjects.Warehouse>();

        //    string strSQL;
        //    try
        //    {
        //        //fill warehouses
        //        strSQL = "select WID,WName from [Warehouse] order by WName asc";
        //        dt = db.Get_DataTable(strSQL);
        //        //controlHelper.FillControl(cb_bagliDepo, Enums.RepositoryItemType.ComboBox, ref dt, "WName");
        //        //cb_bagliDepo.Text = cb_bagliDepo.Properties.Items[0].ToString();
        //        WItemslist = controlHelper.GetWarehouses(ref dt);
        //    }
        //    catch (Exception e)
        //    {
        //        ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
        //        excMail.Subject = "Stok Programı, ucAddPrduct.FillWarehouses() hata hk ";
        //        excMail.Send();
        //        ErrorMessages.Message message = new ErrorMessages.Message();
        //        message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
        //       // retValue = 0;
        //    }
        //    finally
        //    {
        //        db.CloseDB();
        //        db = null;
        //        dt.Dispose();
        //    }
         
        //}
        //private void FillCategories()
        //{
        //    //fill categories
        //    DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
        //    string strSQL = "select cat_id,parent_id,cat_name from category_details order by display_order asc";
        //    CItemslist = new List<DBObjects.CategoryItem>();

        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dt = db.GetDataTable(strSQL);
        //        controlHelper.FillControl(cb_bagliKategori, Enums.RepositoryItemType.ComboBox, ref dt, "cat_name");
        //        cb_bagliKategori.Text = cb_bagliKategori.Properties.Items[0].ToString();
        //        CItemslist = controlHelper.GetCategories(ref dt);
        //        CItemslist.RemoveAt(0);//en ust kategori yazısını cıkar
        //    }
        //    catch (Exception e)
        //    {
        //        string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
        //        excLogger = new ExceptionLogger(e.Message,excSource);
        //        ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
        //        excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
        //        excMail.ErrorSource = excSource+"()";
        //        excMail.Send();

        //        ErrorMessages.Message message = new ErrorMessages.Message();
        //        message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
        //        // retValue = 0;
        //    }
        //    finally
        //    {
        //        db.Close();
        //        db = null;
        //        dt.Dispose();
        //    }
            
        //}

        //private void FillSuppliers()
        //{
        //    //fill Suppliers
        //    DBObjects.CDB db = new DBObjects.CDB(StaticObjects.AccessConnStr);
        //    string strSQL = "select MID,MName from [Supplier] order by MName asc";
        //    MItemslist = new List<DBObjects.Supplier>();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dt = db.Get_DataTable(strSQL);
        //        //controlHelper.FillControl(cb_bagliTedarikci, Enums.RepositoryItemType.ComboBox, ref dt, "MName");
        //        //cb_bagliTedarikci.Text = cb_bagliTedarikci.Properties.Items[0].ToString();
        //        //MItemslist = controlHelper.GetSuppliers(ref dt);
        //    }
        //    catch (Exception e)
        //    {
        //        ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
        //        excMail.Subject = "Stok Programı, ucAddPrduct.FillSuppliers() hata hk ";
        //        excMail.Send();
        //        ErrorMessages.Message message = new ErrorMessages.Message();
        //        message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
        //        // retValue = 0;
        //    }
        //    finally
        //    {
        //        db.CloseDB();
        //        db = null;
        //        dt.Dispose();
        //    }
        //}
     
        #endregion

        private void ucAddProduct_Load(object sender, EventArgs e)
        {

        }

        string old_image_path;
        int color_id;
        private void FillProductProperties()
        {
            //spin_max_no.Properties.Increment = 2;
            //spin_min_no.Properties.Increment = 2;
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            controlHelper = new ControlHelper();
            old_image_path = "";
         
            try
            {
                string strSQL = "select* from v_products where product_id="+this.productId;
                dt = db.GetDataTable(strSQL);
                InitializeCategoryItems(ref dt);

                isProductOnMenu = Convert.ToBoolean(dt.Rows[0]["isOnMenu"]);
                int unit = Convert.ToInt32(dt.Rows[0]["unit"]);
          //      double unit_amount = Convert.ToDouble(dt.Rows[0]["unit_amount"]);
           //     spinAgirlik.Value = Convert.ToDecimal(unit_amount);
                string currency = (Convert.ToInt32(dt.Rows[0]["unit"]) == 0) ? "TL" : "USD";
                cb_para.Text = currency;

                //if (unit==1)
                //{
                //    chk_gram.CheckState = CheckState.Checked;
                //    chk_adet.CheckState = CheckState.Unchecked;
                //}

                //if (unit == 0)
                //{
                //    chk_gram.CheckState = CheckState.Unchecked;
                //    chk_adet.CheckState = CheckState.Checked;
                //}

                if (isProductOnMenu)
                {
                    chk_listede.CheckState = CheckState.Checked; chk_listedeDegil.CheckState = CheckState.Unchecked;
                }
                else
                {
                    chk_listede.CheckState = CheckState.Unchecked; chk_listedeDegil.CheckState = CheckState.Checked;
                }
                txt_urunAdi.Text = dt.Rows[0]["product_name"].ToString();
                lbl_header.Text = "Ürün Düzenleme (" + dt.Rows[0]["product_name"].ToString().Trim() + ")";// +" - " + dt.Rows[0]["product_code_manual"].ToString().Trim() + ")";
                txt_urunTanim.Text = dt.Rows[0]["product_desc"].ToString();
                txt_urun_kodu.Text = dt.Rows[0]["product_code_manual"].ToString();
                txt_kategori.Text = dt.Rows[0]["cat_name"].ToString();
                SetProductPrices();
               // spin_satis_fiyat.Value = Convert.ToDecimal(dt.Rows[0]["product_price"]);
                //spin_min_no.Value = Convert.ToDecimal(dt.Rows[0]["min_size"]);
                //spin_max_no.Value = Convert.ToDecimal(dt.Rows[0]["max_size"]);
                old_image_path = dt.Rows[0]["product_img_path"].ToString();
                //cb_color.Text = dt.Rows[0]["color_name"].ToString();
               // this.color_id = Convert.ToInt32(dt.Rows[0]["product_color"].ToString());

                if (File.Exists(root+old_image_path))
                {
                    pictureEdit1.Image = Image.FromFile(root + dt.Rows[0]["product_img_path"].ToString());
                    pictureEdit1.Visible = true;
                    this.IsImageSelected = true;
                }
               
                productFirstLoad = false;
                dt.Dispose();
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
            return ;
        }


        private void SetProductPrices()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();

            try
            {
                string strSQL = "select* from price_to_product where product_id=" + this.productId;
                dt = db.GetDataTable(strSQL);
          
                foreach (DataRow row in dt.Rows)
                {
                  if (Convert.ToDouble(row["porsion"])==1)
	                {
		                 spin_satis_fiyat.Value=Convert.ToDecimal(row["product_price"]);
	                }
                   else if(Convert.ToDouble(row["porsion"])==1.5)
                  {
                        spin_satis1_5.Value = Convert.ToDecimal(row["product_price"]);
                  }
                  else if (Convert.ToDouble(row["porsion"]) ==2)
                  {
                      spin_satis_duble.Value = Convert.ToDecimal(row["product_price"]);
                  } 
                    
                }
                dt.Dispose();
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
            return;
        }

        protected virtual void onProductGridChanged(EventArgs e)
        {
            if (ProductGridChanged != null)
                ProductGridChanged(this, e);
        }
        private void btn_resimEkle_Click(object sender, EventArgs e)
        {
            if (!IsImageSelected)
            {
                IsImageSelected = false;
            } 
            try
            {
                openFileDialog1.Filter = " (*.jpg)|*.jpg|(*.png)|*.png";
                openFileDialog1.Title = "Resim Dosyası Seçiniz";
                openFileDialog1.FileName = "Dosya";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    System.IO.FileInfo file = new FileInfo(openFileDialog1.FileName.ToString());
                    productImage = StaticObjects.ResizeImage(Image.FromFile(file.FullName), pictureEdit1.Width, pictureEdit1.Height);
                    pictureEdit1.Image = productImage;
                    pictureEdit1.Visible = true;
                    IsImageSelected = true;
                    if (old_image_path == "-1")
                    {
                        imageName = this.productId + "." + GetFileExtension(file.Name);
                    }
                    else imageName = file.Name;
                }
                else return;

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

        }


        private void SaveImage(string fullName)
        {
            string _root = this.root + fullName;
         
            try
            {
                productImage.Save(_root);
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
          

            //b.Save("path of the folder to save");

            //Bitmap b = new Bitmap(@"C:\Documents and Settings\Desktop\7506.jpg");

            //b.Save(@"C:\Extract\test.jpg");
        }

        private void FillColors()
        {
            //fill Suppliers
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select * from color_details where is_deleted=0 order by color_name asc";
            CItemsList = new List<DBObjects.Color>();
            this.SelectedColorsList = new List<DBObjects.Color>();
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);
                if (dt.Rows.Count == 0)
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Ayarlar-> Renkler sayfsından en az 1 renk eklemelisiniz.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    Parent.Controls["pnl_master"].Visible = true;
                    this.Dispose();
                    return;
                }
               // controlHelper.FillControl(db_color_list, Enums.RepositoryItemType.ListBox, ref dt, "color_name");
                //cb_color.Text = "Renk Seçiniz";
                //CItemsList = controlHelper.GetColors(ref dt);
                try
                {
                    dt = new DataTable();
                    strSQL = "select * from v_product_to_color where product_id=" + this.productId;
                    dt = db.GetDataTable(strSQL);
                    //controlHelper.FillControl(color_list, Enums.RepositoryItemType.ListBox, ref dt, "color_name");
                    //SelectedColorsList = controlHelper.GetColors(ref dt);
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
                dt.Dispose();
            }
        }

        private int FillCategory()
        {
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            int retValue = 0;
            controlHelper = new ControlHelper();
            try
            {
                string strSQL = "select cat_id,parent_id,cat_name from category_details where is_deleted=0 order by display_order ,cat_name asc";
                dt = db.GetDataTable(strSQL);
                //InitializeCategoryItems(ref dt);
                FillCategoryTree(ref dt);
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

        private void InitializeCategoryItems(ref DataTable dt)
        {
            CItemList = new List<CategoryItem>();
           // CItemList.Add(new CategoryItem { ParentId = 0, Id = 0, Name = "En Üst Kategori" });
            foreach (DataRow row in dt.Rows)
            {
                CItem = new CategoryItem();
                CItem.Id = Convert.ToInt32(row["product_cat"].ToString());
              //  CItem.ParentId = Convert.ToInt32(row["parent_id"].ToString());
                CItem.Name = row["cat_name"].ToString();
                CItemList.Add(CItem);
            }
        }

        private void FillCategoryTree(ref DataTable dt)
        {
            tree_category.DataSource = dt;
        }

        private void btn_urunEkle_Click(object sender, EventArgs e)
        {
            ErrorMessages.Message message = new ErrorMessages.Message();

            if (cb_para.Text == "TL" || cb_para.Text == "USD")
            {
                //true
            }
            else
            {
                message.WriteMessage("Para birimi seçimi hatalı.", MessageBoxIcon.Error, MessageBoxButtons.OK);
                return;
            }

            //if (chk_adet.CheckState == CheckState.Unchecked && chk_gram.CheckState == CheckState.Unchecked)
            //{
            //    message.WriteMessage("Adet ya da gram bazlı seçeneklerinden birini seçiniz.", MessageBoxIcon.Error, MessageBoxButtons.OK);
            //    return;
            //}
            DBObjects.Product product = new DBObjects.Product();
            product.Desc = txt_urunTanim.Text.ToUpper();
            product.Code = txt_urun_kodu.Text.ToUpper();
            product.Currency = (cb_para.Text == "TL") ? Enums.Currency.TL : Enums.Currency.USD;
        //    product.Unit = (chk_adet.CheckState == CheckState.Checked) ? Enums.Unit.adet : Enums.Unit.g;
         //   product.UnitAmount = Convert.ToDouble(spinAgirlik.Value);
            product.Name = txt_urunAdi.Text.ToUpper();
            product.IsOnMenu = Convert.ToInt32(isProductOnMenu);
            product.SalePrice = Convert.ToDouble(spin_satis_fiyat.Value);
            product.SalePrice_bucuk = Convert.ToDouble(spin_satis1_5.Value);
            product.SalePrice_double = Convert.ToDouble(spin_satis_duble.Value);
            product.CategoryId = CItemList[0].Id;
            //product.MinSize = Convert.ToInt32(spin_min_no.Value);
            //product.MaxSize = Convert.ToInt32(spin_max_no.Value);
            product.Id = this.productId;

            //if (unChangedColorId==CItemsList[cb_color.SelectedIndex].Id) // Renk değişitirilmemiş
            //{
            //    product.ColorId = this.color_id;
            //    isColorChanged = false;
            //}
            //else
            //{
            //    product.ColorId = CItemsList[cb_color.SelectedIndex].Id;
            //    isColorChanged = true;
            //}
            if (IsImageSelected == true)
            {
                if (imageName=="")// eski resim duruyor
                {
                    product.ImagePath = old_image_path;
                    productImage = (Bitmap)Image.FromFile(this.root+old_image_path);
                }
                else
                product.ImagePath = imageName;
            }
            else product.ImagePath = "-1";

            EditProduct(ref product);
         //   Migo(ref product);//stoklara da eklenecek
        }
        /// <summary>
        /// Edits a new product record
        /// </summary>
        /// <param name="p"></param>
        private  void EditProduct(ref DBObjects.Product p)
        {
            DBObjects.MySqlCmd cmd = new MySqlCmd(StaticObjects.MySqlConn);
            string proc_name = "editProduct";
            string fileExtension = GetFileExtension(p.ImagePath);
            string fileName =(old_image_path=="-1")?	 GetFileName(p.ImagePath):GetFileName(old_image_path);
            string fullName = "_"+fileName + "." + fileExtension;
            try
            {
                cmd.CreateSetParameter("pCode", MySql.Data.MySqlClient.MySqlDbType.VarChar, p.Code);
                cmd.CreateSetParameter("pCat",MySql.Data.MySqlClient.MySqlDbType.Int32,p.CategoryId);
                cmd.CreateSetParameter("pName",MySql.Data.MySqlClient.MySqlDbType.VarChar,p.Name);
                cmd.CreateSetParameter("pDesc", MySql.Data.MySqlClient.MySqlDbType.VarChar, p.Desc);
                cmd.CreateSetParameter("pPrice", MySql.Data.MySqlClient.MySqlDbType.Double, p.SalePrice);
                cmd.CreateSetParameter("pPrice_bucuk", MySql.Data.MySqlClient.MySqlDbType.Double, p.SalePrice_bucuk);
                cmd.CreateSetParameter("pPrice_double", MySql.Data.MySqlClient.MySqlDbType.Double, p.SalePrice_double);
                cmd.CreateSetParameter("pImg", MySql.Data.MySqlClient.MySqlDbType.VarChar,fullName);
              //  cmd.CreateSetParameter("pColor", MySql.Data.MySqlClient.MySqlDbType.Int32, p.ColorId);
              //  cmd.CreateSetParameter("pCurrency", MySql.Data.MySqlClient.MySqlDbType.Int16, Convert.ToInt16(p.Currency));
                cmd.CreateSetParameter("pUnit", MySql.Data.MySqlClient.MySqlDbType.Int16, Convert.ToInt16(p.Currency));
                //cmd.CreateSetParameter("pUnitAmount", MySql.Data.MySqlClient.MySqlDbType.Double, p.UnitAmount);
                cmd.CreateSetParameter("pId", MySql.Data.MySqlClient.MySqlDbType.Int32,p.Id);
                cmd.CreateSetParameter("isOnMenu", MySql.Data.MySqlClient.MySqlDbType.Int32, p.IsOnMenu);
                cmd.CreateSetParameter("min_size", MySql.Data.MySqlClient.MySqlDbType.Int32, 0);//gereksiz
                cmd.CreateSetParameter("max_size", MySql.Data.MySqlClient.MySqlDbType.Int32,0);
                cmd.ExecuteNonQuerySP(proc_name);
                if (p.ImagePath!="-1")
                {// save image to related folder
                    int id = Convert.ToInt32(cmd.GetParameterValue("pId"));
                    SaveImage(fullName);                   
                 }
                EditColors();
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, InsertProduct() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
                //retValue = 0;
            }
            finally
            {
                cmd.Close();
                cmd = null;
            }
     //       onProductGridChanged(EventArgs.Empty);
            if (isColorChanged)
            { //Ürün rengi değiştirildiği için product_code güncellenmesi gerekli
                //EditProductCode(p.Id,p.ColorId);
            }
            EditSuccess();
        }

        /// <summary>
        /// yeni kaydı yapılan ürün adedini stoklara ekler
        /// </summary>
        /// <param name="p"></param>
        private void Migo(ref DBObjects.Product p)
        {
            //DBObjects.CDB db = new DBObjects.CDB(StaticObjects.AccessConnStr);

            //try
            //{
            //    p.Id = Convert.ToInt32(db.Get_Scalar("select top 1 Id from [Product] order by Id desc"));
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            //finally
            //{
            //    db.CloseDB();
            //    db = null;
            //}
            //string strSQL = "insert into [AllStocks] (ASRefId,ASRefWID,ASRefMID,ASAmount,ModifyDate)"
            //+ "values (@Id,@WID,@MID,@Amount,@ModifyDate)";

            //DBObjects.MySqlCmd cmd = new DBObjects.MySqlCmd(StaticObjects.MySqlConn);
   

            //try
            //{
            //    cmd.ExecuteNonQuery();
            //    InsertSuccess();
            //}
            //catch (Exception ex)
            //{
            //    string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
            //    excLogger = new ExceptionLogger(ex.Message, excSource);// DB ye log yaz
            //    ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(ex, ErrorMessages.MailServerNames.Gmail);
            //    excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
            //    excMail.ErrorSource = excSource + "()";
            //    excMail.Send(); // Mail at
            //}
            //finally
            //{
            //    db.CloseDB();
            //    db = null;
            //}
        }

        
        private void EditColors()
        {
            DBObjects.MySqlCmd cmd = new MySqlCmd(StaticObjects.MySqlConn);
            string sql = "insert into product_to_color (product_id,color_id) values (@pId,@cId)";
           // SelectedColorsList.RemoveAt(0); //ilk elemanı önceden kaydettik.
            try
            {
                cmd.CreateSetParameter("pId", MySql.Data.MySqlClient.MySqlDbType.Int32, this.productId);
                cmd.ExecuteNonQuery("delete from product_to_color where product_id=@pId");
                cmd.CreateParameter("cId", MySql.Data.MySqlClient.MySqlDbType.Int32);
                foreach (DBObjects.Color color in SelectedColorsList)
                {
                    cmd.SetParameterAt("cId", color.Id);
                    cmd.ExecuteNonQuery(sql);
                }
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, SaveColors() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
                //retValue = 0;
            }
            finally
            {
                cmd.Close();
                cmd = null;
            }
        
        }
        

        private void EditProductCode(int id, int color_id)
        {
            DBObjects.MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            string SQL1 = "select product_code from stock_details where product_id=" + id;
            List<string> old_p_code_list = new List<string>(); // ilgili product_id ye ait product code listemiz
            List<string> new_p_code_list = new List<string>();
            try
            {
                DataTable dt = db.GetDataTable(SQL1);

                if (dt.Rows.Count>0) // bu üründen yanlış renkte mal girişi yapılmış mı?
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        old_p_code_list.Add(item["product_code"].ToString().Trim());
                    }
                    new_p_code_list = SetNewProductCodes(old_p_code_list, color_id);
                }
            
            }
              
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, InsertProduct() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
                //retValue = 0;
            }
            finally
            {
                db.Close();
                db= null;
            }
            string SQL2 = "update stock_details set product_code=@new_product_code,product_color=@product_color where product_code=@old_product_code";
            DBObjects.MySqlCmd cmd;

             try
             {
                 for (int i = 0; i < new_p_code_list.Count; i++)
                 {
                     cmd= new MySqlCmd(StaticObjects.MySqlConn);
                     cmd.CreateSetParameter("product_color", MySql.Data.MySqlClient.MySqlDbType.Int32,color_id);
                     cmd.CreateSetParameter("new_product_code", MySql.Data.MySqlClient.MySqlDbType.Text, new_p_code_list[i]);
                     cmd.CreateSetParameter("old_product_code", MySql.Data.MySqlClient.MySqlDbType.Text, old_p_code_list[i]);
                     cmd.ExecuteNonQuery(SQL2);
                     cmd.Close();
                 }
               
             }
             catch (Exception e)
             {
                 ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                 excMail.Subject = "Stok Programı, InsertProduct() hata hk ";
                 excMail.Send();
                 ErrorMessages.Message message = new ErrorMessages.Message();
                 message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
                 //retValue = 0;
             }
            finally
            {
                cmd = null;
            }

        }
        private void  EditSuccess()
        {
            ErrorMessages.Message message = new ErrorMessages.Message();
            message.WriteMessage("Ürün,  başarılı bir şekilde düzenlendi.", MessageBoxIcon.Information, MessageBoxButtons.OK);
            Parent.Controls["pnl_master"].Visible = true;
            this.onProductGridChanged(EventArgs.Empty);
            this.Dispose();
            return;
        }

        private List<string> SetNewProductCodes( List<string> code_list, int color_id)
        {
            List<string> new_list = new List<string>();
            for(int i=0;i<code_list.Count;i++)
            {
                string []splits=code_list[i].Split(':');
              new_list.Add(splits[0]+":"+splits[1]+":"+color_id.ToString());
            }
            return new_list;
        }
        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }

        /// <summary>
        /// dosya uzantısını verir (örnek: jpg)
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        private string GetFileExtension(string fullName)
        { 
            string [] array=fullName.Split('.');
            return array[array.Length-1];
        }

       private string  GetFileName (string fullName)
        {
            if (fullName=="-1")
            {
                return fullName;
            }
            string[] array = fullName.Split('.');
            return array[array.Length - 2];
        }

        private void txt_urun_kodu_TextChanged(object sender, EventArgs e)
        {
           
         
        }

        /// <summary>
        /// checks if there is another product_code_manual on product_details table
        /// </summary>
        /// <param name="p_code_manual"></param>
        /// <returns></returns>
        private bool CheckProductCodeDuplicate(string p_code_manual)
        {
            bool retValue = false;
            MySqlDbHelper db = new MySqlDbHelper(StaticObjects.MySqlConn);
            try
            {
                object o = db.Get_Scalar("select product_code_manual from product_details where product_code_manual='"+p_code_manual+"'");

                if (o == (DBNull)null)
                {
                    retValue = false;
                }
                else retValue = true;
            }
            catch(Exception e)
            {
                    ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                    excMail.Subject = "Stok Programı, InsertProduct() hata hk ";
                    excMail.Send();
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
                    //retValue = 0;
            }
            finally
            {
                db.Close();
                db = null;
            }
            return retValue;
        
        }

        /// <summary>
        /// clear controls after product added
        /// </summary>
        private void ClearControls()
        {
            txt_urun_kodu.Text = "";
            txt_urunAdi.Text = "";
            txt_urunTanim.Text = "";
            spin_satis_fiyat.Value = 0;
            pictureEdit1.Visible = false;
        }

        //private void tree_category_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        //{
        //    if (!categoryFirstLoad)
        //    {
        //        DataRow dr = ((System.Data.DataRowView)(tree_category.GetDataRecordByNode(tree_category.FocusedNode))).Row;
        //        string cat_name = dr["cat_name"].ToString();
        //        txt_kategori.Text = cat_name;
        //        CItemList[0].Id = Convert.ToInt32(dr["cat_id"]);
        //        CItemList[0].Name = cat_name;
        //    }
        //    else categoryFirstLoad = !categoryFirstLoad;
        //}

        private void txt_urun_kodu_EditValueChanged(object sender, EventArgs e)
        {
            if (!productFirstLoad)
            {
                txt_urun_kodu.Text = txt_urun_kodu.Text.ToUpper();
                if (txt_urun_kodu.Text.Length == 15)
                {
                    if (CheckProductCodeDuplicate(txt_urun_kodu.Text))
                    {
                        ErrorMessages.Message message = new ErrorMessages.Message();
                        message.WriteMessage("Bu ürün kodu sistemde mevcut.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void txt_urun_kodu_EditValueChanged_1(object sender, EventArgs e)
        {
            if (!productFirstLoad)
            {
                if (txt_urun_kodu.Text.Length == 11)
                {
                    if (CheckProductCodeDuplicate(txt_urun_kodu.Text))
                    {
                        ErrorMessages.Message message = new ErrorMessages.Message();
                        message.WriteMessage("Bu ürün kodu sistemde mevcut.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void chk_listede_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_listede.CheckState == CheckState.Checked)
            {
                chk_listedeDegil.CheckState = CheckState.Unchecked;
                isProductOnMenu = true;
                
            }
        }

        private void chk_listedeDegil_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_listedeDegil.CheckState == CheckState.Checked)
            {
                chk_listede.CheckState = CheckState.Unchecked;
                isProductOnMenu = false;
            }
        }

        private void db_color_list_DoubleClick(object sender, EventArgs e)
        {
            //if (!isItemInColorList(db_color_list.SelectedValue))
            //{
            //    color_list.Items.Add(db_color_list.SelectedItem);
            //    SelectedColorsList.Add(CItemsList[db_color_list.SelectedIndex]);
            //    isColorChanged = true;
            //}
        }

       /* private bool isItemInColorList(object value)
        {
            bool retValue = false;
            foreach (object itemValue in color_list.Items)
            {
                if (itemValue.ToString() == value.ToString())
                {
                    retValue = true;
                    break;
                }
                else retValue = false;
            }
            return retValue;
        }*/

        private void btn_in_Click(object sender, EventArgs e)
        {
            //if (!isItemInColorList(db_color_list.SelectedValue))
            //{
            //    color_list.Items.Add(db_color_list.SelectedItem);
            //    SelectedColorsList.Add(CItemsList[db_color_list.SelectedIndex]);
            //    isColorChanged = true;
            //}
        }

        private void btn_out_Click(object sender, EventArgs e)
        {
            //if (color_list.Items.Count > 0)
            //{
            //    SelectedColorsList.RemoveAt(color_list.SelectedIndex);
            //    color_list.Items.Remove(color_list.SelectedItem);
            //    isColorChanged = true;
            //}       
        }

        private void color_list_DoubleClick(object sender, EventArgs e)
        {
            //if (color_list.Items.Count > 0)
            //{
            //    SelectedColorsList.RemoveAt(color_list.SelectedIndex);
            //    color_list.Items.Remove(color_list.SelectedItem);
            //    isColorChanged = true;
            //}       
        }

        private void tree_category_FocusedNodeChanged_1(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (!categoryFirstLoad)
            {
                DataRow dr = ((System.Data.DataRowView)(tree_category.GetDataRecordByNode(tree_category.FocusedNode))).Row;
                string cat_name = dr["cat_name"].ToString();
                txt_kategori.Text = cat_name;
                CItemList[0].Id = Convert.ToInt32(dr["cat_id"]);
                CItemList[0].Name = cat_name;
            }
            else categoryFirstLoad = !categoryFirstLoad;
        }

        private void spin_satis_fiyat_Click(object sender, EventArgs e)
        {
            this.spin_satis_fiyat.SelectAll();
        }

        private void spin_min_no_Click(object sender, EventArgs e)
        {
        //    this.spin_min_no.SelectAll();
        }

        private void spin_max_no_Click(object sender, EventArgs e)
        {
          //  this.spin_max_no.SelectAll();
        }

        private void btn_resimEkle_Click_1(object sender, EventArgs e)
        {
            if (!IsImageSelected)
            {
                IsImageSelected = false;
            }
            try
            {
                openFileDialog1.Filter = " (*.jpg)|*.jpg|(*.png)|*.png";
                openFileDialog1.Title = "Resim Dosyası Seçiniz";
                openFileDialog1.FileName = "Dosya";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    System.IO.FileInfo file = new FileInfo(openFileDialog1.FileName.ToString());
                  //  productImage = StaticObjects.ResizeImage(Image.FromFile(file.FullName), pictureEdit1.Width, pictureEdit1.Height);
                    productImage = StaticObjects.ResizeImage(Image.FromFile(file.FullName), StaticObjects.Settings.menu_item_width,StaticObjects.Settings.menu_item_height) ;
                    pictureEdit1.Image = productImage;
                    pictureEdit1.Visible = true;
                    IsImageSelected = true;
                    if (old_image_path == "-1")
                    {
                        imageName = this.productId + "." + GetFileExtension(file.Name);
                    }
                    else imageName = file.Name;
                }
                else return;

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

        }

        private void spin_satis1_5_Click(object sender, EventArgs e)
        {
            spin_satis1_5.SelectAll();
        }

        private void spin_satis_duble_Click(object sender, EventArgs e)
        {
            spin_satis_duble.SelectAll();
        }

        private void spliter_Panel2_Click(object sender, EventArgs e)
        {
            txt_urunTanim.Focus();
            txt_urunTanim.SelectAll();
        }

        //private void cb_color_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (productFirstLoad)
        //    {
        //        this.unChangedColorId = CItemsList[cb_color.SelectedIndex].Id;
        //    }
         
        //}

        //private void chk_adet_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chk_adet.CheckState == CheckState.Checked)
        //    {
        //        chk_gram.CheckState = CheckState.Unchecked;
        //        spinAgirlik.Enabled = false;
        //        spinAgirlik.Value = 0;
        //    }
        //}

        //private void chk_gram_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chk_gram.CheckState == CheckState.Checked)
        //    {
        //        chk_adet.CheckState = CheckState.Unchecked;
        //        spinAgirlik.Enabled = true;
        //        // spinAgirlik.Value = 1;
        //    }
        //}
    }
}
