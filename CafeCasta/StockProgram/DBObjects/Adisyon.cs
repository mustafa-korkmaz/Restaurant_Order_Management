using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockProgram.Sales;
using System.Data;

namespace StockProgram.DBObjects
{
    public class Adisyon
    {
        private Orders order;
        private Customer c;
        List<Orders> orderList;
        private Printer printer;
        private Table table;
        public string waiter_name { get; set; }
        public delegate void ConnectionHandler(object sender, EventArgs e);
        public event ConnectionHandler ConnectionFailed;

        public Adisyon(Orders order)
        {
            this.order = order;
        }

        public Adisyon(List<Orders> orderList)
        {
            this.orderList = orderList;
        }

        public void SetTable(Table t)
        {
            this.table = t;
        }

        protected virtual void OnConnectionFailed(EventArgs e)
        {
            if (ConnectionFailed != null)
                ConnectionFailed(this.printer, e);
        }

        void printer_PrinterConnectionFailed(object sender, EventArgs e)
        {
            OnConnectionFailed(EventArgs.Empty);
        }

        /// <summary>
        /// .mutfak fişi yazdırır
        /// </summary>
        public void PrintKitchenSlip()
        {
            List<string> siparisString = new List<string>();
            RetrieveHeader(ref siparisString);

            List<SiparisKalem> newSiparisList = new List<SiparisKalem>();
            List<SiparisKalem> tempList = this.order.siparisList;
            int length = this.order.siparisList.Count;
            int index = 0;
            int i = 0;

            foreach (SiparisKalem item in tempList)
            {
                 while (i < this.order.siparisList.Count)
                {
                    if (index == i)
                    {
                        i++;
                        continue;
                    }
                    else
                    {
                        if (item.Amount>0 && item.ProductId == tempList[i].ProductId && item.Porsion == tempList[i].Porsion && item.Desc==tempList[i].Desc)
                        {
                            item.Amount+=tempList[i].Amount; //ürün, acıklama ve porsiyon aynı. siprişte görünecek miktarı arttır.
                            tempList[i].Amount=0; //gecici listedeki bir öncekine aktarılan menu için miktarı 1 azalt
                        }
                    }
                    i++;                      
                }
                index++;
                i = index;
                newSiparisList.Add(item);
            }

            RetrieveOrderContentForKitchen(ref newSiparisList, ref siparisString);
        
            if (this.order.type == Enums.OrderType.Paket)
            {
                RetrieveFooter(ref siparisString);
            }
            this.printer.SendData(GetRawString(siparisString));
        }


        /// <summary>
        /// Hesap fişi yazdırır
        /// </summary>
        //public void PrintCheckSlip()
        //{
        //    List<string> siparisString = new List<string>();

        //    RetrieveHeader(ref siparisString);

        //    List<SiparisKalem> newSiparisList = new List<SiparisKalem>();
        //    List<SiparisKalem> tempList = this.order.siparisList;
        //    int length = this.order.siparisList.Count;
        //    int index = 0;
        //    int i = 0;

        //    foreach (SiparisKalem item in tempList)
        //    {
        //        while (i < this.order.siparisList.Count)
        //        {
        //            if (index == i)
        //            {
        //                i++;
        //                continue;
        //            }
        //            else
        //            {
        //                if (item.Amount > 0 && item.ProductId == tempList[i].ProductId && item.Porsion == tempList[i].Porsion)
        //                {
        //                    item.Amount += tempList[i].Amount; //ürün ve porsiyon aynı. siprişte görünecek miktarı arttır.
        //                    tempList[i].Amount = 0; //gecici listedeki bir öncekine aktarılan menu için miktarı 1 azalt
        //                }
        //            }
        //            i++;
        //        }
        //        index++;
        //        i = index;
        //        newSiparisList.Add(item);
        //    }

        //    RetrieveOrderContentForCheck(ref newSiparisList, ref siparisString);
        //    RetrieveFooter(ref siparisString);
        //    this.printer.SendData(GetRawString(siparisString));
        //}

        /// <summary>
        /// hesaba ait tüm siparişler için mutfak fisi yazdırır
        /// </summary>
        public void PrintKitchenSlipForAllOrders()
        {
            string time = DateTime.Now.ToShortTimeString();
            List<string> siparisString = new List<string>();
            List<SiparisKalem> newSiparisList = new List<SiparisKalem>();
            int x = 0;
            foreach (Orders order in this.orderList)	  
            {
                newSiparisList.Clear();
                //set header
                if (x == 0) //first order, then write time
                {
                    siparisString.Add("Sip. No: #" + order.order_id + "\t(RESTORAN)\t\t" + time);
                    x++;
                }
                else
                {
                    siparisString.Add("Sip. No: #" + order.order_id + "\t(RESTORAN)\t\t");
                    x++;
                }
                siparisString.Add("------------------------------------------------");
                siparisString.Add("MASA: " + this.table.category.Name+" #"+this.table.name);
                siparisString.Add("PERSONEL: " + order.staff_name.Trim());
                siparisString.Add("-------------\n");
                List<SiparisKalem> tempList = order.siparisList;		
                int length = order.siparisList.Count;
                int index = 0;
                int i = 0;

                foreach (SiparisKalem item in tempList)
                {
                    while (i < order.siparisList.Count)
                    {
                        if (index == i)
                        {
                            i++;
                            continue;
                        }
                        else
                        {
                            if (item.Amount > 0 && item.ProductId == tempList[i].ProductId && item.Porsion == tempList[i].Porsion && item.Desc == tempList[i].Desc)
                            {
                                item.Amount += tempList[i].Amount; //ürün, acıklama ve porsiyon aynı. siprişte görünecek miktarı arttır.
                                tempList[i].Amount = 0; //gecici listedeki bir öncekine aktarılan menu için miktarı 1 azalt
                            }
                        }
                        i++;
                    }
                    index++;
                    i = index;
                    newSiparisList.Add(item);
                }
                RetrieveOrderContentForKitchen(order, ref newSiparisList, ref siparisString);
               if(x!=this.orderList.Count)
                siparisString.Add("-------------\n");//footer
           }

            //if (this.order.type == Enums.OrderType.Paket)
            //{
            //    RetrieveFooter(ref siparisString);
            //}
            this.printer.SendData(GetRawString(siparisString));
        }

        /// <summary>
        /// hesaba ait tüm siparişler için hesap fisi yazdırır
        /// </summary>
        public void PrintCheckSlip()
        {
            string time = DateTime.Now.ToShortTimeString();
            List<string> siparisString = new List<string>();
            List<SiparisKalem> newSiparisList = new List<SiparisKalem>();
            int x = 0;
            foreach (Orders order in this.orderList)
            {
                newSiparisList.Clear();
                //set header
                if (x == 0) //first order, then write time
                {
                    siparisString.Add("Sip. No: #" + order.order_id + "\t(RESTORAN)\t\t" + time);
                    siparisString.Add("MASA: " + this.table.category.Name + " #" + this.table.name);
                    siparisString.Add("------------------------------------------------");
                    x++;
                }
                else
                {
                    siparisString.Add("Sip. No: #" + order.order_id );
                    siparisString.Add("------------------------------------------------");
                    x++;
                }
         
                List<SiparisKalem> tempList = order.siparisList;
                int length = order.siparisList.Count;
                int index = 0;
                int i = 0;

                foreach (SiparisKalem item in tempList)
                {
                    while (i < order.siparisList.Count)
                    {
                        if (index == i)
                        {
                            i++;
                            continue;
                        }
                        else
                        {
                            if (item.Amount > 0 && item.ProductId == tempList[i].ProductId && item.Porsion == tempList[i].Porsion)
                            {
                                item.Amount += tempList[i].Amount; //ürün ve porsiyon aynı. siprişte görünecek miktarı arttır.
                                tempList[i].Amount = 0; //gecici listedeki bir öncekine aktarılan menu için miktarı 1 azalt
                            }
                        }
                        i++;
                    }
                    index++;
                    i = index;
                    newSiparisList.Add(item);
                }
                RetrieveOrderContentForCheck(ref newSiparisList, ref siparisString);
                if (x != this.orderList.Count)
                    siparisString.Add("-------------\n");

            }
            RetrieveFooterForAllOrders(ref siparisString);
            this.printer.SendData(GetRawString(siparisString));
        }

        /// <summary>
        /// sets the header of slip
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        private List<string> RetrieveHeader(ref List<string> header)
        {
            string time = DateTime.Now.ToShortTimeString();
            if (this.order.type == Enums.OrderType.Restoran)
            {
                header.Add("Sip. No: #" + this.order.order_id + "\t(RESTORAN)\t\t"+time);
            }
            else //paket
                header.Add("Sip. No: #" + this.order.order_id + "\t(PAKET SERVİS)\t\t"+time);

            header.Add("------------------------------------------------");
            switch (this.order.type)
            {
                case Enums.OrderType.Restoran:
                    header.Add("MASA: " + this.table.category.Name + " #" + this.table.name);
                    if (this.waiter_name!="none") //hesap fişi alırken personel adına gerek yok none gönderiyorum
                    {
                        header.Add("PERSONEL: " + this.order.staff_name);                        
                    }
                    break;
                case Enums.OrderType.Paket:
                    header.Add(c.name);
                    header.Add(c.adress);
                    header.Add("TEL: "+c.tel);
                    if (c.note!="")
                    {
                        header.Add("\nNOT: "+c.note);                       
                    }
                    break;
                default:
                    break;
            }
            header.Add("-------------\n");
            return header;
        }

        /// <summary>
        /// sets the orders of slip
        /// </summary>
        /// <param name="newSiparisList"></param>
        /// <param name="siparisString"></param>
        private void RetrieveOrderContentForKitchen(ref List<SiparisKalem> newSiparisList,ref List<string>siparisString  )
        {
            foreach (SiparisKalem item in newSiparisList)
            {
                if (item.Amount <= 0)
                {
                    continue;
                }
                else
                {
                    string name = (item.Porsion == 1) ? StaticObjects.ConvertWordLength(item.ProductName,"",StaticObjects.SlipProductNameCharLength) : StaticObjects.ConvertWordLength(item.ProductName , "(" +item.PorsionText+ ")",StaticObjects.SlipProductNameCharLength);
                    if (this.order.type==Enums.OrderType.Restoran)
                    {
                        if (item.Desc.Trim() != "")
                        {
                            siparisString.Add(name + "\t\tx" + item.Amount + "\t\n(" + item.Desc + ")\n");
                        }
                        else
                            siparisString.Add(name + "\t\tx" + item.Amount + "\n");
             
                    }
                    else//paket servis
                    if (item.Desc.Trim()!="")
                    {
                        siparisString.Add(name + "\t" + item.SalePrice.ToString("#0.00") + "\tx" + item.Amount + "\t" + (item.Amount * item.SalePrice).ToString("#0.00")+"\n("+item.Desc+")\n");
                    }
                    else
                        siparisString.Add(name + "\t" + item.SalePrice.ToString("#0.00") + "\tx" + item.Amount + "\t" + (item.Amount * item.SalePrice).ToString("#0.00")+"\n");
                }
            }
        }

        /// <summary>
        /// sets the orders of slip
        /// </summary>
        /// <param name="newSiparisList"></param>
        /// <param name="siparisString"></param>
        private void RetrieveOrderContentForKitchen(Orders order, ref List<SiparisKalem> newSiparisList, ref List<string> siparisString)
        {
            foreach (SiparisKalem item in newSiparisList)
            {
                if (item.Amount <= 0)
                {
                    continue;
                }
                else
                {
                    string name = (item.Porsion == 1) ? StaticObjects.ConvertWordLength(item.ProductName, "", StaticObjects.SlipProductNameCharLength) : StaticObjects.ConvertWordLength(item.ProductName, "(" + item.PorsionText + ")", StaticObjects.SlipProductNameCharLength);
                    if (order.type == Enums.OrderType.Restoran)
                    {
                        if (item.Desc.Trim() != "")
                        {
                            siparisString.Add(name + "\t\tx" + item.Amount + "\t\n(" + item.Desc + ")\n");
                        }
                        else
                            siparisString.Add(name + "\t\tx" + item.Amount + "\n");

                    }
                    else//paket servis
                        if (item.Desc.Trim() != "")
                        {
                            siparisString.Add(name + "\t" + item.SalePrice.ToString("#0.00") + "\tx" + item.Amount + "\t" + (item.Amount * item.SalePrice).ToString("#0.00") + "\n(" + item.Desc + ")\n");
                        }
                        else
                            siparisString.Add(name + "\t" + item.SalePrice.ToString("#0.00") + "\tx" + item.Amount + "\t" + (item.Amount * item.SalePrice).ToString("#0.00") + "\n");
                }
            }
        }
        /// <summary>
        /// sets the check slip content
        /// </summary>
        /// <param name="newSiparisList"></param>
        /// <param name="siparisString"></param>
        private void RetrieveOrderContentForCheck(ref List<SiparisKalem> newSiparisList, ref List<string> siparisString)
        {
            foreach (SiparisKalem item in newSiparisList)
            {
                if (item.Amount <= 0)
                {
                    continue;
                }
                else
                {
                    string name = (item.Porsion == 1) ? StaticObjects.ConvertWordLength(item.ProductName, "",StaticObjects.SlipProductNameCharLength) : StaticObjects.ConvertWordLength(item.ProductName ,"(" + item.PorsionText + ")", StaticObjects.SlipProductNameCharLength);
                    siparisString.Add(name + "\t" + item.SalePrice.ToString("#0.00") + "\tx" + item.Amount + "\t" + (item.Amount * item.SalePrice).ToString("#0.00") + "\n");
                }
            }
        }

        /// <summary>
        /// sets the footer of  slip
        /// </summary>
        /// <param name="siparisString"></param>
        private void RetrieveFooter(ref List<string > siparisString)
        {
          siparisString.Add("\t\t\t\t-------------");
          siparisString.Add("\t\t\t\tToplam: " + this.order.GetOrderPrice().ToString("#0.00") + " TL");
        }

        /// <summary>
        /// sets the footer of slip for all  orders list 
        /// </summary>
        /// <param name="siparisString"></param>
        private void RetrieveFooterForAllOrders(ref List<string> siparisString)
        {
            double total = 0;
            foreach (Orders o in this.orderList)
            {
                total += o.GetOrderPrice();  
            }
            siparisString.Add("\t\t\t\t-------------");
            siparisString.Add("\t\t\t\tToplam: " + total.ToString("#0.00") + " TL");
        }
     
        private Customer GetCustomer()
        {
            int customer_id = this.order.owner_id;
            string sql = "select * from customer_details where customer_id=" + customer_id;
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            dt = cmd.GetDataTable(sql);

            Customer c = new Customer();
            c.name = dt.Rows[0]["customer_name"].ToString();
            c.tel = dt.Rows[0]["customer_tel"].ToString().Trim();
            c.note = dt.Rows[0]["customer_note"].ToString().Trim();
            c.adress = dt.Rows[0]["customer_address"].ToString().Trim();
            return c;
        }

        public  void SetCustomer(Customer c)
        {
            this.c = c;
        }

        private StringBuilder GetRawString(List<string> strList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string  item in strList)
            {
                    sb.Append(item + "\n");                    
            }
            return sb;
        }

        public void SetPrinter()
        {
        //    string sql = "select * from printer_details where type=" + Convert.ToInt16(type);
            string sql = "select * from printer_details where is_deleted=0";
            MySqlDbHelper cmd = new MySqlDbHelper(StaticObjects.MySqlConn);
            DataTable dt = new DataTable();
            dt = cmd.GetDataTable(sql);

            this.printer = new Printer( dt.Rows[0]["printer_ip"].ToString().Trim());
            this.printer.name = dt.Rows[0]["printer_desc"].ToString().Trim();
            this.printer.PrinterConnectionFailed += new Printer.PrinterConnectionHandler(printer_PrinterConnectionFailed);
            printer.ConnectToPrinter();
        }
    }
}
