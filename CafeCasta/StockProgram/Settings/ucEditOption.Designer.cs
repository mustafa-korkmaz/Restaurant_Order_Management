namespace StockProgram.Settings
{
    partial class ucEditOption
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_isim = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(496, 39);
            this.panel1.TabIndex = 79;
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(417, 2);
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
            this.lbl_table_number.Size = new System.Drawing.Size(107, 22);
            this.lbl_table_number.TabIndex = 2;
            this.lbl_table_number.Text = "Renk Düzenle";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_renkEkle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 300);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(496, 39);
            this.panelControl1.TabIndex = 82;
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
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(60, 158);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(33, 13);
            this.labelControl1.TabIndex = 84;
            this.labelControl1.Text = "* Renk";
            // 
            // txt_isim
            // 
            this.txt_isim.Location = new System.Drawing.Point(108, 155);
            this.txt_isim.Name = "txt_isim";
            this.txt_isim.Size = new System.Drawing.Size(180, 20);
            this.txt_isim.TabIndex = 83;
            // 
            // ucEditColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txt_isim);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel1);
            this.Name = "ucEditColor";
            this.Size = new System.Drawing.Size(496, 339);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
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
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txt_isim;
    }
}
