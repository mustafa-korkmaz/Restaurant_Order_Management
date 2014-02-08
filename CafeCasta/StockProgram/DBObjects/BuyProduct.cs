using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.DBObjects
{
    class BuyProduct
    {
        public int BuyId { get; set; }
        public int ProductId { get; set; }
        public int Goods_id { get; set; }
        public int SupplierId { get; set; }
        public string Desc { get; set; }
        public int ProductCount { get; set; }
        public int ProductColorId { get; set; }
        public double ProductSize { get; set; }
        public List<ProductAttributes> ProductAttributeList { get; set; }
        public double ProductBuyPrice { get; set; }
        public double ProductSellPrice { get; set; }
        public double KDV { get; set; }
        public Products.Label Label { get; set; }
        public Enums.Currency Currency
        {
            get
            {
                return Enums.Currency.TL; //şimdilik sadece TL 
            }
        }
        public string ProductCode
        {
            get
            { //returns auto generated product code
                char delimeter = ':';
                return this.ProductId.ToString() + delimeter+ this.ProductSize.ToString() +delimeter+ ProductColorId.ToString(); 
            }
        }

        public BuyProduct()
        {
            this.ProductAttributeList = new List<ProductAttributes>();
            this.Label = new Products.Label();
        }

    }

    public class ProductAttributes
    {
        public double Size { get; set; }
        public int Color { get; set; }
        public int Amount { get; set; }
        public string Barcode { get; set; }
    }
}
