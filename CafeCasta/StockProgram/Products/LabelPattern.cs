using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockProgram.DBObjects;
using System.IO;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace StockProgram.Products
{
    class LabelPattern
    {
        private Label label;
        private string patternPath;
        private PrintDialog pd;
     
        public LabelPattern(ref Label label, string patternPath,PrintDialog pd)
        {
            this.label = label;
            this.patternPath = patternPath;
            this.pd = pd;
        }

        public void  PrintLabel()
        {
            try
            {
                     string formattedString=GetPattern();
                     ReplaceInputs(ref formattedString);

                     RawPrinterHelper.SendStringToPrinter(pd.PrinterSettings.PrinterName, formattedString);                            
                                
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void ReplaceInputs( ref string formattedString)
        {
            //label1
            formattedString=formattedString.Replace("PRODUCT_CODE",  StaticObjects.ConvertTurkishChars(label.product_code));
            formattedString = formattedString.Replace("PRODUCT_NAME", StaticObjects.ConvertTurkishChars(label.product_name));
            formattedString=formattedString.Replace("COLOR", StaticObjects.ConvertTurkishChars(label.color));
            formattedString=formattedString.Replace("PRICE", label.price.ToString("#0.00")+"-TL");
            formattedString = formattedString.Replace("SIZE", label.size);
            formattedString=formattedString.Replace("BARCODE", label.barcode);

            ////label2
            //formattedString = formattedString.Replace("PRODUCT_CODE2", label2.product_code);
            //formattedString = formattedString.Replace("PRODUCT_NAME2", StaticObjects.ConvertTurkishChars(label2.product_name));
            //formattedString = formattedString.Replace("COLOR2", StaticObjects.ConvertTurkishChars(label2.color));
            //formattedString = formattedString.Replace("PRICE2", label2.price + "-TL");
            //formattedString = formattedString.Replace("BARCODE2", label2.barcode);
            return;
        }

        public string GetPattern()
        {
            using (StreamReader streamReader = new StreamReader(this.patternPath))
            {
                StringBuilder text = new StringBuilder();
                text.Append(streamReader.ReadToEnd());
                streamReader.Close();
                return text.ToString();
            }
        }

    }

    class Label
    {
        public string barcode { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public double price { get; set; }
        public string product_name { get; set; }
        public string product_code { get; set; }

        public Label()
        { 
        }
    }
}
