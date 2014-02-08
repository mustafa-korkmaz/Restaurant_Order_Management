using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.DBObjects
{
    class SellList
    {
        public SellList()
        { }

        public int Id { get; set; }
        public int SellId { get; set; }
        public int ItemId { get; set; }
        public double ItemPrice { get; set; }
        public int ItemAmount { get; set; }
    }
}
