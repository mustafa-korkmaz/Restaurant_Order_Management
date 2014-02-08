namespace StockProgram.Sales
{
    partial class frmDesc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDesc));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btn_tamamla = new DevExpress.XtraEditors.SimpleButton();
            this.btn_duzenle = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cb_desc = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txt_desc = new System.Windows.Forms.RichTextBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cb_desc.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btn_tamamla);
            this.panelControl2.Controls.Add(this.btn_duzenle);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 196);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(311, 44);
            this.panelControl2.TabIndex = 1;
            // 
            // btn_tamamla
            // 
            this.btn_tamamla.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_tamamla.Image = global::StockProgram.Properties.Resources.ok_blue;
            this.btn_tamamla.Location = new System.Drawing.Point(138, 2);
            this.btn_tamamla.Name = "btn_tamamla";
            this.btn_tamamla.Size = new System.Drawing.Size(87, 40);
            this.btn_tamamla.TabIndex = 3;
            this.btn_tamamla.Text = "Tamamla";
            this.btn_tamamla.Click += new System.EventHandler(this.btn_tamamla_Click);
            // 
            // btn_duzenle
            // 
            this.btn_duzenle.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_duzenle.Image = global::StockProgram.Properties.Resources.delete;
            this.btn_duzenle.Location = new System.Drawing.Point(225, 2);
            this.btn_duzenle.Name = "btn_duzenle";
            this.btn_duzenle.Size = new System.Drawing.Size(84, 40);
            this.btn_duzenle.TabIndex = 4;
            this.btn_duzenle.Text = "Kapat";
            this.btn_duzenle.Click += new System.EventHandler(this.btn_duzenle_Click_1);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.cb_desc);
            this.panel1.Controls.Add(this.txt_desc);
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(311, 196);
            this.panel1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.labelControl1.Location = new System.Drawing.Point(12, 116);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(89, 22);
            this.labelControl1.TabIndex = 12;
            this.labelControl1.Text = "Ek Seçenek";
            // 
            // cb_desc
            // 
            this.cb_desc.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cb_desc.Location = new System.Drawing.Point(12, 46);
            this.cb_desc.Name = "cb_desc";
            this.cb_desc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_desc.Size = new System.Drawing.Size(287, 20);
            this.cb_desc.TabIndex = 0;
            this.cb_desc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cb_desc_KeyPress);
            // 
            // txt_desc
            // 
            this.txt_desc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txt_desc.Location = new System.Drawing.Point(2, 144);
            this.txt_desc.Name = "txt_desc";
            this.txt_desc.Size = new System.Drawing.Size(307, 50);
            this.txt_desc.TabIndex = 1;
            this.txt_desc.Text = "";
            this.txt_desc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_desc_KeyPress);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.labelControl2.Location = new System.Drawing.Point(12, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(83, 22);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Seçenekler";
            // 
            // frmDesc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 240);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelControl2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDesc";
            this.Text = "Seçenek Ekle";
            this.Load += new System.EventHandler(this.frmSatis_Load);
            this.Shown += new System.EventHandler(this.frmMiktar_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cb_desc.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btn_tamamla;
        private DevExpress.XtraEditors.SimpleButton btn_duzenle;
        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.RichTextBox txt_desc;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cb_desc;

    }
}