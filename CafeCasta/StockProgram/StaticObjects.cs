using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;

namespace StockProgram
{
     class StaticObjects
    {
        #region Convertions
        public static double ConvertToLiter(int miktar, Enums.Unit eskiBirim)
        {
            double retValue = 0;
            switch (eskiBirim)
            {
                case Enums.Unit.g: retValue = (double)(miktar * 0.001);
                    break;
                //case Enums.Unit.kg: retValue = (double)miktar;
                //    break;
                //case Enums.Unit.m3: retValue = (double)(miktar * 1000);
                //    break;
                //case Enums.Unit.ton: retValue = (double)(miktar * 1000);
                //    break;
                default: retValue = (double)miktar;
                    break;
            }

            return retValue;
        }
        public static double ConvertToM3(int miktar, Enums.Unit eskiBirim)
        {
            double retValue = 0;
            switch (eskiBirim)
            {
                case Enums.Unit.g: retValue = (double)(miktar * 0.000001);
                    break;
                //case Enums.Unit.kg: retValue = (double)(miktar*0.001);
                //    break;
                //case Enums.Unit.lt: retValue = (double)(miktar * 0.001);
                //    break;
                //case Enums.Unit.ton: retValue = (double)(miktar );
                //    break;
                default: retValue = (double)miktar;
                    break;
            }

            return retValue;
        }
        public static double ConvertToKg(int miktar, Enums.Unit eskiBirim)
        {
            double retValue = 0;
            switch (eskiBirim)
            {
                //case Enums.Unit.m3: retValue = (double)(miktar * 1000);
                //    break;
                //case Enums.Unit.g: retValue = (double)(miktar * 0.001);
                //    break;
                //case Enums.Unit.lt: retValue = (double)(miktar);
                //    break;
                //case Enums.Unit.ton: retValue = (double)(miktar * 1000);
                //    break;
                default: retValue = (double)miktar;
                    break;
            }

            return retValue;
        }
        public static double ConvertToGrams(int miktar, Enums.Unit eskiBirim)
        {
            double retValue = 0;
            switch (eskiBirim)
            {
                //case Enums.Unit.m3: retValue = (double)(miktar * 1000000);
                //    break;
                //case Enums.Unit.kg: retValue = (double)(miktar * 1000);
                //    break;
                //case Enums.Unit.lt: retValue = (double)(miktar * 1000);
                //    break;
                //case Enums.Unit.ton: retValue = (double)(miktar * 1000000);
                //    break;
                default: retValue = (double)miktar;
                    break;
            }

            return retValue;
        }
        public static double ConvertToTons(int miktar, Enums.Unit eskiBirim)
        {
            double retValue = 0;
            switch (eskiBirim)
            {
                //case Enums.Unit.m3: retValue = (double)(miktar);
                //    break;
                //case Enums.Unit.kg: retValue = (double)(miktar *0.001);
                //    break;
                //case Enums.Unit.lt: retValue = (double)(miktar * 0.001);
                //    break;
                case Enums.Unit.g: retValue = (double)(miktar * 0.000001);
                    break;
                default: retValue = (double)miktar;
                    break;
            }

            return retValue;
        }
        public static string ConvertToCurrency(int value)
        {
            string currency = string.Empty;
            switch (value)
            {
                case 0: currency = "TL";
                    break;
                case 1: currency = "USD";
                    break;
                case 2: currency = "EUR";
                    break;
                case 3: currency = "GPB";
                    break;
                default:
                    break;
            }

            return currency;
        }
        #endregion

        //public static string AccessConnStr =" Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:Database Password=48546;Jet OLEDB:Compact Without Replica Repair=True;Data Source=C:\\StockProgram\\Stocks.mdb;Persist Security Info=True";
  //   public static string MySqlConn = "server=127.0.0.1;User Id=root;password=;database=casta_ia";
       public static string MySqlConn = ""; 
         public static string AccessConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Databases\\Stocks.accdb;Jet OLEDB:Database Password=q;";
         public static string MainImagePath = "\\Uploads\\product_images\\";
         public static string MainBackupPath = "\\DBs\\";
         public static string LabelPatternPath =  @"kirkikoglu.prn";
         public static int MenuItemCountPerRow = 4;
         public static int PrinterConnectionTimeout = 2200;
         public static int SlipProductNameCharLength = 23;
         private static char[] DBSpaceChar;
        public static string DBSpace
        {
            get 
            {
                return StaticObjects.InitializeSpaceChar();
            }
        }

        private static string InitializeSpaceChar()
        {
            string retValue = string.Empty;
            DBSpaceChar = new char[3];
            DBSpaceChar[0] = '"';
            DBSpaceChar[1] = ' ';
            DBSpaceChar[2] = '"';
            foreach (char item in DBSpaceChar)
            {
                retValue += item.ToString();
            }
            return retValue;
        }

        /// <summary>
        /// Gridview GroupPanelText
        /// </summary>
        public static  string GroupPanelText = "Aramalarınızı kategorilere göre sınflandırmak için buraya istediğiniz kolonu sürükleyebilirsiniz.";

         /// <summary>
         /// Disposes given control
         /// </summary>
         /// <param name="control"></param>
         /// <returns></returns>
        public static int ClearControl(IDisposable control)
        {
            try
            {
                control.Dispose();
                return 1;
            }
            catch (Exception ex)
            {

                return 0;
            }

        }

   
         /// <summary>
         /// kaliteden minimum kayıp ile istenilen boyutta image döndürür
         /// </summary>
         /// <param name="image"></param>
         /// <param name="width"></param>
         /// <param name="height"></param>
         /// <returns></returns>
        public static System.Drawing.Bitmap ResizeImage(System.Drawing.Image image, int width, int height)
        {
            //a holder for the result
            System.Drawing.Bitmap result = new System.Drawing.Bitmap(width, height);
            // set the resolutions the same to avoid cropping due to resolution differences
            result.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //use a graphics object to draw the resized image into the bitmap
            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(result))
            {
                //set the resize quality modes to high quality
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //draw the image into the target bitmap
                graphics.DrawImage(image, 0, 0, result.Width, result.Height);
            }

            //return the resulting bitmap
            return result;
        }

         /// <summary>
         /// gives the unique product_code number
         /// </summary>
         /// <param name="dr"></param>
         public static string GetProductCode(ref DataRow dr)
         {
             char delimeter = ':';
             string p_id = (dr["product_id"]).ToString();
             string p_size = (dr["product_size"]).ToString();
             string p_color = (dr["product_color"]).ToString();
             return p_id + delimeter + p_size + delimeter + p_color;
         }
       
      /// <summary>
     /// User Class
     /// </summary>
       public  class User
        {
            public static int Id { get; set; }
            public static string UserName { get; set; }
            public static string Name { get; set; }
            public static string Password { get; set; }
            public static int IsDeleted { get; set; }
            public static bool IsLoggedIn = false;
            public static Enums.UserRole Role { get; set; }
         
         }

         /// <summary>
         /// converts the string contains Ş,İ,Ğ,Ö,Ç,Ü by replacing with S,I,G,O,C,U
         /// </summary>
         /// <param name="turkish_word"></param>
         /// <returns></returns>
       public static string ConvertTurkishChars(string turkish_word)
       {
           string word = turkish_word.ToUpper();
           word = word.Replace("Ş", "S");
           word = word.Replace("Ç", "C");
           word = word.Replace("Ğ", "G");
           word = word.Replace("Ö", "O");
           word = word.Replace("Ü", "U");
           word = word.Replace("İ", "I");
           word = word.Replace("X", "x"); //x harfini büyütme
           return word;
       }

       public static string ConvertWordLength(string word,string desc, int length)
       {
           //desc=porsiyon biligisi
           string retValue = string.Empty;
           if (word.Length >= length)
           {
               retValue = word.Substring(0, length-5);
               retValue +=(desc=="")? word.Substring(length-5,5): desc.Substring(0, 5);
           }
           else
           {
               word += desc;
               while (word.Length+desc.Length < length)
               {
                   word += " ";
               }
               retValue = word;
           }
           return retValue;
       }

       public static class Currencies
       {

           public static double DolarAlis
           {
               get
               {
                   XmlTextReader oku = new XmlTextReader("http://www.tcmb.gov.tr/kurlar/today.xml");
                   XmlDocument myDoc = new XmlDocument();
                   myDoc.Load(oku);
                 //  int index = myDoc.DocumentElement.ChildNodes.Count - 1;
                   string alis = myDoc.DocumentElement.ChildNodes[0].SelectSingleNode("BanknoteBuying").InnerText;
                   return Convert.ToDouble(alis.Replace('.',','));
               }
           }
           public static double DolarSatis
           {
               get
               {
                   XmlTextReader oku = new XmlTextReader("http://www.tcmb.gov.tr/kurlar/today.xml");
                   XmlDocument myDoc = new XmlDocument();
                   myDoc.Load(oku);
                   //  int index = myDoc.DocumentElement.ChildNodes.Count - 1;
                   string satis = myDoc.DocumentElement.ChildNodes[0].SelectSingleNode("BanknoteSelling").InnerText;
                   return Convert.ToDouble(satis.Replace('.',','));     
               }
           }


           public static double AltinAlis
           {
               get {
                   XmlTextReader oku = new XmlTextReader("http://www.altinkaclira.com/altin.xml");
                   XmlDocument myDoc = new XmlDocument();
                   myDoc.Load(oku);
                   int index = myDoc.DocumentElement.ChildNodes.Count - 1;
                   string alis = myDoc.DocumentElement.ChildNodes[index].SelectSingleNode("al").InnerText;
                 return Convert.ToDouble(alis.Replace('.', ','));
               }
           }
           public static double AltinSatis
           {
               get
               {
                   XmlTextReader oku = new XmlTextReader("http://www.altinkaclira.com/altin.xml");
                   XmlDocument myDoc = new XmlDocument();
                   myDoc.Load(oku);
                   int index = myDoc.DocumentElement.ChildNodes.Count - 1;
                   string satis = myDoc.DocumentElement.ChildNodes[index].SelectSingleNode("sat").InnerText;
                   return Convert.ToDouble( satis.Replace('.', ','));
               }
           }
 
       }

       public static class Settings
       {
           public static string mainImageName = "";
           public static int table_count_per_line = 0;
           public static int table_width = 0;
           public static int table_height = 0;
           public static int tables_refresh_time = 0;
           public static int menu_item_width = 0;
           public static int menu_item_height = 0;
           public static int menu_item_count_per_line = 0;
           public static int menu_item_name_panel_height = 0;
           public static string db_user = "";
           public static string db_host = "";
           public static string db_name = "";
       }
    }
}
