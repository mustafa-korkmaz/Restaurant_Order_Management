using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace StockProgram.Sales
{
    public partial class ucAdisyon : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void SiparisHandler(object sender, EventArgs e);
        public event SiparisHandler SiparisTutarChanged;
        public event SiparisHandler SiparisCanceled;
        public event SiparisHandler DescButtonPressed;
        private SiparisKalem siparisKalem;
        public ucAdisyon(SiparisKalem sk)
        {
            InitializeComponent();
            this.siparisKalem = sk;
            sk.SiparisAmountChanged += new SiparisKalem.SiparisKalemHandler(sk_SiparisAmountChanged);
            SetLabels();// label controllerindeki fiyatları set edelim.
           
        }

        void sk_SiparisAmountChanged(object sender, EventArgs e)
        {
            if (this.siparisKalem.Amount <= this.siparisKalem.TotalAmount)
            {
                lbl_miktar.Text = this.siparisKalem.Amount.ToString();
                SetLabels();
            }
            else this.siparisKalem.Amount--;
          
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            if (this.siparisKalem.Amount < this.siparisKalem.TotalAmount)
            {
                double amount = ++this.siparisKalem.Amount;
                SetLabels();
            }
         
        }

        private void btn_cikar_Click(object sender, EventArgs e)
        {
            double amount = --this.siparisKalem.Amount;
            
            if (amount<=0)
            {
               ++this.siparisKalem.Amount;
                OnSiparisCanceled(EventArgs.Empty);
                return;
            }
            else
            SetLabels();
        }

        private void SetLabels()
        {
            double amount=this.siparisKalem.Amount;
            double price=this.siparisKalem.SalePrice;
            this.lbl_miktar.Text =   amount.ToString();
            this.spin_birim_fiyat.Text=price.ToString();
            this.lbl_tutar.Text = Convert.ToDouble(amount * price).ToString() +" TL";
            string product_name = (this.siparisKalem.Porsion == 1) ? StaticObjects.ConvertWordLength(this.siparisKalem.ProductName, "", StaticObjects.SlipProductNameCharLength) : StaticObjects.ConvertWordLength(this.siparisKalem.ProductName, "(" + this.siparisKalem.PorsionText + ")", StaticObjects.SlipProductNameCharLength);
            lbl_product.Text = product_name;
       
            OnTutarChanged(EventArgs.Empty); // fire tutarChanged Event
        }

        private void ucAdisyon_Load(object sender, EventArgs e)
        {

            spin_birim_fiyat.Properties.Increment = 5;
        }

        public SiparisKalem GetSiparisKalem()
        {
            return this.siparisKalem;
        }

        private void spin_birim_fiyat_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(spin_birim_fiyat.Text) < 0)
            {
                spin_birim_fiyat.Value += spin_birim_fiyat.Properties.Increment;
                return;
            }
            this.siparisKalem.SalePrice = Convert.ToDouble(this.spin_birim_fiyat.Text);
            SetLabels();
        }

        /// <summary>
        /// fires when tutar successfully changed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnTutarChanged(EventArgs e)
        {
            if (SiparisTutarChanged != null)
                SiparisTutarChanged(this.siparisKalem, e);
        }

         /// <summary>
        /// fires when desc button successfully pressed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDescButtonPressed(EventArgs e)
        {
            if (DescButtonPressed != null)
                DescButtonPressed(this.siparisKalem, e);
        }

        /// <summary>
        /// fires when siparis successfully canceled
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSiparisCanceled(EventArgs e)
        {
            if (SiparisCanceled != null)
                SiparisCanceled(this, e);
        }

        private void lbl_miktar_Click(object sender, EventArgs e)
        {
            using ( frmMiktar frm = new frmMiktar(ref this.siparisKalem))
            {
                frm.SiparisMiktarChanged += new frmMiktar.SiparisHandler(frm_SiparisMiktarChanged);
                frm.ShowDialog(this);
                frm.Focus();
            }
        }

        void frm_SiparisMiktarChanged(object sender, EventArgs e)
        {
            SetLabels();
        }

        private void btn_desc_Click(object sender, EventArgs e)
        {
            using (frmDesc frm = new frmDesc(ref this.siparisKalem))
            {
                frm.ShowDialog(this);
                frm.Focus();
            }
            if (this.siparisKalem.Desc.Length>0) //açıklama girilmiş butonu yeşil yap
            {
                   this.btn_desc.Image = global::StockProgram.Properties.Resources.edit_red;
            }
        }

        private void spin_birim_fiyat_Click(object sender, EventArgs e)
        {
            this.spin_birim_fiyat.SelectAll();
        }

    }
}
