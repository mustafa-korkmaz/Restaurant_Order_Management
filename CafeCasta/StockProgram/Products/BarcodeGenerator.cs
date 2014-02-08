using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockProgram.DBObjects;

namespace StockProgram.Products
{
    class BarcodeGenerator
    {
        string product_code_pattern;
        public BarcodeGenerator(int id,int size,int color)
        {
            this.product_code_pattern = id.ToString() + size.ToString() + color.ToString();
          //  this.product_code_pattern = id.ToString(); //pattern için şimdilik sadece id yeterli
        }
        public string GetBarcode()
        {
            string barcode = (Convert.ToInt32(this.product_code_pattern) + 1000000000).ToString();
            return barcode;
        }
    }

}
