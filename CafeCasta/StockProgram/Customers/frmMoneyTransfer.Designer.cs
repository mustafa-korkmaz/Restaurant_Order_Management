namespace StockProgram.Customers
{
    partial class frmMoneyTransfer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMoneyTransfer));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.lbl_musteri = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btn_tamamla = new DevExpress.XtraEditors.SimpleButton();
            this.btn_duzenle = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txt_aciklama = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lbl_isim = new DevExpress.XtraEditors.LabelControl();
            this.chk_kayit = new DevExpress.XtraEditors.CheckEdit();
            this.chk_ode = new DevExpress.XtraEditors.CheckEdit();
            this.spin_miktar = new DevExpress.XtraEditors.SpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_aciklama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_kayit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_ode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_miktar.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_musteri);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(543, 39);
            this.panel1.TabIndex = 49;
            // 
            // lbl_musteri
            // 
            this.lbl_musteri.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_musteri.Location = new System.Drawing.Point(5, 8);
            this.lbl_musteri.Name = "lbl_musteri";
            this.lbl_musteri.Size = new System.Drawing.Size(97, 22);
            this.lbl_musteri.TabIndex = 2;
            this.lbl_musteri.Text = "Müşteri İsmi";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btn_tamamla);
            this.panelControl2.Controls.Add(this.btn_duzenle);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 343);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(543, 44);
            this.panelControl2.TabIndex = 51;
            // 
            // btn_tamamla
            // 
            this.btn_tamamla.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_tamamla.Image = global::StockProgram.Properties.Resources.ok_blue;
            this.btn_tamamla.Location = new System.Drawing.Point(297, 2);
            this.btn_tamamla.Name = "btn_tamamla";
            this.btn_tamamla.Size = new System.Drawing.Size(116, 40);
            this.btn_tamamla.TabIndex = 2;
            this.btn_tamamla.Text = "Transfer Et";
            this.btn_tamamla.Click += new System.EventHandler(this.btn_tamamla_Click);
            // 
            // btn_duzenle
            // 
            this.btn_duzenle.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_duzenle.Image = global::StockProgram.Properties.Resources.edit;
            this.btn_duzenle.Location = new System.Drawing.Point(413, 2);
            this.btn_duzenle.Name = "btn_duzenle";
            this.btn_duzenle.Size = new System.Drawing.Size(128, 40);
            this.btn_duzenle.TabIndex = 3;
            this.btn_duzenle.Text = "Müşteri Değiştir";
            this.btn_duzenle.Click += new System.EventHandler(this.btn_duzenle_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txt_aciklama);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.lbl_isim);
            this.groupControl1.Controls.Add(this.chk_kayit);
            this.groupControl1.Controls.Add(this.chk_ode);
            this.groupControl1.Controls.Add(this.spin_miktar);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 39);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(543, 304);
            this.groupControl1.TabIndex = 10;
            this.groupControl1.Text = "Transfer Şekli";
            // 
            // txt_aciklama
            // 
            this.txt_aciklama.Location = new System.Drawing.Point(174, 174);
            this.txt_aciklama.Name = "txt_aciklama";
            this.txt_aciklama.Size = new System.Drawing.Size(239, 20);
            this.txt_aciklama.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(67, 177);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(41, 13);
            this.labelControl1.TabIndex = 87;
            this.labelControl1.Text = "Açıklama";
            // 
            // lbl_isim
            // 
            this.lbl_isim.Location = new System.Drawing.Point(67, 90);
            this.lbl_isim.Name = "lbl_isim";
            this.lbl_isim.Size = new System.Drawing.Size(70, 13);
            this.lbl_isim.TabIndex = 86;
            this.lbl_isim.Text = "Ödenen Miktar";
            // 
            // chk_kayit
            // 
            this.chk_kayit.Location = new System.Drawing.Point(326, 124);
            this.chk_kayit.Name = "chk_kayit";
            this.chk_kayit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.chk_kayit.Properties.Appearance.Options.UseFont = true;
            this.chk_kayit.Properties.Caption = "Borç Kayıt";
            this.chk_kayit.Size = new System.Drawing.Size(136, 23);
            this.chk_kayit.TabIndex = 2;
            this.chk_kayit.CheckedChanged += new System.EventHandler(this.chk_tahsilat_CheckedChanged);
            // 
            // chk_ode
            // 
            this.chk_ode.Location = new System.Drawing.Point(326, 84);
            this.chk_ode.Name = "chk_ode";
            this.chk_ode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.chk_ode.Properties.Appearance.Options.UseFont = true;
            this.chk_ode.Properties.Caption = "Borç Öde";
            this.chk_ode.Size = new System.Drawing.Size(112, 23);
            this.chk_ode.TabIndex = 1;
            this.chk_ode.CheckedChanged += new System.EventHandler(this.chk_odeme_CheckedChanged);
            // 
            // spin_miktar
            // 
            this.spin_miktar.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_miktar.Location = new System.Drawing.Point(174, 87);
            this.spin_miktar.Name = "spin_miktar";
            this.spin_miktar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spin_miktar.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spin_miktar.Properties.Mask.BeepOnError = true;
            this.spin_miktar.Properties.Mask.EditMask = "n";
            this.spin_miktar.Size = new System.Drawing.Size(103, 20);
            this.spin_miktar.TabIndex = 0;
            this.spin_miktar.EditValueChanged += new System.EventHandler(this.spin_miktar_EditValueChanged);
            // 
            // frmMoneyTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 387);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMoneyTransfer";
            this.Text = "Para Transfer İşlemleri";
            this.Load += new System.EventHandler(this.frmSatis_Load);
            this.Shown += new System.EventHandler(this.frmMoneyTransfer_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_aciklama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_kayit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_ode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_miktar.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.LabelControl lbl_musteri;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btn_tamamla;
        private DevExpress.XtraEditors.SimpleButton btn_duzenle;
        private DevExpress.XtraEditors.CheckEdit chk_ode;
        private DevExpress.XtraEditors.SpinEdit spin_miktar;
        private DevExpress.XtraEditors.CheckEdit chk_kayit;
        private DevExpress.XtraEditors.LabelControl lbl_isim;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txt_aciklama;
    }
}