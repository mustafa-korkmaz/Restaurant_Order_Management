namespace StockProgram.Tables
{
    partial class ucTablesMainPage
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
            Timer.exitFlag = true; //dispose ederken timer ı devreden cıkaralım
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_refresh = new DevExpress.XtraEditors.SimpleButton();
            this.btn_back = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_header = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_checkout = new DevExpress.XtraEditors.SimpleButton();
            this.btn_new_order = new DevExpress.XtraEditors.SimpleButton();
            this.btn_add_order = new DevExpress.XtraEditors.SimpleButton();
            this.btn_print = new DevExpress.XtraEditors.SimpleButton();
            this.btn_order_list = new DevExpress.XtraEditors.SimpleButton();
            this.btn_change_table = new DevExpress.XtraEditors.SimpleButton();
            this.btn_cancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.pnl_total = new DevExpress.XtraEditors.PanelControl();
            this.lbl_all_checks_total = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.lbl_table_counter = new DevExpress.XtraEditors.LabelControl();
            this.lbl_dolu_masa = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.lbl_table = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_total)).BeginInit();
            this.pnl_total.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_refresh);
            this.panel1.Controls.Add(this.btn_back);
            this.panel1.Controls.Add(this.lbl_header);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(980, 39);
            this.panel1.TabIndex = 55;
            // 
            // btn_refresh
            // 
            this.btn_refresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_refresh.Image = global::StockProgram.Properties.Resources.refresh;
            this.btn_refresh.Location = new System.Drawing.Point(824, 2);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(77, 35);
            this.btn_refresh.TabIndex = 43;
            this.btn_refresh.Text = "Yenile";
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(901, 2);
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
            this.lbl_header.Size = new System.Drawing.Size(112, 22);
            this.lbl_header.TabIndex = 2;
            this.lbl_header.Text = "Masa İşlemleri";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_checkout);
            this.panelControl1.Controls.Add(this.btn_new_order);
            this.panelControl1.Controls.Add(this.btn_add_order);
            this.panelControl1.Controls.Add(this.btn_print);
            this.panelControl1.Controls.Add(this.btn_order_list);
            this.panelControl1.Controls.Add(this.btn_change_table);
            this.panelControl1.Controls.Add(this.btn_cancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 358);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(980, 42);
            this.panelControl1.TabIndex = 1;
            // 
            // btn_checkout
            // 
            this.btn_checkout.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_checkout.Image = global::StockProgram.Properties.Resources.cash_register;
            this.btn_checkout.Location = new System.Drawing.Point(173, 2);
            this.btn_checkout.Name = "btn_checkout";
            this.btn_checkout.Size = new System.Drawing.Size(115, 38);
            this.btn_checkout.TabIndex = 60;
            this.btn_checkout.Text = "Hesap Kes";
            this.btn_checkout.Click += new System.EventHandler(this.btn_checkout_Click);
            // 
            // btn_new_order
            // 
            this.btn_new_order.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_new_order.Image = global::StockProgram.Properties.Resources.plus_green;
            this.btn_new_order.Location = new System.Drawing.Point(288, 2);
            this.btn_new_order.Name = "btn_new_order";
            this.btn_new_order.Size = new System.Drawing.Size(115, 38);
            this.btn_new_order.TabIndex = 59;
            this.btn_new_order.Text = "Hesap Aç";
            this.btn_new_order.Click += new System.EventHandler(this.btn_new_order_Click);
            // 
            // btn_add_order
            // 
            this.btn_add_order.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_add_order.Image = global::StockProgram.Properties.Resources.add_blue;
            this.btn_add_order.Location = new System.Drawing.Point(403, 2);
            this.btn_add_order.Name = "btn_add_order";
            this.btn_add_order.Size = new System.Drawing.Size(115, 38);
            this.btn_add_order.TabIndex = 20;
            this.btn_add_order.Text = "Sipariş Ekle";
            this.btn_add_order.Click += new System.EventHandler(this.btn_add_order_Click);
            // 
            // btn_print
            // 
            this.btn_print.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_print.Image = global::StockProgram.Properties.Resources.print;
            this.btn_print.Location = new System.Drawing.Point(518, 2);
            this.btn_print.Name = "btn_print";
            this.btn_print.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_print.Size = new System.Drawing.Size(115, 38);
            this.btn_print.TabIndex = 19;
            this.btn_print.Text = "Adisyon Yazdır";
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // btn_order_list
            // 
            this.btn_order_list.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_order_list.Image = global::StockProgram.Properties.Resources.to_do_list_cheked_all;
            this.btn_order_list.Location = new System.Drawing.Point(633, 2);
            this.btn_order_list.Name = "btn_order_list";
            this.btn_order_list.Size = new System.Drawing.Size(115, 38);
            this.btn_order_list.TabIndex = 18;
            this.btn_order_list.Text = "Sipariş Listesi";
            this.btn_order_list.Click += new System.EventHandler(this.btn_order_list_Click);
            // 
            // btn_change_table
            // 
            this.btn_change_table.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_change_table.Image = global::StockProgram.Properties.Resources._switch;
            this.btn_change_table.Location = new System.Drawing.Point(748, 2);
            this.btn_change_table.Name = "btn_change_table";
            this.btn_change_table.Size = new System.Drawing.Size(115, 38);
            this.btn_change_table.TabIndex = 16;
            this.btn_change_table.Text = "Hesap Aktar";
            this.btn_change_table.Click += new System.EventHandler(this.btn_change_table_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_cancel.Image = global::StockProgram.Properties.Resources.delete;
            this.btn_cancel.Location = new System.Drawing.Point(863, 2);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(115, 38);
            this.btn_cancel.TabIndex = 15;
            this.btn_cancel.Text = "Hesabı İptal Et";
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.tabControl);
            this.panelControl2.Controls.Add(this.panelControl5);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 39);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(980, 319);
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
            this.tabControl.Size = new System.Drawing.Size(980, 277);
            this.tabControl.TabIndex = 57;
            // 
            // panelControl5
            // 
            this.panelControl5.Controls.Add(this.pnl_total);
            this.panelControl5.Controls.Add(this.panelControl4);
            this.panelControl5.Controls.Add(this.panelControl3);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl5.Location = new System.Drawing.Point(0, 277);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(980, 42);
            this.panelControl5.TabIndex = 56;
            // 
            // pnl_total
            // 
            this.pnl_total.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_total.Controls.Add(this.lbl_all_checks_total);
            this.pnl_total.Controls.Add(this.labelControl2);
            this.pnl_total.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnl_total.Location = new System.Drawing.Point(763, 2);
            this.pnl_total.Name = "pnl_total";
            this.pnl_total.Padding = new System.Windows.Forms.Padding(10, 5, 10, 0);
            this.pnl_total.Size = new System.Drawing.Size(215, 38);
            this.pnl_total.TabIndex = 59;
            // 
            // lbl_all_checks_total
            // 
            this.lbl_all_checks_total.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_all_checks_total.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lbl_all_checks_total.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_all_checks_total.Location = new System.Drawing.Point(124, 5);
            this.lbl_all_checks_total.Name = "lbl_all_checks_total";
            this.lbl_all_checks_total.Size = new System.Drawing.Size(81, 22);
            this.lbl_all_checks_total.TabIndex = 22;
            this.lbl_all_checks_total.Text = "192,90 TL";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl2.Location = new System.Drawing.Point(10, 5);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(111, 22);
            this.labelControl2.TabIndex = 21;
            this.labelControl2.Text = "Açık Hesaplar:";
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.lbl_table_counter);
            this.panelControl4.Controls.Add(this.lbl_dolu_masa);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl4.Location = new System.Drawing.Point(287, 2);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Padding = new System.Windows.Forms.Padding(10, 5, 10, 0);
            this.panelControl4.Size = new System.Drawing.Size(181, 38);
            this.panelControl4.TabIndex = 58;
            // 
            // lbl_table_counter
            // 
            this.lbl_table_counter.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_table_counter.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lbl_table_counter.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_table_counter.Location = new System.Drawing.Point(151, 5);
            this.lbl_table_counter.Name = "lbl_table_counter";
            this.lbl_table_counter.Size = new System.Drawing.Size(20, 22);
            this.lbl_table_counter.TabIndex = 22;
            this.lbl_table_counter.Text = "12";
            // 
            // lbl_dolu_masa
            // 
            this.lbl_dolu_masa.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_dolu_masa.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_dolu_masa.Location = new System.Drawing.Point(10, 5);
            this.lbl_dolu_masa.Name = "lbl_dolu_masa";
            this.lbl_dolu_masa.Size = new System.Drawing.Size(138, 22);
            this.lbl_dolu_masa.TabIndex = 21;
            this.lbl_dolu_masa.Text = "Dolu Masa Sayısı:";
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.lbl_table);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl3.Location = new System.Drawing.Point(2, 2);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Padding = new System.Windows.Forms.Padding(10, 5, 0, 0);
            this.panelControl3.Size = new System.Drawing.Size(285, 38);
            this.panelControl3.TabIndex = 57;
            // 
            // lbl_table
            // 
            this.lbl_table.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_table.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_table.Location = new System.Drawing.Point(10, 5);
            this.lbl_table.Name = "lbl_table";
            this.lbl_table.Size = new System.Drawing.Size(99, 22);
            this.lbl_table.TabIndex = 21;
            this.lbl_table.Text = "Masa Seçiniz";
            // 
            // ucTablesMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel1);
            this.Name = "ucTablesMainPage";
            this.Size = new System.Drawing.Size(980, 400);
            this.Load += new System.EventHandler(this.ucTablesMainPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_total)).EndInit();
            this.pnl_total.ResumeLayout(false);
            this.pnl_total.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.SimpleButton btn_back;
        private DevExpress.XtraEditors.LabelControl lbl_header;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btn_add_order;
        private DevExpress.XtraEditors.SimpleButton btn_print;
        private DevExpress.XtraEditors.SimpleButton btn_order_list;
        private DevExpress.XtraEditors.SimpleButton btn_change_table;
        private DevExpress.XtraEditors.SimpleButton btn_cancel;
        private DevExpress.XtraEditors.SimpleButton btn_refresh;
        private DevExpress.XtraEditors.SimpleButton btn_new_order;
        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.LabelControl lbl_table_counter;
        private DevExpress.XtraEditors.LabelControl lbl_dolu_masa;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl lbl_table;
        private DevExpress.XtraEditors.SimpleButton btn_checkout;
        private DevExpress.XtraEditors.PanelControl pnl_total;
        private DevExpress.XtraEditors.LabelControl lbl_all_checks_total;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}
