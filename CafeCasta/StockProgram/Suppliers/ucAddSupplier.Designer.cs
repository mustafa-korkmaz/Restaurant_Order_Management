﻿namespace StockProgram.Suppliers
{
    partial class ucAddSupplier
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
            this.txt_isim = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txt_tanim = new DevExpress.XtraEditors.TextEdit();
            this.txt_tel = new DevExpress.XtraEditors.TextEdit();
            this.txt_adres = new DevExpress.XtraEditors.TextEdit();
            this.txt_mail = new DevExpress.XtraEditors.TextEdit();
            this.btn_tedarikciEkle = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_isim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_tanim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_tel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_adres.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_mail.Properties)).BeginInit();
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
            this.panel1.Size = new System.Drawing.Size(849, 39);
            this.panel1.TabIndex = 54;
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(770, 2);
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
            this.lbl_table_number.Size = new System.Drawing.Size(81, 22);
            this.lbl_table_number.TabIndex = 2;
            this.lbl_table_number.Text = "Firma Ekle";
            // 
            // txt_isim
            // 
            this.txt_isim.Location = new System.Drawing.Point(109, 116);
            this.txt_isim.Name = "txt_isim";
            this.txt_isim.Size = new System.Drawing.Size(180, 20);
            this.txt_isim.TabIndex = 55;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(33, 119);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 13);
            this.labelControl1.TabIndex = 56;
            this.labelControl1.Text = "* İsim";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(341, 178);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(28, 13);
            this.labelControl2.TabIndex = 57;
            this.labelControl2.Text = "Adres";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(32, 178);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 13);
            this.labelControl3.TabIndex = 58;
            this.labelControl3.Text = "Telefon";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(341, 119);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(28, 13);
            this.labelControl4.TabIndex = 59;
            this.labelControl4.Text = "Tanım";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(32, 235);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(18, 13);
            this.labelControl5.TabIndex = 60;
            this.labelControl5.Text = "Mail";
            // 
            // txt_tanim
            // 
            this.txt_tanim.Location = new System.Drawing.Point(418, 116);
            this.txt_tanim.Name = "txt_tanim";
            this.txt_tanim.Size = new System.Drawing.Size(253, 20);
            this.txt_tanim.TabIndex = 61;
            // 
            // txt_tel
            // 
            this.txt_tel.Location = new System.Drawing.Point(109, 175);
            this.txt_tel.Name = "txt_tel";
            this.txt_tel.Size = new System.Drawing.Size(180, 20);
            this.txt_tel.TabIndex = 62;
            // 
            // txt_adres
            // 
            this.txt_adres.Location = new System.Drawing.Point(418, 175);
            this.txt_adres.Name = "txt_adres";
            this.txt_adres.Size = new System.Drawing.Size(410, 20);
            this.txt_adres.TabIndex = 63;
            // 
            // txt_mail
            // 
            this.txt_mail.Location = new System.Drawing.Point(109, 228);
            this.txt_mail.Name = "txt_mail";
            this.txt_mail.Size = new System.Drawing.Size(180, 20);
            this.txt_mail.TabIndex = 64;
            // 
            // btn_tedarikciEkle
            // 
            this.btn_tedarikciEkle.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_tedarikciEkle.Image = global::StockProgram.Properties.Resources.add_blue;
            this.btn_tedarikciEkle.Location = new System.Drawing.Point(2, 2);
            this.btn_tedarikciEkle.Name = "btn_tedarikciEkle";
            this.btn_tedarikciEkle.Size = new System.Drawing.Size(115, 35);
            this.btn_tedarikciEkle.TabIndex = 65;
            this.btn_tedarikciEkle.Text = "Firma Ekle";
            this.btn_tedarikciEkle.Click += new System.EventHandler(this.btn_tedarikciEkle_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_tedarikciEkle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 283);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(849, 39);
            this.panelControl1.TabIndex = 66;
            // 
            // ucAddSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.txt_mail);
            this.Controls.Add(this.txt_adres);
            this.Controls.Add(this.txt_tel);
            this.Controls.Add(this.txt_tanim);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txt_isim);
            this.Controls.Add(this.panel1);
            this.Name = "ucAddSupplier";
            this.Size = new System.Drawing.Size(849, 322);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_isim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_tanim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_tel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_adres.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_mail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.LabelControl lbl_table_number;
        private DevExpress.XtraEditors.TextEdit txt_isim;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txt_tanim;
        private DevExpress.XtraEditors.TextEdit txt_tel;
        private DevExpress.XtraEditors.TextEdit txt_adres;
        private DevExpress.XtraEditors.TextEdit txt_mail;
        private DevExpress.XtraEditors.SimpleButton btn_tedarikciEkle;
        private DevExpress.XtraEditors.SimpleButton btn_back;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}
