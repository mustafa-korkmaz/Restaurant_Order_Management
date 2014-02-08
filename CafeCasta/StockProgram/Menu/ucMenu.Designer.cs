namespace StockProgram.Menu
{
    partial class ucMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMenu));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_back = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_header = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.pnl_masa_siparis = new System.Windows.Forms.Panel();
            this.pnl_content = new System.Windows.Forms.Panel();
            this.pnl_top = new DevExpress.XtraEditors.PanelControl();
            this.lbl_siparis_toplam = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.pnl_footer = new DevExpress.XtraEditors.PanelControl();
            this.btn_cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_order = new DevExpress.XtraEditors.SimpleButton();
            this.pnl_musteri = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btn_quick_customer_add = new DevExpress.XtraEditors.SimpleButton();
            this.cbo_customer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btn_desc = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txt_tel = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_adres = new System.Windows.Forms.RichTextBox();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txt_note = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.pnl_masa_siparis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_top)).BeginInit();
            this.pnl_top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_footer)).BeginInit();
            this.pnl_footer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_musteri)).BeginInit();
            this.pnl_musteri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_customer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_tel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_back);
            this.panel1.Controls.Add(this.lbl_header);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(794, 39);
            this.panel1.TabIndex = 55;
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(715, 2);
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
            this.lbl_header.Size = new System.Drawing.Size(95, 22);
            this.lbl_header.TabIndex = 2;
            this.lbl_header.Text = "Paket Servis";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.tabControl);
            this.panelControl2.Controls.Add(this.pnl_masa_siparis);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 39);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(794, 510);
            this.panelControl2.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.tabControl.Appearance.Options.UseForeColor = true;
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.tabControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.tabControl.Name = "tabControl";
            this.tabControl.Size = new System.Drawing.Size(522, 510);
            this.tabControl.TabIndex = 54;
            this.tabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabControl_SelectedPageChanged);
            // 
            // pnl_masa_siparis
            // 
            this.pnl_masa_siparis.Controls.Add(this.pnl_content);
            this.pnl_masa_siparis.Controls.Add(this.pnl_top);
            this.pnl_masa_siparis.Controls.Add(this.pnl_footer);
            this.pnl_masa_siparis.Controls.Add(this.pnl_musteri);
            this.pnl_masa_siparis.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnl_masa_siparis.Location = new System.Drawing.Point(522, 0);
            this.pnl_masa_siparis.Name = "pnl_masa_siparis";
            this.pnl_masa_siparis.Size = new System.Drawing.Size(272, 510);
            this.pnl_masa_siparis.TabIndex = 50;
            // 
            // pnl_content
            // 
            this.pnl_content.AutoScroll = true;
            this.pnl_content.BackColor = System.Drawing.SystemColors.Window;
            this.pnl_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_content.Location = new System.Drawing.Point(0, 278);
            this.pnl_content.Name = "pnl_content";
            this.pnl_content.Size = new System.Drawing.Size(272, 172);
            this.pnl_content.TabIndex = 5;
            this.pnl_content.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_content_Paint);
            // 
            // pnl_top
            // 
            this.pnl_top.Appearance.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.pnl_top.Appearance.Options.UseBackColor = true;
            this.pnl_top.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.pnl_top.Controls.Add(this.lbl_siparis_toplam);
            this.pnl_top.Controls.Add(this.labelControl2);
            this.pnl_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_top.Location = new System.Drawing.Point(0, 217);
            this.pnl_top.Name = "pnl_top";
            this.pnl_top.Size = new System.Drawing.Size(272, 61);
            this.pnl_top.TabIndex = 3;
            // 
            // lbl_siparis_toplam
            // 
            this.lbl_siparis_toplam.Appearance.Font = new System.Drawing.Font("Tahoma", 35F);
            this.lbl_siparis_toplam.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lbl_siparis_toplam.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_siparis_toplam.Location = new System.Drawing.Point(139, 2);
            this.lbl_siparis_toplam.Name = "lbl_siparis_toplam";
            this.lbl_siparis_toplam.Size = new System.Drawing.Size(131, 57);
            this.lbl_siparis_toplam.TabIndex = 1;
            this.lbl_siparis_toplam.Text = "0,0 TL";
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl2.Location = new System.Drawing.Point(6, 29);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(69, 23);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "TOPLAM";
            // 
            // pnl_footer
            // 
            this.pnl_footer.Controls.Add(this.btn_cancel);
            this.pnl_footer.Controls.Add(this.btn_order);
            this.pnl_footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_footer.Location = new System.Drawing.Point(0, 450);
            this.pnl_footer.Name = "pnl_footer";
            this.pnl_footer.Size = new System.Drawing.Size(272, 60);
            this.pnl_footer.TabIndex = 4;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.btn_cancel.Appearance.Options.UseFont = true;
            this.btn_cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_cancel.Image = global::StockProgram.Properties.Resources.delete;
            this.btn_cancel.Location = new System.Drawing.Point(194, 2);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(76, 56);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Text = "İptal";
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_order
            // 
            this.btn_order.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.btn_order.Appearance.Options.UseFont = true;
            this.btn_order.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_order.Image = global::StockProgram.Properties.Resources.to_do_list_cheked_all;
            this.btn_order.Location = new System.Drawing.Point(2, 2);
            this.btn_order.Name = "btn_order";
            this.btn_order.Size = new System.Drawing.Size(192, 56);
            this.btn_order.TabIndex = 0;
            this.btn_order.Text = "Sipariş Ver";
            this.btn_order.Click += new System.EventHandler(this.btn_order_Click);
            // 
            // pnl_musteri
            // 
            this.pnl_musteri.Appearance.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.pnl_musteri.Appearance.Options.UseBackColor = true;
            this.pnl_musteri.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.pnl_musteri.Controls.Add(this.groupControl1);
            this.pnl_musteri.Controls.Add(this.groupControl2);
            this.pnl_musteri.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_musteri.Location = new System.Drawing.Point(0, 0);
            this.pnl_musteri.Name = "pnl_musteri";
            this.pnl_musteri.Size = new System.Drawing.Size(272, 217);
            this.pnl_musteri.TabIndex = 2;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btn_quick_customer_add);
            this.groupControl1.Controls.Add(this.cbo_customer);
            this.groupControl1.Controls.Add(this.btn_desc);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txt_tel);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txt_adres);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(268, 142);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Müşteri Bilgileri";
            // 
            // btn_quick_customer_add
            // 
            this.btn_quick_customer_add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_quick_customer_add.Appearance.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.btn_quick_customer_add.Appearance.ForeColor = System.Drawing.Color.Green;
            this.btn_quick_customer_add.Appearance.Options.UseFont = true;
            this.btn_quick_customer_add.Appearance.Options.UseForeColor = true;
            this.btn_quick_customer_add.Image = ((System.Drawing.Image)(resources.GetObject("btn_quick_customer_add.Image")));
            this.btn_quick_customer_add.Location = new System.Drawing.Point(237, 93);
            this.btn_quick_customer_add.Margin = new System.Windows.Forms.Padding(4);
            this.btn_quick_customer_add.Name = "btn_quick_customer_add";
            this.btn_quick_customer_add.Size = new System.Drawing.Size(29, 29);
            this.btn_quick_customer_add.TabIndex = 25;
            this.btn_quick_customer_add.Text = "?";
            this.btn_quick_customer_add.Click += new System.EventHandler(this.btn_quick_customer_add_Click);
            // 
            // cbo_customer
            // 
            this.cbo_customer.Location = new System.Drawing.Point(52, 30);
            this.cbo_customer.Name = "cbo_customer";
            this.cbo_customer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_customer.Size = new System.Drawing.Size(181, 20);
            this.cbo_customer.TabIndex = 22;
            this.cbo_customer.SelectedIndexChanged += new System.EventHandler(this.cbo_customer_SelectedIndexChanged);
            // 
            // btn_desc
            // 
            this.btn_desc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_desc.Appearance.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.btn_desc.Appearance.ForeColor = System.Drawing.Color.Green;
            this.btn_desc.Appearance.Options.UseFont = true;
            this.btn_desc.Appearance.Options.UseForeColor = true;
            this.btn_desc.Image = ((System.Drawing.Image)(resources.GetObject("btn_desc.Image")));
            this.btn_desc.Location = new System.Drawing.Point(244, 28);
            this.btn_desc.Margin = new System.Windows.Forms.Padding(4);
            this.btn_desc.Name = "btn_desc";
            this.btn_desc.Size = new System.Drawing.Size(22, 22);
            this.btn_desc.TabIndex = 21;
            this.btn_desc.Text = "?";
            this.btn_desc.Click += new System.EventHandler(this.btn_desc_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl3.Location = new System.Drawing.Point(9, 37);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(26, 13);
            this.labelControl3.TabIndex = 20;
            this.labelControl3.Text = "İSİM:";
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl4.Location = new System.Drawing.Point(9, 63);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(21, 13);
            this.labelControl4.TabIndex = 18;
            this.labelControl4.Text = "TEL:";
            // 
            // txt_tel
            // 
            this.txt_tel.Location = new System.Drawing.Point(52, 58);
            this.txt_tel.Name = "txt_tel";
            this.txt_tel.Properties.Mask.EditMask = "f0";
            this.txt_tel.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txt_tel.Size = new System.Drawing.Size(181, 20);
            this.txt_tel.TabIndex = 23;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl1.Location = new System.Drawing.Point(9, 97);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(37, 13);
            this.labelControl1.TabIndex = 16;
            this.labelControl1.Text = "ADRES:";
            // 
            // txt_adres
            // 
            this.txt_adres.Location = new System.Drawing.Point(52, 84);
            this.txt_adres.Name = "txt_adres";
            this.txt_adres.Size = new System.Drawing.Size(181, 38);
            this.txt_adres.TabIndex = 24;
            this.txt_adres.Text = "";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txt_note);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl2.Location = new System.Drawing.Point(2, 144);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(268, 71);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "Müşteri Notu";
            // 
            // txt_note
            // 
            this.txt_note.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_note.Location = new System.Drawing.Point(2, 22);
            this.txt_note.Name = "txt_note";
            this.txt_note.Size = new System.Drawing.Size(264, 47);
            this.txt_note.TabIndex = 0;
            this.txt_note.Text = "";
            // 
            // ucMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panel1);
            this.Name = "ucMenu";
            this.Size = new System.Drawing.Size(794, 549);
            this.Load += new System.EventHandler(this.ucCourier_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.pnl_masa_siparis.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_top)).EndInit();
            this.pnl_top.ResumeLayout(false);
            this.pnl_top.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_footer)).EndInit();
            this.pnl_footer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_musteri)).EndInit();
            this.pnl_musteri.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_customer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_tel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btn_back;
        private DevExpress.XtraEditors.LabelControl lbl_header;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.Panel pnl_masa_siparis;
        private System.Windows.Forms.Panel pnl_content;
        private DevExpress.XtraEditors.PanelControl pnl_footer;
        private DevExpress.XtraEditors.SimpleButton btn_order;
        private DevExpress.XtraEditors.PanelControl pnl_top;
        private DevExpress.XtraEditors.LabelControl lbl_siparis_toplam;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl pnl_musteri;
        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraEditors.SimpleButton btn_cancel;
        private System.Windows.Forms.RichTextBox txt_note;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txt_tel;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.RichTextBox txt_adres;
        private DevExpress.XtraEditors.SimpleButton btn_desc;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_customer;
        private DevExpress.XtraEditors.SimpleButton btn_quick_customer_add;
    }
}
