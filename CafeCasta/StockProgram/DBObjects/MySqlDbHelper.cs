using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace StockProgram.DBObjects
{
    class MySqlDbHelper
    {
            private MySqlTransaction sqlTrans { get; set; }
            private MySqlConnection conn { get; set; }
            
        #region Constructors
        public MySqlDbHelper(string conn)
        {
            this.conn = new MySqlConnection(conn);
         
            this.conn.Open();
        }
        #endregion 

        public DataSet GetDataSet(string strSQL, string TableName)
        {
            MySqlDataAdapter DA;
            DataSet ds = new DataSet();
            try
            {
                DA = new MySqlDataAdapter(strSQL, this.conn);
                this.sqlTrans = this.conn.BeginTransaction();
                //this.SqlCmd.Transaction = sqlTrans;
                DA.Fill(ds, TableName);
                this.sqlTrans.Commit();
                DA.Dispose();
                
            }

            catch (Exception ex)
            {

                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);

                // Attempt to roll back the transaction.
                try
                {
                    sqlTrans.Rollback();
                }
                catch (Exception ex2)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                }
            }
            return ds;

        }

        public DataSet GetDataSet(string strSQL)
        {
            MySqlDataAdapter DA;
            DataSet ds = new DataSet();
            try
            {
                DA = new MySqlDataAdapter(strSQL, this.conn);
                // this.sqlTrans = this.conn.BeginTransaction("GetDataSetTransaction");
                //this.SqlCmd.Transaction = sqlTrans;
                DA.Fill(ds);
                //this.sqlTrans.Commit();
                DA.Dispose();

            }

            catch (Exception)
            {

                // Attempt to roll back the transaction.
                try
                {
                    sqlTrans.Rollback();
                    throw;
                }
                catch (Exception)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    throw;
                }
            }
            return ds;
        }

        public DataTable GetDataTable(string strSQL)
        {
            MySqlDataAdapter DA;
            DataTable dt = new DataTable();
            try
            {
                DA = new MySqlDataAdapter(strSQL, this.conn);
                //this.sqlTrans = this.conn.BeginTransaction("GetDataTableTransaction");
                //this.SqlCmd.Transaction = sqlTrans;
                DA.Fill(dt);
                // this.sqlTrans.Commit();
                DA.Dispose();

            }

            catch (Exception)
            {

                // Attempt to roll back the transaction.
                try
                {
                    //sqlTrans.Rollback();
                    throw;
                }
                catch (Exception)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    throw;
                }
            }
            return dt;
        }
        /// <summary>
        /// executes the query, returns the first column of the first row
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public object Get_Scalar(string query)
        {
            object retValue = null;
            try
            {
                MySqlCommand SqlCmd = new MySqlCommand(query, this.conn, this.sqlTrans);
                retValue = SqlCmd.ExecuteScalar();
        
            }

            catch (Exception)
            {

                try
                {
                    throw;
                }
                catch (Exception)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    throw;
                }
            }

            return retValue;
        }

        public void ExecuteNonQuery(string query)
        {
            try
            {
                this.sqlTrans = this.conn.BeginTransaction();
                // this.sqlTrans = this.conn.BeginTransaction();
                MySqlCommand SqlCmd = new MySqlCommand(query, this.conn, this.sqlTrans);
                SqlCmd.ExecuteNonQuery();
                sqlTrans.Commit();
            }

            catch (Exception)
            {

                // Attempt to roll back the transaction.
                try
                {
                    sqlTrans.Rollback();
                    throw;
                }
                catch (Exception)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    throw;
                }
            }

        }
        /// <summary>
        /// returns a new SqlCmd object
        /// </summary>
        /// <returns></returns>
        public MySqlCmd CreateCommand(string cmdText)
        {
            return new MySqlCmd(this.conn, cmdText);
        }

        public MySqlCmd CreateCommand(CommandType type, string cmdText)
        {
            return new MySqlCmd(this.conn, cmdText,type);
        }
        /// <summary>
        /// returns a new SqlCmd object
        /// </summary>
        /// <returns></returns>
        public MySqlCmd CreateCommand()
        {
            return new MySqlCmd(this.conn);
        }

        public MySqlConnection GetConnection()
        {
            return this.conn;
        }

        /// <summary>
        /// Closes connection
        /// </summary>
        public void Close()
        {
            this.conn.Close();
            this.conn.Dispose();
        }

    }
    public class MySqlCmd : IDbCommand
    {
        private MySqlCommand cmd;
        public string CommandText
        {
            set { this.cmd.CommandText = value; }
        }
        public CommandType CommandType
        {
            set { this.cmd.CommandType= value; }
        }
        private MySqlTransaction sqlTrans;

        //private SqlConnection conn;
        public MySqlCmd(MySqlConnection conn)
        {
            this.cmd = new MySqlCommand();
            this.cmd.Connection = conn; 

        }
        public MySqlCmd(string conn)
        {
            this.cmd = new MySqlCommand();
            this.cmd.Connection = new MySqlConnection(conn);
            this.cmd.Connection.Open();

        }
        public MySqlCmd(MySqlConnection conn, string cmdText, CommandType type)
        {
            this.cmd = new MySqlCommand();
            this.cmd.Connection = conn;
            this.cmd.CommandText = cmdText;
            this.cmd.CommandType = type;

        }
        public MySqlCmd(MySqlConnection conn, string cmdText)
        {
            this.cmd = new MySqlCommand();
            this.cmd.Connection = conn;
            this.cmd.CommandText = cmdText;

        }

        public DataTable GetDataTable(string SQL)
        {
            MySqlDataAdapter DA;
            DataTable dt = new DataTable();
            try
            {
                this.cmd.CommandText = SQL;
                DA = new MySqlDataAdapter(this.cmd);
                //this.sqlTrans = this.conn.BeginTransaction("GetDataTableTransaction");
                //this.SqlCmd.Transaction = sqlTrans;
                DA.Fill(dt);
                // this.sqlTrans.Commit();
                DA.Dispose();

            }

            catch (Exception)
            {

                // Attempt to roll back the transaction.
                try
                {
                    //sqlTrans.Rollback();
                    throw;
                }
                catch (Exception)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    throw;
                }
            }
            return dt;
        }
        public object GetScalarFromProcedure(string proc_name)
        {
            object retValue = 0;
            try
            {
                this.sqlTrans = this.cmd.Connection.BeginTransaction();
                this.CommandText = proc_name;
                this.CommandType = CommandType.StoredProcedure;
                retValue = this.cmd.ExecuteScalar();
                sqlTrans.Commit();
            }

            catch (Exception)
            {

                // Attempt to roll back the transaction.
                try
                {
                    sqlTrans.Rollback();
                    throw;
                }
                catch (Exception)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    throw;
                }
            }
            return retValue;
        }
        public int ExecuteNonQuery()
        {
            int retValue = 0;
            try
            {
                this.sqlTrans = this.cmd.Connection.BeginTransaction();
                // this.sqlTrans = this.conn.BeginTransaction();
                //SqlCommand SqlCmd = new SqlCommand(query, this.cmd.Connection, this.sqlTrans);
                retValue= this.cmd.ExecuteNonQuery();
                sqlTrans.Commit();
            }

            catch (Exception)
            {

                // Attempt to roll back the transaction.
                try
                {
                    sqlTrans.Rollback();
                    throw;
                }
                catch (Exception)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    throw;
                }
            }
            return retValue;
        }
        public void ExecuteNonQuery(string query)
        {
            try
            {
                this.sqlTrans = this.cmd.Connection.BeginTransaction();
                // this.sqlTrans = this.conn.BeginTransaction();
                //SqlCommand SqlCmd = new SqlCommand(query, this.cmd.Connection, this.sqlTrans);
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                sqlTrans.Commit();
            }

            catch (Exception)
            {

                // Attempt to roll back the transaction.
                try
                {
                    sqlTrans.Rollback();
                    throw;
                }
                catch (Exception)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    throw;
                }
            }

        }

        /// <summary>
        /// Executes the given stored procedure
        /// </summary>
        /// <param name="proc_name"></param>
        /// <returns></returns>
        public int ExecuteNonQuerySP(string proc_name)
        {
            int retValue = 0;
            try
            {
                this.sqlTrans = this.cmd.Connection.BeginTransaction();
                this.cmd.CommandText = proc_name;
                this.cmd.CommandType = CommandType.StoredProcedure;
                retValue = this.cmd.ExecuteNonQuery();
                sqlTrans.Commit();
            }

            catch (Exception )
            {

                // Attempt to roll back the transaction.
                try
                {
                    sqlTrans.Rollback();
                    throw;
                }
                catch (Exception )
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    throw;
                }
            }
            return retValue;
        }

        public object ExecuteScalar()
        {
            object retValue = 0;
            try
            {
                this.sqlTrans = this.cmd.Connection.BeginTransaction();
                // this.sqlTrans = this.conn.BeginTransaction();
                //SqlCommand SqlCmd = new SqlCommand(query, this.cmd.Connection, this.sqlTrans);
                retValue=this.cmd.ExecuteScalar();
                sqlTrans.Commit();
            }

            catch (Exception)
            {

                // Attempt to roll back the transaction.
                try
                {
                    sqlTrans.Rollback();
                    throw;
                }
                catch (Exception)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    throw;
                }
            }
            return retValue;
        }
        public object ExecuteScalar(string query)
        {
            object retValue = 0;
            try
            {
                this.sqlTrans = this.cmd.Connection.BeginTransaction();
                cmd.CommandText = query;
                // this.sqlTrans = this.conn.BeginTransaction();
                //SqlCommand SqlCmd = new SqlCommand(query, this.cmd.Connection, this.sqlTrans);
                retValue = this.cmd.ExecuteScalar();
                sqlTrans.Commit();
            }

            catch (Exception)
            {

                // Attempt to roll back the transaction.
                try
                {
                    sqlTrans.Rollback();
                    throw;
                }
                catch (Exception)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    throw;
                }
            }
            return retValue;
        }
     

        /// <summary>
        /// SourceColumn için Name adıında bir parametre oluşturur.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="type"></param>
        /// <param name="SourceColumn"></param>
        public void CreateParameter(string Name, MySqlDbType type, string SourceColumn)
        {
            MySqlParameter param = cmd.CreateParameter();
            param.MySqlDbType = type;
            param.ParameterName = Name;
            param.SourceColumn = SourceColumn;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);
        }

        /// <summary>
        /// Name adlı kolon için Name adında bir parametre oluşturur. 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="type"></param>
        public void CreateParameter(string Name, MySqlDbType type)
        {
            MySqlParameter param = cmd.CreateParameter();
            param.MySqlDbType = type;
            param.ParameterName = "@" + Name;
            param.SourceColumn = Name;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);
        }

        public void CreateOuterParameter(string Name, MySqlDbType type)
        {
            MySqlParameter param = cmd.CreateParameter();
            param.MySqlDbType = type;
            param.ParameterName = "@" + Name;
            param.SourceColumn = Name;
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);
        }

        /// <summary>
        /// command nesnesi içindeki parametrelerden, verilen indexin karşılığındaki parametre değerini döndürür
        /// </summary>
        /// <param name="paramIndex"></param>
        /// <returns></returns>
        public object GetParameterValue(int paramIndex)
        {
            return this.cmd.Parameters[paramIndex].Value;
        }

        /// <summary>
        ///  command nesnesi içindeki parametrelerden, ismi verilen parametrenin değerini döndürür
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public object GetParameterValue(string  paramName)
        {
            return this.cmd.Parameters["@"+paramName].Value;
        }

        /// <summary>
        ///  Name adlı kolon için Name adında ve Value değeriyle set edilmiş bir parametre oluşturur. 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="type"></param>
        /// <param name="Value"></param>
        public void CreateSetParameter(string Name, MySqlDbType type, object Value)
        {
            MySqlParameter param = cmd.CreateParameter();
            param.MySqlDbType = type;
            param.ParameterName = "@" + Name;
            param.SourceColumn = Name;
            param.Direction = ParameterDirection.Input;
            param.Value = Value;
            cmd.Parameters.Add(param);
        }

        /// <summary>
        /// set parameter name with @ char
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        public void SetParameter(string Name, object Value)
        {
            cmd.Parameters[Name].Value = Value;
        }

        /// <summary>
        ///  set parameter name without @ char
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        public void SetParameterAt(string Name, object Value)
        {
            cmd.Parameters["@" + Name].Value = Value;
        }

        public  DataTable GetDataTableFromProcedure(string procName)
        {
            MySqlDataAdapter DA;
            DataTable dt = new DataTable();
            this.cmd.CommandType = CommandType.StoredProcedure;
            this.cmd.CommandText = procName;
            try
            {
                DA = new MySqlDataAdapter(this.cmd);
                //this.sqlTrans = this.conn.BeginTransaction("GetDataTableTransaction");
                //this.SqlCmd.Transaction = sqlTrans;
                DA.Fill(dt);
                // this.sqlTrans.Commit();
                DA.Dispose();

            }

            catch (Exception)
            {

                // Attempt to roll back the transaction.
                try
                {
                    //sqlTrans.Rollback();
                    throw;
                }
                catch (Exception)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    throw;
                }
            }
            return  dt;
        }
        public DataSet GetDataSetFromProcedure(string procName)
        {
            MySqlDataAdapter DA;
            DataSet ds= new DataSet();
            this.cmd.CommandType = CommandType.StoredProcedure;
            this.cmd.CommandText = procName;
            try
            {
                DA = new MySqlDataAdapter(this.cmd);
                //this.sqlTrans = this.conn.BeginTransaction("GetDataTableTransaction");
                //this.SqlCmd.Transaction = sqlTrans;
                DA.Fill(ds);
                // this.sqlTrans.Commit();
                DA.Dispose();

            }

            catch (Exception)
            {

                // Attempt to roll back the transaction.
                try
                {
                    //sqlTrans.Rollback();
                    throw;
                }
                catch (Exception)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    throw;
                }
            }
            return ds;
        }

        /// <summary>
        /// Closes the connection of related command
        /// </summary>
        public void Close()
        {
            this.cmd.Connection.Close();
            this.cmd.Connection.Dispose();
            this.cmd.Dispose();
        }

        #region IDbCommand Members

        void IDbCommand.Cancel()
        {
            throw new NotImplementedException();
        }

        string IDbCommand.CommandText
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        int IDbCommand.CommandTimeout
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

         CommandType IDbCommand.CommandType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        IDbConnection IDbCommand.Connection
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        IDbDataParameter IDbCommand.CreateParameter()
        {
            throw new NotImplementedException();
        }

        int IDbCommand.ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        IDataReader IDbCommand.ExecuteReader(CommandBehavior behavior)
        {
            return cmd.ExecuteReader(behavior);
        }

        IDataReader IDbCommand.ExecuteReader()
        {
            //throw new NotImplementedException();
            return cmd.ExecuteReader();
        }

        object IDbCommand.ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        IDataParameterCollection IDbCommand.Parameters
        {
            get { throw new NotImplementedException(); }
        }

        void IDbCommand.Prepare()
        {
            throw new NotImplementedException();
        }

        IDbTransaction IDbCommand.Transaction
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        UpdateRowSource IDbCommand.UpdatedRowSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IDisposable Members
        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
