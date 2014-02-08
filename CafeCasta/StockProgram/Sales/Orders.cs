using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.Sales
{
    public class Orders
    {
        public List<SiparisKalem> siparisList { get;set;}
        public int owner_id { get; set; } //paket servis için customer_id; restoran içi servis için table_id
        public int order_id { get; set; }
        public int staff_id { get; set; }
        public string  staff_name { get; set; } //siparişi alan
        public Enums.OrderType type{get;set;}
        public int account_id { get; set; }
        public string owner_name { get; set; }  //paket servis için customer_name; restoran içi servis için table_name
        public Orders(List<SiparisKalem> siparisList, int owner_id, int order_id,int account_id)
        {
            this.siparisList = siparisList;
            this.owner_id = owner_id;
            this.order_id = order_id;
            this.account_id = account_id;
        }

        public double GetOrderPrice()
        {
            double price = 0;
            foreach (SiparisKalem item in this.siparisList)
            {
                price += item.SalePrice * item.Amount;
            }
            return price;
        }
    }
}
