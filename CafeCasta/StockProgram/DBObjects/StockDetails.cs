using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.DBObjects
{
    class StockDetails
    {
        public StockDetails()
        { 
        
        }

        public int ProductId { get; set; }
        public int ProductAmount { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
