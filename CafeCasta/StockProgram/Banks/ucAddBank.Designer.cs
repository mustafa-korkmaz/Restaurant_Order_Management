namespace StockProgram.Banks
{
    partial class ucAddBank
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
            this.btn_bankaEkle = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_isim = new DevExpress.XtraEditors.TextEdit();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_back = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_table_number = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txt_ay_oran = new DevExpress.XtraEditors.TextEdit();
            this.spin_gun = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txt_isim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ay_oran.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_gun.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_bankaEkle
            // 
            this.btn_bankaEkle.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_bankaEkle.Image = global::StockProgram.Properties.Resources.add_blue;
            this.btn_bankaEkle.Location = new System.Drawing.Point(2, 2);
            this.btn_bankaEkle.Name = "btn_bankaEkle";
            this.btn_bankaEkle.Size = new System.Drawing.Size(115, 35);
            this.btn_bankaEkle.TabIndex = 67;
            this.btn_bankaEkle.Text = "Banka Ekle";
            this.btn_bankaEkle.Click += new System.EventHandler(this.btn_bankaEkle_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(79, 97);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 13);
            this.labelControl1.TabIndex = 67;
            this.labelControl1.Text = "* İsim";
            // 
            // txt_isim
            // 
            this.txt_isim.Location = new System.Drawing.Point(127, 94);
            this.txt_isim.Name = "txt_isim";
            this.txt_isim.Size = new System.Drawing.Size(180, 20);
            this.txt_isim.TabIndex = 66;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_back);
            this.panel1.Controls.Add(this.lbl_table_number);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(778, 39);
            this.panel1.TabIndex = 77;
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(699, 2);
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
            this.lbl_table_number.Size = new System.Drawing.Size(86, 22);
            this.lbl_table_number.TabIndex = 2;
            this.lbl_table_number.Text = "Banka Ekle";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_bankaEkle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 302);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(778, 39);
            this.panelControl1.TabIndex = 78;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(60, 142);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(47, 13);
            this.labelControl2.TabIndex = 80;
            this.labelControl2.Text = "Ay / Oran";
            // 
            // txt_ay_oran
            // 
            this.txt_ay_oran.Location = new System.Drawing.Point(127, 139);
            this.txt_ay_oran.Name = "txt_ay_oran";
            this.txt_ay_oran.Size = new System.Drawing.Size(180, 20);
            this.txt_ay_oran.TabIndex = 67;
            // 
            // spin_gun
            // 
            this.spin_gun.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_gun.Location = new System.Drawing.Point(443, 139);
            this.spin_gun.Name = "spin_gun";
            this.spin_gun.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spin_gun.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spin_gun.Properties.Mask.BeepOnError = true;
            this.spin_gun.Properties.Mask.EditMask = "d";
            this.spin_gun.Size = new System.Drawing.Size(86, 20);
            this.spin_gun.TabIndex = 68;
            this.spin_gun.Click += new System.EventHandler(this.spin_gun_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(358, 142);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(64, 13);
            this.labelControl3.TabIndex = 82;
            this.labelControl3.Text = "Aktarım Günü";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic);
            this.labelControl4.Location = new System.Drawing.Point(60, 207);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(232, 13);
            this.labelControl4.TabIndex = 83;
            this.labelControl4.Text = "3 taksit %2,5 ve 6 taksit %4,5 kesim için örnek: ";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Location = new System.Drawing.Point(298, 207);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(59, 13);
            this.labelControl5.TabIndex = 84;
            this.labelControl5.Text = "3:2,5-6:4,5";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic);
            this.labelControl6.Location = new System.Drawing.Point(60, 239);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(286, 13);
            this.labelControl6.TabIndex = 85;
            this.labelControl6.Text = "Kesinti yapmayan bankalar için Ay / Oran alanına 0:0 giriniz.";
            // 
            // ucAddBank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.spin_gun);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txt_ay_oran);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txt_isim);
            this.Name = "ucAddBank";
            this.Size = new System.Drawing.Size(778, 341);
            ((System.ComponentModel.ISupportInitialize)(this.txt_isim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txt_ay_oran.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_gun.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_bankaEkle;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txt_isim;
        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btn_back;
        private DevExpress.XtraEditors.LabelControl lbl_table_number;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txt_ay_oran;
        private DevExpress.XtraEditors.SpinEdit spin_gun;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}
