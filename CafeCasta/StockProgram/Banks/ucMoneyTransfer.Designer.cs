namespace StockProgram.Banks
{
    partial class ucMoneyTransfer
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
            this.btn_bankaEkle = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_isim = new DevExpress.XtraEditors.LabelControl();
            this.spin_miktar = new DevExpress.XtraEditors.SpinEdit();
            this.chk_cek = new DevExpress.XtraEditors.CheckEdit();
            this.chk_yatir = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spin_miktar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_cek.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_yatir.Properties)).BeginInit();
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
            this.panel1.Size = new System.Drawing.Size(623, 39);
            this.panel1.TabIndex = 78;
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(544, 2);
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
            this.lbl_table_number.Size = new System.Drawing.Size(109, 22);
            this.lbl_table_number.TabIndex = 2;
            this.lbl_table_number.Text = "Para Transferi";
            // 
            // btn_bankaEkle
            // 
            this.btn_bankaEkle.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_bankaEkle.Image = global::StockProgram.Properties.Resources.add_blue;
            this.btn_bankaEkle.Location = new System.Drawing.Point(2, 2);
            this.btn_bankaEkle.Name = "btn_bankaEkle";
            this.btn_bankaEkle.Size = new System.Drawing.Size(115, 35);
            this.btn_bankaEkle.TabIndex = 81;
            this.btn_bankaEkle.Text = "Transfer Et";
            this.btn_bankaEkle.Click += new System.EventHandler(this.btn_bankaEkle_Click);
            // 
            // lbl_isim
            // 
            this.lbl_isim.Location = new System.Drawing.Point(115, 106);
            this.lbl_isim.Name = "lbl_isim";
            this.lbl_isim.Size = new System.Drawing.Size(65, 13);
            this.lbl_isim.TabIndex = 80;
            this.lbl_isim.Text = "Bankanın İsmi";
            // 
            // spin_miktar
            // 
            this.spin_miktar.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_miktar.Location = new System.Drawing.Point(199, 103);
            this.spin_miktar.Name = "spin_miktar";
            this.spin_miktar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spin_miktar.Properties.Mask.BeepOnError = true;
            this.spin_miktar.Properties.Mask.EditMask = "n";
            this.spin_miktar.Size = new System.Drawing.Size(103, 20);
            this.spin_miktar.TabIndex = 82;
            // 
            // chk_cek
            // 
            this.chk_cek.Location = new System.Drawing.Point(366, 83);
            this.chk_cek.Name = "chk_cek";
            this.chk_cek.Properties.Caption = "Para Çek";
            this.chk_cek.Size = new System.Drawing.Size(75, 19);
            this.chk_cek.TabIndex = 84;
            this.chk_cek.CheckedChanged += new System.EventHandler(this.chk_cek_CheckedChanged);
            // 
            // chk_yatir
            // 
            this.chk_yatir.Location = new System.Drawing.Point(366, 119);
            this.chk_yatir.Name = "chk_yatir";
            this.chk_yatir.Properties.Caption = "Para Yatır";
            this.chk_yatir.Size = new System.Drawing.Size(75, 19);
            this.chk_yatir.TabIndex = 85;
            this.chk_yatir.CheckedChanged += new System.EventHandler(this.chk_yatir_CheckedChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_bankaEkle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 196);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(623, 39);
            this.panelControl1.TabIndex = 86;
            // 
            // ucMoneyTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.chk_yatir);
            this.Controls.Add(this.chk_cek);
            this.Controls.Add(this.spin_miktar);
            this.Controls.Add(this.lbl_isim);
            this.Controls.Add(this.panel1);
            this.Name = "ucMoneyTransfer";
            this.Size = new System.Drawing.Size(623, 235);
            this.Load += new System.EventHandler(this.ucMoneyTransfer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spin_miktar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_cek.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_yatir.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btn_back;
        private DevExpress.XtraEditors.LabelControl lbl_table_number;
        private DevExpress.XtraEditors.SimpleButton btn_bankaEkle;
        private DevExpress.XtraEditors.LabelControl lbl_isim;
        private DevExpress.XtraEditors.SpinEdit spin_miktar;
        private DevExpress.XtraEditors.CheckEdit chk_cek;
        private DevExpress.XtraEditors.CheckEdit chk_yatir;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}
