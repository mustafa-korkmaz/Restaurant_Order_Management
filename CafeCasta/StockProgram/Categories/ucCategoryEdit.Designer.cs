namespace StockProgram.Categories
{
    partial class ucCategoryEdit
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
            this.btn_back = new DevExpress.XtraEditors.SimpleButton();
            this.cb_bagli_kategori = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btn_edit = new DevExpress.XtraEditors.SimpleButton();
            this.txt_kategoriAdi = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txt_bagli_kategori = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txt_display_order = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cb_bagli_kategori.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_kategoriAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_bagli_kategori.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_display_order.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_table_number);
            this.panel1.Controls.Add(this.btn_back);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 39);
            this.panel1.TabIndex = 58;
            // 
            // lbl_table_number
            // 
            this.lbl_table_number.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_table_number.Location = new System.Drawing.Point(5, 8);
            this.lbl_table_number.Name = "lbl_table_number";
            this.lbl_table_number.Size = new System.Drawing.Size(133, 22);
            this.lbl_table_number.TabIndex = 2;
            this.lbl_table_number.Text = "Kategori Düzenle";
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(545, 2);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(77, 35);
            this.btn_back.TabIndex = 41;
            this.btn_back.Text = "Geri";
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // cb_bagli_kategori
            // 
            this.cb_bagli_kategori.Location = new System.Drawing.Point(55, 202);
            this.cb_bagli_kategori.Name = "cb_bagli_kategori";
            this.cb_bagli_kategori.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_bagli_kategori.Size = new System.Drawing.Size(171, 20);
            this.cb_bagli_kategori.TabIndex = 57;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(280, 120);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(168, 13);
            this.labelControl2.TabIndex = 56;
            this.labelControl2.Text = "Bağlı Olduğu Üst Kategori Adı (Eski)";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(55, 120);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 13);
            this.labelControl1.TabIndex = 54;
            this.labelControl1.Text = "Kategori Adı";
            // 
            // btn_edit
            // 
            this.btn_edit.Image = global::StockProgram.Properties.Resources.edit;
            this.btn_edit.Location = new System.Drawing.Point(339, 245);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(109, 43);
            this.btn_edit.TabIndex = 53;
            this.btn_edit.Text = "Düzenle";
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // txt_kategoriAdi
            // 
            this.txt_kategoriAdi.Location = new System.Drawing.Point(55, 139);
            this.txt_kategoriAdi.Name = "txt_kategoriAdi";
            this.txt_kategoriAdi.Size = new System.Drawing.Size(171, 20);
            this.txt_kategoriAdi.TabIndex = 56;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(55, 183);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(139, 13);
            this.labelControl3.TabIndex = 61;
            this.labelControl3.Text = "Bağlı Olduğu Üst Kategori Adı";
            // 
            // txt_bagli_kategori
            // 
            this.txt_bagli_kategori.Location = new System.Drawing.Point(280, 139);
            this.txt_bagli_kategori.Name = "txt_bagli_kategori";
            this.txt_bagli_kategori.Properties.ReadOnly = true;
            this.txt_bagli_kategori.Size = new System.Drawing.Size(171, 20);
            this.txt_bagli_kategori.TabIndex = 62;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(280, 183);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(70, 13);
            this.labelControl4.TabIndex = 64;
            this.labelControl4.Text = "Gösterim Sırası";
            // 
            // txt_display_order
            // 
            this.txt_display_order.EditValue = "0";
            this.txt_display_order.Location = new System.Drawing.Point(280, 202);
            this.txt_display_order.Name = "txt_display_order";
            this.txt_display_order.Properties.Mask.EditMask = "n0";
            this.txt_display_order.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txt_display_order.Size = new System.Drawing.Size(29, 20);
            this.txt_display_order.TabIndex = 58;
            // 
            // ucCategoryEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txt_display_order);
            this.Controls.Add(this.txt_bagli_kategori);
            this.Controls.Add(this.txt_kategoriAdi);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cb_bagli_kategori);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btn_edit);
            this.Name = "ucCategoryEdit";
            this.Size = new System.Drawing.Size(624, 407);
            this.Load += new System.EventHandler(this.ucCategoryEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cb_bagli_kategori.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_kategoriAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_bagli_kategori.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_display_order.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.LabelControl lbl_table_number;
        private DevExpress.XtraEditors.SimpleButton btn_back;
        private DevExpress.XtraEditors.ComboBoxEdit cb_bagli_kategori;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_edit;
        private DevExpress.XtraEditors.TextEdit txt_kategoriAdi;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txt_bagli_kategori;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txt_display_order;
    }
}
