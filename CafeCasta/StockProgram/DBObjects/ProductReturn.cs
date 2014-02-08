using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.DBObjects
{
    class ProductReturn
    {
        public ProductReturn()
        { }
        public string ProductName { get; set; }
        public string ProductColor { get; set; }
        public int ProductId { get; set; }
        public int SuppliersId { get; set; }
        public double ProductPrice { get; set; }
        public double ProductSize { get; set; }
        public double ProductAmount { get; set; }
        public int ProductColorId {get; set; }
        public string ProductUnit { get; set; }
        public int sell_id { get; set; }
        public int customer_id { get; set; }
        public string ProductCode
        {
            get
            { //returns auto generated product code
              //  char delimeter = ':';
                return this.ProductId.ToString() ;
               // return this.ProductId.ToString() + delimeter + this.ProductSize.ToString() + delimeter + ProductColorId.ToString();
            }
        }
    }
}
