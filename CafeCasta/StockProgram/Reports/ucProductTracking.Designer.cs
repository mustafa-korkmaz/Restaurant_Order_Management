namespace StockProgram.Reports
{
    partial class ucProductTracking
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucProductTracking));
            this.btn_urunIzle = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.tree_category = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.spliter = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repo_button = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.lbl_table_number = new DevExpress.XtraEditors.LabelControl();
            this.btn_back = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btn_yazdir = new DevExpress.XtraEditors.SimpleButton();
            this.printingSystem1 = new DevExpress.XtraPrinting.PrintingSystem(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tree_category)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spliter)).BeginInit();
            this.spliter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_urunIzle
            // 
            this.btn_urunIzle.Image = global::StockProgram.Properties.Resources.tracking;
            this.btn_urunIzle.Location = new System.Drawing.Point(304, 119);
            this.btn_urunIzle.Name = "btn_urunIzle";
            this.btn_urunIzle.Size = new System.Drawing.Size(124, 41);
            this.btn_urunIzle.TabIndex = 70;
            this.btn_urunIzle.Text = "Hepsini İzle";
            this.btn_urunIzle.Click += new System.EventHandler(this.btn_urunIzle_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.tree_category);
            this.panelControl1.Controls.Add(this.btn_urunIzle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(668, 167);
            this.panelControl1.TabIndex = 71;
            this.panelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl1_Paint);
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
            this.tree_category.Location = new System.Drawing.Point(2, 2);
            this.tree_category.Name = "tree_category";
            this.tree_category.ParentFieldName = "parent_id";
            this.tree_category.Size = new System.Drawing.Size(296, 163);
            this.tree_category.TabIndex = 97;
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
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.spliter);
            this.panelControl2.Controls.Add(this.panel1);
            this.panelControl2.Controls.Add(this.panelControl3);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(672, 412);
            this.panelControl2.TabIndex = 72;
            // 
            // spliter
            // 
            this.spliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spliter.Horizontal = false;
            this.spliter.Location = new System.Drawing.Point(2, 41);
            this.spliter.Name = "spliter";
            this.spliter.Panel1.Controls.Add(this.panelControl1);
            this.spliter.Panel1.Text = "Panel1";
            this.spliter.Panel2.Controls.Add(this.gridControl1);
            this.spliter.Panel2.Text = "Panel2";
            this.spliter.Size = new System.Drawing.Size(668, 332);
            this.spliter.SplitterPosition = 167;
            this.spliter.TabIndex = 73;
            this.spliter.Text = "splitContainerControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repo_button});
            this.gridControl1.Size = new System.Drawing.Size(668, 160);
            this.gridControl1.TabIndex = 71;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn2,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn10,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn9});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupPanelText = "Aramalarınızı kategorilere göre sınflandırmak için buraya istediğiniz kolonu sürü" +
    "kleyebilirsiniz.";
            this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "count", this.gridColumn4, "Toplam= {0}")});
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Ürün Kodu";
            this.gridColumn1.FieldName = "product_code_manual";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 143;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Ürün Adı";
            this.gridColumn3.FieldName = "product_name";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 128;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Toplam Miktar";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "product_net_buy_amount";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.SummaryItem.DisplayFormat = "Toplam={0}";
            this.gridColumn4.SummaryItem.FieldName = "count";
            this.gridColumn4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 7;
            this.gridColumn4.Width = 111;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Kategori";
            this.gridColumn2.FieldName = "cat_name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 5;
            this.gridColumn2.Width = 101;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Açıklama";
            this.gridColumn7.FieldName = "product_desc";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.ReadOnly = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            this.gridColumn7.Width = 135;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Ürün Resmi";
            this.gridColumn8.FieldName = "Image";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.ReadOnly = true;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            this.gridColumn8.Width = 121;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Renk";
            this.gridColumn10.FieldName = "color_name";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsColumn.ReadOnly = true;
            this.gridColumn10.Width = 74;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Üst Kategori";
            this.gridColumn5.FieldName = "top_cat_name";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 112;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Birim Alış Fiyatı";
            this.gridColumn6.DisplayFormat.FormatString = "c2";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn6.FieldName = "buy_price";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.ReadOnly = true;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 79;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn9.AppearanceCell.Options.UseFont = true;
            this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn9.AppearanceHeader.Options.UseFont = true;
            this.gridColumn9.Caption = "Toplam Maliyet";
            this.gridColumn9.DisplayFormat.FormatString = "c2";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn9.FieldName = "total_buy_price";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsColumn.ReadOnly = true;
            this.gridColumn9.SummaryItem.DisplayFormat = "Toplam= {0} TL";
            this.gridColumn9.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            this.gridColumn9.Width = 160;
            // 
            // repo_button
            // 
            this.repo_button.AutoHeight = false;
            this.repo_button.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repo_button.Name = "repo_button";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_table_number);
            this.panel1.Controls.Add(this.btn_back);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(668, 39);
            this.panel1.TabIndex = 72;
            // 
            // lbl_table_number
            // 
            this.lbl_table_number.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_table_number.Location = new System.Drawing.Point(5, 8);
            this.lbl_table_number.Name = "lbl_table_number";
            this.lbl_table_number.Size = new System.Drawing.Size(210, 22);
            this.lbl_table_number.TabIndex = 2;
            this.lbl_table_number.Text = "Kategori Bazlı Stok Raporu";
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(589, 2);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(77, 35);
            this.btn_back.TabIndex = 41;
            this.btn_back.Text = "Geri";
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btn_yazdir);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(2, 373);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(668, 37);
            this.panelControl3.TabIndex = 0;
            // 
            // btn_yazdir
            // 
            this.btn_yazdir.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_yazdir.Image = ((System.Drawing.Image)(resources.GetObject("btn_yazdir.Image")));
            this.btn_yazdir.Location = new System.Drawing.Point(2, 2);
            this.btn_yazdir.Name = "btn_yazdir";
            this.btn_yazdir.Size = new System.Drawing.Size(124, 33);
            this.btn_yazdir.TabIndex = 72;
            this.btn_yazdir.Text = "Yazdır";
            this.btn_yazdir.Click += new System.EventHandler(this.btn_yazdir_Click);
            // 
            // ucProductTracking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Name = "ucProductTracking";
            this.Size = new System.Drawing.Size(672, 412);
            this.Load += new System.EventHandler(this.ucProductTracking_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tree_category)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spliter)).EndInit();
            this.spliter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_urunIzle;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btn_yazdir;
        private DevExpress.XtraPrinting.PrintingSystem printingSystem1;
        private DevExpress.XtraTreeList.TreeList tree_category;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.LabelControl lbl_table_number;
        private DevExpress.XtraEditors.SimpleButton btn_back;
        private DevExpress.XtraEditors.SplitContainerControl spliter;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repo_button;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
    }
}
