namespace StockProgram.Products
{
    partial class ucMigo
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
            this.lbl_header = new DevExpress.XtraEditors.LabelControl();
            this.btn_back = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_malGirisi = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelNumara = new DevExpress.XtraEditors.PanelControl();
            this.gControlFiyat = new DevExpress.XtraEditors.GroupControl();
            this.spinMiktar = new DevExpress.XtraEditors.SpinEdit();
            this.spin_KDV = new DevExpress.XtraEditors.SpinEdit();
            this.lbl_birim_fiyat = new DevExpress.XtraEditors.SpinEdit();
            this.chk_pesin_ode = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.spin_toplam = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelUst = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txt_aciklama = new DevExpress.XtraEditors.TextEdit();
            this.cb_bagliTedarikci = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelNumara)).BeginInit();
            this.panelNumara.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gControlFiyat)).BeginInit();
            this.gControlFiyat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinMiktar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_KDV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_birim_fiyat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_pesin_ode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_toplam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelUst)).BeginInit();
            this.panelUst.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_aciklama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_bagliTedarikci.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_header);
            this.panel1.Controls.Add(this.btn_back);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(867, 40);
            this.panel1.TabIndex = 51;
            // 
            // lbl_header
            // 
            this.lbl_header.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_header.Location = new System.Drawing.Point(5, 8);
            this.lbl_header.Name = "lbl_header";
            this.lbl_header.Size = new System.Drawing.Size(227, 22);
            this.lbl_header.TabIndex = 2;
            this.lbl_header.Text = "Ürün Depo Girişi (Ürün Alım)";
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(788, 2);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(77, 36);
            this.btn_back.TabIndex = 41;
            this.btn_back.Text = "Geri";
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_malGirisi);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 359);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(867, 43);
            this.panelControl1.TabIndex = 3;
            // 
            // btn_malGirisi
            // 
            this.btn_malGirisi.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_malGirisi.Image = global::StockProgram.Properties.Resources.add_blue;
            this.btn_malGirisi.Location = new System.Drawing.Point(2, 2);
            this.btn_malGirisi.Name = "btn_malGirisi";
            this.btn_malGirisi.Size = new System.Drawing.Size(124, 39);
            this.btn_malGirisi.TabIndex = 19;
            this.btn_malGirisi.Text = "Mal Girişi Yap";
            this.btn_malGirisi.Click += new System.EventHandler(this.btn_malGirisi_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.panelNumara);
            this.panelControl2.Controls.Add(this.panelUst);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 40);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(867, 319);
            this.panelControl2.TabIndex = 52;
            // 
            // panelNumara
            // 
            this.panelNumara.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelNumara.Controls.Add(this.gControlFiyat);
            this.panelNumara.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNumara.Location = new System.Drawing.Point(2, 146);
            this.panelNumara.Name = "panelNumara";
            this.panelNumara.Size = new System.Drawing.Size(863, 171);
            this.panelNumara.TabIndex = 2;
            // 
            // gControlFiyat
            // 
            this.gControlFiyat.Controls.Add(this.spinMiktar);
            this.gControlFiyat.Controls.Add(this.spin_KDV);
            this.gControlFiyat.Controls.Add(this.lbl_birim_fiyat);
            this.gControlFiyat.Controls.Add(this.chk_pesin_ode);
            this.gControlFiyat.Controls.Add(this.labelControl3);
            this.gControlFiyat.Controls.Add(this.labelControl8);
            this.gControlFiyat.Controls.Add(this.labelControl4);
            this.gControlFiyat.Controls.Add(this.spin_toplam);
            this.gControlFiyat.Controls.Add(this.labelControl2);
            this.gControlFiyat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gControlFiyat.Location = new System.Drawing.Point(0, 0);
            this.gControlFiyat.Name = "gControlFiyat";
            this.gControlFiyat.Size = new System.Drawing.Size(863, 171);
            this.gControlFiyat.TabIndex = 2;
            this.gControlFiyat.Text = "Fiyatlandırma";
            // 
            // spinMiktar
            // 
            this.spinMiktar.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinMiktar.Location = new System.Drawing.Point(18, 75);
            this.spinMiktar.Name = "spinMiktar";
            this.spinMiktar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinMiktar.Properties.Mask.BeepOnError = true;
            this.spinMiktar.Properties.Mask.EditMask = "n2";
            this.spinMiktar.Size = new System.Drawing.Size(80, 20);
            this.spinMiktar.TabIndex = 14;
            this.spinMiktar.EditValueChanged += new System.EventHandler(this.spinMiktar_EditValueChanged_1);
            this.spinMiktar.Click += new System.EventHandler(this.spinMiktar_Click);
            // 
            // spin_KDV
            // 
            this.spin_KDV.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_KDV.Location = new System.Drawing.Point(297, 75);
            this.spin_KDV.Name = "spin_KDV";
            this.spin_KDV.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spin_KDV.Properties.Mask.BeepOnError = true;
            this.spin_KDV.Properties.Mask.EditMask = "n2";
            this.spin_KDV.Size = new System.Drawing.Size(80, 20);
            this.spin_KDV.TabIndex = 16;
            this.spin_KDV.EditValueChanged += new System.EventHandler(this.spin_KDV_EditValueChanged_2);
            this.spin_KDV.Click += new System.EventHandler(this.spin_KDV_Click);
            // 
            // lbl_birim_fiyat
            // 
            this.lbl_birim_fiyat.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.lbl_birim_fiyat.Location = new System.Drawing.Point(159, 75);
            this.lbl_birim_fiyat.Name = "lbl_birim_fiyat";
            this.lbl_birim_fiyat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.lbl_birim_fiyat.Properties.Mask.BeepOnError = true;
            this.lbl_birim_fiyat.Properties.Mask.EditMask = "n2";
            this.lbl_birim_fiyat.Size = new System.Drawing.Size(80, 20);
            this.lbl_birim_fiyat.TabIndex = 15;
            this.lbl_birim_fiyat.EditValueChanged += new System.EventHandler(this.lbl_birim_fiyat_EditValueChanged_2);
            this.lbl_birim_fiyat.Click += new System.EventHandler(this.lbl_birim_fiyat_Click);
            // 
            // chk_pesin_ode
            // 
            this.chk_pesin_ode.Location = new System.Drawing.Point(547, 75);
            this.chk_pesin_ode.Name = "chk_pesin_ode";
            this.chk_pesin_ode.Properties.Caption = "Peşin Öde";
            this.chk_pesin_ode.Size = new System.Drawing.Size(75, 19);
            this.chk_pesin_ode.TabIndex = 18;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(433, 56);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(70, 13);
            this.labelControl3.TabIndex = 57;
            this.labelControl3.Text = "* Toplam Fiyat";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(18, 56);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(38, 13);
            this.labelControl8.TabIndex = 42;
            this.labelControl8.Text = "* Miktar";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(297, 56);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(19, 13);
            this.labelControl4.TabIndex = 59;
            this.labelControl4.Text = "KDV";
            // 
            // spin_toplam
            // 
            this.spin_toplam.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_toplam.Location = new System.Drawing.Point(433, 75);
            this.spin_toplam.Name = "spin_toplam";
            this.spin_toplam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spin_toplam.Properties.Mask.BeepOnError = true;
            this.spin_toplam.Properties.Mask.EditMask = "n";
            this.spin_toplam.Properties.ReadOnly = true;
            this.spin_toplam.Size = new System.Drawing.Size(80, 20);
            this.spin_toplam.TabIndex = 101;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(159, 56);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(79, 13);
            this.labelControl2.TabIndex = 55;
            this.labelControl2.Text = "* Birim Alış Fiyatı";
            // 
            // panelUst
            // 
            this.panelUst.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelUst.Controls.Add(this.groupControl2);
            this.panelUst.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUst.Location = new System.Drawing.Point(2, 2);
            this.panelUst.Name = "panelUst";
            this.panelUst.Size = new System.Drawing.Size(863, 144);
            this.panelUst.TabIndex = 1;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txt_aciklama);
            this.groupControl2.Controls.Add(this.cb_bagliTedarikci);
            this.groupControl2.Controls.Add(this.labelControl12);
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(863, 144);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Alış Bilgileri";
            // 
            // txt_aciklama
            // 
            this.txt_aciklama.Location = new System.Drawing.Point(16, 73);
            this.txt_aciklama.Name = "txt_aciklama";
            this.txt_aciklama.Size = new System.Drawing.Size(182, 20);
            this.txt_aciklama.TabIndex = 1;
            // 
            // cb_bagliTedarikci
            // 
            this.cb_bagliTedarikci.Location = new System.Drawing.Point(297, 73);
            this.cb_bagliTedarikci.Name = "cb_bagliTedarikci";
            this.cb_bagliTedarikci.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_bagliTedarikci.Size = new System.Drawing.Size(160, 20);
            this.cb_bagliTedarikci.TabIndex = 2;
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(296, 54);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(53, 13);
            this.labelControl12.TabIndex = 44;
            this.labelControl12.Text = "* Firma Adı";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(16, 54);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(83, 13);
            this.labelControl7.TabIndex = 65;
            this.labelControl7.Text = "Alışveriş Açıklama";
            // 
            // ucMigo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel1);
            this.Name = "ucMigo";
            this.Size = new System.Drawing.Size(867, 402);
            this.Load += new System.EventHandler(this.ucMIGO_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelNumara)).EndInit();
            this.panelNumara.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gControlFiyat)).EndInit();
            this.gControlFiyat.ResumeLayout(false);
            this.gControlFiyat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinMiktar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_KDV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_birim_fiyat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_pesin_ode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_toplam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelUst)).EndInit();
            this.panelUst.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_aciklama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_bagliTedarikci.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_malGirisi;
        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.LabelControl lbl_header;
        private DevExpress.XtraEditors.SimpleButton btn_back;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelNumara;
        private DevExpress.XtraEditors.GroupControl gControlFiyat;
        private DevExpress.XtraEditors.SpinEdit spinMiktar;
        private DevExpress.XtraEditors.SpinEdit spin_KDV;
        private DevExpress.XtraEditors.SpinEdit lbl_birim_fiyat;
        private DevExpress.XtraEditors.CheckEdit chk_pesin_ode;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SpinEdit spin_toplam;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl panelUst;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.TextEdit txt_aciklama;
        private DevExpress.XtraEditors.ComboBoxEdit cb_bagliTedarikci;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl7;
      
    }
}
