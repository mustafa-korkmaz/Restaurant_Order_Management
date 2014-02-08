using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.DBObjects
{
    public class Product
    {
        #region Variables
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Code { get; set; }
  //      public string  PSupplierID { get; set; }  // tedarikçiden gelebilecek olan hazır ürün kodları
        public string Name { get; set; }
        public string Desc { get; set; }
        public Enums.Currency Currency { get; set; }
        public double SelectedSalePrice { get; set; }
        public double SalePrice { get; set; }
        public double SalePrice_bucuk { get; set; }
        public double SalePrice_double { get; set; }
        public double BuyingPrice { get; set; }
        public int CategoryId { get; set; }
        public string ImagePath { get; set; }
        public Enums.Unit Unit { get; set; }
        public double UnitAmount { get; set; }
        public int MinSize { get; set; }
        public int MaxSize { get; set; }
        public int TotalAmount { get; set; }
        public int SupplierId { get; set; } // Supplier ID
        public int IsDeleted { get; set; }
        public int IsOnMenu { get; set; }
        public int KDV { get; set; }
        public List<DBObjects.Color> ColorList { get; set; }
        public int ColorId { get; set; }
        public string ColorName { get; set; }
     //   public Enums.Decision InStock { get; set; }
        public int RefWID { get; set; } //warehouse ID
        public DateTime ModifiedDate
        {
            get { return DateTime.Now; }
        }
      
        #endregion

        public Product()
        {
        }

    }
    public class CategoryItem
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

}
