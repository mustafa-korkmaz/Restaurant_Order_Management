using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.DBObjects
{
    class Warehouse
    {
        #region Variables
        public int wID { get; set; }
        public int wRefMID { get; set; } //deponun baglı oldugu tedarikçinin ID si
        public Enums.WarehouseStatus wStatus { get; set; }
        public string wDescription { get; set; }
        public string wName { get; set; }
        public string wRefMName { get; set; } //deponun baglı oldugu tedarikçinin adı
        #endregion

        public Warehouse()
        {
        }
    }
}
