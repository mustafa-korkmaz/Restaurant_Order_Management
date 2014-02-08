using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.Sales
{
    public class SiparisKalem
    {
        public delegate void SiparisKalemHandler(object sender,EventArgs e);
        public event SiparisKalemHandler SiparisAmountChanged;
        public SiparisKalem()
        {
            this.Desc = string.Empty;
        }
        public ucAdisyon ParentAdisyon;
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public double Amount { get; set; }
        public double TotalAmount { get; set; }
        public int ProductId { get; set; }
        public double Porsion { get; set; }
        public double SalePrice { get; set; }
        public double BuyPrice { get; set; }
        public double UndiscountedPrice { get; set; }
        public string Barcode{ get;set; }
        public string Color { get; set; }
        public int ColorId { get; set; }
        public int Size { get; set; }
        public string Desc { get; set; }
        public string PorsionText 
        {
            get
            {
                string value = string.Empty;
                if(this.Porsion==2)
                {
                        value= "DUBLE";
                }
                else
                        value= this.Porsion.ToString();
                
                return value;
            }
      }

        /// <summary>
        /// fires when amount successfully changed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnAmountChanged(EventArgs e)
        {
            if (SiparisAmountChanged != null)
                SiparisAmountChanged(this, e);
        }

        /// <summary>
        /// Refreshes the amount on inherited control
        /// </summary>
        public void RefreshAmount()
        {
            OnAmountChanged(EventArgs.Empty);
        }

        public void SetParentAdisyon( ucAdisyon a)
        {
            this.ParentAdisyon = a;
        }
        public ucAdisyon GetParentAdisyon()
        {
            return this.ParentAdisyon;
        }

    }
}
