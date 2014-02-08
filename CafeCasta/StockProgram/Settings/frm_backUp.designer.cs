namespace StockProgram.Settings
{
    partial class frm_backUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_backUp));
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.timerRead = new System.Windows.Forms.Timer(this.components);
            this.timerStop = new System.Windows.Forms.Timer(this.components);
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btn_backUp = new DevExpress.XtraEditors.SimpleButton();
            this.btn_restore = new DevExpress.XtraEditors.SimpleButton();
            this.btn_close = new DevExpress.XtraEditors.SimpleButton();
            this.lb_Progress = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar1.Location = new System.Drawing.Point(14, 36);
            this.ProgressBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(550, 38);
            this.ProgressBar1.TabIndex = 3;
            this.ProgressBar1.Click += new System.EventHandler(this.ProgressBar1_Click);
            // 
            // timerRead
            // 
            this.timerRead.Tick += new System.EventHandler(this.timerRead_Tick);
            // 
            // timerStop
            // 
            this.timerStop.Interval = 200;
            this.timerStop.Tick += new System.EventHandler(this.timerStop_Tick);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btn_backUp);
            this.panelControl2.Controls.Add(this.btn_restore);
            this.panelControl2.Controls.Add(this.btn_close);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 94);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(584, 44);
            this.panelControl2.TabIndex = 53;
            // 
            // btn_backUp
            // 
            this.btn_backUp.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_backUp.Image = global::StockProgram.Properties.Resources.back_up;
            this.btn_backUp.Location = new System.Drawing.Point(300, 2);
            this.btn_backUp.Name = "btn_backUp";
            this.btn_backUp.Size = new System.Drawing.Size(94, 40);
            this.btn_backUp.TabIndex = 12;
            this.btn_backUp.Text = "Yedekle";
            this.btn_backUp.Click += new System.EventHandler(this.btn_backUp_Click);
            // 
            // btn_restore
            // 
            this.btn_restore.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_restore.Image = ((System.Drawing.Image)(resources.GetObject("btn_restore.Image")));
            this.btn_restore.Location = new System.Drawing.Point(394, 2);
            this.btn_restore.Name = "btn_restore";
            this.btn_restore.Size = new System.Drawing.Size(94, 40);
            this.btn_restore.TabIndex = 11;
            this.btn_restore.Text = "Geri Yükle";
            this.btn_restore.Click += new System.EventHandler(this.btn_restore_Click);
            // 
            // btn_close
            // 
            this.btn_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_close.Image = global::StockProgram.Properties.Resources.delete;
            this.btn_close.Location = new System.Drawing.Point(488, 2);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(94, 40);
            this.btn_close.TabIndex = 10;
            this.btn_close.Text = "Kapat";
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // lb_Progress
            // 
            this.lb_Progress.AutoSize = true;
            this.lb_Progress.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Progress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.lb_Progress.Location = new System.Drawing.Point(12, 14);
            this.lb_Progress.Name = "lb_Progress";
            this.lb_Progress.Size = new System.Drawing.Size(49, 13);
            this.lb_Progress.TabIndex = 102;
            this.lb_Progress.Text = "Progress";
            // 
            // frm_backUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 138);
            this.Controls.Add(this.lb_Progress);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.ProgressBar1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frm_backUp";
            this.Text = "Yedekle / Geri Yükle";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.ProgressBar ProgressBar1;
        private System.Windows.Forms.Timer timerRead;
        private System.Windows.Forms.Timer timerStop;
        #endregion
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btn_backUp;
        private DevExpress.XtraEditors.SimpleButton btn_restore;
        private DevExpress.XtraEditors.SimpleButton btn_close;
        private System.Windows.Forms.Label lb_Progress;

    }
}