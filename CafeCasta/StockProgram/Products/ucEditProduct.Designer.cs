namespace StockProgram.Products
{
    partial class ucEditProduct
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btn_urunEkle = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.lbl_header = new DevExpress.XtraEditors.LabelControl();
            this.btn_back = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.chk_listedeDegil = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.spliter = new DevExpress.XtraEditors.SplitContainerControl();
            this.tree_category = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.spin_satis_duble = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.spin_satis1_5 = new DevExpress.XtraEditors.SpinEdit();
            this.chk_listede = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.btn_resimEkle = new DevExpress.XtraEditors.SimpleButton();
            this.spin_satis_fiyat = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.cb_para = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txt_urunAdi = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txt_urunTanim = new DevExpress.XtraEditors.TextEdit();
            this.txt_kategori = new DevExpress.XtraEditors.TextEdit();
            this.txt_urun_kodu = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_listedeDegil.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spliter)).BeginInit();
            this.spliter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tree_category)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_satis_duble.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_satis1_5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_listede.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_satis_fiyat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_para.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_urunAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_urunTanim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_kategori.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_urun_kodu.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btn_urunEkle
            // 
            this.btn_urunEkle.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_urunEkle.Image = global::StockProgram.Properties.Resources.edit;
            this.btn_urunEkle.Location = new System.Drawing.Point(2, 2);
            this.btn_urunEkle.Name = "btn_urunEkle";
            this.btn_urunEkle.Size = new System.Drawing.Size(152, 35);
            this.btn_urunEkle.TabIndex = 15;
            this.btn_urunEkle.Text = "Düzenlemeyi Tamamla";
            this.btn_urunEkle.Click += new System.EventHandler(this.btn_urunEkle_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_header);
            this.panel1.Controls.Add(this.btn_back);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(961, 39);
            this.panel1.TabIndex = 50;
            // 
            // lbl_header
            // 
            this.lbl_header.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_header.Location = new System.Drawing.Point(5, 8);
            this.lbl_header.Name = "lbl_header";
            this.lbl_header.Size = new System.Drawing.Size(130, 22);
            this.lbl_header.TabIndex = 2;
            this.lbl_header.Text = "Ürün Düzenleme";
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(882, 2);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(77, 35);
            this.btn_back.TabIndex = 41;
            this.btn_back.Text = "Geri";
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_urunEkle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 345);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(961, 39);
            this.panelControl1.TabIndex = 51;
            // 
            // chk_listedeDegil
            // 
            this.chk_listedeDegil.Location = new System.Drawing.Point(323, 115);
            this.chk_listedeDegil.Name = "chk_listedeDegil";
            this.chk_listedeDegil.Properties.Caption = "Satış Listesinden Çıkar";
            this.chk_listedeDegil.Size = new System.Drawing.Size(133, 19);
            this.chk_listedeDegil.TabIndex = 18;
            this.chk_listedeDegil.CheckedChanged += new System.EventHandler(this.chk_listedeDegil_CheckedChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.spliter);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 39);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(961, 306);
            this.panelControl2.TabIndex = 52;
            // 
            // spliter
            // 
            this.spliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spliter.Location = new System.Drawing.Point(2, 2);
            this.spliter.Name = "spliter";
            this.spliter.Panel1.Controls.Add(this.tree_category);
            this.spliter.Panel1.Text = "Panel1";
            this.spliter.Panel2.AutoScroll = true;
            this.spliter.Panel2.Controls.Add(this.labelControl2);
            this.spliter.Panel2.Controls.Add(this.spin_satis_duble);
            this.spliter.Panel2.Controls.Add(this.labelControl4);
            this.spliter.Panel2.Controls.Add(this.spin_satis1_5);
            this.spliter.Panel2.Controls.Add(this.chk_listede);
            this.spliter.Panel2.Controls.Add(this.labelControl9);
            this.spliter.Panel2.Controls.Add(this.chk_listedeDegil);
            this.spliter.Panel2.Controls.Add(this.pictureEdit1);
            this.spliter.Panel2.Controls.Add(this.btn_resimEkle);
            this.spliter.Panel2.Controls.Add(this.spin_satis_fiyat);
            this.spliter.Panel2.Controls.Add(this.labelControl10);
            this.spliter.Panel2.Controls.Add(this.cb_para);
            this.spliter.Panel2.Controls.Add(this.txt_urunAdi);
            this.spliter.Panel2.Controls.Add(this.labelControl1);
            this.spliter.Panel2.Controls.Add(this.labelControl5);
            this.spliter.Panel2.Controls.Add(this.labelControl3);
            this.spliter.Panel2.Controls.Add(this.txt_urunTanim);
            this.spliter.Panel2.Controls.Add(this.txt_kategori);
            this.spliter.Panel2.Controls.Add(this.txt_urun_kodu);
            this.spliter.Panel2.Controls.Add(this.labelControl6);
            this.spliter.Panel2.Text = "Panel2";
            this.spliter.Panel2.Click += new System.EventHandler(this.spliter_Panel2_Click);
            this.spliter.Size = new System.Drawing.Size(957, 302);
            this.spliter.SplitterPosition = 245;
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
            this.tree_category.Size = new System.Drawing.Size(245, 302);
            this.tree_category.TabIndex = 96;
            this.tree_category.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.tree_category_FocusedNodeChanged_1);
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
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(173, 172);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(71, 13);
            this.labelControl2.TabIndex = 111;
            this.labelControl2.Text = "Duble Porsiyon";
            // 
            // spin_satis_duble
            // 
            this.spin_satis_duble.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_satis_duble.Location = new System.Drawing.Point(173, 191);
            this.spin_satis_duble.Name = "spin_satis_duble";
            this.spin_satis_duble.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spin_satis_duble.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.spin_satis_duble.Properties.Mask.BeepOnError = true;
            this.spin_satis_duble.Properties.Mask.EditMask = "n";
            this.spin_satis_duble.Size = new System.Drawing.Size(61, 20);
            this.spin_satis_duble.TabIndex = 11;
            this.spin_satis_duble.Click += new System.EventHandler(this.spin_satis_duble_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(96, 172);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 13);
            this.labelControl4.TabIndex = 110;
            this.labelControl4.Text = "1,5 Porsiyon";
            // 
            // spin_satis1_5
            // 
            this.spin_satis1_5.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_satis1_5.Location = new System.Drawing.Point(96, 191);
            this.spin_satis1_5.Name = "spin_satis1_5";
            this.spin_satis1_5.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spin_satis1_5.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.spin_satis1_5.Properties.Mask.BeepOnError = true;
            this.spin_satis1_5.Properties.Mask.EditMask = "n";
            this.spin_satis1_5.Size = new System.Drawing.Size(61, 20);
            this.spin_satis1_5.TabIndex = 10;
            this.spin_satis1_5.Click += new System.EventHandler(this.spin_satis1_5_Click);
            // 
            // chk_listede
            // 
            this.chk_listede.Location = new System.Drawing.Point(456, 321);
            this.chk_listede.Name = "chk_listede";
            this.chk_listede.Properties.Caption = "Satış Listesine al";
            this.chk_listede.Size = new System.Drawing.Size(133, 19);
            this.chk_listede.TabIndex = 105;
            this.chk_listede.Visible = false;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(13, 172);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(50, 13);
            this.labelControl9.TabIndex = 103;
            this.labelControl9.Text = "1 Porsiyon";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::StockProgram.Properties.Resources.ok_blue;
            this.pictureEdit1.Location = new System.Drawing.Point(471, 113);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Size = new System.Drawing.Size(150, 100);
            this.pictureEdit1.TabIndex = 45;
            // 
            // btn_resimEkle
            // 
            this.btn_resimEkle.Image = global::StockProgram.Properties.Resources.picture_add;
            this.btn_resimEkle.Location = new System.Drawing.Point(325, 172);
            this.btn_resimEkle.Name = "btn_resimEkle";
            this.btn_resimEkle.Size = new System.Drawing.Size(125, 41);
            this.btn_resimEkle.TabIndex = 16;
            this.btn_resimEkle.Text = "Ürün Resmi Ekle";
            this.btn_resimEkle.Click += new System.EventHandler(this.btn_resimEkle_Click_1);
            // 
            // spin_satis_fiyat
            // 
            this.spin_satis_fiyat.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_satis_fiyat.Location = new System.Drawing.Point(13, 191);
            this.spin_satis_fiyat.Name = "spin_satis_fiyat";
            this.spin_satis_fiyat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spin_satis_fiyat.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.spin_satis_fiyat.Properties.Mask.BeepOnError = true;
            this.spin_satis_fiyat.Properties.Mask.EditMask = "n";
            this.spin_satis_fiyat.Size = new System.Drawing.Size(66, 20);
            this.spin_satis_fiyat.TabIndex = 9;
            this.spin_satis_fiyat.Click += new System.EventHandler(this.spin_satis_fiyat_Click);
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(252, 172);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(49, 13);
            this.labelControl10.TabIndex = 104;
            this.labelControl10.Text = "Para Birimi";
            // 
            // cb_para
            // 
            this.cb_para.Location = new System.Drawing.Point(252, 191);
            this.cb_para.Name = "cb_para";
            this.cb_para.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_para.Properties.Items.AddRange(new object[] {
            "TL"});
            this.cb_para.Size = new System.Drawing.Size(55, 20);
            this.cb_para.TabIndex = 12;
            // 
            // txt_urunAdi
            // 
            this.txt_urunAdi.Location = new System.Drawing.Point(13, 48);
            this.txt_urunAdi.Name = "txt_urunAdi";
            this.txt_urunAdi.Size = new System.Drawing.Size(208, 20);
            this.txt_urunAdi.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "* Ürün Adı";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(325, 29);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(56, 13);
            this.labelControl5.TabIndex = 14;
            this.labelControl5.Text = "Ürün Tanımı";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(381, 275);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(141, 13);
            this.labelControl3.TabIndex = 16;
            this.labelControl3.Text = "* Ürün Kodu (13-LES-Z-1234)";
            this.labelControl3.Visible = false;
            // 
            // txt_urunTanim
            // 
            this.txt_urunTanim.Location = new System.Drawing.Point(325, 48);
            this.txt_urunTanim.Name = "txt_urunTanim";
            this.txt_urunTanim.Size = new System.Drawing.Size(208, 20);
            this.txt_urunTanim.TabIndex = 8;
            // 
            // txt_kategori
            // 
            this.txt_kategori.EditValue = "Kategori Seçiniz";
            this.txt_kategori.Location = new System.Drawing.Point(13, 115);
            this.txt_kategori.Name = "txt_kategori";
            this.txt_kategori.Properties.ReadOnly = true;
            this.txt_kategori.Size = new System.Drawing.Size(208, 20);
            this.txt_kategori.TabIndex = 83;
            // 
            // txt_urun_kodu
            // 
            this.txt_urun_kodu.Location = new System.Drawing.Point(381, 295);
            this.txt_urun_kodu.Name = "txt_urun_kodu";
            this.txt_urun_kodu.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.None;
            this.txt_urun_kodu.Properties.Mask.EditMask = "\\d\\d\\p{Lu}\\p{Lu}\\p{Lu}\\p{Lu}\\d\\d\\d\\d";
            this.txt_urun_kodu.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txt_urun_kodu.Size = new System.Drawing.Size(208, 20);
            this.txt_urun_kodu.TabIndex = 9;
            this.txt_urun_kodu.Visible = false;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(13, 96);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(66, 13);
            this.labelControl6.TabIndex = 84;
            this.labelControl6.Text = "Ürün Kategori";
            // 
            // ucEditProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel1);
            this.Name = "ucEditProduct";
            this.Size = new System.Drawing.Size(961, 384);
            this.Load += new System.EventHandler(this.ucAddProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chk_listedeDegil.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spliter)).EndInit();
            this.spliter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tree_category)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_satis_duble.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_satis1_5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_listede.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_satis_fiyat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_para.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_urunAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_urunTanim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_kategori.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_urun_kodu.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_urunEkle;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.LabelControl lbl_header;
        private DevExpress.XtraEditors.SimpleButton btn_back;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckEdit chk_listedeDegil;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SplitContainerControl spliter;
        private DevExpress.XtraTreeList.TreeList tree_category;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.SpinEdit spin_satis_fiyat;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.ComboBoxEdit cb_para;
        private DevExpress.XtraEditors.TextEdit txt_urunAdi;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txt_urunTanim;
        private DevExpress.XtraEditors.TextEdit txt_kategori;
        private DevExpress.XtraEditors.TextEdit txt_urun_kodu;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.CheckEdit chk_listede;
        private DevExpress.XtraEditors.SimpleButton btn_resimEkle;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SpinEdit spin_satis_duble;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SpinEdit spin_satis1_5;
    }
}
