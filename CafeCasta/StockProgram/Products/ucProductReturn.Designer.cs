namespace StockProgram.Products
{
    partial class ucProductReturn
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
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.lbl_product_name = new DevExpress.XtraEditors.LabelControl();
            this.btn_back = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_iade = new DevExpress.XtraEditors.SimpleButton();
            this.pnl_barkod = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_barkod = new DevExpress.XtraEditors.TextEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repo_button = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_barkod)).BeginInit();
            this.pnl_barkod.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_barkod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_button)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Ürün Kodu";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Renk";
            this.gridColumn6.FieldName = "product_color";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Numara";
            this.gridColumn7.FieldName = "product_size";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 2;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Miktar";
            this.gridColumn8.FieldName = "product_count";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "id";
            this.gridColumn10.FieldName = "product_id";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_product_name);
            this.panel1.Controls.Add(this.btn_back);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(763, 39);
            this.panel1.TabIndex = 52;
            // 
            // lbl_product_name
            // 
            this.lbl_product_name.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_product_name.Location = new System.Drawing.Point(5, 8);
            this.lbl_product_name.Name = "lbl_product_name";
            this.lbl_product_name.Size = new System.Drawing.Size(155, 22);
            this.lbl_product_name.TabIndex = 2;
            this.lbl_product_name.Text = "Tedarikçi Ürün İade";
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(684, 2);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(77, 35);
            this.btn_back.TabIndex = 41;
            this.btn_back.Text = "Geri";
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_iade);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 347);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(763, 39);
            this.panelControl1.TabIndex = 57;
            this.panelControl1.Visible = false;
            // 
            // btn_iade
            // 
            this.btn_iade.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_iade.Image = global::StockProgram.Properties.Resources._return;
            this.btn_iade.Location = new System.Drawing.Point(2, 2);
            this.btn_iade.Name = "btn_iade";
            this.btn_iade.Size = new System.Drawing.Size(124, 35);
            this.btn_iade.TabIndex = 48;
            this.btn_iade.Text = "Ürün İadesi Yap";
            this.btn_iade.Visible = false;
            this.btn_iade.Click += new System.EventHandler(this.btn_iade_Click);
            // 
            // pnl_barkod
            // 
            this.pnl_barkod.Controls.Add(this.labelControl1);
            this.pnl_barkod.Controls.Add(this.txt_barkod);
            this.pnl_barkod.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_barkod.Location = new System.Drawing.Point(0, 39);
            this.pnl_barkod.Name = "pnl_barkod";
            this.pnl_barkod.Size = new System.Drawing.Size(763, 39);
            this.pnl_barkod.TabIndex = 86;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.labelControl1.Location = new System.Drawing.Point(5, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(89, 22);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "Barkod No:";
            // 
            // txt_barkod
            // 
            this.txt_barkod.Location = new System.Drawing.Point(98, 9);
            this.txt_barkod.Name = "txt_barkod";
            this.txt_barkod.Size = new System.Drawing.Size(218, 20);
            this.txt_barkod.TabIndex = 13;
            this.txt_barkod.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_barkod_KeyPress);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 78);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repo_button});
            this.gridControl1.Size = new System.Drawing.Size(763, 269);
            this.gridControl1.TabIndex = 87;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.ChildGridLevelName = "Level1";
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn2,
            this.gridColumn9,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupPanelText = "Aramalarınızı kategorilere göre sınflandırmak için buraya istediğiniz kolonu sürü" +
    "kleyebilirsiniz.";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Ürün Kodu";
            this.gridColumn1.FieldName = "product_code_manual";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Width = 134;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Ürün Adı";
            this.gridColumn3.FieldName = "goods_name";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 320;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.Caption = "Toplam Miktar";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "product_count";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.SummaryItem.DisplayFormat = "Toplam={0}";
            this.gridColumn4.SummaryItem.FieldName = "product_amount";
            this.gridColumn4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 225;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Kategori";
            this.gridColumn2.FieldName = "cat_name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Width = 224;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "id";
            this.gridColumn9.FieldName = "product_id";
            this.gridColumn9.Name = "gridColumn9";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Birim";
            this.gridColumn11.FieldName = "unit";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.OptionsColumn.ReadOnly = true;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 1;
            this.gridColumn11.Width = 92;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Gram Bazlı Toplam";
            this.gridColumn12.FieldName = "total_weight";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.SummaryItem.DisplayFormat = "Toplam {0} gr";
            this.gridColumn12.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn12.Width = 168;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Son Alım Tarihi";
            this.gridColumn13.FieldName = "modified_date";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.OptionsColumn.ReadOnly = true;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 3;
            // 
            // repo_button
            // 
            this.repo_button.AutoHeight = false;
            this.repo_button.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repo_button.Name = "repo_button";
            // 
            // ucProductReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.pnl_barkod);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel1);
            this.Name = "ucProductReturn";
            this.Size = new System.Drawing.Size(763, 386);
            this.Load += new System.EventHandler(this.ucProductReturn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_barkod)).EndInit();
            this.pnl_barkod.ResumeLayout(false);
            this.pnl_barkod.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_barkod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_button)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.LabelControl lbl_product_name;
        private DevExpress.XtraEditors.SimpleButton btn_back;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_iade;
       // private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.PanelControl pnl_barkod;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txt_barkod;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repo_button;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
    }
}
