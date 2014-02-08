
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockProgram.DBObjects;

namespace StockProgram.Tables
{
    class TableWithAccount
    {
        private Table table;
        private int account_id;
        public TableWithAccount(Table t,int account_id)
        {
            this.table = t;
            this.account_id = account_id;
        }

        public int GetAccountID()
        {
        return  this.account_id;
        }
        public Table GetTable()
        {
            return this.table;
        }
    }
}
