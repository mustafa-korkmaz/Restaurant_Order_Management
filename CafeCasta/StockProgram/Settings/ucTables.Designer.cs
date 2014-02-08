namespace StockProgram.Settings
{
    partial class ucTables
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_product_name = new DevExpress.XtraEditors.LabelControl();
            this.split = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repo_button = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnl_master = new DevExpress.XtraEditors.PanelControl();
            this.btn_urun_gir = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_master)).BeginInit();
            this.pnl_master.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this.lbl_product_name);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(622, 39);
            this.panel1.TabIndex = 73;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.simpleButton1.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.simpleButton1.Location = new System.Drawing.Point(556, 2);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(64, 35);
            this.simpleButton1.TabIndex = 43;
            this.simpleButton1.Text = "Geri";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // lbl_product_name
            // 
            this.lbl_product_name.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_product_name.Location = new System.Drawing.Point(5, 8);
            this.lbl_product_name.Name = "lbl_product_name";
            this.lbl_product_name.Size = new System.Drawing.Size(112, 22);
            this.lbl_product_name.TabIndex = 2;
            this.lbl_product_name.Text = "Masa İşlemleri";
            // 
            // split
            // 
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.Horizontal = false;
            this.split.Location = new System.Drawing.Point(0, 39);
            this.split.Name = "split";
            this.split.Panel1.Controls.Add(this.gridControl1);
            this.split.Panel1.Text = "Panel1";
            this.split.Panel2.Controls.Add(this.pnl_master);
            this.split.Panel2.Text = "Panel2";
            this.split.Size = new System.Drawing.Size(622, 477);
            this.split.SplitterPosition = 297;
            this.split.TabIndex = 74;
            this.split.Text = "splitContainerControl1";
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
            this.gridControl1.Size = new System.Drawing.Size(622, 297);
            this.gridControl1.TabIndex = 79;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn5,
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn4});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupPanelText = "Aramalarınızı kategorilere göre sınflandırmak için buraya istediğiniz kolonu sürü" +
    "kleyebilirsiniz.";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Masa Adı";
            this.gridColumn2.FieldName = "table_name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 421;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "İşlemler";
            this.gridColumn5.ColumnEdit = this.repo_button;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 174;
            // 
            // repo_button
            // 
            this.repo_button.AutoHeight = false;
            this.repo_button.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repo_button.Name = "repo_button";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Masa Kodu";
            this.gridColumn1.FieldName = "table_id";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Width = 340;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Yerleşim Alanı";
            this.gridColumn3.FieldName = "table_category_name";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 338;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Durumu";
            this.gridColumn4.FieldName = "status_name";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 230;
            // 
            // pnl_master
            // 
            this.pnl_master.Controls.Add(this.btn_urun_gir);
            this.pnl_master.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_master.Location = new System.Drawing.Point(0, 0);
            this.pnl_master.Name = "pnl_master";
            this.pnl_master.Size = new System.Drawing.Size(622, 66);
            this.pnl_master.TabIndex = 2;
            // 
            // btn_urun_gir
            // 
            this.btn_urun_gir.Image = global::StockProgram.Properties.Resources.add_blue;
            this.btn_urun_gir.Location = new System.Drawing.Point(5, 6);
            this.btn_urun_gir.Name = "btn_urun_gir";
            this.btn_urun_gir.Size = new System.Drawing.Size(145, 55);
            this.btn_urun_gir.TabIndex = 0;
            this.btn_urun_gir.Text = "Yeni Masa Ekle";
            this.btn_urun_gir.Click += new System.EventHandler(this.btn_urun_gir_Click);
            // 
            // ucTables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.split);
            this.Controls.Add(this.panel1);
            this.Name = "ucTables";
            this.Size = new System.Drawing.Size(622, 516);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_master)).EndInit();
            this.pnl_master.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl lbl_product_name;
        private DevExpress.XtraEditors.SplitContainerControl split;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repo_button;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.PanelControl pnl_master;
        private DevExpress.XtraEditors.SimpleButton btn_urun_gir;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;

    }
}
