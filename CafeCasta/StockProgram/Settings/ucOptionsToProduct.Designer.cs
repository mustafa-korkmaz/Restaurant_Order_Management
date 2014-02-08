namespace StockProgram.Settings
{
    partial class ucOptionsToProducts
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_back = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_table_number = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_tamamla = new DevExpress.XtraEditors.SimpleButton();
            this.btn_out = new DevExpress.XtraEditors.SimpleButton();
            this.btn_in = new DevExpress.XtraEditors.SimpleButton();
            this.lb_options = new DevExpress.XtraEditors.ListBoxControl();
            this.split = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repo_button = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cb_products = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lb_options)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_products.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_back);
            this.panel1.Controls.Add(this.lbl_table_number);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(646, 39);
            this.panel1.TabIndex = 78;
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(567, 2);
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
            this.lbl_table_number.Size = new System.Drawing.Size(174, 22);
            this.lbl_table_number.TabIndex = 2;
            this.lbl_table_number.Text = "Ürünlere Seçenek Ekle";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_tamamla);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 338);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(646, 39);
            this.panelControl1.TabIndex = 83;
            // 
            // btn_tamamla
            // 
            this.btn_tamamla.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_tamamla.Image = global::StockProgram.Properties.Resources.ok_blue;
            this.btn_tamamla.Location = new System.Drawing.Point(2, 2);
            this.btn_tamamla.Name = "btn_tamamla";
            this.btn_tamamla.Size = new System.Drawing.Size(115, 35);
            this.btn_tamamla.TabIndex = 67;
            this.btn_tamamla.Text = "Tamamla";
            this.btn_tamamla.Click += new System.EventHandler(this.btn_tamamla_Click);
            // 
            // btn_out
            // 
            this.btn_out.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_out.Appearance.Options.UseFont = true;
            this.btn_out.Location = new System.Drawing.Point(23, 150);
            this.btn_out.Name = "btn_out";
            this.btn_out.Size = new System.Drawing.Size(27, 20);
            this.btn_out.TabIndex = 111;
            this.btn_out.Text = "<<";
            this.btn_out.Click += new System.EventHandler(this.btn_out_Click);
            // 
            // btn_in
            // 
            this.btn_in.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_in.Appearance.Options.UseFont = true;
            this.btn_in.Location = new System.Drawing.Point(23, 111);
            this.btn_in.Name = "btn_in";
            this.btn_in.Size = new System.Drawing.Size(27, 20);
            this.btn_in.TabIndex = 110;
            this.btn_in.Text = ">>";
            this.btn_in.Click += new System.EventHandler(this.btn_in_Click);
            // 
            // lb_options
            // 
            this.lb_options.Location = new System.Drawing.Point(67, 52);
            this.lb_options.Name = "lb_options";
            this.lb_options.Size = new System.Drawing.Size(185, 241);
            this.lb_options.TabIndex = 106;
            // 
            // split
            // 
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.Location = new System.Drawing.Point(0, 39);
            this.split.Name = "split";
            this.split.Panel1.Controls.Add(this.gridControl1);
            this.split.Panel1.Text = "Panel1";
            this.split.Panel2.Controls.Add(this.labelControl1);
            this.split.Panel2.Controls.Add(this.cb_products);
            this.split.Panel2.Controls.Add(this.lb_options);
            this.split.Panel2.Controls.Add(this.btn_out);
            this.split.Panel2.Controls.Add(this.btn_in);
            this.split.Panel2.Text = "Panel2";
            this.split.Size = new System.Drawing.Size(646, 299);
            this.split.SplitterPosition = 297;
            this.split.TabIndex = 112;
            this.split.Text = "splitContainerControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode2.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repo_button});
            this.gridControl1.Size = new System.Drawing.Size(297, 299);
            this.gridControl1.TabIndex = 79;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupPanelText = "Aramalarınızı kategorilere göre sınflandırmak için buraya istediğiniz kolonu sürü" +
    "kleyebilirsiniz.";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Seçenek Adı";
            this.gridColumn1.FieldName = "option_name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 658;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "gridColumn3";
            this.gridColumn3.FieldName = "option_id";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // repo_button
            // 
            this.repo_button.AutoHeight = false;
            this.repo_button.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repo_button.Name = "repo_button";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(67, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(67, 13);
            this.labelControl1.TabIndex = 113;
            this.labelControl1.Text = "* Ürün Seçiniz";
            // 
            // cb_products
            // 
            this.cb_products.Location = new System.Drawing.Point(67, 26);
            this.cb_products.Name = "cb_products";
            this.cb_products.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_products.Size = new System.Drawing.Size(185, 20);
            this.cb_products.TabIndex = 112;
            this.cb_products.SelectedIndexChanged += new System.EventHandler(this.cb_products_SelectedIndexChanged);
            // 
            // ucOptionsToProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.split);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel1);
            this.Name = "ucOptionsToProducts";
            this.Size = new System.Drawing.Size(646, 377);
            this.Load += new System.EventHandler(this.ucAddOption_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lb_options)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_products.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btn_back;
        private DevExpress.XtraEditors.LabelControl lbl_table_number;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_tamamla;
        private DevExpress.XtraEditors.SimpleButton btn_out;
        private DevExpress.XtraEditors.SimpleButton btn_in;
        private DevExpress.XtraEditors.ListBoxControl lb_options;
        private DevExpress.XtraEditors.SplitContainerControl split;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repo_button;
        private DevExpress.XtraEditors.ComboBoxEdit cb_products;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
