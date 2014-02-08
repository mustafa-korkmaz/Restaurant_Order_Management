using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.DBObjects
{
    class Supplier
    {
        #region Variables
        public int Id { get; set; }
        public int DisplayOrder{ get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public int IsDeleted { get; set; }
        public DateTime ModifiedDate { get; set; }
        #endregion

        public Supplier()
        {
        }
        
    }
}
