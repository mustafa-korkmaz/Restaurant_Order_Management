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
    public partial class ucEditOption : DevExpress.XtraEditors.XtraUserControl
    {
        private int color_id;
        private string color_name;
        private ExceptionLogger excLogger;
        public delegate void ColorGridHandler(object sender, EventArgs e);
        public event ColorGridHandler ColorGridChanged;

        public ucEditOption(int id,string name)
        {
            InitializeComponent();
            this.color_id = id;
            this.color_name = name;
            this.txt_isim.Text = color_name;
        }

        private void EditColorToDB(string color_name)
        {
            MySqlCmd db = new MySqlCmd(StaticObjects.MySqlConn);
            string strSQL = "update  color_details set color_name='" + color_name + "' where color_id=" + this.color_id;
            try
            {
                // db.CreateSetParameter("modified_date", MySql.Data.MySqlClient.MySqlDbType.DateTime, DateTime.Now);
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
                string color = string.Empty;
                color = txt_isim.Text.ToUpper();

                //add bank object
                EditColorToDB(color);
            }
        }
    }
}
