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
    public partial class ucAddStaff : DevExpress.XtraEditors.XtraUserControl
    {
        private ExceptionLogger excLogger;
        public delegate void ColorGridHandler(object sender, EventArgs e);
        public event ColorGridHandler ColorGridChanged;
        public ucAddStaff()
        {
            InitializeComponent();
        }

        protected virtual void OnColorGridChanged(EventArgs e)
        {
            if (ColorGridChanged != null)
                ColorGridChanged(this, e);
        }

        private void btn_renkEkle_Click(object sender, EventArgs e)
        {
            if (txt_isim.Text != "")
            {// create  bank object
                Staff staff = new Staff();
                staff.name = txt_isim.Text.ToUpper().Trim();
                staff.password = txt_password.Text.Trim();
                staff.username = txt_username.Text.Trim();
                staff.role = (Enums.UserRole)cb_role.SelectedIndex;
                //add personel
                AddStaffToDB(ref staff);
            }
        }

        private void AddStaffToDB(ref Staff staff)
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string strSQL = "insert into staff (username,password,type,name,is_deleted,modified_date) values (@uname,@pass,@role,@name,0,@modified_date)";
            try
            {
                db.CreateSetParameter("name", MySql.Data.MySqlClient.MySqlDbType.VarChar, staff.name);
                db.CreateSetParameter("pass", MySql.Data.MySqlClient.MySqlDbType.VarChar, staff.password);
                db.CreateSetParameter("uname", MySql.Data.MySqlClient.MySqlDbType.VarChar, staff.username);
                db.CreateSetParameter("role", MySql.Data.MySqlClient.MySqlDbType.Int16, Convert.ToInt16(staff.role));
                db.CreateSetParameter("modified_date",MySql.Data.MySqlClient.MySqlDbType.DateTime,DateTime.Now);
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
            //Parent.Controls["pnl_master"].Visible = true;
            //this.Dispose();
            Success();
        }

        private void Success()
        {
            //ErrorMessages.Message m = new ErrorMessages.Message();
            //m.WriteMessage(txt_isim.Text+", başarılı bir şekilde renklere eklendi.", MessageBoxIcon.Information, MessageBoxButtons.OK);
            txt_isim.Text = "";
            txt_username.Text = "";
            txt_password.Text = "";
        }


    }
}
