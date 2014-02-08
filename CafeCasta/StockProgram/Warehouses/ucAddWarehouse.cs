using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StockProgram.DBObjects;

namespace StockProgram.Warehouses
{
    public partial class ucAddWarehouse : DevExpress.XtraEditors.XtraUserControl
    {
        private StockProgram.ControlHelper controlHelper;
        private ErrorMessages.Message ErrorMessage;
        private Warehouse WItem;  // my warehouse items in cbox
        private List<Warehouse> WItemList; // my warehouse item list
        public ucAddWarehouse()
        {
            InitializeComponent();
            ErrorMessage = new ErrorMessages.Message();
        }

        private void ucAddWarehouse_Load(object sender, EventArgs e)
        {
            cb_depoDurum.Text = "Seçiniz";
            FillWarehouse();
        }
        private int InitializeWarehouseItems(ref DataTable dt)
        {
            WItemList = new List<Warehouse>();
           
            foreach (DataRow row in dt.Rows)
            {
                WItem = new Warehouse();
                WItem.wRefMID= Convert.ToInt32(row["MID"].ToString());
                WItem.wRefMName = (row["MName"].ToString());
               // WItem.wDescription = row["WDescription"].ToString();
               // WItem.wStatus = Convert.ToInt32(row["IsOpen"].ToString());
                WItemList.Add(WItem);
            }
            return 0;
        }

        private void FillWarehouse()
        {
            //CDB db = new CDB(StaticObjects.AccessConnStr);
            //DataTable dt = new DataTable();
            //int retValue = 0;
            //controlHelper = new ControlHelper();
            //try
            //{
            //    cb_bagliTedarikci.Properties.Items.Clear();
            //    string strSQL = "select MID,MName from [Supplier] order by MName asc";
            //    dt = db.Get_DataTable(strSQL);
            //    InitializeWarehouseItems(ref dt);
            //    controlHelper.FillControl(cb_bagliTedarikci, Enums.RepositoryItemType.ComboBox, ref dt, "MName");
            //    cb_bagliTedarikci.Text = cb_bagliTedarikci.Properties.Items[0].ToString();
            //    dt.Dispose();
            //    retValue = 1;
            //}
            //catch (Exception e)
            //{
            //    ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
            //    excMail.Subject = "Stok Programı, FillWarehouse() hata hk ";
            //    excMail.Send();
            //   // ErrorMessage = new ErrorMessages.Message();
            //    ErrorMessage.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            //    retValue = 0;
            //}
            //finally
            //{
            //    db.CloseDB();
            //    db = null;
            //}
            //return retValue;
        }

        private void btn_depoEkle_Click(object sender, EventArgs e)
        {
            if (txt_depoAdi.Text == "" || cb_depoDurum.Text == "" || cb_depoDurum.Text == "Seçiniz" || cb_bagliTedarikci.Text == "")
            {
               // ErrorMessage = new ErrorMessages.Message();
                ErrorMessage.WriteMessage("* İşaretli alanları boş bırakmayınız.", MessageBoxIcon.Warning, MessageBoxButtons.OK);
            }
            else DepoEkle();
            return;
        }
        private void DepoEkle()
        {
            //CDB db = new CDB(StaticObjects.AccessConnStr);
            //int index = cb_bagliTedarikci.SelectedIndex;
            //int wRefMID = WItemList[index].wRefMID;
            //string WDescription = txt_depoTanim.Text;
            //string WName = txt_depoAdi.Text;
            //int wIsOpen = (cb_depoDurum.SelectedIndex+1)%2;//açık secili iken (0+1)%2=1 olacak db ye 1(true) yazacak
            //string strSQL = "insert into [Warehouse] (WDescription,WRefMID,WName,IsOpen) values ('" + WDescription + "'," + wRefMID + ",'"+WName+"',"+wIsOpen+")";
            //try
            //{
            //    db.ExecuteNonQuery(strSQL);
            //    ErrorMessage.WriteMessage("Depo, başarılı bir şekilde eklendi.", MessageBoxIcon.Information, MessageBoxButtons.OK);
            //    txt_depoAdi.Text = "";
            //    txt_depoTanim.Text = "";
            //}
            //catch (Exception e)
            //{
            //    ErrorMessages.ExceptionMail excMail = new ErrorMessages.ExceptionMail(e, ErrorMessages.MailServerNames.Gmail);
            //    excMail.Subject = "Stok Programı, Depo Ekle() hata hk ";
            //    excMail.Send();
            //   // ErrorMessages.Message message = new ErrorMessages.Message();
            //    ErrorMessage.WriteMessage(e.Message, MessageBoxIcon.Error, MessageBoxButtons.OK);
            //    //retValue = 0;
            //}
            //finally
            //{
            //    db.CloseDB();
            //    db = null;
            //}
        }
    }
}
