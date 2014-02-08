using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.DBObjects
{
    public class TableCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status status { get; set; }
        public int display_order { get; set; }
        public TableCategory()
        {
            status = new Status();
        }
        public TableCategory(int id)
        {
            this.Id = id;
        }
    }
}
