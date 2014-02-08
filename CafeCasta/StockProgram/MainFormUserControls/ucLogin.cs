using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Win32;
using StockProgram.DBObjects;
using System.IO;

namespace StockProgram.MainFormUserControls
{
    public partial class ucLogin : DevExpress.XtraEditors.XtraUserControl
    {
        RegistryKey MyReg = Registry.CurrentUser.CreateSubKey("SOFTWARE\\STOCKS");
       
        public ucLogin()
        {
            try
            { 
                InitializeComponent();

                txt_UserName.Text = MyReg.GetValue("USER", "").ToString();
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void test_Click(object sender, EventArgs e)
        {

        }

        private void ucLogin_Load(object sender, EventArgs e)
        {
            txt_Password.Focus();
            btn_Login.TabIndex = 0;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            Login();
        }
        #region Login
        private string Login()
        {
            ReadIP();
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (txt_Password.Text.Length < 4)
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Şifre en  az 4 karakter içermelidir.", MessageBoxIcon.None, MessageBoxButtons.OK);
              
                    return "1";
                }

                else
                {
                    string strSQL = "select * from staff where username=@username and password=@password";

                    DataTable dt;
                    try
                    {
                        db.CreateSetParameter("username", MySql.Data.MySqlClient.MySqlDbType.VarChar, txt_UserName.Text.Trim());
                        db.CreateSetParameter("password", MySql.Data.MySqlClient.MySqlDbType.VarChar, txt_Password.Text.Trim());
                        dt= db.GetDataTable(strSQL);
                    }
                    catch (Exception e)
                    {
                        
                        throw;
                    }
             
                    if (dt.Rows.Count == 0)
                    {

                    //   modSabit.glb_Personel_ID = 0;

                        MessageBox.Show("Kullanıcı Bulunmadı Lütfen Tekrar Deneyiniz...!");
                    }
                    else
                    {

                        MyReg.SetValue("USER", txt_UserName.Text);
                        ParentForm.Controls["pnl_Left"].Visible = true;
                        StaticObjects.User.Id = Convert.ToInt32(dt.Rows[0]["id"]);
                        StaticObjects.User.IsDeleted = (Convert.ToInt16(dt.Rows[0]["is_deleted"]));
                        StaticObjects.User.Name = (dt.Rows[0]["name"].ToString());
                        StaticObjects.User.Password = (dt.Rows[0]["password"].ToString());
                        StaticObjects.User.UserName = (dt.Rows[0]["username"].ToString());
                        StaticObjects.User.Role = (Enums.UserRole)(Convert.ToInt16(dt.Rows[0]["type"]));
                        StaticObjects.User.IsLoggedIn = true;
                        ((MainForm)this.ParentForm).SettingStatus();
                  

                        this.Dispose();

                        return "0";

                    }
                }


                return "0";
            }
            catch
            {
                throw;
            }
            finally
            {
                db.Close();
                db = null;

                Cursor.Current = Cursors.Default;
            }

        }
        #endregion

        private void ReadIP()
        {
            try
            {
                using (StreamReader sr = new StreamReader(Application.StartupPath + "\\conn.txt"))
                {
                    String line = sr.ReadToEnd();
                    string[] array = line.Split(';');
                    string ip = array[0].Trim();
                    string user = array[1].Trim();
                    string pass = array[2].Trim();
                    string dbName = array[3].Trim();

                    StaticObjects.Settings.db_user = user;
                    StaticObjects.Settings.db_host = ip;
                    StaticObjects.Settings.db_host = ip;
                    StaticObjects.Settings.db_name = dbName;

                    StaticObjects.MySqlConn = "server=" + ip + ";User Id=" + user + ";password=" + pass + ";database="+dbName;

                }
            }
            catch
            {
                MessageBox.Show("Bağlantı dosyası eksik ya da hatalı...!");
            }
        }

        private void txt_Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Login();
            }
        }

    }
}
