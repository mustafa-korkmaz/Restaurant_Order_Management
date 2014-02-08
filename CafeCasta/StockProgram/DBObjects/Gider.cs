using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.DBObjects
{
    class Gider
    {
        public Gider()
        { 
        
        }

        public int Id { get; set; }
        public int ProcessId { get; set; }
        public int type { get; set; }
        public double Price { get; set; }
        public string ProcessName { get; set; }
        public string Desc { get; set; }
    }
}
