using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace StockProgram
{
    class DataTableColumn:DataColumn
    {
        private DataTable dt;
        private string type;
        private string columnName;
   
        public DataTableColumn(ref DataTable dt)
        {
            this.dt = dt;
        }

        /// <summary>
        /// ilgili dt ye kolonu ekler
        /// </summary>
        /// <returns></returns>
        public virtual bool AddColumn(string columnName,string type)
        {
            switch (type)
            {
                case "String":
                    type = "System.String";
                    break;
                case "Int":
                    type = "System.Int32";
                    break;
                default:
                    break;
            }
            try
            {
                DataColumn c = new DataColumn(columnName);
                c.DataType = System.Type.GetType(type);
                this.dt.Columns.Add(c);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
