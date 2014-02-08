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
    public partial class ucAddProduct : DevExpress.XtraEditors.XtraUserControl
    {
        private List<DBObjects.Color> CItemsList;
        private List<DBObjects.Color> SelectedColorList;
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

        public ucAddProduct()
        {
            this.controlHelper = new ControlHelper();
            categoryFirstLoad = true;
            InitializeComponent();
         //   FillWarehouses();
            FillCategory();
            FillColors();

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
        //    categoryFirstLoad = true;
            SetFormElements();
        }

        private void SetFormElements()
        {
            this.chk_gram.CheckState = CheckState.Checked;
            this.chk_adet.CheckState = CheckState.Unchecked;
            this.cb_para.Text = cb_para.Properties.Items[0].ToString(); //default TL gelsin
            //spin_max_no.Properties.Increment = 2;
            //spin_min_no.Properties.Increment = 2;
        }

        protected virtual void onProductGridChanged(EventArgs e)
        {
            if (ProductGridChanged != null)
                ProductGridChanged(this, e);
        }
        private void btn_resimEkle_Click(object sender, EventArgs e)
        {
            IsImageSelected = false;
            try
            {
                openFileDialog1.Filter = " (*.jpg)|*.jpg|(*.png)|*.png";
                openFileDialog1.Title = "Resim Dosyası Seçiniz";
                openFileDialog1.FileName = "Dosya";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    System.IO.FileInfo file = new FileInfo(openFileDialog1.FileName.ToString());
                 //   productImage = StaticObjects.ResizeImage(Image.FromFile(file.FullName), pictureEdit1.Width, pictureEdit1.Height);
                    productImage = StaticObjects.ResizeImage(Image.FromFile(file.FullName),StaticObjects.Settings.menu_item_width,StaticObjects.Settings.menu_item_height);
                    pictureEdit1.Image = productImage;
                    pictureEdit1.Visible = true;
                    IsImageSelected = true;
                    imageName = file.Name;
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


        private void SaveImage(int product_id,string fileExtension)
        {
            string root = Application.StartupPath + StaticObjects.MainImagePath+product_id+"."+fileExtension;
            try
            {
                productImage.Save(root);

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
                InitializeCategoryItems(ref dt);
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

        private void FillColors()
        {
            //fill Suppliers
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select * from color_details where is_deleted=0 order by color_name asc";
            CItemsList = new List<DBObjects.Color>();
            SelectedColorList = new List<DBObjects.Color>();
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
              //  controlHelper.FillControl(cb_color, Enums.RepositoryItemType.ComboBox, ref dt, "color_name");
               // controlHelper.FillControl(db_color_list, Enums.RepositoryItemType.ListBox, ref dt, "color_name");
                cb_color.Text = "Renk Seçiniz";
                CItemsList = controlHelper.GetColors(ref dt);
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

        private void InitializeCategoryItems(ref DataTable dt)
        {
            CItemList = new List<CategoryItem>();
           // CItemList.Add(new CategoryItem { ParentId = 0, Id = 0, Name = "En Üst Kategori" });
            foreach (DataRow row in dt.Rows)
            {
                CItem = new CategoryItem();
                CItem.Id = Convert.ToInt32(row["cat_id"].ToString());
                CItem.ParentId = Convert.ToInt32(row["parent_id"].ToString());
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

            //if (spinAgirlik.Value <= 0 && chk_gram.CheckState == CheckState.Checked || spinAgirlik.Value < 0)
            //{
            //    message.WriteMessage("Birim ağırlık hatalı.", MessageBoxIcon.Error, MessageBoxButtons.OK);
            //    return;
            //}
            //if (spin_max_no.Value%2==1 || spin_min_no.Value%2==1)
            //{
            //     message.WriteMessage("Beden seçenekleri çift olmalıdır.", MessageBoxIcon.Error, MessageBoxButtons.OK);
            //     return;
            //}

            if (cb_para.Text == "TL" || cb_para.Text == "USD")
            {
                //true
            }
            else
            {
                message.WriteMessage("Para birimi seçimi hatalı.", MessageBoxIcon.Error, MessageBoxButtons.OK);
                return;
            }
            if (chk_adet.CheckState==CheckState.Unchecked&&chk_gram.CheckState==CheckState.Unchecked)
            {
                message.WriteMessage("Adet ya da gram bazlı seçeneklerinden birini seçiniz.", MessageBoxIcon.Error, MessageBoxButtons.OK);
                return;
            }
            //if (txt_urun_kodu.Text=="")
            //{
            //    message.WriteMessage("Ürün kodu giriniz", MessageBoxIcon.Error, MessageBoxButtons.OK);
            //    return;
            //}
            if (txt_urunAdi.Text == "")
            {
                message.WriteMessage("Ürün adı giriniz", MessageBoxIcon.Error, MessageBoxButtons.OK);
                return;
            }
            //if (color_list.Items.Count==0)
            //{
            //    message.WriteMessage("Renk seçiniz", MessageBoxIcon.Error, MessageBoxButtons.OK);
            //    return;
            //}

            DBObjects.Product product = new DBObjects.Product();
         //   product.ColorList=new List<DBObjects.Color>();
            product.Desc = txt_urunTanim.Text.ToUpper();
            product.Currency = (cb_para.Text == "TL") ? Enums.Currency.TL : Enums.Currency.USD; 
         //   product.Code = txt_urun_kodu.Text;
            product.Code = "YEMEK";
            product.Name = txt_urunAdi.Text.ToUpper();
            product.Unit = (chk_adet.CheckState == CheckState.Checked) ? Enums.Unit.adet : Enums.Unit.g;
            product.UnitAmount = Convert.ToDouble(spinAgirlik.Value);
            //product.MinSize = Convert.ToInt32(spin_min_no.Value);
            //product.MaxSize = Convert.ToInt32(spin_max_no.Value);
            product.SalePrice = Convert.ToDouble(spin_satis_fiyat1.Value);
            product.SalePrice_bucuk = Convert.ToDouble(spin_satis1_5.Value);
            product.SalePrice_double = Convert.ToDouble(spin_satis_duble.Value);
            product.CategoryId = CItemList[tree_category.FocusedNode.Id].Id;
            product.ColorList = this.SelectedColorList;
     //       product.ColorId = product.ColorList[0].Id;

            if (IsImageSelected == true)
            {
                product.ImagePath = imageName;
            }
            else product.ImagePath = "-1";

            InsertProduct(ref product);
        }
        /// <summary>
        /// inserts a new product record
        /// </summary>
        /// <param name="p"></param>
        private  void InsertProduct(ref DBObjects.Product p)
        {
            int id;
            DBObjects.MySqlCmd cmd = new MySqlCmd(StaticObjects.MySqlConn);
            string proc_name = "saveProduct";
            string fileExtension = GetFileExtension(p.ImagePath);
            try
            {
                cmd.CreateSetParameter("pCode", MySql.Data.MySqlClient.MySqlDbType.VarChar, p.Code);
                cmd.CreateSetParameter("pCat",MySql.Data.MySqlClient.MySqlDbType.Int32,p.CategoryId);
                cmd.CreateSetParameter("pName",MySql.Data.MySqlClient.MySqlDbType.VarChar,p.Name);
                cmd.CreateSetParameter("pDesc", MySql.Data.MySqlClient.MySqlDbType.VarChar, p.Desc);
                cmd.CreateSetParameter("pPrice", MySql.Data.MySqlClient.MySqlDbType.Double, p.SalePrice);
                cmd.CreateSetParameter("pPrice_bucuk", MySql.Data.MySqlClient.MySqlDbType.Double, p.SalePrice_bucuk);
                cmd.CreateSetParameter("pPrice_double", MySql.Data.MySqlClient.MySqlDbType.Double, p.SalePrice_double);
            //    cmd.CreateSetParameter("pColor", MySql.Data.MySqlClient.MySqlDbType.Int32,p.ColorId);
                cmd.CreateSetParameter("pImg", MySql.Data.MySqlClient.MySqlDbType.VarChar,fileExtension);
                cmd.CreateSetParameter("min_size", MySql.Data.MySqlClient.MySqlDbType.Int32, 0);
                cmd.CreateSetParameter("max_size", MySql.Data.MySqlClient.MySqlDbType.Int32,0);//gereksiz alanlar
             //   cmd.CreateSetParameter("pCurrency", MySql.Data.MySqlClient.MySqlDbType.Int16,Convert.ToInt16(p.Currency));
                cmd.CreateSetParameter("pUnit", MySql.Data.MySqlClient.MySqlDbType.Int16, Convert.ToInt16(p.Currency));
                //cmd.CreateSetParameter("pUnitAmount", MySql.Data.MySqlClient.MySqlDbType.Double, p.UnitAmount);
                cmd.CreateOuterParameter("pId", MySql.Data.MySqlClient.MySqlDbType.Int32);
                cmd.ExecuteNonQuerySP(proc_name);
                id = Convert.ToInt32(cmd.GetParameterValue("pId"));
                if (p.ImagePath!="-1")
                {// save image to related folder               
                    SaveImage(id,fileExtension);                   
                 }
                //if (p.ColorList.Count>1)
                //{
                //    SaveColors(id,p.ColorList);
                //}

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
            onProductGridChanged(EventArgs.Empty);
            ClearControls();
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

        /// <summary>
        /// Ürüne ekelenen 1 den fazla renk seçeneği oldugunda db kayıt işlemini yapar.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="colors"></param>
        private void SaveColors(int id, List<DBObjects.Color> colors)
        {
            DBObjects.MySqlCmd cmd = new MySqlCmd(StaticObjects.MySqlConn);
            string sql = "insert into product_to_color (product_id,color_id) values (@pId,@cId)";
            colors.RemoveAt(0); //ilk elemanı önceden kaydettik.
            try
            {
                cmd.CreateSetParameter("pId", MySql.Data.MySqlClient.MySqlDbType.Int32, id);
                cmd.CreateParameter("cId", MySql.Data.MySqlClient.MySqlDbType.Int32);
                foreach (DBObjects.Color color in colors)
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

        private void  InsertSuccess()
        {
            ErrorMessages.Message message = new ErrorMessages.Message();
            message.WriteMessage("Ürün,  başarılı bir şekilde eklendi.", MessageBoxIcon.Information, MessageBoxButtons.OK);
            txt_urunTanim.Text = "";
            txt_urunAdi.Text = "";
         //   txt_barkodNo.Text = "";
       //     txt_tedarikciUrunKodu.Text = "";
         //   txt_toplamAdet.Text ="1";
            return;
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

        private void txt_urun_kodu_TextChanged(object sender, EventArgs e)
        {
            txt_urun_kodu.Text = txt_urun_kodu.Text.ToUpper();
            if (txt_urun_kodu.Text.Length==15)
            {
                if (CheckProductCodeDuplicate(txt_urun_kodu.Text))
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Bu ürün kodu sistemde mevcut.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                }
            }
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
            lbl_isim.BringToFront();
            lbl_currency.BringToFront();
            lbl_birim_Satis.BringToFront();
            lbl_cat.BringToFront();
            lbl_code.BringToFront();
            lbl_tanim.BringToFront();
            txt_urun_kodu.Text = "";
            txt_urunAdi.Text = "";
            txt_urunTanim.Text = "";
            spin_satis_fiyat1.Value = 0;
            pictureEdit1.Visible = false;
            cb_color.SelectedIndex = -1;
            cb_color.Text = "Renk Seçiniz";
            SelectedColorList.Clear();
            //color_list.Items.Clear();
            spliter.Panel2.SendToBack();
            //tabControl.SelectedTabPageIndex = 0;
            spin_satis1_5.Value = 0;
            spin_satis_fiyat1.Value = 0;
            spin_satis_duble.Value = 0;
        }

        private void spin_satis_fiyat_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(spin_satis_fiyat1.Value) < 0)
            {
                spin_satis_fiyat1.Value += spin_satis_fiyat1.Properties.Increment;
                return;
            }
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
      //      txt_urun_kodu.Text = txt_urun_kodu.Text.ToUpper();
            if (txt_urun_kodu.Text.Length == 11)
            {
                if (CheckProductCodeDuplicate(txt_urun_kodu.Text))
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Bu ürün kodu sistemde mevcut.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                }
            }
        }

        private void tree_category_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (!categoryFirstLoad)
            {
                DataRow dr = ((System.Data.DataRowView)(tree_category.GetDataRecordByNode(tree_category.FocusedNode))).Row;
                string cat_name = dr["cat_name"].ToString();
                txt_kategori.Text = cat_name;
            }
            else categoryFirstLoad = !categoryFirstLoad;
        }

        private void chk_adet_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_adet.CheckState == CheckState.Checked)
            {
                chk_gram.CheckState = CheckState.Unchecked;
                spinAgirlik.Enabled = false;
                spinAgirlik.Value = 0;
            }
      
        }

        private void chk_gram_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_gram.CheckState == CheckState.Checked)
            {
                chk_adet.CheckState = CheckState.Unchecked;
                spinAgirlik.Enabled = true;
               // spinAgirlik.Value = 1;
            }
        }

        private void color_list_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void spin_min_no_Click(object sender, EventArgs e)
        {
           // this.spin_min_no.SelectAll();
        }

        private void spin_max_no_Click(object sender, EventArgs e)
        {
            //this.spin_max_no.SelectAll();
        }

        private void btn_in_Click(object sender, EventArgs e)
        {
            //if (!isItemInColorList(db_color_list.SelectedValue))
            //{
            //    color_list.Items.Add(db_color_list.SelectedItem);
            //    SelectedColorList.Add(CItemsList[db_color_list.SelectedIndex]);
            //}
        }

        private void db_color_list_DoubleClick(object sender, EventArgs e)
        {
            //if (!isItemInColorList(db_color_list.SelectedValue))
            //{
            //    color_list.Items.Add(db_color_list.SelectedItem);
            //    SelectedColorList.Add(CItemsList[db_color_list.SelectedIndex]);       
            //}
        }

        private void color_list_DoubleClick(object sender, EventArgs e)
        {
            //if (color_list.Items.Count > 0)
            //{
            //    SelectedColorList.RemoveAt(color_list.SelectedIndex);
            //    color_list.Items.Remove(color_list.SelectedItem);
            //}
        }

        private void btn_out_Click(object sender, EventArgs e)
        {
            //if (color_list.Items.Count>0)
            //{
            //    SelectedColorList.RemoveAt(color_list.SelectedIndex);
            //    color_list.Items.Remove(color_list.SelectedItem);
            //}       
        }

        //private bool isItemInColorList(object value)
        //{
        //    bool retValue=false;
        //    foreach (object itemValue in color_list.Items)
        //    {
        //        if (itemValue == value)
        //        {
        //            retValue = true;
        //            break;
        //        }
        //        else retValue = false;
        //    }
        //    return retValue;
        //}

        private void spin_satis_fiyat_Click(object sender, EventArgs e)
        {
            this.spin_satis_fiyat1.SelectAll();
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
    }
}
