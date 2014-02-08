using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockProgram.Sales;
using System.Data;

namespace StockProgram.DBObjects
{
    public  class SiparisHandler
    {
        private DataTable siparisTable;

        public SiparisHandler(ref DataTable siparisTable)
        {
            this.siparisTable = siparisTable;
        }

        public List<SiparisKalem> GetSiparisList()
        {
            SiparisKalem sk;
            List<SiparisKalem> sList=new List<SiparisKalem>();
            foreach (DataRow row in siparisTable.Rows)
            {
                sk = new Sales.SiparisKalem();
                sk.Id = Convert.ToInt32(row["log_id"]);
                sk.ProductId = Convert.ToInt32(row["product_id"]);
                sk.ProductName = row["product_name"].ToString();
                sk.Amount = Convert.ToInt32(row["amount"]);
                sk.Porsion = Convert.ToDouble(row["porsion"]);
                sk.SalePrice = sk.UndiscountedPrice = Convert.ToDouble(row["product_price"]);
                sk.Desc = row["product_desc"].ToString();
                sList.Add(sk);
            }

            return sList;
        }
    }
}
