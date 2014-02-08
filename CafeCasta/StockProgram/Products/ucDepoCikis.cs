using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace StockProgram.Products
{
    public partial class ucDepoCikis : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool IsPageLoaded; //for use when page loaded 
        private StockProgram.ControlHelper controlHelper;
        private List<DBObjects.Warehouse> WItemsList;
        private List<DBObjects.Supplier> MItemsList;
        private List<DBObjects.Product> PItemsList;

        public ucDepoCikis()
        {
            controlHelper = new ControlHelper();
            InitializeComponent();
            IsPageLoaded=false;
        }

        /// <summary>
        /// depo çıkısını yapar
        /// </summary>
        /// <param name="p"></param>
        private void DepoCikis(ref DBObjects.Product p)
        {
            DBObjects.MySqlCmd cmd = new DBObjects.MySqlCmd(StaticObjects.MySqlConn);
            string strSQL = "insert into [AllStocks] (ASRefId,ASRefWID,ASRefMID,ASAmount)"
            + "values (@Id,@WID,@MID,@Amount)";

           // DBObjects.myCommand cmd = db.CreateMyCommand(strSQL);
            //create params
            //cmd.CreateParameter("@Id", System.Data.OleDb.OleDbType.Integer, "ASRefId");
            //cmd.CreateParameter("@WID", System.Data.OleDb.OleDbType.Integer, "ASRefWID");
            //cmd.CreateParameter("@MID", System.Data.OleDb.OleDbType.Integer, "ASRefMID");
            //cmd.CreateParameter("@Amount", System.Data.OleDb.OleDbType.Integer, "ASAmount");
            ////set params
            //cmd.SetParameterA("Id", PItemsList[cb_urunAdi.SelectedIndex].Id);
            //cmd.SetParameterA("WID", WItemsList[cb_bagliDepo.SelectedIndex].wID);
            //cmd.SetParameterA("MID", MItemsList[cb_bagliTedarikci.SelectedIndex].Id);
            //cmd.SetParameterA("Amount",(-1* Convert.ToInt32(txt_urunAdet.Text)));

            try
            {
                cmd.ExecuteNonQuery();
                Success();
                FillGrid();
            }
            catch (Exception e)
            {
                ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
                excMail.Subject = "Stok Programı, ucMigo.Migo() hata hk ";
                excMail.Send();
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
                //retValue = 0;
            }
            finally
            {
                cmd.Close();
                cmd = null;
            }
        }

           /// <summary>
        /// max ürün adedi ile depo çıkısını yapar (toplam ürün adedi=0 olur)
        /// </summary>
        /// <param name="p"></param>
        private void DepoCikis(ref DBObjects.Product p,int amount)
        {
    //        DBObjects.CDB db = new DBObjects.CDB(StaticObjects.AccessConnStr);
    //        string strSQL = "insert into [AllStocks] (ASRefId,ASRefWID,ASRefMID,ASAmount)"
    //        + "values (@Id,@WID,@MID,@Amount)";

    ////        DBObjects.myCommand cmd = db.CreateMyCommand(strSQL);
    ////        //create params
    ////        cmd.CreateParameter("@Id", System.Data.OleDb.OleDbType.Integer, "ASRefId");
    ////        cmd.CreateParameter("@WID", System.Data.OleDb.OleDbType.Integer, "ASRefWID");
    ////        cmd.CreateParameter("@MID", System.Data.OleDb.OleDbType.Integer, "ASRefMID");
    ////        cmd.CreateParameter("@Amount", System.Data.OleDb.OleDbType.Integer, "ASAmount");
    ////        //set params
    ////        cmd.SetParameterA("Id", PItemsList[cb_urunAdi.SelectedIndex].Id);
    //////        cmd.SetParameterA("WID", WItemsList[cb_bagliDepo.SelectedIndex].wID);
    ////        cmd.SetParameterA("MID", MItemsList[cb_bagliTedarikci.SelectedIndex].Id);
    ////        cmd.SetParameterA("Amount",(-1* amount));

    //        try
    //        {
    //            //cmd.ExecuteNonQuery();
    //            Success();
    //            FillGrid();
    //        }
    //        catch (Exception e)
    //        {
    //            ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
    //            excMail.Subject = "Stok Programı, ucMigo.Migo() hata hk ";
    //            excMail.Send();
    //            ErrorMessages.Message message = new ErrorMessages.Message();
    //            message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
    //            //retValue = 0;
    //        }
    //        finally
    //        {
    //            db.CloseDB();
    //            db = null;
    //        }
        }

        /// <summary>
        /// ürün adedinin yeterlilik kontrolunu yapar
        /// </summary>
        /// <returns></returns>
        private int ISProductEnough()
        {
            return 1;
            //DBObjects.CDB db = new DBObjects.CDB(StaticObjects.AccessConnStr);
            //string strSQL = "select iif(IsNull(sum(ASAmount)),0,sum(ASAmount)) as Total from [AllStocks] where ASRefId=" + PItemsList[cb_urunAdi.SelectedIndex].Id + " and ASRefWID=" + WItemsList[cb_bagliDepo.SelectedIndex].wID;
            //strSQL += " and ASrefMID=" + MItemsList[cb_bagliTedarikci.SelectedIndex].Id;
            //int totalAmount=0;
            //try
            //{
            //    totalAmount =Convert.ToInt32( db.Get_Scalar(strSQL));
            //}
            //catch (Exception e)
            //{
            //    ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
            //    excMail.Subject = "Stok Programı, ISProductEnough() hata hk ";
            //    excMail.Send();
            //    ErrorMessages.Message message = new ErrorMessages.Message();
            //    message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            //}
            //finally
            //{
            //    db.CloseDB();
            //    db = null;
            //}
            //return totalAmount;
        }
        private void ucDepoCikis_Load(object sender, EventArgs e)
        {
            FillProducts();
            FillWarehouses();
            FillSuppliers();
        //    FillGrid();
            IsPageLoaded = true;
        }

        #region Fill comboboxes
        private void FillWarehouses()
        {
            //StockProgram.DBObjects.CDB db = new DBObjects.CDB(StaticObjects.AccessConnStr);
            //DataTable dt = new DataTable();
            //WItemsList = new List<DBObjects.Warehouse>();

            //string strSQL;
            //try
            //{
            //    //fill warehouses
            //    strSQL = "select WID,WName from [Warehouse] order by WName asc";
            //    dt = db.Get_DataTable(strSQL);
            //    WItemsList = controlHelper.GetWarehouses(ref dt);
            //    controlHelper.FillControl(cb_bagliDepo, Enums.RepositoryItemType.ComboBox, ref dt, "WName");
            //    cb_bagliDepo.Text = cb_bagliDepo.Properties.Items[0].ToString();
               
            //}
            //catch (Exception e)
            //{
            //    ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
            //    excMail.Subject = "Stok Programı, ucDepoCikis.FillWarehouses() hata hk ";
            //    excMail.Send();
            //    ErrorMessages.Message message = new ErrorMessages.Message();
            //    message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            //    // retValue = 0;
            //}
            //finally
            //{
            //    db.CloseDB();
            //    db = null;
            //    dt.Dispose();
            //}

        }
        private void FillSuppliers()
        {
            //fill Suppliers
            //DBObjects.CDB db = new DBObjects.CDB(StaticObjects.AccessConnStr);
            //string strSQL = "select MID,MName from [Supplier] order by MName asc";
            //MItemsList = new List<DBObjects.Supplier>();
            //DataTable dt = new DataTable();
            //try
            //{
            //    dt = db.Get_DataTable(strSQL);
            //    MItemsList = controlHelper.GetSuppliers(ref dt);
            //    controlHelper.FillControl(cb_bagliTedarikci, Enums.RepositoryItemType.ComboBox, ref dt, "MName");
            //    cb_bagliTedarikci.Text = cb_bagliTedarikci.Properties.Items[0].ToString();
             
            //}
            //catch (Exception e)
            //{
            //    ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
            //    excMail.Subject = "Stok Programı, ucDepoCikis.FillSuppliers() hata hk ";
            //    excMail.Send();
            //    ErrorMessages.Message message = new ErrorMessages.Message();
            //    message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            //    // retValue = 0;
            //}
            //finally
            //{
            //    db.CloseDB();
            //    db = null;
            //    dt.Dispose();
            //}
        }
        private void FillProducts()
        {
            //fill Suppliers
            //DBObjects.CDB db = new DBObjects.CDB(StaticObjects.AccessConnStr);
            //string strSQL = "select Id,PName from [Product] order by PName asc";
            //PItemsList = new List<DBObjects.Product>();
            //DataTable dt = new DataTable();
            //try
            //{
            //    dt = db.Get_DataTable(strSQL);
            //    PItemsList = controlHelper.GetProducts(ref dt);
            //    controlHelper.FillControl(cb_urunAdi, Enums.RepositoryItemType.ComboBox, ref dt, "PName");
            //    cb_urunAdi.Text = cb_urunAdi.Properties.Items[0].ToString();         
            //}
            //catch (Exception e)
            //{
            //    ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
            //    excMail.Subject = "Stok Programı, ucDepoCikis.FillProducts() hata hk ";
            //    excMail.Send();
            //    ErrorMessages.Message message = new ErrorMessages.Message();
            //    message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            //    // retValue = 0;
            //}
            //finally
            //{
            //    db.CloseDB();
            //    db = null;
            //    dt.Dispose();
            //}
        }
        #endregion

        private void Success()
        {
            ErrorMessages.Message message = new ErrorMessages.Message();
            message.WriteMessage("Depo çıkışı, başarılı bir şekilde gerçekleştirildi.", MessageBoxIcon.Information, MessageBoxButtons.OK);
            cb_bagliTedarikci.Text = cb_bagliTedarikci.Properties.Items[0].ToString();
            cb_bagliDepo.Text = cb_bagliDepo.Properties.Items[0].ToString();
            cb_urunAdi.Text = cb_urunAdi.Properties.Items[0].ToString();
        }

        /// <summary>
        /// fills the grid with related product
        /// </summary>
        private void FillGrid()
        {
            //DataTable dt = new DataTable();
            //DBObjects.CDB db = new DBObjects.CDB(StaticObjects.AccessConnStr);
            //string strSQL = "select * from [GroupedProducts] where Id=" + Id + " and WID=" + wID + " AND MID=" + mID + " order by PName asc";
            //try
            //{
            //    dt = db.Get_DataTable(strSQL);
            ////    gridControl1.DataSource = dt;
            //}
            //catch (Exception e)
            //{
            //    ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
            //    excMail.Subject = "Stok Programı, FillGrid() hata hk ";
            //    excMail.Send();
            //    ErrorMessages.Message message = new ErrorMessages.Message();
            //    message.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);

            //    //retValue = 0;
            //}
            //finally
            //{
            //    db.CloseDB();
            //    db = null;
            //}

        }

        int Id;
        private void cb_urunAdi_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Id = PItemsList[cb_urunAdi.SelectedIndex].Id;

            if (IsPageLoaded) // sayfa load eventini gerçekleştirmediği sırada return
            {
                FillGrid();
            }
            else return;
        }

        int wID;
        private void cb_bagliDepo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            wID = WItemsList[cb_bagliDepo.SelectedIndex].wID;
            if (IsPageLoaded) // sayfa load eventini gerçekleştirmediği sırada return
            {
                FillGrid();
            }
            else return;
        }

        int mID;
        private void cb_bagliTedarikci_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            mID = MItemsList[cb_bagliTedarikci.SelectedIndex].Id;
            if (IsPageLoaded) // sayfa load eventini gerçekleştirmediği sırada return
            {
                FillGrid();
            }
            else return;
        }

        private void btn_depoCikis_Click_1(object sender, EventArgs e)
        {
            if (txt_urunAdet.Text == "")
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("* İşaretli alanları boş bırakmayınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                return;
            }
            int totalProductAmount = ISProductEnough();

            if (totalProductAmount == 0)
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                message.WriteMessage("Ürün stokta tükenmiştir", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                return;
            }
            if (totalProductAmount >= Convert.ToInt32(txt_urunAdet.Text))
            {//depo cıkış yap

                DBObjects.Product p = new DBObjects.Product();
                //p.pRefWID = WItemsList[cb_bagliDepo.SelectedIndex].wID;
                p.Id = PItemsList[cb_urunAdi.SelectedIndex].Id;
                p.SupplierId = MItemsList[cb_bagliTedarikci.SelectedIndex].Id;
                DepoCikis(ref p);
            }
            else
            {
                ErrorMessages.Message message = new ErrorMessages.Message();
                if (message.WriteMessage("Ürün adedi yetersiz!\nToplam adet=" + totalProductAmount + "\nHepsini stoktan düşmek ister misiniz?", MessageBoxIcon.Warning, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {// max adet depo çıkıs yap 
                    DBObjects.Product p = new DBObjects.Product();
                  //  p.pRefWID = WItemsList[cb_bagliDepo.SelectedIndex].wID;
                    p.Id = PItemsList[cb_urunAdi.SelectedIndex].Id;
                    p.SupplierId = MItemsList[cb_bagliTedarikci.SelectedIndex].Id;
                    DepoCikis(ref p, totalProductAmount);
                }
                else return;
            }

        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Parent.Controls["pnl_master"].Visible = true;
            this.Dispose();
        }
    }
}
