namespace StockProgram.Categories
{
    partial class ucCategoriesMasterPage
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.spliter = new DevExpress.XtraEditors.SplitContainerControl();
            this.tree_category = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.pnl_urun_category = new DevExpress.XtraEditors.PanelControl();
            this.btn_kategori_gir = new DevExpress.XtraEditors.SimpleButton();
            this.btn_kategori_duzelt = new DevExpress.XtraEditors.SimpleButton();
            this.btn_kategori_sil = new DevExpress.XtraEditors.SimpleButton();
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.tab_page_urun = new DevExpress.XtraTab.XtraTabPage();
            this.tab_page_gider = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.splitGider = new DevExpress.XtraEditors.SplitContainerControl();
            this.tree_gider = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.pnl_gider_category = new DevExpress.XtraEditors.PanelControl();
            this.btn_gider = new DevExpress.XtraEditors.SimpleButton();
            this.editCategory = new DevExpress.XtraEditors.SimpleButton();
            this.btn_gider_sil = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spliter)).BeginInit();
            this.spliter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tree_category)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_urun_category)).BeginInit();
            this.pnl_urun_category.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tab_page_urun.SuspendLayout();
            this.tab_page_gider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGider)).BeginInit();
            this.splitGider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tree_gider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_gider_category)).BeginInit();
            this.pnl_gider_category.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.spliter);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(767, 375);
            this.panelControl1.TabIndex = 52;
            // 
            // spliter
            // 
            this.spliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spliter.Location = new System.Drawing.Point(2, 2);
            this.spliter.Name = "spliter";
            this.spliter.Panel1.Controls.Add(this.tree_category);
            this.spliter.Panel1.Text = "Panel1";
            this.spliter.Panel2.Controls.Add(this.pnl_urun_category);
            this.spliter.Panel2.Text = "Panel2";
            this.spliter.Size = new System.Drawing.Size(763, 371);
            this.spliter.SplitterPosition = 296;
            this.spliter.TabIndex = 19;
            this.spliter.Text = "splitContainerControl1";
            // 
            // tree_category
            // 
            this.tree_category.Appearance.FocusedCell.BackColor = System.Drawing.Color.Blue;
            this.tree_category.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.tree_category.Appearance.FocusedCell.Options.UseBackColor = true;
            this.tree_category.Appearance.FocusedCell.Options.UseForeColor = true;
            this.tree_category.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.tree_category.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree_category.KeyFieldName = "cat_id";
            this.tree_category.Location = new System.Drawing.Point(0, 0);
            this.tree_category.Name = "tree_category";
            this.tree_category.ParentFieldName = "parent_id";
            this.tree_category.Size = new System.Drawing.Size(296, 371);
            this.tree_category.TabIndex = 1;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Ürünler";
            this.treeListColumn1.FieldName = "cat_name";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.AllowEdit = false;
            this.treeListColumn1.OptionsColumn.ReadOnly = true;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // pnl_urun_category
            // 
            this.pnl_urun_category.Controls.Add(this.btn_kategori_gir);
            this.pnl_urun_category.Controls.Add(this.btn_kategori_duzelt);
            this.pnl_urun_category.Controls.Add(this.btn_kategori_sil);
            this.pnl_urun_category.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_urun_category.Location = new System.Drawing.Point(0, 0);
            this.pnl_urun_category.Name = "pnl_urun_category";
            this.pnl_urun_category.Size = new System.Drawing.Size(462, 74);
            this.pnl_urun_category.TabIndex = 17;
            // 
            // btn_kategori_gir
            // 
            this.btn_kategori_gir.Image = global::StockProgram.Properties.Resources.add_blue;
            this.btn_kategori_gir.Location = new System.Drawing.Point(16, 15);
            this.btn_kategori_gir.Name = "btn_kategori_gir";
            this.btn_kategori_gir.Size = new System.Drawing.Size(118, 44);
            this.btn_kategori_gir.TabIndex = 14;
            this.btn_kategori_gir.Text = "Kategori Ekle";
            this.btn_kategori_gir.Click += new System.EventHandler(this.btn_kategori_gir_Click_1);
            // 
            // btn_kategori_duzelt
            // 
            this.btn_kategori_duzelt.Image = global::StockProgram.Properties.Resources.edit;
            this.btn_kategori_duzelt.Location = new System.Drawing.Point(169, 15);
            this.btn_kategori_duzelt.Name = "btn_kategori_duzelt";
            this.btn_kategori_duzelt.Size = new System.Drawing.Size(118, 44);
            this.btn_kategori_duzelt.TabIndex = 16;
            this.btn_kategori_duzelt.Text = "Kategori Düzelt";
            this.btn_kategori_duzelt.Click += new System.EventHandler(this.btn_kategori_duzelt_Click);
            // 
            // btn_kategori_sil
            // 
            this.btn_kategori_sil.Image = global::StockProgram.Properties.Resources.remove;
            this.btn_kategori_sil.Location = new System.Drawing.Point(325, 15);
            this.btn_kategori_sil.Name = "btn_kategori_sil";
            this.btn_kategori_sil.Size = new System.Drawing.Size(118, 44);
            this.btn_kategori_sil.TabIndex = 15;
            this.btn_kategori_sil.Text = "Kategori Sil";
            this.btn_kategori_sil.Click += new System.EventHandler(this.btn_kategori_sil_Click);
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.tab_page_urun;
            this.tabControl.Size = new System.Drawing.Size(773, 411);
            this.tabControl.TabIndex = 53;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tab_page_urun,
            this.tab_page_gider});
            // 
            // tab_page_urun
            // 
            this.tab_page_urun.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.tab_page_urun.Appearance.Header.Options.UseFont = true;
            this.tab_page_urun.Controls.Add(this.panelControl1);
            this.tab_page_urun.Name = "tab_page_urun";
            this.tab_page_urun.Size = new System.Drawing.Size(767, 375);
            this.tab_page_urun.Text = "Ürün Kategorileri";
            // 
            // tab_page_gider
            // 
            this.tab_page_gider.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.tab_page_gider.Appearance.Header.Options.UseFont = true;
            this.tab_page_gider.Controls.Add(this.panelControl2);
            this.tab_page_gider.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.tab_page_gider.Name = "tab_page_gider";
            this.tab_page_gider.Size = new System.Drawing.Size(767, 375);
            this.tab_page_gider.Text = "Gider Kategorileri";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.splitGider);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(767, 375);
            this.panelControl2.TabIndex = 53;
            // 
            // splitGider
            // 
            this.splitGider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGider.Location = new System.Drawing.Point(2, 2);
            this.splitGider.Name = "splitGider";
            this.splitGider.Panel1.Controls.Add(this.tree_gider);
            this.splitGider.Panel1.Text = "Panel1";
            this.splitGider.Panel2.Controls.Add(this.pnl_gider_category);
            this.splitGider.Panel2.Text = "Panel2";
            this.splitGider.Size = new System.Drawing.Size(763, 371);
            this.splitGider.SplitterPosition = 307;
            this.splitGider.TabIndex = 19;
            this.splitGider.Text = "splitContainerControl1";
            // 
            // tree_gider
            // 
            this.tree_gider.Appearance.FocusedCell.BackColor = System.Drawing.Color.Blue;
            this.tree_gider.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.tree_gider.Appearance.FocusedCell.Options.UseBackColor = true;
            this.tree_gider.Appearance.FocusedCell.Options.UseForeColor = true;
            this.tree_gider.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2});
            this.tree_gider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree_gider.KeyFieldName = "process_id";
            this.tree_gider.Location = new System.Drawing.Point(0, 0);
            this.tree_gider.Name = "tree_gider";
            this.tree_gider.ParentFieldName = "parent_id";
            this.tree_gider.Size = new System.Drawing.Size(307, 371);
            this.tree_gider.TabIndex = 1;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Giderler";
            this.treeListColumn2.FieldName = "process_name";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.OptionsColumn.AllowEdit = false;
            this.treeListColumn2.OptionsColumn.ReadOnly = true;
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            // 
            // pnl_gider_category
            // 
            this.pnl_gider_category.Controls.Add(this.btn_gider);
            this.pnl_gider_category.Controls.Add(this.editCategory);
            this.pnl_gider_category.Controls.Add(this.btn_gider_sil);
            this.pnl_gider_category.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_gider_category.Location = new System.Drawing.Point(0, 0);
            this.pnl_gider_category.Name = "pnl_gider_category";
            this.pnl_gider_category.Size = new System.Drawing.Size(451, 74);
            this.pnl_gider_category.TabIndex = 17;
            // 
            // btn_gider
            // 
            this.btn_gider.Image = global::StockProgram.Properties.Resources.add_blue;
            this.btn_gider.Location = new System.Drawing.Point(16, 15);
            this.btn_gider.Name = "btn_gider";
            this.btn_gider.Size = new System.Drawing.Size(118, 44);
            this.btn_gider.TabIndex = 14;
            this.btn_gider.Text = "Gider Ekle";
            this.btn_gider.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // editCategory
            // 
            this.editCategory.Image = global::StockProgram.Properties.Resources.edit;
            this.editCategory.Location = new System.Drawing.Point(169, 15);
            this.editCategory.Name = "editCategory";
            this.editCategory.Size = new System.Drawing.Size(118, 44);
            this.editCategory.TabIndex = 16;
            this.editCategory.Text = "Kategori Düzelt";
            this.editCategory.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // btn_gider_sil
            // 
            this.btn_gider_sil.Image = global::StockProgram.Properties.Resources.remove;
            this.btn_gider_sil.Location = new System.Drawing.Point(325, 15);
            this.btn_gider_sil.Name = "btn_gider_sil";
            this.btn_gider_sil.Size = new System.Drawing.Size(118, 44);
            this.btn_gider_sil.TabIndex = 15;
            this.btn_gider_sil.Text = "Kategori Sil";
            this.btn_gider_sil.Click += new System.EventHandler(this.btn_gider_sil_Click);
            // 
            // ucCategoriesMasterPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "ucCategoriesMasterPage";
            this.Size = new System.Drawing.Size(773, 411);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spliter)).EndInit();
            this.spliter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tree_category)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_urun_category)).EndInit();
            this.pnl_urun_category.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tab_page_urun.ResumeLayout(false);
            this.tab_page_gider.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGider)).EndInit();
            this.splitGider.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tree_gider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_gider_category)).EndInit();
            this.pnl_gider_category.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SplitContainerControl spliter;
        private DevExpress.XtraEditors.SimpleButton btn_kategori_gir;
        private DevExpress.XtraEditors.SimpleButton btn_kategori_sil;
        private DevExpress.XtraEditors.SimpleButton btn_kategori_duzelt;
        private DevExpress.XtraTreeList.TreeList tree_category;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.PanelControl pnl_urun_category;
        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage tab_page_urun;
        private DevExpress.XtraTab.XtraTabPage tab_page_gider;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SplitContainerControl splitGider;
        private DevExpress.XtraTreeList.TreeList tree_gider;
        private DevExpress.XtraEditors.PanelControl pnl_gider_category;
        private DevExpress.XtraEditors.SimpleButton btn_gider;
        private DevExpress.XtraEditors.SimpleButton editCategory;
        private DevExpress.XtraEditors.SimpleButton btn_gider_sil;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
    }
}
