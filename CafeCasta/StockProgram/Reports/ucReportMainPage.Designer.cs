namespace StockProgram.Reports
{
    partial class ucReportMainPage
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
            this.btn_urun_sil = new DevExpress.XtraEditors.SimpleButton();
            this.btn_urun_gir = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_back = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_table_number = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btn_gider = new DevExpress.XtraEditors.SimpleButton();
            this.btn_gun_ay_yil = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_urun_sil
            // 
            this.btn_urun_sil.Image = global::StockProgram.Properties.Resources.statistics;
            this.btn_urun_sil.Location = new System.Drawing.Point(473, 275);
            this.btn_urun_sil.Name = "btn_urun_sil";
            this.btn_urun_sil.Size = new System.Drawing.Size(163, 52);
            this.btn_urun_sil.TabIndex = 8;
            this.btn_urun_sil.Text = "Ürün Kar-Zarar Raporu";
            this.btn_urun_sil.Visible = false;
            this.btn_urun_sil.Click += new System.EventHandler(this.btn_urun_sil_Click);
            // 
            // btn_urun_gir
            // 
            this.btn_urun_gir.Image = global::StockProgram.Properties.Resources.tracking;
            this.btn_urun_gir.Location = new System.Drawing.Point(5, 39);
            this.btn_urun_gir.Name = "btn_urun_gir";
            this.btn_urun_gir.Size = new System.Drawing.Size(160, 52);
            this.btn_urun_gir.TabIndex = 7;
            this.btn_urun_gir.Text = "Kategori Bazlı Ürün İzle";
            this.btn_urun_gir.Click += new System.EventHandler(this.btn_urun_gir_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_back);
            this.panel1.Controls.Add(this.lbl_table_number);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(772, 39);
            this.panel1.TabIndex = 53;
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(700, 2);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(70, 35);
            this.btn_back.TabIndex = 42;
            this.btn_back.Text = "Geri";
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // lbl_table_number
            // 
            this.lbl_table_number.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_table_number.Location = new System.Drawing.Point(5, 8);
            this.lbl_table_number.Name = "lbl_table_number";
            this.lbl_table_number.Size = new System.Drawing.Size(186, 22);
            this.lbl_table_number.TabIndex = 2;
            this.lbl_table_number.Text = "Ürün Takibi ve Raporlar";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btn_gider);
            this.groupControl1.Controls.Add(this.btn_gun_ay_yil);
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.btn_urun_gir);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 39);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(772, 181);
            this.groupControl1.TabIndex = 54;
            this.groupControl1.Text = "Stoklar";
            // 
            // btn_gider
            // 
            this.btn_gider.Image = global::StockProgram.Properties.Resources.wallet;
            this.btn_gider.Location = new System.Drawing.Point(564, 39);
            this.btn_gider.Name = "btn_gider";
            this.btn_gider.Size = new System.Drawing.Size(160, 52);
            this.btn_gider.TabIndex = 10;
            this.btn_gider.Text = "Gider İzle";
            this.btn_gider.Click += new System.EventHandler(this.btn_gider_Click);
            // 
            // btn_gun_ay_yil
            // 
            this.btn_gun_ay_yil.Image = global::StockProgram.Properties.Resources.statistics;
            this.btn_gun_ay_yil.Location = new System.Drawing.Point(376, 39);
            this.btn_gun_ay_yil.Name = "btn_gun_ay_yil";
            this.btn_gun_ay_yil.Size = new System.Drawing.Size(160, 52);
            this.btn_gun_ay_yil.TabIndex = 9;
            this.btn_gun_ay_yil.Text = "Periyodik Karlılık İzle";
            this.btn_gun_ay_yil.Click += new System.EventHandler(this.btn_gun_ay_yil_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Image = global::StockProgram.Properties.Resources.date;
            this.simpleButton1.Location = new System.Drawing.Point(189, 39);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(160, 52);
            this.simpleButton1.TabIndex = 8;
            this.simpleButton1.Text = "Tarih Bazlı Ürün İzle";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // ucReportMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_urun_sil);
            this.Name = "ucReportMainPage";
            this.Size = new System.Drawing.Size(772, 364);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_urun_sil;
        private DevExpress.XtraEditors.SimpleButton btn_urun_gir;
        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.LabelControl lbl_table_number;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btn_back;
        private DevExpress.XtraEditors.SimpleButton btn_gun_ay_yil;
        private DevExpress.XtraEditors.SimpleButton btn_gider;
    }
}
