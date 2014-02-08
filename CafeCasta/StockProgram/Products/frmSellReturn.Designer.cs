namespace StockProgram.Products
{
    partial class frmSellReturn
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btn_tamamla = new DevExpress.XtraEditors.SimpleButton();
            this.pnl_grid = new DevExpress.XtraEditors.PanelControl();
            this.spin_birim_fiyat = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txt_desc = new DevExpress.XtraEditors.TextEdit();
            this.txt_urun_adi = new DevExpress.XtraEditors.TextEdit();
            this.txt_numara = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txt_renk = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.spin_toplam = new DevExpress.XtraEditors.SpinEdit();
            this.spin_urun_adet = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_grid)).BeginInit();
            this.pnl_grid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spin_birim_fiyat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_desc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_urun_adi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_numara.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_renk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_toplam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_urun_adet.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btn_tamamla);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 343);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(543, 44);
            this.panelControl2.TabIndex = 52;
            // 
            // btn_tamamla
            // 
            this.btn_tamamla.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_tamamla.Image = global::StockProgram.Properties.Resources.ok_blue;
            this.btn_tamamla.Location = new System.Drawing.Point(412, 2);
            this.btn_tamamla.Name = "btn_tamamla";
            this.btn_tamamla.Size = new System.Drawing.Size(129, 40);
            this.btn_tamamla.TabIndex = 10;
            this.btn_tamamla.Text = "İadeyi Tamamla";
            this.btn_tamamla.Click += new System.EventHandler(this.btn_tamamla_Click);
            // 
            // pnl_grid
            // 
            this.pnl_grid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_grid.Controls.Add(this.spin_birim_fiyat);
            this.pnl_grid.Controls.Add(this.labelControl4);
            this.pnl_grid.Controls.Add(this.labelControl2);
            this.pnl_grid.Controls.Add(this.txt_desc);
            this.pnl_grid.Controls.Add(this.txt_urun_adi);
            this.pnl_grid.Controls.Add(this.txt_numara);
            this.pnl_grid.Controls.Add(this.labelControl6);
            this.pnl_grid.Controls.Add(this.labelControl5);
            this.pnl_grid.Controls.Add(this.txt_renk);
            this.pnl_grid.Controls.Add(this.labelControl3);
            this.pnl_grid.Controls.Add(this.spin_toplam);
            this.pnl_grid.Controls.Add(this.spin_urun_adet);
            this.pnl_grid.Controls.Add(this.labelControl8);
            this.pnl_grid.Controls.Add(this.labelControl1);
            this.pnl_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_grid.Location = new System.Drawing.Point(0, 0);
            this.pnl_grid.Name = "pnl_grid";
            this.pnl_grid.Size = new System.Drawing.Size(543, 343);
            this.pnl_grid.TabIndex = 53;
            // 
            // spin_birim_fiyat
            // 
            this.spin_birim_fiyat.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_birim_fiyat.Location = new System.Drawing.Point(52, 232);
            this.spin_birim_fiyat.Name = "spin_birim_fiyat";
            this.spin_birim_fiyat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spin_birim_fiyat.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spin_birim_fiyat.Properties.Mask.BeepOnError = true;
            this.spin_birim_fiyat.Properties.Mask.EditMask = "n";
            this.spin_birim_fiyat.Properties.ReadOnly = true;
            this.spin_birim_fiyat.Size = new System.Drawing.Size(80, 20);
            this.spin_birim_fiyat.TabIndex = 106;
            this.spin_birim_fiyat.EditValueChanged += new System.EventHandler(this.spin_birim_fiyat_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(52, 213);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(58, 13);
            this.labelControl4.TabIndex = 105;
            this.labelControl4.Text = "* Birim Fiyat";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(52, 147);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(92, 13);
            this.labelControl2.TabIndex = 104;
            this.labelControl2.Text = "* Ürün İade Sebebi";
            // 
            // txt_desc
            // 
            this.txt_desc.Location = new System.Drawing.Point(52, 166);
            this.txt_desc.Name = "txt_desc";
            this.txt_desc.Size = new System.Drawing.Size(185, 20);
            this.txt_desc.TabIndex = 103;
            // 
            // txt_urun_adi
            // 
            this.txt_urun_adi.Location = new System.Drawing.Point(52, 95);
            this.txt_urun_adi.Name = "txt_urun_adi";
            this.txt_urun_adi.Properties.ReadOnly = true;
            this.txt_urun_adi.Size = new System.Drawing.Size(185, 20);
            this.txt_urun_adi.TabIndex = 100;
            // 
            // txt_numara
            // 
            this.txt_numara.Location = new System.Drawing.Point(387, 95);
            this.txt_numara.Name = "txt_numara";
            this.txt_numara.Properties.ReadOnly = true;
            this.txt_numara.Size = new System.Drawing.Size(80, 20);
            this.txt_numara.TabIndex = 99;
            this.txt_numara.Visible = false;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(273, 76);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(24, 13);
            this.labelControl6.TabIndex = 98;
            this.labelControl6.Text = "Renk";
            this.labelControl6.Visible = false;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(387, 76);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(37, 13);
            this.labelControl5.TabIndex = 97;
            this.labelControl5.Text = "Numara";
            this.labelControl5.Visible = false;
            // 
            // txt_renk
            // 
            this.txt_renk.Location = new System.Drawing.Point(273, 95);
            this.txt_renk.Name = "txt_renk";
            this.txt_renk.Properties.ReadOnly = true;
            this.txt_renk.Size = new System.Drawing.Size(80, 20);
            this.txt_renk.TabIndex = 96;
            this.txt_renk.Visible = false;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(387, 213);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(70, 13);
            this.labelControl3.TabIndex = 95;
            this.labelControl3.Text = "* Toplam Fiyat";
            // 
            // spin_toplam
            // 
            this.spin_toplam.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_toplam.Location = new System.Drawing.Point(387, 232);
            this.spin_toplam.Name = "spin_toplam";
            this.spin_toplam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spin_toplam.Properties.Mask.BeepOnError = true;
            this.spin_toplam.Properties.Mask.EditMask = "n";
            this.spin_toplam.Properties.ReadOnly = true;
            this.spin_toplam.Size = new System.Drawing.Size(80, 20);
            this.spin_toplam.TabIndex = 94;
            this.spin_toplam.EditValueChanged += new System.EventHandler(this.spin_toplam_EditValueChanged);
            // 
            // spin_urun_adet
            // 
            this.spin_urun_adet.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_urun_adet.Location = new System.Drawing.Point(157, 232);
            this.spin_urun_adet.Name = "spin_urun_adet";
            this.spin_urun_adet.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spin_urun_adet.Properties.Mask.BeepOnError = true;
            this.spin_urun_adet.Properties.Mask.EditMask = "n2";
            this.spin_urun_adet.Size = new System.Drawing.Size(80, 20);
            this.spin_urun_adet.TabIndex = 93;
            this.spin_urun_adet.EditValueChanged += new System.EventHandler(this.spin_urun_adet_EditValueChanged);
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(157, 213);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(58, 13);
            this.labelControl8.TabIndex = 90;
            this.labelControl8.Text = "* Ürün Adet";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(52, 76);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(41, 13);
            this.labelControl1.TabIndex = 89;
            this.labelControl1.Text = "Ürün Adı";
            // 
            // frmSellReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 387);
            this.Controls.Add(this.pnl_grid);
            this.Controls.Add(this.panelControl2);
            this.Name = "frmSellReturn";
            this.Text = "Ürün İade Detayları";
            this.Load += new System.EventHandler(this.frmColorSizePopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_grid)).EndInit();
            this.pnl_grid.ResumeLayout(false);
            this.pnl_grid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spin_birim_fiyat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_desc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_urun_adi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_numara.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_renk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_toplam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_urun_adet.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btn_tamamla;
        private DevExpress.XtraEditors.PanelControl pnl_grid;
        private DevExpress.XtraEditors.TextEdit txt_urun_adi;
        private DevExpress.XtraEditors.TextEdit txt_numara;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txt_renk;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SpinEdit spin_toplam;
        private DevExpress.XtraEditors.SpinEdit spin_urun_adet;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txt_desc;
        private DevExpress.XtraEditors.SpinEdit spin_birim_fiyat;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}