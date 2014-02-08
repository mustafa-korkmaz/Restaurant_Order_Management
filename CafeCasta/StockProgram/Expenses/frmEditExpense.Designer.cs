namespace StockProgram.Expenses
{
    partial class frmEditExpense
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btn_tamamla = new DevExpress.XtraEditors.SimpleButton();
            this.pnl_grid = new DevExpress.XtraEditors.PanelControl();
            this.txt_kategori = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.spin_fiyat = new DevExpress.XtraEditors.SpinEdit();
            this.tree_category = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.txt_tanim = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_grid)).BeginInit();
            this.pnl_grid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_kategori.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_fiyat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tree_category)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_tanim.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btn_tamamla);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 343);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(543, 44);
            this.panelControl2.TabIndex = 52;
            // 
            // btn_tamamla
            // 
            this.btn_tamamla.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_tamamla.Image = global::StockProgram.Properties.Resources.ok_blue;
            this.btn_tamamla.Location = new System.Drawing.Point(412, 2);
            this.btn_tamamla.Name = "btn_tamamla";
            this.btn_tamamla.Size = new System.Drawing.Size(129, 40);
            this.btn_tamamla.TabIndex = 10;
            this.btn_tamamla.Text = "Düzenlemeyi Bitir";
            this.btn_tamamla.Click += new System.EventHandler(this.btn_tamamla_Click);
            // 
            // pnl_grid
            // 
            this.pnl_grid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_grid.Controls.Add(this.txt_kategori);
            this.pnl_grid.Controls.Add(this.labelControl1);
            this.pnl_grid.Controls.Add(this.labelControl9);
            this.pnl_grid.Controls.Add(this.spin_fiyat);
            this.pnl_grid.Controls.Add(this.tree_category);
            this.pnl_grid.Controls.Add(this.txt_tanim);
            this.pnl_grid.Controls.Add(this.labelControl4);
            this.pnl_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_grid.Location = new System.Drawing.Point(0, 0);
            this.pnl_grid.Name = "pnl_grid";
            this.pnl_grid.Size = new System.Drawing.Size(543, 343);
            this.pnl_grid.TabIndex = 53;
            // 
            // txt_kategori
            // 
            this.txt_kategori.Location = new System.Drawing.Point(278, 161);
            this.txt_kategori.Name = "txt_kategori";
            this.txt_kategori.Properties.ReadOnly = true;
            this.txt_kategori.Size = new System.Drawing.Size(253, 20);
            this.txt_kategori.TabIndex = 87;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(198, 164);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 13);
            this.labelControl1.TabIndex = 92;
            this.labelControl1.Text = "Masraf Kategori";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(198, 214);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(64, 13);
            this.labelControl9.TabIndex = 91;
            this.labelControl9.Text = "Masraf Tutarı";
            // 
            // spin_fiyat
            // 
            this.spin_fiyat.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_fiyat.Location = new System.Drawing.Point(278, 211);
            this.spin_fiyat.Name = "spin_fiyat";
            this.spin_fiyat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spin_fiyat.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spin_fiyat.Properties.Mask.BeepOnError = true;
            this.spin_fiyat.Properties.Mask.EditMask = "n";
            this.spin_fiyat.Size = new System.Drawing.Size(80, 20);
            this.spin_fiyat.TabIndex = 88;
            this.spin_fiyat.Click += new System.EventHandler(this.spin_fiyat_Click);
            // 
            // tree_category
            // 
            this.tree_category.Appearance.FocusedCell.BackColor = System.Drawing.Color.Blue;
            this.tree_category.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.tree_category.Appearance.FocusedCell.Options.UseBackColor = true;
            this.tree_category.Appearance.FocusedCell.Options.UseForeColor = true;
            this.tree_category.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.tree_category.Dock = System.Windows.Forms.DockStyle.Left;
            this.tree_category.KeyFieldName = "cat_id";
            this.tree_category.Location = new System.Drawing.Point(0, 0);
            this.tree_category.Name = "tree_category";
            this.tree_category.ParentFieldName = "parent_id";
            this.tree_category.Size = new System.Drawing.Size(183, 343);
            this.tree_category.TabIndex = 90;
            this.tree_category.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.tree_category_FocusedNodeChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Kategori Seçiniz";
            this.treeListColumn1.FieldName = "cat_name";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.AllowEdit = false;
            this.treeListColumn1.OptionsColumn.ReadOnly = true;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // txt_tanim
            // 
            this.txt_tanim.Location = new System.Drawing.Point(278, 111);
            this.txt_tanim.Name = "txt_tanim";
            this.txt_tanim.Size = new System.Drawing.Size(253, 20);
            this.txt_tanim.TabIndex = 86;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(198, 114);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(41, 13);
            this.labelControl4.TabIndex = 89;
            this.labelControl4.Text = "Açıklama";
            // 
            // frmEditExpense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 387);
            this.Controls.Add(this.pnl_grid);
            this.Controls.Add(this.panelControl2);
            this.Name = "frmEditExpense";
            this.Text = "Ürün İade Detayları";
            this.Load += new System.EventHandler(this.frmEditExpense_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_grid)).EndInit();
            this.pnl_grid.ResumeLayout(false);
            this.pnl_grid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_kategori.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_fiyat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tree_category)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_tanim.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btn_tamamla;
        private DevExpress.XtraEditors.PanelControl pnl_grid;
        private DevExpress.XtraEditors.TextEdit txt_kategori;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.SpinEdit spin_fiyat;
        private DevExpress.XtraTreeList.TreeList tree_category;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.TextEdit txt_tanim;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}