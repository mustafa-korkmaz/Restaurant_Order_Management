using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.DBObjects
{
    class Staff
    {
         public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public Enums.UserRole role { get; set; }
        public bool isDeleted { get; set; }

        public Staff()
        { 
        //default constructor
        }
        public Staff(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
