namespace StockProgram.Customers
{
    partial class ucEditCustomer
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.txt_adress = new System.Windows.Forms.RichTextBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txt_tel = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txt_name = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_mail = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_addCustomer = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_back = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_table_number = new DevExpress.XtraEditors.LabelControl();
            this.txt_note = new System.Windows.Forms.RichTextBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_tel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_mail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.txt_note);
            this.panelControl2.Controls.Add(this.labelControl5);
            this.panelControl2.Controls.Add(this.txt_adress);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.txt_tel);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.txt_name);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.txt_mail);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 39);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(794, 237);
            this.panelControl2.TabIndex = 80;
            // 
            // txt_adress
            // 
            this.txt_adress.Location = new System.Drawing.Point(151, 97);
            this.txt_adress.Name = "txt_adress";
            this.txt_adress.Size = new System.Drawing.Size(253, 63);
            this.txt_adress.TabIndex = 2;
            this.txt_adress.Text = "";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(83, 97);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(32, 13);
            this.labelControl3.TabIndex = 83;
            this.labelControl3.Text = "Adres:";
            // 
            // txt_tel
            // 
            this.txt_tel.Location = new System.Drawing.Point(502, 40);
            this.txt_tel.Name = "txt_tel";
            this.txt_tel.Properties.Mask.EditMask = "f0";
            this.txt_tel.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txt_tel.Size = new System.Drawing.Size(253, 20);
            this.txt_tel.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(434, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 13);
            this.labelControl2.TabIndex = 82;
            this.labelControl2.Text = "Telefon:";
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(151, 40);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(253, 20);
            this.txt_name.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(83, 41);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(32, 13);
            this.labelControl1.TabIndex = 80;
            this.labelControl1.Text = "İsim: *";
            // 
            // txt_mail
            // 
            this.txt_mail.Location = new System.Drawing.Point(151, 192);
            this.txt_mail.Name = "txt_mail";
            this.txt_mail.Size = new System.Drawing.Size(253, 20);
            this.txt_mail.TabIndex = 4;
            this.txt_mail.EditValueChanged += new System.EventHandler(this.txt_mail_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(83, 195);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(22, 13);
            this.labelControl4.TabIndex = 78;
            this.labelControl4.Text = "Mail:";
            this.labelControl4.Click += new System.EventHandler(this.labelControl4_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_addCustomer);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 276);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(794, 39);
            this.panelControl1.TabIndex = 81;
            // 
            // btn_addCustomer
            // 
            this.btn_addCustomer.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_addCustomer.Image = global::StockProgram.Properties.Resources.edit;
            this.btn_addCustomer.Location = new System.Drawing.Point(2, 2);
            this.btn_addCustomer.Name = "btn_addCustomer";
            this.btn_addCustomer.Size = new System.Drawing.Size(115, 35);
            this.btn_addCustomer.TabIndex = 4;
            this.btn_addCustomer.Text = "Düzenle";
            this.btn_addCustomer.Click += new System.EventHandler(this.btn_addCustomer_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_back);
            this.panel1.Controls.Add(this.lbl_table_number);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(794, 39);
            this.panel1.TabIndex = 78;
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(715, 2);
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
            this.lbl_table_number.Size = new System.Drawing.Size(125, 22);
            this.lbl_table_number.TabIndex = 2;
            this.lbl_table_number.Text = "Müşteri Düzenle";
            // 
            // txt_note
            // 
            this.txt_note.Location = new System.Drawing.Point(502, 97);
            this.txt_note.Name = "txt_note";
            this.txt_note.Size = new System.Drawing.Size(253, 63);
            this.txt_note.TabIndex = 3;
            this.txt_note.Text = "";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(431, 100);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(65, 13);
            this.labelControl5.TabIndex = 85;
            this.labelControl5.Text = "Müşteri Notu:";
            // 
            // ucEditCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel1);
            this.Name = "ucEditCustomer";
            this.Size = new System.Drawing.Size(794, 315);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_tel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_mail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.RichTextBox txt_adress;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txt_tel;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txt_name;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txt_mail;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_addCustomer;
        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btn_back;
        private DevExpress.XtraEditors.LabelControl lbl_table_number;
        private System.Windows.Forms.RichTextBox txt_note;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}
