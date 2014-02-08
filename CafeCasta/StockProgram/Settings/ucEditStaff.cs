using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using StockProgram.DBObjects;

namespace StockProgram.Settings
{
    public partial class ucEditStaff : DevExpress.XtraEditors.XtraUserControl
    {
        private Staff staff;
        private ExceptionLogger excLogger;
        public delegate void ColorGridHandler(object sender, EventArgs e);
        public event ColorGridHandler ColorGridChanged;

        public ucEditStaff(int id,string name)
        {
            InitializeComponent();
            this.staff = new Staff(id,name);
            FillStaffProperties();
       }

        private void FillStaffProperties()
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string sql = "select *, if(type=0,'ADMIN',if(type=1,'KASA',if(type=2,'GARSON','')))as type_text from staff where id=" + this.staff.id;
            try
            {
                DataTable dt= db.GetDataTable(sql);
                this.txt_isim.Text = dt.Rows[0]["name"].ToString();
                this.txt_password.Text = dt.Rows[0]["password"].ToString();
                this.txt_username.Text = dt.Rows[0]["username"].ToString();
                this.cb_role.Text = dt.Rows[0]["type_text"].ToString();
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at
            }
            finally
            {
                db.Close();
                db = null;
            }
     
        }

        private void EditStaffToDB()
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string strSQL = "update  staff set name=@name, username=@username, password=@password, type=@type, modified_date=@modified_date where id=" + this.staff.id;
            try
            {
                db.CreateSetParameter("name", MySql.Data.MySqlClient.MySqlDbType.VarChar, staff.name);
                db.CreateSetParameter("password", MySql.Data.MySqlClient.MySqlDbType.VarChar, staff.password);
                db.CreateSetParameter("username", MySql.Data.MySqlClient.MySqlDbType.VarChar, staff.username);
                db.CreateSetParameter("modified_date", MySql.Data.MySqlClient.MySqlDbType.DateTime, DateTime.Now);
                db.CreateSetParameter("type", MySql.Data.MySqlClient.MySqlDbType.Int16, Convert.ToInt16(staff.role));
                db.ExecuteNonQuery(strSQL);
                OnColorGridChanged(EventArgs.Empty);// fire the gridChanged event
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at
            }
            finally
            {
                db.Close();
                db = null;
            }
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
           // Success();
        }

        protected virtual void OnColorGridChanged(EventArgs e)
        {
            if (ColorGridChanged != null)
                ColorGridChanged(this, e);
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
            // Success();
        }

        private void btn_renkEkle_Click(object sender, EventArgs e)
        {
            if (txt_isim.Text != "")
            {// create  bank object
                staff.name = txt_isim.Text.ToUpper().Trim();
                staff.username = txt_username.Text.Trim();
                staff.password = txt_password.Text.Trim();
                staff.role = (Enums.UserRole)cb_role.SelectedIndex;
                //add bank object
                EditStaffToDB();
            }
        }
    }
}
