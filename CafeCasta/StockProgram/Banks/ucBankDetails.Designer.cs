namespace StockProgram.Banks
{
    partial class ucBankDetails
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_AlisverisEkle = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_back = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_header = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repo_button = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.lbl_bakiye = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).BeginInit();
            this.panelControl7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl7);
            this.panelControl1.Controls.Add(this.btn_AlisverisEkle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 305);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(622, 39);
            this.panelControl1.TabIndex = 79;
            // 
            // btn_AlisverisEkle
            // 
            this.btn_AlisverisEkle.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_AlisverisEkle.Image = global::StockProgram.Properties.Resources.case_big;
            this.btn_AlisverisEkle.Location = new System.Drawing.Point(2, 2);
            this.btn_AlisverisEkle.Name = "btn_AlisverisEkle";
            this.btn_AlisverisEkle.Size = new System.Drawing.Size(132, 35);
            this.btn_AlisverisEkle.TabIndex = 76;
            this.btn_AlisverisEkle.Text = "Para Transfer Et";
            this.btn_AlisverisEkle.Click += new System.EventHandler(this.btn_AlisverisEkle_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_back);
            this.panel1.Controls.Add(this.lbl_header);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(622, 39);
            this.panel1.TabIndex = 81;
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(543, 2);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(77, 35);
            this.btn_back.TabIndex = 42;
            this.btn_back.Text = "Geri";
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // lbl_header
            // 
            this.lbl_header.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_header.Location = new System.Drawing.Point(5, 8);
            this.lbl_header.Name = "lbl_header";
            this.lbl_header.Size = new System.Drawing.Size(117, 22);
            this.lbl_header.TabIndex = 2;
            this.lbl_header.Text = "İşlem Detayları";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.Location = new System.Drawing.Point(0, 39);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repo_button});
            this.gridControl1.Size = new System.Drawing.Size(622, 266);
            this.gridControl1.TabIndex = 82;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupPanelText = "Aramalarınızı kategorilere göre sınflandırmak için buraya istediğiniz kolonu sürü" +
    "kleyebilirsiniz.";
            this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "payment_price", this.gridColumn2, "Toplam= {0}")});
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "İşlem Tipi";
            this.gridColumn1.FieldName = "type";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.SummaryItem.DisplayFormat = "{0} İşlem";
            this.gridColumn1.SummaryItem.FieldName = "payment_id";
            this.gridColumn1.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 135;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tutar";
            this.gridColumn2.DisplayFormat.FormatString = "c2";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn2.FieldName = "toplam_borc";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.SummaryItem.DisplayFormat = "Toplam = {0:c2}";
            this.gridColumn2.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 118;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "İşlem Tarihi";
            this.gridColumn3.FieldName = "modified_date";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 7;
            this.gridColumn3.Width = 130;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "İşlemler";
            this.gridColumn4.ColumnEdit = this.repo_button;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 8;
            this.gridColumn4.Width = 99;
            // 
            // repo_button
            // 
            this.repo_button.AutoHeight = false;
            this.repo_button.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repo_button.Name = "repo_button";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Açıklama";
            this.gridColumn5.FieldName = "desc";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 248;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Net Tutar";
            this.gridColumn6.DisplayFormat.FormatString = "c2";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn6.FieldName = "kalan";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.ReadOnly = true;
            this.gridColumn6.SummaryItem.DisplayFormat = "Net Toplam= {0:c2}";
            this.gridColumn6.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 156;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Taksit";
            this.gridColumn7.FieldName = "instalment";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.ReadOnly = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            this.gridColumn7.Width = 50;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "% Kesinti";
            this.gridColumn8.FieldName = "rate";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.ReadOnly = true;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            this.gridColumn8.Width = 63;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Aktarım Tarihi";
            this.gridColumn9.FieldName = "payment_date";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsColumn.ReadOnly = true;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 6;
            this.gridColumn9.Width = 164;
            // 
            // panelControl7
            // 
            this.panelControl7.Controls.Add(this.labelControl12);
            this.panelControl7.Controls.Add(this.lbl_bakiye);
            this.panelControl7.Controls.Add(this.labelControl13);
            this.panelControl7.Controls.Add(this.labelControl14);
            this.panelControl7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl7.Location = new System.Drawing.Point(491, 2);
            this.panelControl7.Name = "panelControl7";
            this.panelControl7.Size = new System.Drawing.Size(129, 35);
            this.panelControl7.TabIndex = 77;
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(5, 11);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(70, 13);
            this.labelControl12.TabIndex = 5;
            this.labelControl12.Text = "Güncel Bakiye:";
            // 
            // lbl_bakiye
            // 
            this.lbl_bakiye.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lbl_bakiye.Location = new System.Drawing.Point(79, 11);
            this.lbl_bakiye.Name = "lbl_bakiye";
            this.lbl_bakiye.Size = new System.Drawing.Size(29, 13);
            this.lbl_bakiye.TabIndex = 6;
            this.lbl_bakiye.Text = "%100";
            // 
            // labelControl13
            // 
            this.labelControl13.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl13.Location = new System.Drawing.Point(561, 11);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(30, 13);
            this.labelControl13.TabIndex = 4;
            this.labelControl13.Text = "0.0 TL";
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(421, 11);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(134, 13);
            this.labelControl14.TabIndex = 3;
            this.labelControl14.Text = "Geçmiş Satışlardan İadeler=";
            // 
            // ucBankDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelControl1);
            this.Name = "ucBankDetails";
            this.Size = new System.Drawing.Size(622, 344);
            this.Load += new System.EventHandler(this.ucShoppingDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).EndInit();
            this.panelControl7.ResumeLayout(false);
            this.panelControl7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_AlisverisEkle;
        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btn_back;
        private DevExpress.XtraEditors.LabelControl lbl_header;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repo_button;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.PanelControl panelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl lbl_bakiye;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl labelControl14;
    }
}
