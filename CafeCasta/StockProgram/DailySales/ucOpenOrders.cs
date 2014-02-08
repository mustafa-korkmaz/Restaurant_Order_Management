using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StockProgram.DBObjects;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;

namespace StockProgram.DailySales
{
    public partial class ucOpenOrders : DevExpress.XtraEditors.XtraUserControl
    {
        private bool firstLoad;
        private bool resTabfirstLoad;
        private GridView detailsView;
        private GridView detailsViewRestaurant;
        private GridView detailsViewRestaurantOrder;
        private ExceptionLogger excLogger;
        public double totalReturnPrice;
        public ucOpenOrders()
        {
            InitializeComponent();
        }

        private void ucGunlukSatis_Load(object sender, EventArgs e)
        {
            firstLoad = true;
            resTabfirstLoad = true;
            FillCourierGrid();
            FillRestaurantGrid();
        }

      
        /// <summary>
        /// fills the grid between the given date time
        /// </summary>
        /// <param name="dateTime"></param>
        private void FillCourierGrid()
        {
            //string dayOne = DateTime.Now.ToString("yyyy-MM-d");
            //string dayTwo = DateTime.Now.AddDays(1).ToString("yyyy-MM-d");

            string strSQL = "select om.*, TIME (om.account_create_date) as time_of_day from v_orders_master om where om.account_status=1 and om.account_type=2 and om.is_deleted=0; ";
            strSQL += " select * from v_product_to_order where  account_status=1 and account_type=2 and is_deleted=0  order by product_name asc;";
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataSet ds = new DataSet();
            try
            {
                ds = db.GetDataSet(strSQL);
             //   SetProductCodesColumn(ref ds);// ürün kodlarını barındıran kolonu hazırla
                gridView1.RowHeight = 35;
                //set relations
                DataColumn keyColumn = ds.Tables[0].Columns["order_id"];
                DataColumn foreignKeyColumn = ds.Tables[1].Columns["order_id"];
                ds.Relations.Add("PurchaseDetails", keyColumn, foreignKeyColumn);

                gridControl1.DataSource = ds.Tables[0];
             //   SetConditionalFormatting("[total]");
                gridControl1.ForceInitialize();
                gridView1.BestFitColumns();
                detailsView = new GridView(gridControl1);
                //Specify text to be displayed within detail tabs.
                SetDetailsView("Sipariş Kalemleri", ref ds);
                gridControl1.LevelTree.Nodes.Add("PurchaseDetails", detailsView);
               

                if (firstLoad)
                {
               
                    //paket servis tabı için
                    repo_button.Buttons[0].Image = global::StockProgram.Properties.Resources.cash_register_small;
                    repo_button.Buttons[0].Caption = "Ödeme Al";                
                    repo_button.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    repo_button.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());
                    repo_button.Buttons[1].Image = global::StockProgram.Properties.Resources.print_small;
                    repo_button.Buttons[1].Caption = "Yazdır";
                    repo_button.Buttons[1].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    repo_button.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());
                    repo_button.Buttons[2].Image = global::StockProgram.Properties.Resources.edit_small;
                    repo_button.Buttons[2].Caption = "Düzenle";
                    repo_button.Buttons[2].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    repo_button.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());
                    repo_button.Buttons[3].Image = global::StockProgram.Properties.Resources.delete;
                    repo_button.Buttons[3].Caption = "Sil";
                    repo_button.Buttons[3].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    repo_button.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
                    repo_button.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repo_button_ButtonClick);
                }
                firstLoad = false;
            
            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new DBObjects.ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at

                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);

            }
            finally
            {
                ds.Dispose();
                db.Close();
                db = null;
            }
      
        }

        /// <summary>
        /// fills the grid between the given date time
        /// </summary>
        /// <param name="dateTime"></param>
        private void FillRestaurantGrid()
        {
            //string dayOne = DateTime.Now.ToString("yyyy-MM-d");
            //string dayTwo = DateTime.Now.AddDays(1).ToString("yyyy-MM-d");
            string strSQL = "select am.*, TIME (am.account_create_date) as time_of_day from v_accounts_master am where am.account_status=1 and am.account_type=1 ;";
            strSQL += "select om.*, TIME (om.order_create_date) as time_of_day from v_orders_master om where om.account_status=1 and om.account_type=1 and om.is_deleted=0;";
            strSQL += " select * from v_product_to_order where account_status=1 and account_type=1 and is_deleted=0 order by product_name asc";
            DBObjects.MySqlDbHelper db = new DBObjects.MySqlDbHelper(StaticObjects.MySqlConn);
            DataSet ds = new DataSet();
            try
            {
                ds = db.GetDataSet(strSQL);
                gridView2.RowHeight = 35;
                //set relations
                DataColumn keyColumn0= ds.Tables[0].Columns["account_id"];
                DataColumn foreignKeyColumn0 = ds.Tables[1].Columns["account_id"];
                ds.Relations.Add("AccountDetails", keyColumn0, foreignKeyColumn0);

                DataColumn keyColumn1 = ds.Tables[1].Columns["order_id"];
                DataColumn foreignKeyColumn1 = ds.Tables[2].Columns["order_id"];
                ds.Relations.Add("OrderDetails", keyColumn1, foreignKeyColumn1);

                detailsViewRestaurant = new GridView(gridControl2);
                detailsViewRestaurantOrder = new GridView(gridControl2);

                gridControl2.DataSource = ds.Tables[0];
                //   SetConditionalFormatting("[total]");
                gridControl2.ForceInitialize();
                gridView2.BestFitColumns();

                SetDetailsViewRestaurant("Siparişler", ref ds);
                gridControl2.LevelTree.Nodes.Add("AccountDetails", detailsViewRestaurant);

                //Specify text to be displayed within detail tabs.
                detailsViewRestaurantOrder.PopulateColumns(ds.Tables[2]);
                gridControl2.LevelTree.Nodes[1].Nodes.Add("OrderDetails", detailsViewRestaurantOrder);
                SetDetailsViewRestaurantOrder("Sipariş Kalemleri", ref ds);

              
                if (resTabfirstLoad)
                {

                    //restoran içi servis tabı için
                    repo_button_restaurant.Buttons[0].Image = global::StockProgram.Properties.Resources.cash_register_small;
                    repo_button_restaurant.Buttons[0].Caption = "Ödeme Al";
                    repo_button_restaurant.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    repo_button_restaurant.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());
                    repo_button_restaurant.Buttons[1].Image = global::StockProgram.Properties.Resources.print_small;
                    repo_button_restaurant.Buttons[1].Caption = "Yazdır";
                    repo_button_restaurant.Buttons[1].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    repo_button_restaurant.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());
                    repo_button_restaurant.Buttons[2].Image = global::StockProgram.Properties.Resources.add_blue_small;
                    repo_button_restaurant.Buttons[2].Caption = "İlave";
                    repo_button_restaurant.Buttons[2].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    repo_button_restaurant.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());
                    repo_button_restaurant.Buttons[3].Image = global::StockProgram.Properties.Resources.delete;
                    repo_button_restaurant.Buttons[3].Caption = "Sil";
                    repo_button_restaurant.Buttons[3].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                 
                    repo_button_restaurant.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
                    repo_button_restaurant.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(root_repo_button_restaurant_ButtonClick);
                }
                resTabfirstLoad = false;

            }
            catch (Exception e)
            {
                string excSource = this.GetType().ToString() + ": " + new StackFrame().GetMethod().Name;
                excLogger = new DBObjects.ExceptionLogger(e.Message, excSource);// DB ye log yaz
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "İsmail Ahmet Stok Programı Hatası";
                excMail.ErrorSource = excSource + "()";
                excMail.Send(); // Mail at

                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);

            }
            finally
            {
                ds.Dispose();
                db.Close();
                db = null;
            }

        }

        void root_repo_button_restaurant_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        { // açık hesaplar için
            DataRow dr = gridView2.GetFocusedDataRow();
             int account_id = Convert.ToInt32(dr["account_id"]);
            int table_id = Convert.ToInt32(dr["owner_id"]);
            string table_name = dr["owner_name"].ToString();
            string table_category_name = dr["table_category_name"].ToString();

            switch (e.Button.Caption)
            {
                case "Ödeme Al":
                   Checkout(account_id, table_id, table_name, Enums.OrderType.Restoran);
                    break;
                case "Düzenle":

            //        EditOrder(order_id, table_id, table_name, Enums.OrderType.Restoran);
                    break;
                case "İlave":
                    AddNewOrder(table_id, table_name, Enums.OrderType.Restoran);
                    break;
                case "Yazdır":
                   PrintKitchenSlipForAllOrders(account_id, table_id, table_name, table_category_name,Enums.OrderType.Restoran);
                    break;

                case "Sil":
                    ErrorMessages.Message msg = new ErrorMessages.Message();
                    if (msg.WriteMessage("Açılmış bir hesabı silme işlemini onaylıyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                      DeleteAccount(account_id,table_id);
                    }
                    break;
                default:
                    break;
            }
        }
      
        void repo_button_restaurant_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {  //siparişler için
            GridView details = gridControl2.FocusedView as GridView;
            DataRow dr = details.GetFocusedDataRow();
            int order_id = 0;
            order_id = Convert.ToInt32(dr["order_id"]);
            int table_id = Convert.ToInt32(dr["owner_id"]);
            string table_name = dr["table_name"].ToString();
              
            switch (e.Button.Caption)
            {
        
                case "Düzenle":
                 
                    EditOrder(order_id, table_id, table_name,Enums.OrderType.Restoran);
                    break;
                case "Yazdır":
                    PrintKitchenSlip(order_id, table_id, table_name, Enums.OrderType.Restoran);
                    break;
       
                case "Sil":
                    ErrorMessages.Message msg = new ErrorMessages.Message();
                    if (msg.WriteMessage("Sipariş silme işlemini onaylıyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        DeleteOrder(order_id, 2);
                    }
                    break;
                default:
                    break;
            }
        }
        void repo_button_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            int order_id = 0;
            order_id = Convert.ToInt32(dr["order_id"]);
            int account_id = Convert.ToInt32(dr["account_id"]);
            int customer_id = Convert.ToInt32(dr["owner_id"]);
            string customer_name = dr["customer_name"].ToString();

            switch (e.Button.Caption)
            {
                case "Ödeme Al":
                    Checkout(account_id, customer_id, customer_name, Enums.OrderType.Paket);
                    break;
                case "Düzenle":
                  
                    EditOrder(order_id, customer_id, customer_name,Enums.OrderType.Paket);
                    break;
                case "Yazdır":
                    PrintKitchenSlip(order_id, customer_id,customer_name, Enums.OrderType.Paket);
              
                    break;
           
                case "Sil":
                    ErrorMessages.Message msg = new ErrorMessages.Message();
                    if (msg.WriteMessage("Sipariş silme işlemini onaylıyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        DeleteOrder(order_id,1);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// details gridinin kolonlarını ve özelliklerini set eder
        /// </summary>
        /// <param name="viewHeader"></param>
        /// <param name="ds"></param>
        private void SetDetailsView(string viewHeader, ref DataSet ds)
        {
            //add repo button
            DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit details_repo_button = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit(); ;
            //details_repo_button.Buttons[0].Image = global::StockProgram.Properties.Resources.edit_small;
            //details_repo_button.Buttons[0].Caption = "Düzenle";
            //details_repo_button.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            //details_repo_button.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());
            details_repo_button.Buttons[0].Image = global::StockProgram.Properties.Resources.delete;
            details_repo_button.Buttons[0].Caption = "Sil";
            details_repo_button.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            details_repo_button.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            ds.Tables[1].Columns.Add("actions");

            detailsView.ViewCaption = viewHeader;
            detailsView.PopulateColumns(ds.Tables[1]);

            detailsView.Columns["actions"].ColumnEdit = details_repo_button;
            detailsView.Columns["is_deleted"].VisibleIndex = -1;
            detailsView.Columns["account_id"].VisibleIndex = -1;
            detailsView.Columns["order_staff_name"].VisibleIndex = -1;
            detailsView.Columns["staff_id"].VisibleIndex = -1;
            detailsView.Columns["account_status"].VisibleIndex = -1;
            detailsView.Columns["account_type"].VisibleIndex = -1;
            detailsView.Columns["order_status"].VisibleIndex = -1;
            detailsView.Columns["log_id"].VisibleIndex = -1; 
            detailsView.Columns["order_id"].VisibleIndex = -1;      
            detailsView.Columns["product_id"].VisibleIndex = -1;
            detailsView.Columns["order_type"].VisibleIndex = -1;

            //product_name column
            detailsView.Columns["product_name"].Caption = "Ürün Adı";
            detailsView.Columns["product_name"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_name"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["product_name"].VisibleIndex = 0;

            //amount column
            detailsView.Columns["amount"].Caption = "Miktar";
            detailsView.Columns["amount"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["amount"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["amount"].VisibleIndex = 4;

            //porsion column
            detailsView.Columns["porsion"].Caption = "Porsiyon";
            detailsView.Columns["porsion"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["porsion"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["porsion"].VisibleIndex = 2;

            //product_price column
            detailsView.Columns["product_price"].Caption = "Birim Satış Fiyatı";
            detailsView.Columns["product_price"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_price"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["product_price"].VisibleIndex = 3;

            //product_desc column
            detailsView.Columns["product_desc"].Caption = "Seçenek";
            detailsView.Columns["product_desc"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["product_desc"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["product_desc"].VisibleIndex = 1;

            //total_price column
            detailsView.Columns["total_price"].Caption = "Tutar";
            detailsView.Columns["total_price"].OptionsColumn.AllowEdit = false;
            detailsView.Columns["total_price"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["total_price"].VisibleIndex = 5;

            //actions column
            detailsView.Columns["actions"].Caption = "İşlemler";
            detailsView.Columns["actions"].OptionsColumn.ReadOnly = false;
            detailsView.Columns["actions"].VisibleIndex = 6;

            detailsView.BestFitColumns();
            detailsView.OptionsView.ShowGroupPanel = false;
            details_repo_button.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(details_repo_button_ButtonClick);
        }

        /// <summary>
        /// details gridinin kolonlarını ve özelliklerini set eder
        /// </summary>
        /// <param name="viewHeader"></param>
        /// <param name="ds"></param>
        private void SetDetailsViewRestaurant(string viewHeader, ref DataSet ds)
        {
            //add repo button for orders
            DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit details_repo_button = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit(); ;
            details_repo_button.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());
            details_repo_button.Buttons[0].Image = global::StockProgram.Properties.Resources.print_small;
            details_repo_button.Buttons[0].Caption = "Yazdır";
            details_repo_button.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            details_repo_button.Buttons[1].Image = global::StockProgram.Properties.Resources.edit_small;
            details_repo_button.Buttons[1].Caption = "Düzenle";
            details_repo_button.Buttons[1].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            details_repo_button.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());
            details_repo_button.Buttons[2].Image = global::StockProgram.Properties.Resources.delete;
            details_repo_button.Buttons[2].Caption = "Sil";
            details_repo_button.Buttons[2].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            details_repo_button.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            ds.Tables[1].Columns.Add("actions");

            detailsViewRestaurant.ViewCaption = viewHeader;
            detailsViewRestaurant.PopulateColumns(ds.Tables[1]);

            detailsViewRestaurant.Columns["actions"].ColumnEdit = details_repo_button;
            detailsViewRestaurant.Columns["is_deleted"].VisibleIndex = -1;
            detailsViewRestaurant.Columns["table_category_name"].VisibleIndex = -1;
            detailsViewRestaurant.Columns["account_status"].VisibleIndex = -1;
            detailsViewRestaurant.Columns["account_type"].VisibleIndex = -1;
            detailsViewRestaurant.Columns["order_status"].VisibleIndex = -1;
            detailsViewRestaurant.Columns["order_type"].VisibleIndex = -1;
            detailsViewRestaurant.Columns["account_id"].VisibleIndex = -1;
            detailsViewRestaurant.Columns["table_name"].VisibleIndex = -1;
            detailsViewRestaurant.Columns["account_staff_name"].VisibleIndex = -1;
            detailsViewRestaurant.Columns["owner_id"].VisibleIndex = -1;
            detailsViewRestaurant.Columns["customer_name"].VisibleIndex = -1;
            detailsViewRestaurant.Columns["account_create_date"].VisibleIndex = -1;
            detailsViewRestaurant.Columns["order_modified_date"].VisibleIndex = -1;
            detailsViewRestaurant.Columns["order_create_date"].VisibleIndex = -1;
            detailsViewRestaurant.Columns["account_staff_id"].VisibleIndex = -1;
            detailsViewRestaurant.Columns["order_staff_id"].VisibleIndex = -1;
            detailsViewRestaurant.Columns["status_name"].VisibleIndex = -1;

            //order_id column
            detailsViewRestaurant.Columns["order_id"].Caption = "Sipariş No.";
            detailsViewRestaurant.Columns["order_id"].OptionsColumn.AllowEdit = false;
            detailsViewRestaurant.Columns["order_id"].OptionsColumn.ReadOnly = false;
            detailsViewRestaurant.Columns["order_id"].VisibleIndex = 0;

            //order_staff_name column
            detailsViewRestaurant.Columns["order_staff_name"].Caption = "Siparişi Alan";
            detailsViewRestaurant.Columns["order_staff_name"].OptionsColumn.AllowEdit = false;
            detailsViewRestaurant.Columns["order_staff_name"].OptionsColumn.ReadOnly = false;
            detailsViewRestaurant.Columns["order_staff_name"].VisibleIndex = 1;

            //order_desc column
            detailsViewRestaurant.Columns["order_desc"].Caption = "Açıklama";
            detailsViewRestaurant.Columns["order_desc"].OptionsColumn.AllowEdit = false;
            detailsViewRestaurant.Columns["order_desc"].OptionsColumn.ReadOnly = false;
            detailsViewRestaurant.Columns["order_desc"].VisibleIndex = 2;

            //amount column
            detailsViewRestaurant.Columns["order_amount"].Caption = "Miktar";
            detailsViewRestaurant.Columns["order_amount"].OptionsColumn.AllowEdit = false;
            detailsViewRestaurant.Columns["order_amount"].OptionsColumn.ReadOnly = false;
            detailsViewRestaurant.Columns["order_amount"].VisibleIndex = 3;

            //order_price column
            detailsViewRestaurant.Columns["order_price"].Caption = "Sipariş Tutarı";
            detailsViewRestaurant.Columns["order_price"].OptionsColumn.AllowEdit = false;
            detailsViewRestaurant.Columns["order_price"].OptionsColumn.ReadOnly = false;
            detailsViewRestaurant.Columns["order_price"].VisibleIndex = 4;

            //time_of_day column
            detailsViewRestaurant.Columns["time_of_day"].Caption = "Saat";
            detailsViewRestaurant.Columns["time_of_day"].OptionsColumn.AllowEdit = false;
            detailsViewRestaurant.Columns["time_of_day"].OptionsColumn.ReadOnly = false;
            detailsViewRestaurant.Columns["time_of_day"].VisibleIndex = 5;

            //actions column
            detailsViewRestaurant.Columns["actions"].Caption = "İşlemler";
            detailsViewRestaurant.Columns["actions"].OptionsColumn.ReadOnly = false;
            detailsViewRestaurant.Columns["actions"].VisibleIndex = 6;

            detailsViewRestaurant.BestFitColumns();
            detailsViewRestaurant.OptionsView.ShowGroupPanel = false;
            detailsViewRestaurant.DoubleClick += new EventHandler(detailsViewRestaurant_DoubleClick);
            details_repo_button.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repo_button_restaurant_ButtonClick);
        }

        void detailsViewRestaurant_DoubleClick(object sender, EventArgs e)
        {
            GridView View = gridControl2.FocusedView as GridView;
            int rHandle = View.FocusedRowHandle;
            selectedMasterRow = rHandle;
            CollapseDetailRows(rHandle,ref View, 3);
            View.SetMasterRowExpanded(rHandle, !View.GetMasterRowExpanded(rHandle));
        }

        private void SetDetailsViewRestaurantOrder(string viewHeader, ref DataSet ds)
        {
            // şimdi de sipariş kalemleri için yani 3. basamak için (ds.Tables[2]) gerekli işlemleri yapalım
            //add repo button for orders_details
            DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit details_repo_button = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit(); ;
            details_repo_button.Buttons[0].Image = global::StockProgram.Properties.Resources.delete;
            details_repo_button.Buttons[0].Caption = "Sil";
            details_repo_button.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            details_repo_button.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            ds.Tables[2].Columns.Add("actions");

            detailsViewRestaurantOrder.ViewCaption = viewHeader;
            detailsViewRestaurantOrder.PopulateColumns(ds.Tables[2]);

            detailsViewRestaurantOrder.Columns["actions"].ColumnEdit = details_repo_button;
            detailsViewRestaurantOrder.Columns["is_deleted"].VisibleIndex = -1;
            detailsViewRestaurantOrder.Columns["log_id"].VisibleIndex = -1;
            detailsViewRestaurantOrder.Columns["order_id"].VisibleIndex = -1;
            detailsViewRestaurantOrder.Columns["order_staff_name"].VisibleIndex = -1;
            detailsViewRestaurantOrder.Columns["staff_id"].VisibleIndex = -1;
            detailsViewRestaurantOrder.Columns["account_status"].VisibleIndex = -1;
            detailsViewRestaurantOrder.Columns["account_type"].VisibleIndex = -1;
            detailsViewRestaurantOrder.Columns["order_status"].VisibleIndex = -1;
            detailsViewRestaurantOrder.Columns["product_id"].VisibleIndex = -1;
            detailsViewRestaurantOrder.Columns["order_type"].VisibleIndex = -1;
            detailsViewRestaurantOrder.Columns["account_id"].VisibleIndex = -1;

            //product_name column
            detailsViewRestaurantOrder.Columns["product_name"].Caption = "Ürün Adı";
            detailsViewRestaurantOrder.Columns["product_name"].OptionsColumn.AllowEdit = false;
            detailsViewRestaurantOrder.Columns["product_name"].OptionsColumn.ReadOnly = false;
            detailsViewRestaurantOrder.Columns["product_name"].VisibleIndex = 0;

            //product_desc column
            detailsViewRestaurantOrder.Columns["product_desc"].Caption = "Seçenek";
            detailsViewRestaurantOrder.Columns["product_desc"].OptionsColumn.AllowEdit = false;
            detailsViewRestaurantOrder.Columns["product_desc"].OptionsColumn.ReadOnly = false;
            detailsViewRestaurantOrder.Columns["product_desc"].VisibleIndex = 1;

            //porsion column
            detailsViewRestaurantOrder.Columns["porsion"].Caption = "Porsiyon";
            detailsViewRestaurantOrder.Columns["porsion"].OptionsColumn.AllowEdit = false;
            detailsViewRestaurantOrder.Columns["porsion"].OptionsColumn.ReadOnly = false;
            detailsViewRestaurantOrder.Columns["porsion"].VisibleIndex = 2;

            //amount column
            detailsViewRestaurantOrder.Columns["amount"].Caption = "Miktar";
            detailsViewRestaurantOrder.Columns["amount"].OptionsColumn.AllowEdit = false;
            detailsViewRestaurantOrder.Columns["amount"].OptionsColumn.ReadOnly = false;
            detailsViewRestaurantOrder.Columns["amount"].VisibleIndex = 3;

            //product_price column
            detailsViewRestaurantOrder.Columns["product_price"].Caption = "Birim Satış Fiyatı";
            detailsViewRestaurantOrder.Columns["product_price"].OptionsColumn.AllowEdit = false;
            detailsViewRestaurantOrder.Columns["product_price"].OptionsColumn.ReadOnly = false;
            detailsViewRestaurantOrder.Columns["product_price"].VisibleIndex = 4;

            //total_price column
            detailsViewRestaurantOrder.Columns["total_price"].Caption = "Tutar";
            detailsViewRestaurantOrder.Columns["total_price"].OptionsColumn.AllowEdit = false;
            detailsViewRestaurantOrder.Columns["total_price"].OptionsColumn.ReadOnly = false;
            detailsViewRestaurantOrder.Columns["total_price"].VisibleIndex = 5;

            //actions column
            detailsViewRestaurantOrder.Columns["actions"].Caption = "İşlemler";
            detailsViewRestaurantOrder.Columns["actions"].OptionsColumn.ReadOnly = false;
            detailsViewRestaurantOrder.Columns["actions"].VisibleIndex = 6;

            detailsViewRestaurantOrder.BestFitColumns();
            detailsViewRestaurantOrder.OptionsView.ShowGroupPanel = false;
            details_repo_button.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(details_repo_button_restaurant_ButtonClick);

        }
        void details_repo_button_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            GridView view = gridControl1.FocusedView as GridView;
            int masterViewRowHandle = gridView1.FocusedRowHandle;
            int masterViewRowCount=gridView1.RowCount;
            int detailViewRowCount = view.RowCount;
            DataRow dr = view.GetFocusedDataRow();
            int log_id = 0;

            log_id =Convert.ToInt32 (dr["log_id"]);
            switch (e.Button.Caption)
            {
                   case "Sil":
                    ErrorMessages.Message msg = new ErrorMessages.Message();
                    if (msg.WriteMessage("Sipariş kalem silme işlemini onaylıyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                     DeleteMenuItem(log_id,1);
                     CollapseDetailRows(masterViewRowHandle, masterViewRowCount,1);
                     if (detailViewRowCount>1)
                     {
                         gridView1.SetMasterRowExpanded(this.selectedMasterRow, true);                         
                     }
                    }
                    break;
                default:
                    break;
            }
        }
        void details_repo_button_restaurant_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {//restoran içi sipariş kalemleri için
            GridView view = gridControl2.FocusedView as GridView;
            int masterViewRowHandle = gridView2.FocusedRowHandle;
            int masterViewRowCount = gridView2.RowCount;
            int detailViewRowCount = view.RowCount;
            DataRow dr = view.GetFocusedDataRow();
            int log_id = 0;

            log_id = Convert.ToInt32(dr["log_id"]);
            switch (e.Button.Caption)
            {
                case "Sil":
                    ErrorMessages.Message msg = new ErrorMessages.Message();
                    if (msg.WriteMessage("Sipariş silme işlemini onaylıyor musunuz?", MessageBoxIcon.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        DeleteMenuItem(log_id, 2);
                        CollapseDetailRows(masterViewRowHandle, masterViewRowCount, 2);
                        if (detailViewRowCount > 1)
                        {
                            gridView2.SetMasterRowExpanded(this.selectedMasterRow, true);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        private int selectedMasterRow = 0;
        private bool refresh_view;
        private void gridView1_DoubleClick_1(object sender, EventArgs e)
        {
            if (firstLoad)
            {
                firstLoad = false;
                refresh_view = false;
            }
            else
                if (refresh_view) // amac grid refresh ise hiç bir row expand etme
                {
                    refresh_view = false;
                    return;
                }
            GridView View = gridControl1.FocusedView as GridView;
            int rHandle = View.FocusedRowHandle;
            selectedMasterRow = rHandle;
            CollapseDetailRows(rHandle, View.RowCount,1);
            gridView1.SetMasterRowExpanded(rHandle, !gridView1.GetMasterRowExpanded(rHandle));

        }

        /// <summary>
        /// collapses all the grid rows except the selected one
        /// </summary>
        /// <param name="rowHandle"></param>
        private void CollapseDetailRows(int rowHandle, int masterViewRowCount,int viewIndex)
        {
            int rowCount = masterViewRowCount;
            for (int i = 0; i < rowCount; i++)
            {
                if (i == rowHandle)
                {
                    continue;
                }
                if (viewIndex==1)
                {
                    gridView1.SetMasterRowExpanded(i, false);                    
                }
                else if (viewIndex == 2)
                {
                    gridView2.SetMasterRowExpanded(i, false);
                }
                else if (viewIndex == 3)
                {
                    detailsViewRestaurant.SetMasterRowExpanded(i, false);
                }
            }
        }

        /// <summary>
        /// collapses all the grid rows except the selected one
        /// </summary>
        /// <param name="rowHandle"></param>
        private void CollapseDetailRows(int rowHandle, ref GridView View, int viewIndex)
        {
            int rowCount = View.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                if (i == rowHandle)
                {
                    continue;
                }
                if (viewIndex == 1)
                {
                    View.SetMasterRowExpanded(i, false);
                }
                else if (viewIndex == 2)
                {
                    View.SetMasterRowExpanded(i, false);
                }
                else if (viewIndex == 3)
                {
                    View.SetMasterRowExpanded(i, false);
                }
            }
        }

        /// <summary>
        /// colon toplamı 0 ise satırı kırmızı boyar
        /// </summary>
        /// <param name="columnName"></param>
        private void SetConditionalFormatting(string columnName)
        {
            StyleFormatCondition condition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            condition1.Appearance.ForeColor = System.Drawing.Color.Red;
            condition1.Appearance.Options.UseForeColor = true;
            condition1.Condition = FormatConditionEnum.Expression;
            condition1.Expression = columnName + "<=0";
            gridView1.FormatConditions.Add(condition1);
        }

        private void PrintKitchenSlip(int order_id,int owner_id,string owner_name,Enums.OrderType orderType)
        {
            isPrinterReady = true;
            string sql = "select * from v_product_to_order where is_deleted=0 and order_id=" + order_id;
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt;
            try
            {
                dt = cmd.GetDataTable(sql);
                SiparisHandler sh = new SiparisHandler(ref dt);
                int account_id = Convert.ToInt32(dt.Rows[0]["account_id"]);
                string staff = dt.Rows[0]["order_staff_name"].ToString();
                Sales.Orders order = new Sales.Orders(sh.GetSiparisList(), owner_id, order_id, account_id);
                order.type = orderType;
                order.owner_name = owner_name;
                order.staff_name = staff;
                Adisyon a = new Adisyon(order);
                if (orderType==Enums.OrderType.Paket)
                    a.SetCustomer(GetCustomer(owner_id));

                a.ConnectionFailed += new Adisyon.ConnectionHandler(a_ConnectionFailed);
                a.SetPrinter();
            //    a.waiter_name = StaticObjects.User.Name;
                if (isPrinterReady)
                    a.PrintKitchenSlip();
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, EditOrder() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                cmd.Close();
                cmd = null;
            }
     
        }
        
        /// <summary>
        /// tüm siparişleri kapsayan hesap için mutfak fişi yazdır
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="owner_id"></param>
        /// <param name="owner_name"></param>
        /// <param name="orderType"></param>
        private void PrintKitchenSlipForAllOrders(int account_id, int owner_id, string owner_name, string table_category_name,Enums.OrderType orderType)
        {
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt1=new DataTable();  //orders
            DataTable dt2=new DataTable(); //order products
            string sql = "select *from v_orders_to_accounts where is_order_deleted=0 and account_id=" + account_id;
            try
            {
                dt1 = cmd.GetDataTable(sql);
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, EditOrder() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                cmd.Close();
                cmd = null;
            }
            isPrinterReady = true;
            
            try
            {
                cmd= new MySqlDbHelper(StaticObjects.MySqlConn);
                List<Sales.Orders> ordersList = new List<Sales.Orders>();
                SiparisHandler sh;
                Sales.Orders order;
                int order_id = 0;
                foreach (DataRow row in dt1.Rows )
                {
                    order_id = Convert.ToInt32(row["order_id"]);
                    sql = "select * from v_product_to_order where is_deleted=0 and order_id=" + order_id;
                    dt2 = cmd.GetDataTable(sql);
                    sh = new SiparisHandler(ref dt2);
                    order = new Sales.Orders(sh.GetSiparisList(), owner_id, order_id, account_id);
                    order.type = orderType;
                    order.owner_name = owner_name;
                    order.staff_name = dt2.Rows[0]["order_staff_name"].ToString();
                    ordersList.Add(order);
                }
                Adisyon a = new Adisyon(ordersList);
                Table t = new Table(owner_id);
                t.name = owner_name;
                t.category.Name = table_category_name;
                a.SetTable(t);
                a.ConnectionFailed += new Adisyon.ConnectionHandler(a_ConnectionFailed);
                a.SetPrinter();
                a.waiter_name = StaticObjects.User.Name;
                if (isPrinterReady)
                    a.PrintKitchenSlipForAllOrders();
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, EditOrder() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                cmd.Close();
                cmd = null;
                dt1.Dispose();
                dt2.Dispose();
            }

        }

        bool isPrinterReady;
        void a_ConnectionFailed(object sender, EventArgs e)
        { //yazıcıya baglanılamadı
            isPrinterReady = false;
            Printer p = sender as Printer;
            ErrorMessages.Message message = new ErrorMessages.Message();
            message.WriteMessage(p.name + " yazıcısına erişilemiyor.\nLütfen yazıcınızın bağlantı ayarlarını kontrol ediniz", MessageBoxIcon.Warning, MessageBoxButtons.OK);
        }
         private void SetProductCodesColumn(ref DataSet ds)
         {
             ds.Tables[0].Columns.Add("p_codes", typeof(String)); //satılan ürün kodları için bir kolon ekleyelim
             int sell_id=0;
             List<string > product_codes=new List<string>();

             foreach (DataRow row in ds.Tables[0].Rows) //değerleri hesapla
             {
                 sell_id = Convert.ToInt32(row["sell_id"]);
                 foreach (DataRow row1 in ds.Tables[1].Rows)
                 {
                     if (sell_id==Convert.ToInt32( row1["sell_id"]))
                     {
                         product_codes.Add(row1["product_code_manual"].ToString());
                     }
                 }
                 row["p_codes"] = GetCodesFormCodeList(product_codes);
                 product_codes.Clear();
             }
         }

         private string GetCodesFormCodeList(List<string> product_codes)
        {
            string ret_value = string.Empty;
            for (int i = 0; i < product_codes.Count; i++)
            {
                if (i != product_codes.Count - 1)
                {
                    ret_value += product_codes[i] + " - ";
                }
                else
                    ret_value += product_codes[i];
            }
            return ret_value;
        }

         private Customer GetCustomer(int customer_id)
         {
             string sql = "select * from customer_details where customer_id=" + customer_id;
             MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
             DataTable dt = new DataTable();
             dt = cmd.GetDataTable(sql);

             Customer c = new Customer();
             c.name = dt.Rows[0]["customer_name"].ToString();
             c.tel = dt.Rows[0]["customer_tel"].ToString().Trim();
             c.note = dt.Rows[0]["customer_note"].ToString().Trim();
             c.adress = dt.Rows[0]["customer_address"].ToString().Trim();
             dt.Dispose();
             return c;
         }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            ((MainForm)this.ParentForm).SettingStatus();
            this.Dispose();
        }

        private void Checkout(int account_id, int owner_id, string owner_name, Enums.OrderType order_type)
        {
            string sql = "select * from v_product_to_order where is_deleted=0 and account_status=1 and account_id=" + account_id;
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt;
            try
            {
                dt = cmd.GetDataTable(sql);
                Sales.SiparisKalem sk;
                List<Sales.SiparisKalem> sList = new List<Sales.SiparisKalem>();
                foreach (DataRow row in dt.Rows)
                {
                    sk = new Sales.SiparisKalem();
                    sk.Id = Convert.ToInt32(row["log_id"]);
                    sk.ProductId = Convert.ToInt32(row["product_id"]);
                    sk.ProductName = row["product_name"].ToString();
                    sk.Amount = Convert.ToInt32(row["amount"]);
                    sk.Porsion = Convert.ToDouble(row["porsion"]);
                    sk.SalePrice = sk.UndiscountedPrice = Convert.ToDouble(row["product_price"]);
                    sk.Desc = row["product_desc"].ToString();
                    sList.Add(sk);
                }

                int order_id = Convert.ToInt32(dt.Rows[0]["order_id"]);
                Sales.Orders order = new Sales.Orders(sList, owner_id, order_id,account_id);
                order.owner_name = owner_name;
                order.type = order_type;
                Sales.frmSatisPopup frm = new Sales.frmSatisPopup(ref order);
                frm.PurchaseCompleted += new Sales.frmSatisPopup.PurchaseHandler(frm_PurchaseCompleted);
                frm.ShowDialog(this);
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, EditOrder() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                cmd.Close();
                cmd = null;
            }
        }

        void frm_PurchaseCompleted(object sender, EventArgs e)
        {
            switch (tabControl.SelectedTabPageIndex)
            {
                case 0: FillCourierGrid();
                    break;
                case 1: FillRestaurantGrid();
                    break;
                default:
                    break;
            }
        }

        private void DeleteOrder(int order_id,int type)
        {
            string sql = "update order_details set is_deleted=1 where order_id=" + order_id;
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);

            try
            {
                cmd.ExecuteNonQuery(sql);
                if (type==1)
                {
                FillCourierGrid();                    
                }
                else if (type == 2)
                {
                    FillRestaurantGrid();
                }
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, DeleteCustomer() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                cmd.Close();
                cmd = null;
            }
        }

        private void DeleteAccount(int account_id,int table_id)
        {
            string sql = "update account_details  set account_status=7 where account_id=" + account_id; //status =KAPANDI
            sql += " ; update table_details set table_status=3 where is_deleted=0 and table_id=" + table_id;
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);

            try
            {
                cmd.ExecuteNonQuery(sql);
                FillRestaurantGrid();
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, DeleteAccount() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                cmd.Close();
                cmd = null;
            }
        }
     
        private void DeleteMenuItem(int log_id,int type)
        {
            string sql = "delete from product_to_order where log_id=" + log_id;
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);

            try
            {
                cmd.ExecuteNonQuery(sql);
                if (type==1)
                {
                    FillCourierGrid();                    
                }
                else if (type == 2)
                {
                    FillRestaurantGrid();
                }
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, DeleteCustomer() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                cmd.Close();
                cmd = null;
            }
        }
    
        private void EditOrder(int order_id, int owner_id, string owner_name,Enums.OrderType edit_type)
        {
            string sql = "select * from v_product_to_order where is_deleted=0 and order_id=" + order_id;
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt;
            try
            {
                dt = cmd.GetDataTable(sql);
                SiparisHandler sh = new SiparisHandler(ref dt);
                int account_id = Convert.ToInt32(dt.Rows[0]["account_id"]);
                Sales.Orders order = new Sales.Orders(sh.GetSiparisList(), owner_id, order_id,account_id);
                order.owner_name = owner_name;
                order.type = edit_type;
                Menu.ucMenu ctrl = new Menu.ucMenu(order);
                if (edit_type==Enums.OrderType.Restoran)
                {
                       Table t = new Table(owner_id);
                       t.name = owner_name;
                       ctrl.table = t;
                }
                ctrl.Dock = DockStyle.Fill;
                ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
                this.Dispose();
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, EditOrder() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
            finally
            {
                cmd.Close();
                cmd = null;
            }
        }
        private void AddNewOrder( int owner_id, string owner_name, Enums.OrderType edit_type)
        {       
            try
            {          
                Table t = new Table(owner_id);
                t.name = owner_name;
                Menu.ucMenu ctrl = new Menu.ucMenu(t);            
                ctrl.Dock = DockStyle.Fill;
             
                ParentForm.Controls["pnl_main"].Controls.Add(ctrl);
                this.Dispose();
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, EditOrder() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            }
         
        }
        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            if (firstLoad)
            {
                firstLoad = false;
                refresh_view = false;
            }
            else
                if (refresh_view) // amac grid refresh ise hiç bir row expand etme
                {
                    refresh_view = false;
                    return;
                }
            GridView View = gridControl2.FocusedView as GridView;
            int rHandle = View.FocusedRowHandle;
            selectedMasterRow = rHandle;
            CollapseDetailRows(rHandle, View.RowCount,2);
            gridView2.SetMasterRowExpanded(rHandle, !gridView2.GetMasterRowExpanded(rHandle));
        }
    }
}
