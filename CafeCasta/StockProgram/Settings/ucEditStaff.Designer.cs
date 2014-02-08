namespace StockProgram.Settings
{
    partial class ucEditStaff
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_renkEkle = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txt_username = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txt_password = new DevExpress.XtraEditors.TextEdit();
            this.cb_role = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_isim = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_username.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_password.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_role.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_isim.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_back);
            this.panel1.Controls.Add(this.lbl_table_number);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(569, 39);
            this.panel1.TabIndex = 79;
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(490, 2);
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
            this.lbl_table_number.Size = new System.Drawing.Size(132, 22);
            this.lbl_table_number.TabIndex = 2;
            this.lbl_table_number.Text = "Kullanıcı Düzenle";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_renkEkle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 300);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(569, 39);
            this.panelControl1.TabIndex = 102;
            // 
            // btn_renkEkle
            // 
            this.btn_renkEkle.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_renkEkle.Image = global::StockProgram.Properties.Resources.add_blue;
            this.btn_renkEkle.Location = new System.Drawing.Point(2, 2);
            this.btn_renkEkle.Name = "btn_renkEkle";
            this.btn_renkEkle.Size = new System.Drawing.Size(115, 35);
            this.btn_renkEkle.TabIndex = 67;
            this.btn_renkEkle.Text = "Düzenle";
            this.btn_renkEkle.Click += new System.EventHandler(this.btn_renkEkle_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(39, 97);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(64, 13);
            this.labelControl4.TabIndex = 96;
            this.labelControl4.Text = "* Kullanıcı Adı";
            // 
            // txt_username
            // 
            this.txt_username.Location = new System.Drawing.Point(109, 94);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(180, 20);
            this.txt_username.TabIndex = 89;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(319, 97);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(31, 13);
            this.labelControl3.TabIndex = 95;
            this.labelControl3.Text = "* Şifre";
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(356, 94);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(180, 20);
            this.txt_password.TabIndex = 90;
            // 
            // cb_role
            // 
            this.cb_role.Location = new System.Drawing.Point(356, 190);
            this.cb_role.Name = "cb_role";
            this.cb_role.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_role.Properties.Items.AddRange(new object[] {
            "ADMIN",
            "KASA",
            "GARSON"});
            this.cb_role.Size = new System.Drawing.Size(180, 20);
            this.cb_role.TabIndex = 92;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(326, 193);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 13);
            this.labelControl2.TabIndex = 94;
            this.labelControl2.Text = "* Rol";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(75, 193);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 13);
            this.labelControl1.TabIndex = 93;
            this.labelControl1.Text = "* İsim";
            // 
            // txt_isim
            // 
            this.txt_isim.Location = new System.Drawing.Point(109, 190);
            this.txt_isim.Name = "txt_isim";
            this.txt_isim.Size = new System.Drawing.Size(180, 20);
            this.txt_isim.TabIndex = 91;
            // 
            // ucEditStaff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txt_username);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.cb_role);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txt_isim);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel1);
            this.Name = "ucEditStaff";
            this.Size = new System.Drawing.Size(569, 339);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txt_username.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_password.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_role.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_isim.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btn_back;
        private DevExpress.XtraEditors.LabelControl lbl_table_number;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_renkEkle;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txt_username;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txt_password;
        private DevExpress.XtraEditors.ComboBoxEdit cb_role;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txt_isim;
    }
}
