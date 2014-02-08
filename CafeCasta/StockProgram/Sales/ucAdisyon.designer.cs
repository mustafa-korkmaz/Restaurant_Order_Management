namespace StockProgram.Sales
{
    partial class ucAdisyon
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
            this.btn_ekle = new DevExpress.XtraEditors.SimpleButton();
            this.btn_cikar = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_product = new DevExpress.XtraEditors.LabelControl();
            this.lbl_tutar = new DevExpress.XtraEditors.LabelControl();
            this.lbl_x = new DevExpress.XtraEditors.LabelControl();
            this.spin_birim_fiyat = new DevExpress.XtraEditors.SpinEdit();
            this.lbl_miktar = new DevExpress.XtraEditors.SimpleButton();
            this.btn_desc = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.spin_birim_fiyat.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ekle
            // 
            this.btn_ekle.Appearance.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.btn_ekle.Appearance.ForeColor = System.Drawing.Color.Green;
            this.btn_ekle.Appearance.Options.UseFont = true;
            this.btn_ekle.Appearance.Options.UseForeColor = true;
            this.btn_ekle.Location = new System.Drawing.Point(6, 11);
            this.btn_ekle.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ekle.Name = "btn_ekle";
            this.btn_ekle.Size = new System.Drawing.Size(24, 22);
            this.btn_ekle.TabIndex = 0;
            this.btn_ekle.Text = "+";
            this.btn_ekle.Click += new System.EventHandler(this.btn_ekle_Click);
            // 
            // btn_cikar
            // 
            this.btn_cikar.Appearance.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.btn_cikar.Appearance.ForeColor = System.Drawing.Color.Red;
            this.btn_cikar.Appearance.Options.UseFont = true;
            this.btn_cikar.Appearance.Options.UseForeColor = true;
            this.btn_cikar.Location = new System.Drawing.Point(6, 39);
            this.btn_cikar.Margin = new System.Windows.Forms.Padding(4);
            this.btn_cikar.Name = "btn_cikar";
            this.btn_cikar.Size = new System.Drawing.Size(24, 22);
            this.btn_cikar.TabIndex = 1;
            this.btn_cikar.Text = "-";
            this.btn_cikar.Click += new System.EventHandler(this.btn_cikar_Click);
            // 
            // lbl_product
            // 
            this.lbl_product.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.lbl_product.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lbl_product.Location = new System.Drawing.Point(38, 11);
            this.lbl_product.Margin = new System.Windows.Forms.Padding(4);
            this.lbl_product.Name = "lbl_product";
            this.lbl_product.Size = new System.Drawing.Size(55, 18);
            this.lbl_product.TabIndex = 2;
            this.lbl_product.Text = "Ürün Adı";
            // 
            // lbl_tutar
            // 
            this.lbl_tutar.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lbl_tutar.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lbl_tutar.Location = new System.Drawing.Point(190, 43);
            this.lbl_tutar.Margin = new System.Windows.Forms.Padding(4);
            this.lbl_tutar.Name = "lbl_tutar";
            this.lbl_tutar.Size = new System.Drawing.Size(46, 19);
            this.lbl_tutar.TabIndex = 4;
            this.lbl_tutar.Text = "3,5 TL";
            // 
            // lbl_x
            // 
            this.lbl_x.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lbl_x.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lbl_x.Location = new System.Drawing.Point(99, 43);
            this.lbl_x.Margin = new System.Windows.Forms.Padding(4);
            this.lbl_x.Name = "lbl_x";
            this.lbl_x.Size = new System.Drawing.Size(13, 19);
            this.lbl_x.TabIndex = 5;
            this.lbl_x.Text = "x ";
            // 
            // spin_birim_fiyat
            // 
            this.spin_birim_fiyat.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_birim_fiyat.Location = new System.Drawing.Point(115, 43);
            this.spin_birim_fiyat.Name = "spin_birim_fiyat";
            this.spin_birim_fiyat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spin_birim_fiyat.Properties.Mask.BeepOnError = true;
            this.spin_birim_fiyat.Properties.Mask.EditMask = "n";
            this.spin_birim_fiyat.Size = new System.Drawing.Size(67, 20);
            this.spin_birim_fiyat.TabIndex = 6;
            this.spin_birim_fiyat.EditValueChanged += new System.EventHandler(this.spin_birim_fiyat_EditValueChanged);
            this.spin_birim_fiyat.Click += new System.EventHandler(this.spin_birim_fiyat_Click);
            // 
            // lbl_miktar
            // 
            this.lbl_miktar.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.lbl_miktar.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lbl_miktar.Appearance.Options.UseFont = true;
            this.lbl_miktar.Appearance.Options.UseForeColor = true;
            this.lbl_miktar.Location = new System.Drawing.Point(37, 39);
            this.lbl_miktar.Margin = new System.Windows.Forms.Padding(4);
            this.lbl_miktar.Name = "lbl_miktar";
            this.lbl_miktar.Size = new System.Drawing.Size(55, 23);
            this.lbl_miktar.TabIndex = 7;
            this.lbl_miktar.Text = "100,50";
            this.lbl_miktar.Click += new System.EventHandler(this.lbl_miktar_Click);
            // 
            // btn_desc
            // 
            this.btn_desc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_desc.Appearance.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.btn_desc.Appearance.ForeColor = System.Drawing.Color.Green;
            this.btn_desc.Appearance.Options.UseFont = true;
            this.btn_desc.Appearance.Options.UseForeColor = true;
            this.btn_desc.Image = global::StockProgram.Properties.Resources.edit_blue;
            this.btn_desc.Location = new System.Drawing.Point(230, 11);
            this.btn_desc.Margin = new System.Windows.Forms.Padding(4);
            this.btn_desc.Name = "btn_desc";
            this.btn_desc.Size = new System.Drawing.Size(24, 22);
            this.btn_desc.TabIndex = 8;
            this.btn_desc.Click += new System.EventHandler(this.btn_desc_Click);
            // 
            // ucAdisyon
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Window;
            this.Appearance.Font = new System.Drawing.Font("Verdana", 11F);
            this.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btn_desc);
            this.Controls.Add(this.lbl_miktar);
            this.Controls.Add(this.spin_birim_fiyat);
            this.Controls.Add(this.lbl_x);
            this.Controls.Add(this.lbl_tutar);
            this.Controls.Add(this.lbl_product);
            this.Controls.Add(this.btn_cikar);
            this.Controls.Add(this.btn_ekle);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucAdisyon";
            this.Size = new System.Drawing.Size(258, 73);
            this.Load += new System.EventHandler(this.ucAdisyon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spin_birim_fiyat.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_ekle;
        private DevExpress.XtraEditors.SimpleButton btn_cikar;
        private DevExpress.XtraEditors.LabelControl lbl_product;
        private DevExpress.XtraEditors.LabelControl lbl_tutar;
        private DevExpress.XtraEditors.LabelControl lbl_x;
        private DevExpress.XtraEditors.SpinEdit spin_birim_fiyat;
        private DevExpress.XtraEditors.SimpleButton lbl_miktar;
        private DevExpress.XtraEditors.SimpleButton btn_desc;


    }
}
