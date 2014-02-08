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
    public partial class frmDesc : DevExpress.XtraEditors.XtraForm
    {
        public delegate void SiparisHandler(object sender, EventArgs e);
        public event SiparisHandler SiparisMiktarChanged;
        private ExceptionLogger excLogger;
        private SiparisKalem siparisKalem;

        public frmDesc(ref SiparisKalem siparisKalem)
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
         
         //   this.spin_pesin.Value = Convert.ToDecimal(this.tutar);
            //FillBanks();
            //SetBanksCombo();
        }
        private void frmSatis_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.cb_desc;
            this.txt_desc.Text = this.siparisKalem.Desc;
            this.FillOptions();
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


        private void FillOptions()
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            string strSQL = "select* from v_options_to_product where product_id=" + this.siparisKalem.ProductId;
            DataTable dt = new DataTable();
            try
            {
                dt = db.GetDataTable(strSQL);
                if (dt.Rows.Count>0)
                {
                    ControlHelper controlHelper = new ControlHelper();
                    controlHelper.FillControl(cb_desc, Enums.RepositoryItemType.ComboBox, ref dt, "option_name");
                }
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, LoadCategoryTabıtems() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
                // retValue = 0;
            }
            finally
            {
                db.Close();
                db = null;
                dt.Dispose();
            }
        }

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

        private void SubmitForm()
        {
            this.siparisKalem.Desc = this.txt_desc.Text.Trim().ToUpper();
         //   this.siparisKalem.Amount =Convert.ToDouble (this.spin_miktar.Value);
            //OnMiktarChanged(EventArgs.Empty);
            this.Dispose();
        }

        private void btn_tamamla_Click(object sender, EventArgs e)
        {
            SubmitForm();         
        }

        private void cb_desc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txt_desc.Text+= cb_desc.Text+" ";
                cb_desc.Text = "";
            }
        }

        private void txt_desc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SubmitForm();
            }
        }

    }
}