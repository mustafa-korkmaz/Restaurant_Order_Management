using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.DBObjects
{
    class Goods
    {
        public int id { get; set; }
        public string name { get; set; }
        public string unit { get; set; }
        public bool isDeleted { get; set; }

        public Goods()
        { 
        //default constructor
        }
        public Goods(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
