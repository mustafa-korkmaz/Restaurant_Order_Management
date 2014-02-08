namespace StockProgram.Sales
{
    partial class ucSaleOptions
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_product_name = new DevExpress.XtraEditors.LabelControl();
            this.btn_chekout = new DevExpress.XtraEditors.SimpleButton();
            this.btn_paket = new DevExpress.XtraEditors.SimpleButton();
            this.btn_orders = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this.lbl_product_name);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(633, 39);
            this.panel1.TabIndex = 53;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.simpleButton1.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.simpleButton1.Location = new System.Drawing.Point(567, 2);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(64, 35);
            this.simpleButton1.TabIndex = 43;
            this.simpleButton1.Text = "Geri";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // lbl_product_name
            // 
            this.lbl_product_name.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_product_name.Location = new System.Drawing.Point(5, 8);
            this.lbl_product_name.Name = "lbl_product_name";
            this.lbl_product_name.Size = new System.Drawing.Size(179, 22);
            this.lbl_product_name.TabIndex = 2;
            this.lbl_product_name.Text = "Hesap / Masa İşlemleri";
            // 
            // btn_chekout
            // 
            this.btn_chekout.Image = global::StockProgram.Properties.Resources.cash_register;
            this.btn_chekout.Location = new System.Drawing.Point(200, 45);
            this.btn_chekout.Name = "btn_chekout";
            this.btn_chekout.Size = new System.Drawing.Size(189, 111);
            this.btn_chekout.TabIndex = 77;
            this.btn_chekout.Text = "Hesap Aç / Kes (F2)";
            this.btn_chekout.Click += new System.EventHandler(this.btn_chekout_Click);
            // 
            // btn_paket
            // 
            this.btn_paket.Image = global::StockProgram.Properties.Resources.courier;
            this.btn_paket.Location = new System.Drawing.Point(5, 45);
            this.btn_paket.Name = "btn_paket";
            this.btn_paket.Size = new System.Drawing.Size(189, 111);
            this.btn_paket.TabIndex = 76;
            this.btn_paket.Text = "Yeni Paket Servis (F1)";
            this.btn_paket.Click += new System.EventHandler(this.btn_paket_Click);
            // 
            // btn_orders
            // 
            this.btn_orders.Image = global::StockProgram.Properties.Resources.to_do_list_cheked_all;
            this.btn_orders.Location = new System.Drawing.Point(395, 45);
            this.btn_orders.Name = "btn_orders";
            this.btn_orders.Size = new System.Drawing.Size(189, 111);
            this.btn_orders.TabIndex = 75;
            this.btn_orders.Text = "Açık / Bekleyen Siparişler (F3)";
            this.btn_orders.Click += new System.EventHandler(this.btn_restoranici_Click);
            // 
            // ucSaleOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_chekout);
            this.Controls.Add(this.btn_paket);
            this.Controls.Add(this.btn_orders);
            this.Controls.Add(this.panel1);
            this.Name = "ucSaleOptions";
            this.Size = new System.Drawing.Size(633, 364);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.LabelControl lbl_product_name;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btn_orders;
        private DevExpress.XtraEditors.SimpleButton btn_paket;
        private DevExpress.XtraEditors.SimpleButton btn_chekout;
    }
}
