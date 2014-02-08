namespace StockProgram.Products
{
    public partial class ucShoeSize
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
            this.btn_numara = new DevExpress.XtraEditors.SimpleButton();
            this.spin_adet = new DevExpress.XtraEditors.SpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_adet.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_numara
            // 
            this.btn_numara.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.btn_numara.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.btn_numara.Appearance.Options.UseFont = true;
            this.btn_numara.Appearance.Options.UseForeColor = true;
            this.btn_numara.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_numara.Location = new System.Drawing.Point(0, 0);
            this.btn_numara.Margin = new System.Windows.Forms.Padding(4);
            this.btn_numara.Name = "btn_numara";
            this.btn_numara.Size = new System.Drawing.Size(71, 22);
            this.btn_numara.TabIndex = 0;
            this.btn_numara.Text = "NO";
            this.btn_numara.Click += new System.EventHandler(this.btn_ekle_Click);
            // 
            // spin_adet
            // 
            this.spin_adet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.spin_adet.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_adet.Location = new System.Drawing.Point(0, 30);
            this.spin_adet.Name = "spin_adet";
            this.spin_adet.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spin_adet.Properties.Mask.BeepOnError = true;
            this.spin_adet.Properties.Mask.EditMask = "n0";
            this.spin_adet.Size = new System.Drawing.Size(71, 20);
            this.spin_adet.TabIndex = 70;
            this.spin_adet.EditValueChanged += new System.EventHandler(this.spin_adet_EditValueChanged_1);
            // 
            // ucShoeSize
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
            this.Controls.Add(this.spin_adet);
            this.Controls.Add(this.btn_numara);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucShoeSize";
            this.Size = new System.Drawing.Size(71, 50);
            this.Load += new System.EventHandler(this.ucAdisyon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spin_adet.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.SimpleButton btn_numara;
        public  DevExpress.XtraEditors.SpinEdit spin_adet;


    }
}
