namespace StockProgram.Products
{
    partial class ucPrintLabel
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
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.lbl_product_view = new DevExpress.XtraEditors.LabelControl();
            this.btn_back = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.grpCtrl_numara = new DevExpress.XtraEditors.GroupControl();
            this.lbl_counter = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpCtrl_numara)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_product_view);
            this.panel1.Controls.Add(this.btn_back);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(791, 39);
            this.panel1.TabIndex = 51;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lbl_product_view
            // 
            this.lbl_product_view.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_product_view.Location = new System.Drawing.Point(5, 8);
            this.lbl_product_view.Name = "lbl_product_view";
            this.lbl_product_view.Size = new System.Drawing.Size(96, 22);
            this.lbl_product_view.TabIndex = 2;
            this.lbl_product_view.Text = "Ürün İzleme";
            // 
            // btn_back
            // 
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_back.Image = global::StockProgram.Properties.Resources.buttn_back;
            this.btn_back.Location = new System.Drawing.Point(712, 2);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(77, 35);
            this.btn_back.TabIndex = 41;
            this.btn_back.Text = "Geri";
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lbl_counter);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 335);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(791, 39);
            this.panelControl1.TabIndex = 53;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl1.Location = new System.Drawing.Point(115, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(173, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Toplam Basılacak Etiket Sayısı:";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.simpleButton1.Image = global::StockProgram.Properties.Resources.small_barcode;
            this.simpleButton1.Location = new System.Drawing.Point(2, 2);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(99, 35);
            this.simpleButton1.TabIndex = 41;
            this.simpleButton1.Text = "Etiket Bas";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // grpCtrl_numara
            // 
            this.grpCtrl_numara.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCtrl_numara.Location = new System.Drawing.Point(0, 39);
            this.grpCtrl_numara.Name = "grpCtrl_numara";
            this.grpCtrl_numara.Size = new System.Drawing.Size(791, 296);
            this.grpCtrl_numara.TabIndex = 54;
            this.grpCtrl_numara.Text = "Mevcut Numaralar";
            // 
            // lbl_counter
            // 
            this.lbl_counter.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_counter.Location = new System.Drawing.Point(294, 10);
            this.lbl_counter.Name = "lbl_counter";
            this.lbl_counter.Size = new System.Drawing.Size(20, 18);
            this.lbl_counter.TabIndex = 42;
            this.lbl_counter.Text = "99";
            // 
            // ucPrintLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.grpCtrl_numara);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel1);
            this.Name = "ucPrintLabel";
            this.Size = new System.Drawing.Size(791, 374);
            this.Load += new System.EventHandler(this.ucViewProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpCtrl_numara)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.LabelControl lbl_product_view;
        private DevExpress.XtraEditors.SimpleButton btn_back;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lbl_counter;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.GroupControl grpCtrl_numara;
    }
}
