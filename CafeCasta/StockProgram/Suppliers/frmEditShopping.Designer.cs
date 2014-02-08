namespace StockProgram.Suppliers
{
    partial class frmEditShopping
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
            this.pnl_grid = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_aciklama = new System.Windows.Forms.TextBox();
            this.chk_tahsilat = new DevExpress.XtraEditors.CheckEdit();
            this.chk_odeme = new DevExpress.XtraEditors.CheckEdit();
            this.spin_miktar = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btn_ilave = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.lbl_tedarikci = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_grid)).BeginInit();
            this.pnl_grid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_tahsilat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_odeme.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_miktar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_grid
            // 
            this.pnl_grid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_grid.Controls.Add(this.groupControl1);
            this.pnl_grid.Controls.Add(this.panelControl1);
            this.pnl_grid.Controls.Add(this.panel1);
            this.pnl_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_grid.Location = new System.Drawing.Point(0, 0);
            this.pnl_grid.Name = "pnl_grid";
            this.pnl_grid.Size = new System.Drawing.Size(543, 387);
            this.pnl_grid.TabIndex = 53;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txt_aciklama);
            this.groupControl1.Controls.Add(this.chk_tahsilat);
            this.groupControl1.Controls.Add(this.chk_odeme);
            this.groupControl1.Controls.Add(this.spin_miktar);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 39);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(543, 304);
            this.groupControl1.TabIndex = 52;
            this.groupControl1.Text = "Ödeme Şekli";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.labelControl1.Location = new System.Drawing.Point(49, 108);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(69, 22);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Açıklama";
            // 
            // txt_aciklama
            // 
            this.txt_aciklama.Location = new System.Drawing.Point(138, 109);
            this.txt_aciklama.Name = "txt_aciklama";
            this.txt_aciklama.Size = new System.Drawing.Size(287, 21);
            this.txt_aciklama.TabIndex = 0;
            // 
            // chk_tahsilat
            // 
            this.chk_tahsilat.Location = new System.Drawing.Point(277, 191);
            this.chk_tahsilat.Name = "chk_tahsilat";
            this.chk_tahsilat.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.chk_tahsilat.Properties.Appearance.Options.UseFont = true;
            this.chk_tahsilat.Properties.Caption = "Alacak Tahsil Et";
            this.chk_tahsilat.Size = new System.Drawing.Size(136, 23);
            this.chk_tahsilat.TabIndex = 8;
            this.chk_tahsilat.Visible = false;
            // 
            // chk_odeme
            // 
            this.chk_odeme.EditValue = true;
            this.chk_odeme.Location = new System.Drawing.Point(327, 146);
            this.chk_odeme.Name = "chk_odeme";
            this.chk_odeme.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.chk_odeme.Properties.Appearance.Options.UseFont = true;
            this.chk_odeme.Properties.Caption = "Ödeme Yap";
            this.chk_odeme.Properties.ReadOnly = true;
            this.chk_odeme.Size = new System.Drawing.Size(112, 23);
            this.chk_odeme.TabIndex = 9;
            this.chk_odeme.Visible = false;
            // 
            // spin_miktar
            // 
            this.spin_miktar.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_miktar.Location = new System.Drawing.Point(138, 149);
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
            this.spin_miktar.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.labelControl2.Location = new System.Drawing.Point(76, 147);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(42, 22);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Tutar";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.btn_ilave);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 343);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(543, 44);
            this.panelControl1.TabIndex = 54;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.simpleButton1.Image = global::StockProgram.Properties.Resources.ok_blue;
            this.simpleButton1.Location = new System.Drawing.Point(309, 2);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(116, 40);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "Ödeme Yap";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btn_ilave
            // 
            this.btn_ilave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_ilave.Image = global::StockProgram.Properties.Resources.add_blue;
            this.btn_ilave.Location = new System.Drawing.Point(425, 2);
            this.btn_ilave.Name = "btn_ilave";
            this.btn_ilave.Size = new System.Drawing.Size(116, 40);
            this.btn_ilave.TabIndex = 4;
            this.btn_ilave.Text = "Borç Kayıt";
            this.btn_ilave.Click += new System.EventHandler(this.btn_ilave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_tedarikci);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(543, 39);
            this.panel1.TabIndex = 53;
            // 
            // lbl_tedarikci
            // 
            this.lbl_tedarikci.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_tedarikci.Location = new System.Drawing.Point(5, 8);
            this.lbl_tedarikci.Name = "lbl_tedarikci";
            this.lbl_tedarikci.Size = new System.Drawing.Size(110, 22);
            this.lbl_tedarikci.TabIndex = 2;
            this.lbl_tedarikci.Text = "Tedarikçi İsmi";
            // 
            // frmEditShopping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 387);
            this.Controls.Add(this.pnl_grid);
            this.Name = "frmEditShopping";
            this.Text = "Tedarikçi İşlemleri";
            this.Load += new System.EventHandler(this.frmColorSizePopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_grid)).EndInit();
            this.pnl_grid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_tahsilat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_odeme.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_miktar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnl_grid;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.TextBox txt_aciklama;
        private DevExpress.XtraEditors.CheckEdit chk_tahsilat;
        private DevExpress.XtraEditors.CheckEdit chk_odeme;
        private DevExpress.XtraEditors.SpinEdit spin_miktar;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btn_ilave;
        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.LabelControl lbl_tedarikci;
    }
}