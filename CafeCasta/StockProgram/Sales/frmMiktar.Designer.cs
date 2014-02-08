namespace StockProgram.Sales
{
    partial class frmMiktar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMiktar));
            this.lbl_urun_adi = new DevExpress.XtraEditors.LabelControl();
            this.spin_miktar = new DevExpress.XtraEditors.SpinEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btn_tamamla = new DevExpress.XtraEditors.SimpleButton();
            this.btn_duzenle = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.spin_miktar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_urun_adi
            // 
            this.lbl_urun_adi.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_urun_adi.Location = new System.Drawing.Point(5, 8);
            this.lbl_urun_adi.Name = "lbl_urun_adi";
            this.lbl_urun_adi.Size = new System.Drawing.Size(69, 22);
            this.lbl_urun_adi.TabIndex = 2;
            this.lbl_urun_adi.Text = "Ürün Adı";
            // 
            // spin_miktar
            // 
            this.spin_miktar.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_miktar.Location = new System.Drawing.Point(133, 67);
            this.spin_miktar.Name = "spin_miktar";
            this.spin_miktar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spin_miktar.Properties.Mask.BeepOnError = true;
            this.spin_miktar.Properties.Mask.EditMask = "n2";
            this.spin_miktar.Size = new System.Drawing.Size(103, 20);
            this.spin_miktar.TabIndex = 0;
            this.spin_miktar.EditValueChanged += new System.EventHandler(this.spin_miktar_EditValueChanged_1);
            this.spin_miktar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.spin_miktar_KeyPress);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btn_tamamla);
            this.panelControl2.Controls.Add(this.btn_duzenle);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 147);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(267, 44);
            this.panelControl2.TabIndex = 52;
            // 
            // btn_tamamla
            // 
            this.btn_tamamla.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_tamamla.Image = global::StockProgram.Properties.Resources.ok_blue;
            this.btn_tamamla.Location = new System.Drawing.Point(94, 2);
            this.btn_tamamla.Name = "btn_tamamla";
            this.btn_tamamla.Size = new System.Drawing.Size(87, 40);
            this.btn_tamamla.TabIndex = 6;
            this.btn_tamamla.Text = "Tamamla";
            this.btn_tamamla.Click += new System.EventHandler(this.btn_tamamla_Click_1);
            // 
            // btn_duzenle
            // 
            this.btn_duzenle.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_duzenle.Image = global::StockProgram.Properties.Resources.delete;
            this.btn_duzenle.Location = new System.Drawing.Point(181, 2);
            this.btn_duzenle.Name = "btn_duzenle";
            this.btn_duzenle.Size = new System.Drawing.Size(84, 40);
            this.btn_duzenle.TabIndex = 8;
            this.btn_duzenle.Text = "Kapat";
            this.btn_duzenle.Click += new System.EventHandler(this.btn_duzenle_Click_1);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.spin_miktar);
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Controls.Add(this.lbl_urun_adi);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 147);
            this.panel1.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.labelControl2.Location = new System.Drawing.Point(16, 63);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(111, 22);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Adet / Gramaj";
            // 
            // frmMiktar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 191);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelControl2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMiktar";
            this.Text = "Miktar Giriniz";
            this.Load += new System.EventHandler(this.frmSatis_Load);
            this.Shown += new System.EventHandler(this.frmMiktar_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.spin_miktar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lbl_urun_adi;
        private DevExpress.XtraEditors.SpinEdit spin_miktar;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btn_tamamla;
        private DevExpress.XtraEditors.SimpleButton btn_duzenle;
        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.LabelControl labelControl2;

    }
}