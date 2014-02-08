using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace StockProgram.Categories
{
    class Generation
    {
        public int GenerationID { get; set; } // Top yekün aynı nesile ait olan bir parent id gibi düşünelim.
        public List<StockProgram.DBObjects.CategoryItem> CatItemList { get; set; }
    }
}
