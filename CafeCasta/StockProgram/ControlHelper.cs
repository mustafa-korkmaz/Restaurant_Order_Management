using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace StockProgram
{
  /// <summary>
  ///  gönderilen control nesnesi için yapılacak işlemleri barındıran sınıf
  /// </summary>
    class ControlHelper
    {
        //private Enums.RepositoryItemType rType;
        //private object rItem;

        public ControlHelper()
        { 
        
        }

        /// <summary>
        /// Ürün listesini döndürür 
        /// Liste indexlemesi combobox ya da list box controllerindeki gösterim sırası ile aynıdır.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<StockProgram.DBObjects.Product> GetProducts(ref DataTable dt)
        {
            List<StockProgram.DBObjects.Product> PItemList = new List<DBObjects.Product>();
            foreach (DataRow row in dt.Rows)
            {
                int Id = Convert.ToInt32(row["product_id"].ToString());
                string PName = (row["product_name"].ToString());
                PItemList.Add(new StockProgram.DBObjects.Product { Id = Id, Name = PName });
            }
            return PItemList;
        }

        /// <summary>
        /// status listesini döndürür 
        /// Liste indexlemesi combobox ya da list box controllerindeki gösterim sırası ile aynıdır.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<StockProgram.DBObjects.Status> GetStatusDetails(ref DataTable dt)
        {
            List<StockProgram.DBObjects.Status> SItemList = new List<DBObjects.Status>();
            foreach (DataRow row in dt.Rows)
            {
                int Id = Convert.ToInt32(row["status_id"].ToString());
                string SName = (row["status_name"].ToString());
                SItemList.Add(new StockProgram.DBObjects.Status { id = Id, name = SName });
            }
            return SItemList;
        }
        /// <summary>
        /// Tedarikçi listesini döndürür 
        /// Liste indexlemesi combobox ya da list box controllerindeki gösterim sırası ile aynıdır.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<StockProgram.DBObjects.Supplier> GetSuppliers(ref DataTable dt)
        {
            List<StockProgram.DBObjects.Supplier> MItemList = new List<DBObjects.Supplier>();
            foreach (DataRow row in dt.Rows)
            {
               int MID = Convert.ToInt32(row["suppliers_id"].ToString());
               string MName = (row["suppliers_name"].ToString());
                MItemList.Add(new StockProgram.DBObjects.Supplier { Id=MID,Name=MName});
            }
            return MItemList;
        }

        /// <summary>
        /// Table category listesini döndürür 
        /// Liste indexlemesi combobox ya da list box controllerindeki gösterim sırası ile aynıdır.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<StockProgram.DBObjects.TableCategory> GetTableCategories(ref DataTable dt)
        {
            List<StockProgram.DBObjects.TableCategory> tCatItemList = new List<DBObjects.TableCategory>();
            foreach (DataRow row in dt.Rows)
            {
                int ID = Convert.ToInt32(row["tcategory_id"].ToString());
                string Name = (row["tcategory_name"].ToString());
                tCatItemList.Add(new StockProgram.DBObjects.TableCategory { Id = ID, Name = Name });
            }
            return tCatItemList;
        }

        /// <summary>
        /// Depo listesini döndürür 
        /// Liste indexlemesi combobox ya da list box controllerindeki gösterim sırası ile aynıdır.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<StockProgram.DBObjects.Warehouse> GetWarehouses(ref DataTable dt)
        {
            List<StockProgram.DBObjects.Warehouse> WItemList = new List<DBObjects.Warehouse>();
            foreach (DataRow row in dt.Rows)
            {
                int WID = Convert.ToInt32(row["WID"].ToString());
                string WName = (row["WName"].ToString());
                WItemList.Add(new StockProgram.DBObjects.Warehouse { wID = WID,wName = WName });
            }
            return WItemList;
        }

         /// <summary>
        /// Tedarikçi listesini döndürür 
        /// Liste indexlemesi combobox ya da list box controllerindeki gösterim sırası ile aynıdır.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<StockProgram.DBObjects.CategoryItem> GetCategories(ref DataTable dt)
        {
            List<StockProgram.DBObjects.CategoryItem> CItemList = new List<StockProgram.DBObjects.CategoryItem>();
            CItemList.Add(new StockProgram.DBObjects.CategoryItem { ParentId = 0, Id = 0, Name = "En Üst Kategori" });
            foreach (DataRow row in dt.Rows)
            {
               int id = Convert.ToInt32(row["cat_id"].ToString());
               int parentId = Convert.ToInt32(row["parent_id"].ToString());
               string CName = row["cat_name"].ToString();
               CItemList.Add(new DBObjects.CategoryItem { Id=id,ParentId=parentId,Name=CName});
            }
            return CItemList;
        }

        /// <summary>
        /// Renk listesini döndürür 
        /// Liste indexlemesi combobox ya da list box controllerindeki gösterim sırası ile aynıdır.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<StockProgram.DBObjects.Color> GetColors(ref DataTable dt)
        {
            List<StockProgram.DBObjects.Color> CItemList = new List<StockProgram.DBObjects.Color>();
            foreach (DataRow row in dt.Rows)
            {
                int id = Convert.ToInt32(row["color_id"].ToString());
                string CName = row["color_name"].ToString();
                CItemList.Add(new DBObjects.Color { Id = id, Name = CName });
            }
            return CItemList;
        }

        /// <summary>
        /// Option listesini döndürür 
        /// Liste indexlemesi combobox ya da list box controllerindeki gösterim sırası ile aynıdır.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<StockProgram.DBObjects.Options> GetOptions(ref DataTable dt)
        {
            List<StockProgram.DBObjects.Options> optionsList = new List<StockProgram.DBObjects.Options>();
            foreach (DataRow row in dt.Rows)
            {
                int id = Convert.ToInt32(row["option_id"].ToString());
                string name = row["option_name"].ToString();
                optionsList.Add(new DBObjects.Options { option_id = id, option_name = name });
            }
            return optionsList;
        }

        /// <summary>
        /// Banka listesini döndürür 
        /// Liste indexlemesi combobox ya da list box controllerindeki gösterim sırası ile aynıdır.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<StockProgram.DBObjects.Bank> GetBanks(ref DataTable dt)
        {
            List<StockProgram.DBObjects.Bank> BItemList = new List<StockProgram.DBObjects.Bank>();
       //     BItemList.Add(new DBObjects.Bank { Id = 0, Name = "POS Seçiniz" });
            foreach (DataRow row in dt.Rows)
            {
                int id = Convert.ToInt32(row["bank_id"].ToString());
                string name = row["bank_name"].ToString();
                BItemList.Add(new DBObjects.Bank { Id = id, Name = name });
            }
            return BItemList;
        }

        /// <summary>
        /// Müşteri listesini döndürür 
        /// Liste indexlemesi combobox ya da list box controllerindeki gösterim sırası ile aynıdır. 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<StockProgram.DBObjects.Customer> GetCustomers(ref DataTable dt)
        {
            List<StockProgram.DBObjects.Customer> CItemList = new List<StockProgram.DBObjects.Customer>();
            //     BItemList.Add(new DBObjects.Bank { Id = 0, Name = "POS Seçiniz" });
            foreach (DataRow row in dt.Rows)
            {
                int id = Convert.ToInt32(row["customer_id"].ToString());
                string name = row["customer_name"].ToString();
                string adress = row["customer_address"].ToString();
                string tel = row["customer_tel"].ToString();
                string note = row["customer_note"].ToString();
                CItemList.Add(new DBObjects.Customer { id = id, name = name,tel=tel,adress=adress,note=note });
            }
            return CItemList;
        }

        public List<StockProgram.DBObjects.Staff> GetStaff(ref DataTable dt)
        {
            List<StockProgram.DBObjects.Staff> StaffList = new List<StockProgram.DBObjects.Staff>();
            //     BItemList.Add(new DBObjects.Bank { Id = 0, Name = "POS Seçiniz" });
            foreach (DataRow row in dt.Rows)
            {
                int id = Convert.ToInt32(row["id"].ToString());
                string name = row["name"].ToString();
                StaffList.Add(new DBObjects.Staff { id = id, name = name });
            }
            return StaffList;
        }

        /// <summary>
        ///  Fills the given control with related table records
        /// </summary>
        /// <param name="rItem"></param>
        /// <param name="rType"></param>
        /// <param name="dt"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public  int FillControl(object rItem, Enums.RepositoryItemType rType, ref DataTable dt, string columnName)
        {
            switch (rType)
            {
                case Enums.RepositoryItemType.ComboBox:
                    foreach (DataRow row in dt.Rows)
                    {
                        ((DevExpress.XtraEditors.ComboBoxEdit)rItem).Properties.Items.Add(row[columnName]);
                    }
                    break;

                case Enums.RepositoryItemType.ListBox:
                    foreach (DataRow row in dt.Rows)
                    {
                        ((DevExpress.XtraEditors.ListBoxControl)rItem).Items.Add(row[columnName]);
                    }
                    break;

                default:
                    break;
            }
            return 1;
        }

    }
}
