using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.DBObjects
{
    class SupplierPayment
    {
        public SupplierPayment()
        { }

        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int PaymentType { get; set; }
        public double PaymentPrice { get; set; }
        public double PaymentKDV { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
