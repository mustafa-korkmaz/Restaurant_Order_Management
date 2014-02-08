using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.DBObjects
{
    public class Customer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string tel { get; set; }
        public string mail { get; set; }
        public string adress { get; set; }
        public string note { get; set; }
        public bool isDeleted { get; set; }

        public Customer()
        { 
        //default constructor
        }
        public Customer(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
