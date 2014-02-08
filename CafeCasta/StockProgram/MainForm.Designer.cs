namespace StockProgram
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.bar_program_name = new DevExpress.XtraBars.BarStaticItem();
            this.bar_version = new DevExpress.XtraBars.BarStaticItem();
            this.bar_user = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.pnl_left = new DevExpress.XtraEditors.PanelControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItemSatis = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemUrunler = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemTedarikci = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemKategori = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemBanka = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemRapor = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemGunluk = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGider = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemVeresiye = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemSettings = new DevExpress.XtraNavBar.NavBarItem();
            this.pnl_main = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_left)).BeginInit();
            this.pnl_left.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_main)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bar_program_name,
            this.barEditItem1,
            this.bar_version,
            this.bar_user});
            this.barManager1.MaxItemId = 5;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bar_program_name),
            new DevExpress.XtraBars.LinkPersistInfo(this.bar_version),
            new DevExpress.XtraBars.LinkPersistInfo(this.bar_user)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // bar_program_name
            // 
            this.bar_program_name.Caption = "Cafe CasTa";
            this.bar_program_name.Id = 0;
            this.bar_program_name.Name = "bar_program_name";
            this.bar_program_name.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // bar_version
            // 
            this.bar_version.Caption = "v1.0";
            this.bar_version.Id = 2;
            this.bar_version.Name = "bar_version";
            this.bar_version.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // bar_user
            // 
            this.bar_user.Caption = "Giriş Yapılmadı";
            this.bar_user.Id = 4;
            this.bar_user.Name = "bar_user";
            this.bar_user.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(980, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 495);
            this.barDockControlBottom.Size = new System.Drawing.Size(980, 26);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 495);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(980, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 495);
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "barEditItem1";
            this.barEditItem1.Edit = this.repositoryItemTextEdit1;
            this.barEditItem1.Id = 1;
            this.barEditItem1.Name = "barEditItem1";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // pnl_left
            // 
            this.pnl_left.Controls.Add(this.navBarControl1);
            this.pnl_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_left.Location = new System.Drawing.Point(0, 0);
            this.pnl_left.Name = "pnl_left";
            this.pnl_left.Size = new System.Drawing.Size(172, 495);
            this.pnl_left.TabIndex = 4;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.ContentButtonHint = null;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.ExplorerBarShowGroupButtons = false;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1});
            this.navBarControl1.HideGroupCaptions = true;
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarItemSatis,
            this.navBarItemUrunler,
            this.navBarItemTedarikci,
            this.navBarItemKategori,
            this.navBarItemBanka,
            this.navBarItemGunluk,
            this.navBarItemRapor,
            this.navBarItemSettings,
            this.navBarGider,
            this.navBarItemVeresiye});
            this.navBarControl1.Location = new System.Drawing.Point(2, 2);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 224;
            this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarControl1.ShowLinkHint = false;
            this.navBarControl1.Size = new System.Drawing.Size(168, 491);
            this.navBarControl1.TabIndex = 58;
            this.navBarControl1.Text = "navBarControl1";
            this.navBarControl1.View = new DevExpress.XtraNavBar.ViewInfo.SkinNavigationPaneViewInfoRegistrator();
            this.navBarControl1.NavPaneStateChanged += new System.EventHandler(this.navBarControl1_NavPaneStateChanged);
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "İşlemler";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemSatis),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemUrunler),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemTedarikci),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemKategori),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemBanka),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemRapor),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemGunluk),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarGider),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemVeresiye),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemSettings)});
            this.navBarGroup1.Name = "navBarGroup1";
            this.navBarGroup1.NavigationPaneVisible = false;
            // 
            // navBarItemSatis
            // 
            this.navBarItemSatis.Caption = "Hesap Aç / Kes";
            this.navBarItemSatis.Name = "navBarItemSatis";
            this.navBarItemSatis.SmallImage = global::StockProgram.Properties.Resources.to_do_list_cheked_all;
            this.navBarItemSatis.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemSatis_LinkClicked);
            // 
            // navBarItemUrunler
            // 
            this.navBarItemUrunler.Caption = "Ürünler";
            this.navBarItemUrunler.Name = "navBarItemUrunler";
            this.navBarItemUrunler.SmallImage = global::StockProgram.Properties.Resources.barcode32x32;
            this.navBarItemUrunler.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemUrunler_LinkClicked);
            // 
            // navBarItemTedarikci
            // 
            this.navBarItemTedarikci.Caption = "Firmalar";
            this.navBarItemTedarikci.Name = "navBarItemTedarikci";
            this.navBarItemTedarikci.SmallImage = global::StockProgram.Properties.Resources.suppliers;
            this.navBarItemTedarikci.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemTedarikci_LinkClicked);
            // 
            // navBarItemKategori
            // 
            this.navBarItemKategori.Caption = "Kategoriler";
            this.navBarItemKategori.Name = "navBarItemKategori";
            this.navBarItemKategori.SmallImage = global::StockProgram.Properties.Resources.category_orange;
            this.navBarItemKategori.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemKategori_LinkClicked);
            // 
            // navBarItemBanka
            // 
            this.navBarItemBanka.Caption = "Bankalar";
            this.navBarItemBanka.Name = "navBarItemBanka";
            this.navBarItemBanka.SmallImage = global::StockProgram.Properties.Resources.bank;
            this.navBarItemBanka.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemBanka_LinkClicked);
            // 
            // navBarItemRapor
            // 
            this.navBarItemRapor.Caption = "Raporlar";
            this.navBarItemRapor.Name = "navBarItemRapor";
            this.navBarItemRapor.SmallImage = global::StockProgram.Properties.Resources.report_small;
            this.navBarItemRapor.Visible = false;
            this.navBarItemRapor.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemRapor_LinkClicked);
            // 
            // navBarItemGunluk
            // 
            this.navBarItemGunluk.Caption = "Günlük Siparişler";
            this.navBarItemGunluk.Name = "navBarItemGunluk";
            this.navBarItemGunluk.SmallImage = global::StockProgram.Properties.Resources.cash_register_middle;
            this.navBarItemGunluk.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemGunluk_LinkClicked);
            // 
            // navBarGider
            // 
            this.navBarGider.Caption = "Giderler";
            this.navBarGider.Name = "navBarGider";
            this.navBarGider.SmallImage = global::StockProgram.Properties.Resources.wallet;
            this.navBarGider.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarGider_LinkClicked);
            // 
            // navBarItemVeresiye
            // 
            this.navBarItemVeresiye.Caption = "Müşteriler";
            this.navBarItemVeresiye.Name = "navBarItemVeresiye";
            this.navBarItemVeresiye.SmallImage = global::StockProgram.Properties.Resources.customer;
            this.navBarItemVeresiye.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemVeresiye_LinkClicked);
            // 
            // navBarItemSettings
            // 
            this.navBarItemSettings.Caption = "Ayarlar";
            this.navBarItemSettings.Name = "navBarItemSettings";
            this.navBarItemSettings.SmallImage = global::StockProgram.Properties.Resources.settings;
            this.navBarItemSettings.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemSettings_LinkClicked);
            // 
            // pnl_main
            // 
            this.pnl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_main.Location = new System.Drawing.Point(172, 0);
            this.pnl_main.Name = "pnl_main";
            this.pnl_main.Size = new System.Drawing.Size(808, 495);
            this.pnl_main.TabIndex = 5;
            this.pnl_main.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_main_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 521);
            this.Controls.Add(this.pnl_main);
            this.Controls.Add(this.pnl_left);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "CasTa ";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_left)).EndInit();
            this.pnl_left.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_main)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarStaticItem bar_program_name;
        private DevExpress.XtraBars.BarStaticItem bar_version;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarStaticItem bar_user;
        private DevExpress.XtraEditors.PanelControl pnl_main;
        private DevExpress.XtraEditors.PanelControl pnl_left;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarItem navBarItemSatis;
        private DevExpress.XtraNavBar.NavBarItem navBarItemUrunler;
        private DevExpress.XtraNavBar.NavBarItem navBarItemTedarikci;
        private DevExpress.XtraNavBar.NavBarItem navBarItemKategori;
        private DevExpress.XtraNavBar.NavBarItem navBarItemBanka;
        private DevExpress.XtraNavBar.NavBarItem navBarItemRapor;
        private DevExpress.XtraNavBar.NavBarItem navBarItemGunluk;
        private DevExpress.XtraNavBar.NavBarItem navBarItemSettings;
        private DevExpress.XtraNavBar.NavBarItem navBarGider;
        private DevExpress.XtraNavBar.NavBarItem navBarItemVeresiye;
    }
}