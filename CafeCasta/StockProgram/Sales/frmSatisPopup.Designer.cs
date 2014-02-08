namespace StockProgram.Sales
{
    partial class frmSatisPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSatisPopup));
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.lbl_tutar = new DevExpress.XtraEditors.LabelControl();
            this.lbl_table_number = new DevExpress.XtraEditors.LabelControl();
            this.lbl_owner_name = new DevExpress.XtraEditors.LabelControl();
            this.cb_staff = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lbl_maliyet = new DevExpress.XtraEditors.LabelControl();
            this.chk_maliyet = new DevExpress.XtraEditors.CheckEdit();
            this.btn_tamamla = new DevExpress.XtraEditors.SimpleButton();
            this.btn_duzenle = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cbox_banka = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.spin_pesin = new DevExpress.XtraEditors.SpinEdit();
            this.spin_veresiye = new DevExpress.XtraEditors.SpinEdit();
            this.spin_pos = new DevExpress.XtraEditors.SpinEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lbl_indirim = new DevExpress.XtraEditors.LabelControl();
            this.cb_taksit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.chk_hediye = new DevExpress.XtraEditors.CheckEdit();
            this.btn_veresiye = new DevExpress.XtraEditors.SimpleButton();
            this.btn_banka = new DevExpress.XtraEditors.SimpleButton();
            this.btn_pesin = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txt_aciklama = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cb_staff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_maliyet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbox_banka.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_pesin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_veresiye.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_pos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cb_taksit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_hediye.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_aciklama.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_tutar);
            this.panel1.Controls.Add(this.lbl_table_number);
            this.panel1.Controls.Add(this.lbl_owner_name);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(543, 59);
            this.panel1.TabIndex = 0;
            // 
            // lbl_tutar
            // 
            this.lbl_tutar.Appearance.Font = new System.Drawing.Font("Tahoma", 18.25F);
            this.lbl_tutar.Location = new System.Drawing.Point(76, 14);
            this.lbl_tutar.Name = "lbl_tutar";
            this.lbl_tutar.Size = new System.Drawing.Size(58, 29);
            this.lbl_tutar.TabIndex = 3;
            this.lbl_tutar.Text = "xx TL";
            // 
            // lbl_table_number
            // 
            this.lbl_table_number.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_table_number.Location = new System.Drawing.Point(5, 20);
            this.lbl_table_number.Name = "lbl_table_number";
            this.lbl_table_number.Size = new System.Drawing.Size(65, 22);
            this.lbl_table_number.TabIndex = 2;
            this.lbl_table_number.Text = "Toplam:";
            // 
            // lbl_owner_name
            // 
            this.lbl_owner_name.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_owner_name.Appearance.Font = new System.Drawing.Font("Tahoma", 18.25F);
            this.lbl_owner_name.Location = new System.Drawing.Point(445, 14);
            this.lbl_owner_name.Name = "lbl_owner_name";
            this.lbl_owner_name.Size = new System.Drawing.Size(86, 29);
            this.lbl_owner_name.TabIndex = 19;
            this.lbl_owner_name.Text = "Müşteri ";
            // 
            // cb_staff
            // 
            this.cb_staff.Location = new System.Drawing.Point(321, 199);
            this.cb_staff.Name = "cb_staff";
            this.cb_staff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_staff.Size = new System.Drawing.Size(182, 20);
            this.cb_staff.TabIndex = 0;
            this.cb_staff.Visible = false;
            this.cb_staff.Click += new System.EventHandler(this.cb_staff_Click);
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.labelControl7.Location = new System.Drawing.Point(236, 197);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(72, 22);
            this.labelControl7.TabIndex = 54;
            this.labelControl7.Text = "Personel:";
            this.labelControl7.Visible = false;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.lbl_maliyet);
            this.panelControl2.Controls.Add(this.chk_maliyet);
            this.panelControl2.Controls.Add(this.btn_tamamla);
            this.panelControl2.Controls.Add(this.btn_duzenle);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 375);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(543, 44);
            this.panelControl2.TabIndex = 2;
            // 
            // lbl_maliyet
            // 
            this.lbl_maliyet.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_maliyet.Location = new System.Drawing.Point(109, 10);
            this.lbl_maliyet.Name = "lbl_maliyet";
            this.lbl_maliyet.Size = new System.Drawing.Size(81, 22);
            this.lbl_maliyet.TabIndex = 54;
            this.lbl_maliyet.Text = "999,99 TL";
            // 
            // chk_maliyet
            // 
            this.chk_maliyet.Location = new System.Drawing.Point(12, 13);
            this.chk_maliyet.Name = "chk_maliyet";
            this.chk_maliyet.Properties.Caption = "Maliyet Göster";
            this.chk_maliyet.Size = new System.Drawing.Size(91, 19);
            this.chk_maliyet.TabIndex = 53;
            this.chk_maliyet.Visible = false;
            this.chk_maliyet.CheckedChanged += new System.EventHandler(this.chk_maliyet_CheckedChanged);
            // 
            // btn_tamamla
            // 
            this.btn_tamamla.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_tamamla.Image = global::StockProgram.Properties.Resources.ok_blue;
            this.btn_tamamla.Location = new System.Drawing.Point(321, 2);
            this.btn_tamamla.Name = "btn_tamamla";
            this.btn_tamamla.Size = new System.Drawing.Size(110, 40);
            this.btn_tamamla.TabIndex = 14;
            this.btn_tamamla.Text = "Tamamla";
            this.btn_tamamla.Click += new System.EventHandler(this.btn_tamamla_Click);
            // 
            // btn_duzenle
            // 
            this.btn_duzenle.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_duzenle.Image = global::StockProgram.Properties.Resources.edit;
            this.btn_duzenle.Location = new System.Drawing.Point(431, 2);
            this.btn_duzenle.Name = "btn_duzenle";
            this.btn_duzenle.Size = new System.Drawing.Size(110, 40);
            this.btn_duzenle.TabIndex = 15;
            this.btn_duzenle.Text = "Sipariş Düzelt";
            this.btn_duzenle.Visible = false;
            this.btn_duzenle.Click += new System.EventHandler(this.btn_duzenle_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.labelControl2.Location = new System.Drawing.Point(36, 149);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(71, 22);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Veresiye:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.labelControl3.Location = new System.Drawing.Point(36, 61);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(47, 22);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Peşin:";
            // 
            // cbox_banka
            // 
            this.cbox_banka.Location = new System.Drawing.Point(121, 106);
            this.cbox_banka.Name = "cbox_banka";
            this.cbox_banka.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbox_banka.Size = new System.Drawing.Size(100, 20);
            this.cbox_banka.TabIndex = 8;
            this.cbox_banka.SelectedIndexChanged += new System.EventHandler(this.cbox_banka_SelectedIndexChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.labelControl4.Location = new System.Drawing.Point(36, 102);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(54, 22);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "Banka:";
            // 
            // spin_pesin
            // 
            this.spin_pesin.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_pesin.Location = new System.Drawing.Point(121, 65);
            this.spin_pesin.Name = "spin_pesin";
            this.spin_pesin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spin_pesin.Properties.Mask.BeepOnError = true;
            this.spin_pesin.Properties.Mask.EditMask = "n";
            this.spin_pesin.Size = new System.Drawing.Size(67, 20);
            this.spin_pesin.TabIndex = 7;
            this.spin_pesin.EditValueChanged += new System.EventHandler(this.spin_pesin_EditValueChanged);
            this.spin_pesin.Click += new System.EventHandler(this.spin_pesin_Click);
            // 
            // spin_veresiye
            // 
            this.spin_veresiye.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_veresiye.Location = new System.Drawing.Point(121, 151);
            this.spin_veresiye.Name = "spin_veresiye";
            this.spin_veresiye.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spin_veresiye.Properties.Mask.BeepOnError = true;
            this.spin_veresiye.Properties.Mask.EditMask = "n";
            this.spin_veresiye.Size = new System.Drawing.Size(67, 20);
            this.spin_veresiye.TabIndex = 11;
            this.spin_veresiye.EditValueChanged += new System.EventHandler(this.spin_veresiye_EditValueChanged);
            this.spin_veresiye.Click += new System.EventHandler(this.spin_veresiye_Click);
            // 
            // spin_pos
            // 
            this.spin_pos.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_pos.Location = new System.Drawing.Point(246, 106);
            this.spin_pos.Name = "spin_pos";
            this.spin_pos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spin_pos.Properties.Mask.BeepOnError = true;
            this.spin_pos.Properties.Mask.EditMask = "n";
            this.spin_pos.Size = new System.Drawing.Size(67, 20);
            this.spin_pos.TabIndex = 9;
            this.spin_pos.EditValueChanged += new System.EventHandler(this.spin_pos_EditValueChanged);
            this.spin_pos.Click += new System.EventHandler(this.spin_pos_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cb_staff);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.lbl_indirim);
            this.groupControl1.Controls.Add(this.cb_taksit);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.chk_hediye);
            this.groupControl1.Controls.Add(this.btn_veresiye);
            this.groupControl1.Controls.Add(this.btn_banka);
            this.groupControl1.Controls.Add(this.btn_pesin);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.txt_aciklama);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.cbox_banka);
            this.groupControl1.Controls.Add(this.spin_pos);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.spin_veresiye);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.spin_pesin);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 59);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(543, 316);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Ödeme Şekli";
            this.groupControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl1_Paint);
            // 
            // lbl_indirim
            // 
            this.lbl_indirim.Appearance.Font = new System.Drawing.Font("Tahoma", 18.25F);
            this.lbl_indirim.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lbl_indirim.Location = new System.Drawing.Point(121, 189);
            this.lbl_indirim.Name = "lbl_indirim";
            this.lbl_indirim.Size = new System.Drawing.Size(49, 29);
            this.lbl_indirim.TabIndex = 55;
            this.lbl_indirim.Text = "%50";
            // 
            // cb_taksit
            // 
            this.cb_taksit.Location = new System.Drawing.Point(334, 106);
            this.cb_taksit.Name = "cb_taksit";
            this.cb_taksit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_taksit.Size = new System.Drawing.Size(39, 20);
            this.cb_taksit.TabIndex = 10;
            this.cb_taksit.EditValueChanged += new System.EventHandler(this.cb_taksit_EditValueChanged);
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(334, 89);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(28, 13);
            this.labelControl8.TabIndex = 54;
            this.labelControl8.Text = "Taksit";
            // 
            // chk_hediye
            // 
            this.chk_hediye.Location = new System.Drawing.Point(408, 242);
            this.chk_hediye.Name = "chk_hediye";
            this.chk_hediye.Properties.Caption = "HEDİYE";
            this.chk_hediye.Size = new System.Drawing.Size(61, 19);
            this.chk_hediye.TabIndex = 52;
            this.chk_hediye.CheckedChanged += new System.EventHandler(this.chk_hediye_CheckedChanged);
            // 
            // btn_veresiye
            // 
            this.btn_veresiye.Location = new System.Drawing.Point(410, 147);
            this.btn_veresiye.Name = "btn_veresiye";
            this.btn_veresiye.Size = new System.Drawing.Size(93, 24);
            this.btn_veresiye.TabIndex = 18;
            this.btn_veresiye.Text = "Hepsi Veresiye";
            this.btn_veresiye.Click += new System.EventHandler(this.btn_veresiye_Click);
            // 
            // btn_banka
            // 
            this.btn_banka.Location = new System.Drawing.Point(410, 102);
            this.btn_banka.Name = "btn_banka";
            this.btn_banka.Size = new System.Drawing.Size(93, 24);
            this.btn_banka.TabIndex = 17;
            this.btn_banka.Text = "Hepsi Banka";
            this.btn_banka.Click += new System.EventHandler(this.btn_banka_Click);
            // 
            // btn_pesin
            // 
            this.btn_pesin.Location = new System.Drawing.Point(410, 61);
            this.btn_pesin.Name = "btn_pesin";
            this.btn_pesin.Size = new System.Drawing.Size(93, 24);
            this.btn_pesin.TabIndex = 16;
            this.btn_pesin.Text = "Hepsi Peşin";
            this.btn_pesin.Click += new System.EventHandler(this.btn_pesin_Click);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.labelControl5.Location = new System.Drawing.Point(36, 195);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(62, 22);
            this.labelControl5.TabIndex = 12;
            this.labelControl5.Text = "İndirim:";
            // 
            // txt_aciklama
            // 
            this.txt_aciklama.Location = new System.Drawing.Point(121, 242);
            this.txt_aciklama.Name = "txt_aciklama";
            this.txt_aciklama.Size = new System.Drawing.Size(192, 20);
            this.txt_aciklama.TabIndex = 13;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.labelControl1.Location = new System.Drawing.Point(36, 240);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(75, 22);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "Açıklama:";
            // 
            // frmSatisPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 419);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSatisPopup";
            this.Text = "Satış İşlemleri";
            this.Load += new System.EventHandler(this.frmSatis_Load);
            this.Shown += new System.EventHandler(this.frmSatisPopup_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cb_staff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_maliyet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbox_banka.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_pesin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_veresiye.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_pos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cb_taksit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_hediye.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_aciklama.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.LabelControl lbl_tutar;
        private DevExpress.XtraEditors.LabelControl lbl_table_number;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit cbox_banka;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SpinEdit spin_pesin;
        private DevExpress.XtraEditors.SpinEdit spin_veresiye;
        private DevExpress.XtraEditors.SpinEdit spin_pos;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btn_tamamla;
        private DevExpress.XtraEditors.SimpleButton btn_duzenle;
        private DevExpress.XtraEditors.TextEdit txt_aciklama;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton btn_veresiye;
        private DevExpress.XtraEditors.SimpleButton btn_banka;
        private DevExpress.XtraEditors.SimpleButton btn_pesin;
        private DevExpress.XtraEditors.LabelControl lbl_owner_name;
        private DevExpress.XtraEditors.CheckEdit chk_hediye;
        private DevExpress.XtraEditors.ComboBoxEdit cb_staff;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl lbl_maliyet;
        private DevExpress.XtraEditors.CheckEdit chk_maliyet;
        private DevExpress.XtraEditors.ComboBoxEdit cb_taksit;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl lbl_indirim;
    }
}