namespace StockProgram.Products
{
    partial class ucDepoCikis
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
            this.btn_depoCikis = new DevExpress.XtraEditors.SimpleButton();
            this.cb_bagliDepo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.cb_bagliTedarikci = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.txt_urunAdet = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.cb_urunAdi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.lbl_table_number = new DevExpress.XtraEditors.LabelControl();
            this.btn_back = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cb_bagliDepo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_bagliTedarikci.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_urunAdet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_urunAdi.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_depoCikis
            // 
            this.btn_depoCikis.Image = global::StockProgram.Properties.Resources.remove;
            this.btn_depoCikis.Location = new System.Drawing.Point(251, 292);
            this.btn_depoCikis.Name = "btn_depoCikis";
            this.btn_depoCikis.Size = new System.Drawing.Size(124, 41);
            this.btn_depoCikis.TabIndex = 67;
            this.btn_depoCikis.Text = "Depo Çıkış Yap";
            this.btn_depoCikis.Click += new System.EventHandler(this.btn_depoCikis_Click_1);
            // 
            // cb_bagliDepo
            // 
            this.cb_bagliDepo.Location = new System.Drawing.Point(212, 157);
            this.cb_bagliDepo.Name = "cb_bagliDepo";
            this.cb_bagliDepo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_bagliDepo.Size = new System.Drawing.Size(208, 20);
            this.cb_bagliDepo.TabIndex = 66;
            this.cb_bagliDepo.SelectedIndexChanged += new System.EventHandler(this.cb_bagliDepo_SelectedIndexChanged_1);
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(214, 138);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(87, 13);
            this.labelControl14.TabIndex = 65;
            this.labelControl14.Text = "* Bulunduğu Depo";
            // 
            // cb_bagliTedarikci
            // 
            this.cb_bagliTedarikci.Location = new System.Drawing.Point(212, 211);
            this.cb_bagliTedarikci.Name = "cb_bagliTedarikci";
            this.cb_bagliTedarikci.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_bagliTedarikci.Size = new System.Drawing.Size(208, 20);
            this.cb_bagliTedarikci.TabIndex = 64;
            this.cb_bagliTedarikci.SelectedIndexChanged += new System.EventHandler(this.cb_bagliTedarikci_SelectedIndexChanged_1);
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(212, 192);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(95, 13);
            this.labelControl12.TabIndex = 63;
            this.labelControl12.Text = "* Ürün Tedarikçi Adı";
            // 
            // txt_urunAdet
            // 
            this.txt_urunAdet.Location = new System.Drawing.Point(212, 266);
            this.txt_urunAdet.Name = "txt_urunAdet";
            this.txt_urunAdet.Properties.Mask.EditMask = "n0";
            this.txt_urunAdet.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txt_urunAdet.Size = new System.Drawing.Size(208, 20);
            this.txt_urunAdet.TabIndex = 62;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(212, 247);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(58, 13);
            this.labelControl8.TabIndex = 61;
            this.labelControl8.Text = "* Ürün Adet";
            // 
            // cb_urunAdi
            // 
            this.cb_urunAdi.Location = new System.Drawing.Point(212, 100);
            this.cb_urunAdi.Name = "cb_urunAdi";
            this.cb_urunAdi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_urunAdi.Size = new System.Drawing.Size(208, 20);
            this.cb_urunAdi.TabIndex = 60;
            this.cb_urunAdi.SelectedIndexChanged += new System.EventHandler(this.cb_urunAdi_SelectedIndexChanged_1);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(212, 81);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 13);
            this.labelControl1.TabIndex = 59;
            this.labelControl1.Text = "* Ürün Adı";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_table_number);
            this.panel1.Controls.Add(this.btn_back);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(934, 39);
            this.panel1.TabIndex = 68;
            // 
            // lbl_table_number
            // 
            this.lbl_table_number.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_table_number.Location = new System.Drawing.Point(5, 8);
            this.lbl_table_number.Name = "lbl_table_number";
            this.lbl_table_number.Size = new System.Drawing.Size(140, 22);
            this.lbl_table_number.TabIndex = 2;
            this.lbl_table_number.Text = "Ürün - Depo Çıkış";
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(857, 0);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(77, 39);
            this.btn_back.TabIndex = 41;
            this.btn_back.Text = "Geri";
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // ucDepoCikis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_depoCikis);
            this.Controls.Add(this.cb_bagliDepo);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txt_urunAdet);
            this.Controls.Add(this.labelControl14);
            this.Controls.Add(this.labelControl12);
            this.Controls.Add(this.cb_urunAdi);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.cb_bagliTedarikci);
            this.Name = "ucDepoCikis";
            this.Size = new System.Drawing.Size(934, 360);
            this.Load += new System.EventHandler(this.ucDepoCikis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cb_bagliDepo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_bagliTedarikci.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_urunAdet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_urunAdi.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_depoCikis;
        private DevExpress.XtraEditors.ComboBoxEdit cb_bagliDepo;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.ComboBoxEdit cb_bagliTedarikci;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.TextEdit txt_urunAdet;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.ComboBoxEdit cb_urunAdi;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.LabelControl lbl_table_number;
        private DevExpress.XtraEditors.SimpleButton btn_back;

    }
}
