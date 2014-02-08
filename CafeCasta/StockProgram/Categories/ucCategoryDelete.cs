using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace StockProgram.Categories
{
    public partial class ucCategoryDelete : DevExpress.XtraEditors.XtraUserControl
    {
        public ucCategoryDelete()
        {
            InitializeComponent();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            ErrorMessages.Message message = new ErrorMessages.Message();
            if (message.WriteMessage("Kategori silme işleminde o kategoriye ait tüm ürünler 'Kategorilendirilmemiş' olarak görünecektir.", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
            { 
            //erase category

            }
        }
    }
}
