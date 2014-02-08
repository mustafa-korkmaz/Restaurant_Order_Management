using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace StockProgram.DBObjects
{
    class ExceptionLogger
    {
        private  string excMessage { get; set; }
        private string excSource { get; set; }

        //  Sınıfın amacına uygun şekilde uyarlanması için örnek: 
        // excLogger = new ExceptionLogger(ex.Message, this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name);
        public ExceptionLogger(string excMessage, string excSource)
        {
            this.excMessage = excMessage;
            this.excSource = excSource;
            WriteExceptionLog();
        }

        private void WriteExceptionLog()
        {
            MySqlCmd cmd = new MySqlCmd(StaticObjects.MySqlConn);
            try
            {
                string proc_name = "logEx";
                cmd.CreateSetParameter("msg", MySql.Data.MySqlClient.MySqlDbType.Text, excMessage);
                cmd.CreateSetParameter("source", MySql.Data.MySqlClient.MySqlDbType.VarChar, excSource+"()");
                cmd.ExecuteNonQuerySP(proc_name);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmd.Close();
            }
        }
    }
}
