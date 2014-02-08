using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using System.IO;
using StockProgram.DBObjects;

namespace StockProgram.Settings
{
    public partial class ucGeneralSettings : DevExpress.XtraEditors.XtraUserControl
    {
        private Image mainImage;
        private string mainImagePath;
        private ExceptionLogger excLogger;
        private bool IsImageSelected;
  
        public ucGeneralSettings()
        {
            InitializeComponent();
            IsImageSelected = false;
        }


        private void btn_back_Click(object sender, EventArgs e)
        {
            ucSettings ctrl = new ucSettings();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }

        private void btn_renkEkle_Click(object sender, EventArgs e)
        {
            if (txt_table_per_line.Text=="")
            {
                txt_table_per_line.Text = "1";
            }
            if (txt_width.Text=="")
            {
                txt_width.Text = "10";
            }
            if (txt_heigth.Text == "")
            {
                txt_heigth.Text = "10";
            }
            if (txt_refresh.Text == "")
            {
                txt_refresh.Text = "0";
            }
            EditSettings();
            SubmitForm();

        }

        private void btn_resimEkle_Click(object sender, EventArgs e)
        {
            IsImageSelected = false;
            try
            {
                openFileDialog1.Filter = " (*.jpg)|*.jpg|(*.png)|*.png";
                openFileDialog1.Title = "Resim Dosyası Seçiniz";
                openFileDialog1.FileName = "Dosya";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    System.IO.FileInfo file = new FileInfo(openFileDialog1.FileName.ToString());
                    mainImage= Image.FromFile(file.FullName);
                    pictureEdit1.Image = StaticObjects.ResizeImage(mainImage, pictureEdit1.Width, pictureEdit1.Height);
                    pictureEdit1.Visible = true;
                    IsImageSelected = true;
                    mainImagePath = file.Name;
                }
                else return;

            }
            catch (Exception ex)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(ex.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(ex, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at
            }     
        }

        private void EditSettings()        
        {
          //  string fileExtension = GetFileExtension(mainImagePath);
            MySqlCmd cmd = new MySqlCmd(StaticObjects.MySqlConn);
            string strSQL = "update settings set table_count_per_line=@count, table_width=@width,table_height=@height, tables_refresh_time=@refresh_time, menu_item_width=@menu_width, menu_item_height=@menu_height, menu_item_name_panel_height=@menu_name_panel_height, menu_item_count_per_line=@menu_count, main_form_img_path=@path where 1";
            try
            {
                cmd.CreateSetParameter("count", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt32(txt_table_per_line.Text));
                cmd.CreateSetParameter("width", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt32(txt_width.Text));
                cmd.CreateSetParameter("height", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt32(txt_heigth.Text));
                cmd.CreateSetParameter("refresh_time", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt32(txt_refresh.Text));
                cmd.CreateSetParameter("menu_count", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt32(txt_menu_item_count.Text));
                cmd.CreateSetParameter("menu_width", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt32(txt_menu_item_width.Text));
                cmd.CreateSetParameter("menu_height", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt32(txt_menu_item_height.Text));
                cmd.CreateSetParameter("menu_name_panel_height", MySql.Data.MySqlClient.MySqlDbType.Int32, Convert.ToInt32(txt_name_panel_height.Text));
                cmd.CreateSetParameter("path", MySql.Data.MySqlClient.MySqlDbType.Text, this.mainImagePath);
                cmd.ExecuteNonQuery(strSQL);

                if (IsImageSelected)
                {
                    SaveImage();
                }
                StaticObjects.Settings.table_width = Convert.ToInt16(txt_width.Text);
                StaticObjects.Settings.table_height = Convert.ToInt16(txt_heigth.Text);
                StaticObjects.Settings.table_count_per_line = Convert.ToInt16(txt_table_per_line.Text);
                StaticObjects.Settings.tables_refresh_time = Convert.ToInt16(txt_refresh.Text);
                StaticObjects.Settings.menu_item_width = Convert.ToInt16(txt_menu_item_width.Text);
                StaticObjects.Settings.menu_item_height = Convert.ToInt16(txt_menu_item_height.Text);
                StaticObjects.Settings.menu_item_count_per_line = Convert.ToInt16(txt_menu_item_count.Text);
                StaticObjects.Settings.menu_item_name_panel_height = Convert.ToInt16(txt_name_panel_height.Text);
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new DBObjects.ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at
            }
            finally
            {
                cmd.Close();
                cmd = null;
            }
        }

        private string GetFileExtension(string fullName)
        {
            string[] array = fullName.Split('.');
            return array[array.Length - 1];
        }

        private void SaveImage()
        {
            string root = Application.StartupPath + StaticObjects.MainImagePath + mainImagePath;
            try
            {
                if (File.Exists(root))
                {
                    File.Delete(root); //eski dosyayı sil
                }
                mainImage.Save(root);
                StaticObjects.Settings.mainImageName = mainImagePath;
            }
            catch (Exception ex)
            {

                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new ExceptionLogger(ex.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(ex, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at
            }
        }

        private void ucGeneralSettings_Load(object sender, EventArgs e)
        {
            FillSettings();
        }

        private void FillSettings()
        {
          try 
	        {
                this.mainImagePath = StaticObjects.Settings.mainImageName;
                pictureEdit1.Image= (File.Exists(Application.StartupPath+StaticObjects.MainImagePath+StaticObjects.Settings.mainImageName))? StaticObjects.ResizeImage(Image.FromFile(Application.StartupPath+StaticObjects.MainImagePath+ StaticObjects.Settings.mainImageName),pictureEdit1.Width,pictureEdit1.Height) : null;
                txt_width.Text= StaticObjects.Settings.table_width.ToString();
                txt_heigth.Text=   StaticObjects.Settings.table_height.ToString();
                txt_table_per_line.Text = StaticObjects.Settings.table_count_per_line.ToString();
                txt_refresh.Text = StaticObjects.Settings.tables_refresh_time.ToString();
                txt_menu_item_width.Text = StaticObjects.Settings.menu_item_width.ToString();
                txt_menu_item_height.Text = StaticObjects.Settings.menu_item_height.ToString();
                txt_menu_item_count.Text = StaticObjects.Settings.menu_item_count_per_line.ToString();
                txt_name_panel_height.Text = StaticObjects.Settings.menu_item_name_panel_height.ToString();

            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                //excLogger = new DBObjects.ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at
            }
         
        }
        private void SubmitForm()
        {
            ucSettings ctrl = new ucSettings();
            ctrl.Dock = DockStyle.Fill;
            ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
            this.Dispose();
        }
    }
}
