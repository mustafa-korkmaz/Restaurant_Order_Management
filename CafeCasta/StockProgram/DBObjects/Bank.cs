using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.DBObjects
{
    class Bank
    {
        public Bank()
        { 
        
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Total{ get; set; }
        public DateTime ModifiedDate { get; set; }
        public int IsDeleted{ get; set; }
    }
    class BankInstalment
    {
        public BankInstalment()
        { 
            
        }
        public int instalment { get; set; }
        public double rate { get; set; }
        public int payment_day { get; set; }
    }
}
