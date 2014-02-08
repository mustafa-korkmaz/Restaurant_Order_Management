namespace StockProgram.Tables
{
    partial class ucTableItem
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
            this.lbl_table_number = new DevExpress.XtraEditors.LabelControl();
            this.pnl_back = new DevExpress.XtraEditors.PanelControl();
            this.lbl_price = new DevExpress.XtraEditors.LabelControl();
            this.lbl_time = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_back)).BeginInit();
            this.pnl_back.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_table_number
            // 
            this.lbl_table_number.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lbl_table_number.Appearance.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.lbl_table_number.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.lbl_table_number.Location = new System.Drawing.Point(8, 6);
            this.lbl_table_number.Name = "lbl_table_number";
            this.lbl_table_number.Size = new System.Drawing.Size(40, 22);
            this.lbl_table_number.TabIndex = 3;
            this.lbl_table_number.Text = "Masa";
            // 
            // pnl_back
            // 
            this.pnl_back.Appearance.BackColor = System.Drawing.Color.White;
            this.pnl_back.Appearance.BackColor2 = System.Drawing.SystemColors.ButtonFace;
            this.pnl_back.Appearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.pnl_back.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.pnl_back.Appearance.Options.UseBackColor = true;
            this.pnl_back.Appearance.Options.UseBorderColor = true;
            this.pnl_back.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.pnl_back.Controls.Add(this.lbl_price);
            this.pnl_back.Controls.Add(this.lbl_time);
            this.pnl_back.Controls.Add(this.lbl_table_number);
            this.pnl_back.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_back.Location = new System.Drawing.Point(0, 0);
            this.pnl_back.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.pnl_back.LookAndFeel.UseDefaultLookAndFeel = false;
            this.pnl_back.Name = "pnl_back";
            this.pnl_back.Size = new System.Drawing.Size(182, 160);
            this.pnl_back.TabIndex = 19;
            this.pnl_back.Click += new System.EventHandler(this.pnl_back_Click);
            this.pnl_back.DoubleClick += new System.EventHandler(this.pnl_back_DoubleClick);
            // 
            // lbl_price
            // 
            this.lbl_price.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_price.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lbl_price.Appearance.Font = new System.Drawing.Font("Tahoma", 25.25F);
            this.lbl_price.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lbl_price.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lbl_price.Location = new System.Drawing.Point(23, 53);
            this.lbl_price.Name = "lbl_price";
            this.lbl_price.Size = new System.Drawing.Size(153, 41);
            this.lbl_price.TabIndex = 4;
            this.lbl_price.Text = "915,50 TL";
            this.lbl_price.Click += new System.EventHandler(this.lbl_price_Click);
            this.lbl_price.DoubleClick += new System.EventHandler(this.lbl_price_DoubleClick);
            // 
            // lbl_time
            // 
            this.lbl_time.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_time.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lbl_time.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Italic);
            this.lbl_time.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.lbl_time.Location = new System.Drawing.Point(113, 134);
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Size = new System.Drawing.Size(61, 18);
            this.lbl_time.TabIndex = 5;
            this.lbl_time.Text = "12:56:08";
            // 
            // ucTableItem
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Window;
            this.Appearance.BorderColor = System.Drawing.Color.Red;
            this.Appearance.Font = new System.Drawing.Font("Verdana", 11F);
            this.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseBorderColor = true;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_back);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucTableItem";
            this.Size = new System.Drawing.Size(182, 160);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_back)).EndInit();
            this.pnl_back.ResumeLayout(false);
            this.pnl_back.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lbl_table_number;
        private DevExpress.XtraEditors.PanelControl pnl_back;
        private DevExpress.XtraEditors.LabelControl lbl_time;
        private DevExpress.XtraEditors.LabelControl lbl_price;



    }
}
