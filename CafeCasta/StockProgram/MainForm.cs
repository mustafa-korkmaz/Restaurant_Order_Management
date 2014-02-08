using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using System.Diagnostics;
using System.IO;

namespace StockProgram
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.KeyPreview = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.DesignMode)
                {
                    //pic_main.Visible = false;
                    pnl_left.Visible = false;
                    MainFormUserControls.ucLogin login = new MainFormUserControls.ucLogin();
                    login.Dock = DockStyle.Fill;
                    pnl_main.Controls.Add(login);
          
                }
            }
            catch
            {
                throw;
            }
        }

        private void ClearControl(PanelControl pnl)
        {
            //foreach (DevExpress.XtraEditors.XtraUserControl item in pnl.Controls)
            //{
            //    StaticObjects.ClearControl(item);
            //}
            foreach (Control item in pnl.Controls)
            {
                StaticObjects.ClearControl(item);
            }
        }
        #region SettingStatus
        public void SettingStatus()
        {
            InitializeNavBar();
            if (StaticObjects.Settings.mainImageName == "")
            {
                GetGeneralSettings();
            }
            DevExpress.XtraEditors.PictureEdit pe = new   DevExpress.XtraEditors.PictureEdit();
            pe.Dock = System.Windows.Forms.DockStyle.Fill;
         //   pe.EditValue = global::StockProgram.Properties.Resources.cengiz;            
            //  pe.EditValue = global::StockProgram.Properties.Resources.main;
            pe.Image = (File.Exists(Application.StartupPath + StaticObjects.MainImagePath + StaticObjects.Settings.mainImageName)) ? Image.FromFile(Application.StartupPath + StaticObjects.MainImagePath + StaticObjects.Settings.mainImageName) : null;
            pe.Location = new System.Drawing.Point(2, 2);
            pe.MenuManager = this.barManager1;
            pe.Name = "pe";
            pe.Size = new System.Drawing.Size(804, 491);
            pe.TabIndex = 0;
            bar_user.Caption = StaticObjects.User.Name;
            pnl_main.Controls.Add(pe);
        }
        #endregion

        private void pnl_main_Paint(object sender, PaintEventArgs e)
        {

        }

        private void InitializeNavBar()
        {
            pnl_left.Visible = true;
        }

        private void btn_depo65555555_Click(object sender, EventArgs e)
        {
            //ClearControl(this.pnl_master);
            Warehouses.ucWarehousesMainPage ctrl = new Warehouses.ucWarehousesMainPage();
            ctrl.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl);
        }

        private void GetGeneralSettings()
        {
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            string sql = "select * from settings where 1";
            try
            {
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    StaticObjects.Settings.mainImageName = dt.Rows[0]["main_form_img_path"].ToString();
                    StaticObjects.Settings.table_height = Convert.ToInt16(dt.Rows[0]["table_height"]);
                    StaticObjects.Settings.table_width = Convert.ToInt16(dt.Rows[0]["table_width"]);
                    StaticObjects.Settings.table_count_per_line = Convert.ToInt16(dt.Rows[0]["table_count_per_line"]);
                    StaticObjects.Settings.tables_refresh_time = Convert.ToInt16(dt.Rows[0]["tables_refresh_time"]);
                    StaticObjects.Settings.menu_item_height = Convert.ToInt16(dt.Rows[0]["menu_item_height"]);
                    StaticObjects.Settings.menu_item_width = Convert.ToInt16(dt.Rows[0]["menu_item_width"]);
                    StaticObjects.Settings.menu_item_count_per_line = Convert.ToInt16(dt.Rows[0]["menu_item_count_per_line"]);
                    StaticObjects.Settings.menu_item_name_panel_height = Convert.ToInt16(dt.Rows[0]["menu_item_name_panel_height"]);
                }

            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                //excLogger = new DBObjects.ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at
            }
            finally
            {
                db.Close();
                db = null;
                dt.Dispose();
            }

        }
        private void btn_tedarikci_Click_1(object sender, EventArgs e)
        {
            ClearControl(pnl_main);
            Suppliers.ucSuppliersMainPage ctrl = new Suppliers.ucSuppliersMainPage();
            ctrl.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl);
        }

        private void btn_rapor_Click_1(object sender, EventArgs e)
        {
            ClearControl(pnl_main);
            Reports.ucReportMainPage ctrl = new Reports.ucReportMainPage();
            ctrl.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl);
        }


        private void navBarItemVeresiye_LinkClicked(object sender, EventArgs e)
        {
            ClearControl(pnl_main);
            Customers.ucCustomersMainPage ctrl = new Customers.ucCustomersMainPage();
            ctrl.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl);
        }
        private void btn_satis_Click(object sender, EventArgs e)
        {
            ClearControl(pnl_main);
            Sales.ucSalesMasterPage ctrl = new Sales.ucSalesMasterPage();
            ctrl.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl);
        }

        private void btn_urun_Click(object sender, EventArgs e)
        {
            ClearControl(pnl_main);
            Products.ucProductsMasterPage ctrl = new Products.ucProductsMasterPage();
            ctrl.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl);
        }

        private void btn_kategori_Click(object sender, EventArgs e)
        {
            ClearControl(pnl_main);
            Categories.ucCategoriesMasterPage ctrl = new Categories.ucCategoriesMasterPage();
            ctrl.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl);
        }

        private void btn_banka_Click(object sender, EventArgs e)
        {
            ClearControl(pnl_main);
            Banks.ucBanksMasterPage ctrl = new Banks.ucBanksMasterPage();
            ctrl.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl);
        }

        private void navBarItemSatis_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ClearControl(pnl_main);
            Sales.ucSaleOptions ctrl = new Sales.ucSaleOptions();
            ctrl.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl);
        }

        private void navBarItemUrunler_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ClearControl(pnl_main);
            Products.ucProductsMasterPage ctrl = new Products.ucProductsMasterPage();
            ctrl.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl);
        }

        private void navBarItemTedarikci_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ClearControl(pnl_main);
            Suppliers.ucSuppliersMainPage ctrl = new Suppliers.ucSuppliersMainPage();
            ctrl.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl);
        }

        private void navBarItemKategori_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ClearControl(pnl_main);
            Categories.ucCategoriesMasterPage ctrl = new Categories.ucCategoriesMasterPage();
            ctrl.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl);
        }

        private void navBarItemBanka_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ClearControl(pnl_main);
            Banks.ucBanksMasterPage ctrl = new Banks.ucBanksMasterPage();
            ctrl.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl);
        }

        private void navBarItemRapor_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ClearControl(pnl_main);
            Reports.ucReportMainPage ctrl = new Reports.ucReportMainPage();
            ctrl.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl);
        }

        private void navBarControl1_NavPaneStateChanged(object sender, EventArgs e)
        {
            Size size = new Size();
            size.Height = 0;
            size.Width = 0;
            if (navBarControl1.OptionsNavPane.NavPaneState == NavPaneState.Collapsed)
            {
                size.Width = Convert.ToInt32(pnl_left.Size.Width * 0.75); // 4 te 3 oranında küçülsün
                size.Height = pnl_left.Size.Height;
                pnl_left.Size = pnl_left.Size - size;
            }
            if (navBarControl1.OptionsNavPane.NavPaneState == NavPaneState.Expanded)
            {
                size.Width = Convert.ToInt32(pnl_left.Size.Width * 3);
                size.Height = pnl_left.Size.Height;
                pnl_left.Size = pnl_left.Size + size;
            }
        }

        private void navBarItemGunluk_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            ClearControl(pnl_main);
            DailySales.ucGunlukSatis ctrl = new DailySales.ucGunlukSatis();
            ctrl.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl);
        }

        private void navBarItemSettings_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            ClearControl(pnl_main);
            Settings.ucSettings ctrl = new Settings.ucSettings();
            ctrl.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl);
        }

        private void navBarGider_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            ClearControl(pnl_main);
            Expenses.ucExpensesMainPage ctrl = new Expenses.ucExpensesMainPage();
            ctrl.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(ctrl);
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)  //ürün izleme ekranına getirelecek
            {
                if (StaticObjects.User.IsLoggedIn)
                {
                    ClearControl(pnl_main);
                    Menu.ucMenu ctrl = new Menu.ucMenu(Enums.OrderType.Paket);
                    ctrl.Dock = DockStyle.Fill;
                    pnl_main.Controls.Add(ctrl);
                    navBarControl1.OptionsNavPane.NavPaneState = NavPaneState.Collapsed;
                }
                else
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Önce sisteme giriş yapınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                }
            }
            if (e.KeyCode == Keys.F2)  //ürün izleme ekranına getirelecek
            {
                if(StaticObjects.User.IsLoggedIn)
                {
                ClearControl(pnl_main);
                Tables.ucTablesMainPage ctrl = new Tables.ucTablesMainPage();
                ctrl.Dock = DockStyle.Fill;
                pnl_main.Controls.Add(ctrl);
                navBarControl1.OptionsNavPane.NavPaneState = NavPaneState.Collapsed;
                }   
                else
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Önce sisteme giriş yapınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                }
            }
            if (e.KeyCode==Keys.F3)
            {
                if (StaticObjects.User.IsLoggedIn)
                {
                ClearControl(pnl_main);
                DailySales.ucOpenOrders ctrl = new DailySales.ucOpenOrders();
                ctrl.Dock = DockStyle.Fill;
                pnl_main.Controls.Add(ctrl);
                navBarControl1.OptionsNavPane.NavPaneState = NavPaneState.Collapsed;
                }   
                else
                {
                    ErrorMessages.Message message = new ErrorMessages.Message();
                    message.WriteMessage("Önce sisteme giriş yapınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                }
            }
      
        }
        
    }
}