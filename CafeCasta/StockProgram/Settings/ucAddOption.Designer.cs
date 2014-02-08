namespace StockProgram.Settings
{
    partial class ucAddOption
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
            this.lbl_table_number = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cb_options = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btn_Ekle = new DevExpress.XtraEditors.SimpleButton();
            this.btn_back = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cb_options.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_back);
            this.panel1.Controls.Add(this.lbl_table_number);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(348, 39);
            this.panel1.TabIndex = 78;
            // 
            // lbl_table_number
            // 
            this.lbl_table_number.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_table_number.Location = new System.Drawing.Point(5, 8);
            this.lbl_table_number.Name = "lbl_table_number";
            this.lbl_table_number.Size = new System.Drawing.Size(102, 22);
            this.lbl_table_number.TabIndex = 2;
            this.lbl_table_number.Text = "Seçenek Ekle";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(55, 117);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(49, 13);
            this.labelControl1.TabIndex = 80;
            this.labelControl1.Text = "* Seçenek";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_Ekle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 209);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(348, 39);
            this.panelControl1.TabIndex = 83;
            // 
            // cb_options
            // 
            this.cb_options.Location = new System.Drawing.Point(110, 114);
            this.cb_options.Name = "cb_options";
            this.cb_options.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_options.Size = new System.Drawing.Size(185, 20);
            this.cb_options.TabIndex = 82;
            this.cb_options.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cb_options_KeyPress);
            // 
            // btn_Ekle
            // 
            this.btn_Ekle.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_Ekle.Image = global::StockProgram.Properties.Resources.add_blue;
            this.btn_Ekle.Location = new System.Drawing.Point(2, 2);
            this.btn_Ekle.Name = "btn_Ekle";
            this.btn_Ekle.Size = new System.Drawing.Size(115, 35);
            this.btn_Ekle.TabIndex = 67;
            this.btn_Ekle.Text = "Ekle";
            this.btn_Ekle.Click += new System.EventHandler(this.btn_renkEkle_Click);
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(269, 2);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(77, 35);
            this.btn_back.TabIndex = 42;
            this.btn_back.Text = "Geri";
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // ucAddOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cb_options);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.panel1);
            this.Name = "ucAddOption";
            this.Size = new System.Drawing.Size(348, 248);
            this.Load += new System.EventHandler(this.ucAddOption_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cb_options.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btn_back;
        private DevExpress.XtraEditors.LabelControl lbl_table_number;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_Ekle;
        private DevExpress.XtraEditors.ComboBoxEdit cb_options;
    }
}
