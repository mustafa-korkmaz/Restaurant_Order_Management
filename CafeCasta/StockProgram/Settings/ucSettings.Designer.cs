namespace StockProgram.Settings
{
    partial class ucSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSettings));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.lbl_product_name = new DevExpress.XtraEditors.LabelControl();
            this.btn_settings = new DevExpress.XtraEditors.SimpleButton();
            this.btn_yerlesim = new DevExpress.XtraEditors.SimpleButton();
            this.btn_tablet = new DevExpress.XtraEditors.SimpleButton();
            this.btn_masa = new DevExpress.XtraEditors.SimpleButton();
            this.btn_yazdir = new DevExpress.XtraEditors.SimpleButton();
            this.btn_staff = new DevExpress.XtraEditors.SimpleButton();
            this.btn_options = new DevExpress.XtraEditors.SimpleButton();
            this.btn_back_up = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this.lbl_product_name);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(633, 39);
            this.panel1.TabIndex = 53;
            // 
            // lbl_product_name
            // 
            this.lbl_product_name.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_product_name.Location = new System.Drawing.Point(5, 8);
            this.lbl_product_name.Name = "lbl_product_name";
            this.lbl_product_name.Size = new System.Drawing.Size(108, 22);
            this.lbl_product_name.TabIndex = 2;
            this.lbl_product_name.Text = "CasTa Ayarlar";
            // 
            // btn_settings
            // 
            this.btn_settings.Image = global::StockProgram.Properties.Resources.settings;
            this.btn_settings.Location = new System.Drawing.Point(38, 271);
            this.btn_settings.Name = "btn_settings";
            this.btn_settings.Size = new System.Drawing.Size(161, 60);
            this.btn_settings.TabIndex = 77;
            this.btn_settings.Text = "Genel Ayarlar";
            this.btn_settings.Click += new System.EventHandler(this.btn_settings_Click);
            // 
            // btn_yerlesim
            // 
            this.btn_yerlesim.Image = ((System.Drawing.Image)(resources.GetObject("btn_yerlesim.Image")));
            this.btn_yerlesim.Location = new System.Drawing.Point(237, 81);
            this.btn_yerlesim.Name = "btn_yerlesim";
            this.btn_yerlesim.Size = new System.Drawing.Size(161, 60);
            this.btn_yerlesim.TabIndex = 76;
            this.btn_yerlesim.Text = "Yerleşim Alanları";
            this.btn_yerlesim.Click += new System.EventHandler(this.btn_yerlesim_Click);
            // 
            // btn_tablet
            // 
            this.btn_tablet.Image = global::StockProgram.Properties.Resources.pda;
            this.btn_tablet.Location = new System.Drawing.Point(430, 271);
            this.btn_tablet.Name = "btn_tablet";
            this.btn_tablet.Size = new System.Drawing.Size(161, 60);
            this.btn_tablet.TabIndex = 75;
            this.btn_tablet.Text = "Tabletler";
            this.btn_tablet.Visible = false;
            // 
            // btn_masa
            // 
            this.btn_masa.Image = ((System.Drawing.Image)(resources.GetObject("btn_masa.Image")));
            this.btn_masa.Location = new System.Drawing.Point(38, 81);
            this.btn_masa.Name = "btn_masa";
            this.btn_masa.Size = new System.Drawing.Size(161, 60);
            this.btn_masa.TabIndex = 74;
            this.btn_masa.Text = "Masalar";
            this.btn_masa.Click += new System.EventHandler(this.btn_masa_Click);
            // 
            // btn_yazdir
            // 
            this.btn_yazdir.Image = global::StockProgram.Properties.Resources.print;
            this.btn_yazdir.Location = new System.Drawing.Point(430, 81);
            this.btn_yazdir.Name = "btn_yazdir";
            this.btn_yazdir.Size = new System.Drawing.Size(161, 60);
            this.btn_yazdir.TabIndex = 73;
            this.btn_yazdir.Text = "Yazıcılar";
            this.btn_yazdir.Click += new System.EventHandler(this.btn_yazdir_Click);
            // 
            // btn_staff
            // 
            this.btn_staff.Image = global::StockProgram.Properties.Resources.waiter;
            this.btn_staff.Location = new System.Drawing.Point(237, 174);
            this.btn_staff.Name = "btn_staff";
            this.btn_staff.Size = new System.Drawing.Size(161, 60);
            this.btn_staff.TabIndex = 56;
            this.btn_staff.Text = "Personeller";
            this.btn_staff.Click += new System.EventHandler(this.btn_staff_Click);
            // 
            // btn_options
            // 
            this.btn_options.Image = global::StockProgram.Properties.Resources.color;
            this.btn_options.Location = new System.Drawing.Point(38, 174);
            this.btn_options.Name = "btn_options";
            this.btn_options.Size = new System.Drawing.Size(161, 60);
            this.btn_options.TabIndex = 55;
            this.btn_options.Text = "Ürün Seçenekleri";
            this.btn_options.Click += new System.EventHandler(this.btn_renk_Click);
            // 
            // btn_back_up
            // 
            this.btn_back_up.Image = global::StockProgram.Properties.Resources.backup;
            this.btn_back_up.Location = new System.Drawing.Point(430, 174);
            this.btn_back_up.Name = "btn_back_up";
            this.btn_back_up.Size = new System.Drawing.Size(161, 60);
            this.btn_back_up.TabIndex = 54;
            this.btn_back_up.Text = "Yedekle / Geri Yükle";
            this.btn_back_up.Click += new System.EventHandler(this.btn_back_up_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.simpleButton1.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.simpleButton1.Location = new System.Drawing.Point(567, 2);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(64, 35);
            this.simpleButton1.TabIndex = 43;
            this.simpleButton1.Text = "Geri";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // ucSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_settings);
            this.Controls.Add(this.btn_yerlesim);
            this.Controls.Add(this.btn_tablet);
            this.Controls.Add(this.btn_masa);
            this.Controls.Add(this.btn_yazdir);
            this.Controls.Add(this.btn_staff);
            this.Controls.Add(this.btn_options);
            this.Controls.Add(this.btn_back_up);
            this.Controls.Add(this.panel1);
            this.Name = "ucSettings";
            this.Size = new System.Drawing.Size(633, 364);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.LabelControl lbl_product_name;
        private DevExpress.XtraEditors.SimpleButton btn_back_up;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btn_options;
        private DevExpress.XtraEditors.SimpleButton btn_staff;
        private DevExpress.XtraEditors.SimpleButton btn_yazdir;
        private DevExpress.XtraEditors.SimpleButton btn_masa;
        private DevExpress.XtraEditors.SimpleButton btn_tablet;
        private DevExpress.XtraEditors.SimpleButton btn_yerlesim;
        private DevExpress.XtraEditors.SimpleButton btn_settings;
    }
}
