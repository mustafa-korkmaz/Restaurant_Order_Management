using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.Sales
{
    class CategoryTab:DevExpress.XtraTab.XtraTabPage
    {
        public int cat_id { get; set; }
        public CategoryTab(int cat_id,string name,string text)
        {
          this.Initialize(name,text);
          this.cat_id = cat_id;
        }


        private void Initialize(string name, string text)
        {
  
            this.SuspendLayout();
            this.ResumeLayout(false);
            this.AutoScroll = true;
   
       //     this.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 13.25F);
            this.Appearance.PageClient.BackColor = System.Drawing.SystemColors.MenuBar;
            this.Appearance.PageClient.Options.UseBackColor = true;
            this.Name = name;
            this.Text = text;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
    }

}
