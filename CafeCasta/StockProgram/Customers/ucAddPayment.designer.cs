namespace StockProgram.Customers
{
    partial class ucAddPayment
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
            this.btn_back = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_table_number = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.spin_fiyat = new DevExpress.XtraEditors.SpinEdit();
            this.txt_tanim = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_giderEkle = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spin_fiyat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_tanim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_back);
            this.panel1.Controls.Add(this.lbl_table_number);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(574, 39);
            this.panel1.TabIndex = 55;
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(495, 2);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(77, 35);
            this.btn_back.TabIndex = 42;
            this.btn_back.Text = "Geri";
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // lbl_table_number
            // 
            this.lbl_table_number.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_table_number.Location = new System.Drawing.Point(5, 8);
            this.lbl_table_number.Name = "lbl_table_number";
            this.lbl_table_number.Size = new System.Drawing.Size(73, 22);
            this.lbl_table_number.TabIndex = 2;
            this.lbl_table_number.Text = "Borç Öde";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(96, 161);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(67, 13);
            this.labelControl9.TabIndex = 75;
            this.labelControl9.Text = "Ödenen Tutar";
            // 
            // spin_fiyat
            // 
            this.spin_fiyat.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_fiyat.Location = new System.Drawing.Point(173, 158);
            this.spin_fiyat.Name = "spin_fiyat";
            this.spin_fiyat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spin_fiyat.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spin_fiyat.Properties.Mask.BeepOnError = true;
            this.spin_fiyat.Properties.Mask.EditMask = "n2";
            this.spin_fiyat.Size = new System.Drawing.Size(80, 20);
            this.spin_fiyat.TabIndex = 73;
            // 
            // txt_tanim
            // 
            this.txt_tanim.Location = new System.Drawing.Point(173, 97);
            this.txt_tanim.Name = "txt_tanim";
            this.txt_tanim.Size = new System.Drawing.Size(253, 20);
            this.txt_tanim.TabIndex = 72;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(96, 100);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(41, 13);
            this.labelControl4.TabIndex = 74;
            this.labelControl4.Text = "Açıklama";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_giderEkle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 317);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(574, 39);
            this.panelControl1.TabIndex = 76;
            // 
            // btn_giderEkle
            // 
            this.btn_giderEkle.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_giderEkle.Image = global::StockProgram.Properties.Resources.add_blue;
            this.btn_giderEkle.Location = new System.Drawing.Point(2, 2);
            this.btn_giderEkle.Name = "btn_giderEkle";
            this.btn_giderEkle.Size = new System.Drawing.Size(110, 35);
            this.btn_giderEkle.TabIndex = 3;
            this.btn_giderEkle.Text = "Ödeme Yap";
            this.btn_giderEkle.Click += new System.EventHandler(this.btn_giderEkle_Click);
            // 
            // ucAddCustomers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.spin_fiyat);
            this.Controls.Add(this.txt_tanim);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.panel1);
            this.Name = "ucAddCustomers";
            this.Size = new System.Drawing.Size(574, 356);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spin_fiyat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_tanim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btn_back;
        private DevExpress.XtraEditors.LabelControl lbl_table_number;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.SpinEdit spin_fiyat;
        private DevExpress.XtraEditors.TextEdit txt_tanim;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_giderEkle;
    }
}
