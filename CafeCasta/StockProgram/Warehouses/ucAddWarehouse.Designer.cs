namespace StockProgram.Warehouses
{
    partial class ucAddWarehouse
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
            this.cb_bagliTedarikci = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txt_depoAdi = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btn_depoEkle = new DevExpress.XtraEditors.SimpleButton();
            this.txt_depoTanim = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cb_depoDurum = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.cb_bagliTedarikci.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_depoAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_depoTanim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_depoDurum.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cb_bagliTedarikci
            // 
            this.cb_bagliTedarikci.Location = new System.Drawing.Point(298, 106);
            this.cb_bagliTedarikci.Name = "cb_bagliTedarikci";
            this.cb_bagliTedarikci.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_bagliTedarikci.Size = new System.Drawing.Size(208, 20);
            this.cb_bagliTedarikci.TabIndex = 9;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(298, 87);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(159, 13);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "* Deponun Bağlı Olduğu Tedarikçi";
            // 
            // txt_depoAdi
            // 
            this.txt_depoAdi.Location = new System.Drawing.Point(36, 106);
            this.txt_depoAdi.Name = "txt_depoAdi";
            this.txt_depoAdi.Size = new System.Drawing.Size(242, 20);
            this.txt_depoAdi.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(36, 87);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "* Depo Adı";
            // 
            // btn_depoEkle
            // 
            this.btn_depoEkle.Image = global::StockProgram.Properties.Resources.add_blue;
            this.btn_depoEkle.Location = new System.Drawing.Point(400, 234);
            this.btn_depoEkle.Name = "btn_depoEkle";
            this.btn_depoEkle.Size = new System.Drawing.Size(106, 46);
            this.btn_depoEkle.TabIndex = 5;
            this.btn_depoEkle.Text = "Depo Ekle";
            this.btn_depoEkle.Click += new System.EventHandler(this.btn_depoEkle_Click);
            // 
            // txt_depoTanim
            // 
            this.txt_depoTanim.Location = new System.Drawing.Point(36, 168);
            this.txt_depoTanim.Name = "txt_depoTanim";
            this.txt_depoTanim.Size = new System.Drawing.Size(242, 20);
            this.txt_depoTanim.TabIndex = 11;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(36, 149);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(58, 13);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "Depo Tanımı";
            // 
            // cb_depoDurum
            // 
            this.cb_depoDurum.Location = new System.Drawing.Point(36, 237);
            this.cb_depoDurum.Name = "cb_depoDurum";
            this.cb_depoDurum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_depoDurum.Properties.Items.AddRange(new object[] {
            "Açık",
            "Kapalı"});
            this.cb_depoDurum.Size = new System.Drawing.Size(138, 20);
            this.cb_depoDurum.TabIndex = 13;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(36, 218);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(136, 13);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = "* Depo Durum (Açık - Kapalı)";
            // 
            // ucAddWarehouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cb_depoDurum);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txt_depoTanim);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.cb_bagliTedarikci);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txt_depoAdi);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btn_depoEkle);
            this.Name = "ucAddWarehouse";
            this.Size = new System.Drawing.Size(587, 349);
            this.Load += new System.EventHandler(this.ucAddWarehouse_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cb_bagliTedarikci.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_depoAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_depoTanim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_depoDurum.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit cb_bagliTedarikci;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txt_depoAdi;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_depoEkle;
        private DevExpress.XtraEditors.TextEdit txt_depoTanim;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit cb_depoDurum;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}
