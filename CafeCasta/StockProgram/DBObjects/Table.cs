using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.DBObjects
{
    public class Table
    {
        public int id { get; set; }
        public string name { get; set; }
        public TableCategory category { get; set; }
        public Status status { get; set; }
        public int is_deleted { get; set; }
        public int display_order { get; set; }
        public DateTime create_date { get; set; }

        public Table()
        { 
        }
        public Table(int id)
        {
            this.id = id;
            category = new TableCategory();
            status = new Status();
        }
      
    }
}
