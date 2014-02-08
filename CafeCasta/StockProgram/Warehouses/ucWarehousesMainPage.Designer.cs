namespace StockProgram.Warehouses
{
    partial class ucWarehousesMainPage
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btn_depo_gir = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(46, 51);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(67, 13);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "Depo İşlemleri";
            // 
            // btn_depo_gir
            // 
            this.btn_depo_gir.Image = global::StockProgram.Properties.Resources.add_blue;
            this.btn_depo_gir.Location = new System.Drawing.Point(97, 100);
            this.btn_depo_gir.Name = "btn_depo_gir";
            this.btn_depo_gir.Size = new System.Drawing.Size(140, 57);
            this.btn_depo_gir.TabIndex = 7;
            this.btn_depo_gir.Text = "Depo Ekle";
            this.btn_depo_gir.Click += new System.EventHandler(this.btn_depo_gir_Click);
            // 
            // ucWarehousesMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btn_depo_gir);
            this.Name = "ucWarehousesMainPage";
            this.Size = new System.Drawing.Size(560, 391);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_depo_gir;
    }
}
