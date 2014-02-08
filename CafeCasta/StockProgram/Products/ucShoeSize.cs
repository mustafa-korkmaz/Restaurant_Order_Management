using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StockProgram.DBObjects;

namespace StockProgram.Products
{
    public partial class ucShoeSize : DevExpress.XtraEditors.XtraUserControl
    {
        private ProductAttributes ProductAttributes;
        public delegate void ProductAmountHandler(object sender,EventArgs e);
        public event ProductAmountHandler ProductAmountChanged;
        public ucShoeSize()
        {
            InitializeComponent();
            ProductAttributes = new ProductAttributes();
          //  SetLabels();// label controllerindeki fiyatları set edelim.
           
        }
        
        private void btn_ekle_Click(object sender, EventArgs e)
        {
            spin_adet.Text = (Convert.ToInt32(spin_adet.Text) + 1).ToString() ;
         
        }

        /// <summary>
        /// makes the controls uneditable in the component
        /// </summary>
        public void SetControlsDisabled()
        {
            this.btn_numara.Enabled = false;
            this.spin_adet.Properties.ReadOnly = true;
        }

        private void btn_cikar_Click(object sender, EventArgs e)
        {

        }

        private void ucAdisyon_Load(object sender, EventArgs e)
        {
      //     spin_adet.Properties.Increment = 1;
        }

        public StockProgram.DBObjects.ProductAttributes  GetProductAttributeItem()
        {
            return this.ProductAttributes;
        }

        private void spin_birim_fiyat_EditValueChanged(object sender, EventArgs e)
        {
            //if (Convert.ToDouble(spin_adet.Text) < 0)
            //{
            //    spin_adet.Value += spin_adet.Properties.Increment;
            //    return;
            //}
            //this.ProductAttributes.Amount = Convert.ToInt32(spin_adet.Value);
            //this.ProductAttributes.Size = Convert.ToInt32(btn_numara.Text);
            //OnProductAmountChanged(EventArgs.Empty);
        }

        /// <summary>
        /// fires when amount  successfully changed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnProductAmountChanged(EventArgs e)
        {
            if (ProductAmountChanged != null)
                ProductAmountChanged(this.ProductAttributes, e);
        }

        private void spin_adet_EditValueChanged(object sender, EventArgs e)
        {
            this.ProductAttributes.Amount = Convert.ToInt32(spin_adet.Text);
            this.ProductAttributes.Size = Convert.ToInt32(btn_numara.Text);
            OnProductAmountChanged(EventArgs.Empty);
        }

        private void spin_adet_EditValueChanged_1(object sender, EventArgs e)
        {
            if (Convert.ToDouble(spin_adet.Text) < 0)
            {
                spin_adet.Value += spin_adet.Properties.Increment;
                return;
            }
            this.ProductAttributes.Amount = Convert.ToInt32(spin_adet.Value);
            this.ProductAttributes.Size = Convert.ToInt32(btn_numara.Text);
            OnProductAmountChanged(EventArgs.Empty);
        }

    }
}
