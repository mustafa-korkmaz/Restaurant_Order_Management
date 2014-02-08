using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace StockProgram.Sales
{
    public partial class ucMenuItem : DevExpress.XtraEditors.XtraUserControl
    {
        string root = Application.StartupPath + StaticObjects.MainImagePath;
        public delegate void MenuItemHandler(object sender, EventArgs e);
        public event MenuItemHandler MenuItemClicked;
    //    public event MenuItemHandler SiparisCanceled;
        private SiparisKalem siparisKalem;
        private DBObjects.Product product;
        public double porsion
        {
            get 
            {
                double retValue=0;
                if (chk_1.CheckState==CheckState.Checked)
                {
                    retValue= 1;
                }
                if (chk_1_5.CheckState == CheckState.Checked)
                {
                    retValue= 1.5;
                }
                if (chk_2.CheckState == CheckState.Checked)
                {
                    retValue= 2;
                }
                return retValue;
            }
        }
        public ucMenuItem(DBObjects.Product product)
        {
            this.product = product;
            InitializeComponent();
            
            Settings(); 
            SetImage();
        }

        private void Settings()
        {
            this.pnl_name.Size = new System.Drawing.Size(StaticObjects.Settings.menu_item_width,  StaticObjects.Settings.menu_item_name_panel_height);
            this.Size = new System.Drawing.Size(StaticObjects.Settings.menu_item_width, StaticObjects.Settings.menu_item_height + pnl_name.Size.Height);
            this.lbl_product_name.Text = this.product.Name;
            this.chk_1.CheckState = CheckState.Checked;
            this.chk_1_5.CheckState = CheckState.Unchecked;
            this.chk_2.CheckState = CheckState.Unchecked;
            if (this.product.SalePrice_bucuk==0)
            {
                this.chk_1_5.Enabled = false;
            }
               
            if (this.product.SalePrice_double==0)
            {
                this.chk_2.Enabled = false;
            }
            if (this.product.SalePrice_double == 0 && this.product.SalePrice_bucuk==0)
            {
                this.pnl_porsion_group.Visible = false;
            }
        }
        /// <summary>
        /// fires when tutar successfully changed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMenuItemClicked(EventArgs e)
        {
            if (MenuItemClicked != null)
                MenuItemClicked(this, e);
        }

        /// <summary>
        /// fires when siparis successfully canceled
        /// </summary>
        /// <param name="e"></param>
        //protected virtual void OnSiparisCanceled(EventArgs e)
        //{
        //    if (SiparisCanceled != null)
        //        SiparisCanceled(this, e);
        //}


        public DBObjects.Product GetProduct()
        {
            return this.product;
        }
        private void SetImage()
        {
            if (File.Exists(root + product.ImagePath))
            {
                pic_image.Image = Image.FromFile(root + product.ImagePath);
                pic_image.Visible = true;
            }
        }

        private void chk_1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_1.CheckState==CheckState.Checked)
            {
                chk_1_5.CheckState = CheckState.Unchecked;
                chk_2.CheckState = CheckState.Unchecked;
                this.product.SelectedSalePrice = this.product.SalePrice;
            }
        }

        private void chk_1_5_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_1_5.CheckState == CheckState.Checked)
            {
                chk_1.CheckState = CheckState.Unchecked;
                chk_2.CheckState = CheckState.Unchecked;
                this.product.SelectedSalePrice = this.product.SalePrice_bucuk;
            }
        }

        private void chk_2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_2.CheckState == CheckState.Checked)
            {
                chk_1_5.CheckState = CheckState.Unchecked;
                chk_1.CheckState = CheckState.Unchecked;
                this.product.SelectedSalePrice = this.product.SalePrice_double;
            }
        }

        private void pic_image_Click(object sender, EventArgs e)
        {
            OnMenuItemClicked(EventArgs.Empty);
        }

        private void pic_image_DoubleClick(object sender, EventArgs e)
        {
            OnMenuItemClicked(EventArgs.Empty);
        }
    }
}
