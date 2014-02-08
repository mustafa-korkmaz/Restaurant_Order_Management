using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StockProgram.DBObjects;
using System.Diagnostics;

namespace StockProgram.Sales
{
    public partial class frmMiktar : DevExpress.XtraEditors.XtraForm
    {
        public delegate void SiparisHandler(object sender, EventArgs e);
        public event SiparisHandler SiparisMiktarChanged;
        private ExceptionLogger excLogger;
        private SiparisKalem siparisKalem;

        public frmMiktar( ref SiparisKalem siparisKalem)
        {
          //  this.supplierId = supplier_id;
            this.siparisKalem = siparisKalem;
            InitializeComponent();
            frmSettings();
        }

        /// <summary>
        /// set the satis form properties
        /// </summary>
        private void frmSettings()
        {
            this.lbl_urun_adi.Text = this.siparisKalem.ProductName; 
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.spin_miktar.Value = Convert.ToDecimal(this.siparisKalem.Amount);
         
         //   this.spin_pesin.Value = Convert.ToDecimal(this.tutar);
            //FillBanks();
            //SetBanksCombo();
        }
        private void frmSatis_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.spin_miktar;
        }


        #region Set Form Controls

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }

        #endregion


        /// <summary>
        /// fires when tutar successfully changed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMiktarChanged(EventArgs e)
        {
            if (SiparisMiktarChanged != null)
                SiparisMiktarChanged(this.siparisKalem, e);
        }
        private void btn_duzenle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMiktar_Shown(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void btn_duzenle_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void spin_miktar_EditValueChanged_1(object sender, EventArgs e)
        {
            if (Convert.ToDouble(spin_miktar.Text) <= 0)
            {
                spin_miktar.Value += spin_miktar.Properties.Increment;
                return;
            }
            //if (this.spin_miktar.Value> this.siparisKalem.TotalAmount)
            //{
            //    ErrorMessages.Message message = new ErrorMessages.Message();
            //    message.WriteMessage("Stok miktarını aştınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
            //    spin_miktar.Value = this.siparisKalem.TotalAmount;
            //}
        }

        private void spin_miktar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (Convert.ToDouble(spin_miktar.Text) < 0)
                {
                    spin_miktar.Value += spin_miktar.Properties.Increment;
                    return;
                }
                if (Convert.ToDouble(this.spin_miktar.Value) > this.siparisKalem.TotalAmount)
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Stok miktarını aştınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    spin_miktar.Value = Convert.ToDecimal(this.siparisKalem.TotalAmount);
                    return;
                }
                SubmitForm();
            }
        }

        private void SubmitForm()
        {
            this.siparisKalem.Amount =Convert.ToDouble (this.spin_miktar.Value);
            OnMiktarChanged(EventArgs.Empty);
            this.Dispose();
        }

        private void btn_tamamla_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToDouble(this.spin_miktar.Value )> this.siparisKalem.TotalAmount)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Stok miktarını aştınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                spin_miktar.Value =Convert.ToDecimal( this.siparisKalem.TotalAmount);
                return;
            }
            SubmitForm();
        }
    }
}