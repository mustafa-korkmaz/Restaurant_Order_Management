using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace StockProgram.DBObjects
{
    class Printer
    {
        public int id { get; set; }
        public string ip { get; set; }
        public Enums.PrinterType type {get;set;}
        public string desc { get; set; }
        public string name { get; set; }
        public delegate void PrinterConnectionHandler(object sender, EventArgs e);
        public event PrinterConnectionHandler PrinterConnectionFailed;
        private Socket clientSock;

        public Printer()
        { 
        
        }
        public Printer(int id)
        {
            this.id=id;
        }
        public Printer(string ip)
        {
            this.ip = ip;
        }

        public void ConnectToPrinter()
        {
            clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSock.NoDelay = true;
            IPAddress IP = IPAddress.Parse(this.ip);

            try
            {
     //           IPEndPoint remoteEP = new IPEndPoint(IP, 9100);
                IAsyncResult result = clientSock.BeginConnect(ip, 9100, null, null);
                bool success = result.AsyncWaitHandle.WaitOne(StaticObjects.PrinterConnectionTimeout, true);
            //    clientSock.Connect(remoteEP);
                if (!success)
                {
                    clientSock.Close();
                    OnConnectionFailed(EventArgs.Empty); //bağlantı gerçekleşmediğini bildir.
                }
            }
            catch (Exception)
            {
                OnConnectionFailed(EventArgs.Empty); //bağlantı gerçekleşmediğini bildir.
            }
    
        }

        /// <summary>
        /// fires when socket connection failed
        /// </summary>
        /// <param name="e"></param>
        protected  virtual void OnConnectionFailed(EventArgs e)
        {
            if (PrinterConnectionFailed != null)
                PrinterConnectionFailed(this, e);
        }

        public void SendData(StringBuilder sb)
        {
            Encoding enc = Encoding.ASCII; 
            // Sends an ESC/POS command to the printer to cut the paper
            string output =  Convert.ToChar(29) + "V" + Convert.ToChar(65) + Convert.ToChar(0);
            char[] array = output.ToCharArray();
            byte[] cutPaper = enc.GetBytes(array);
         
            // Data to be sent
            string data = StaticObjects.ConvertTurkishChars(sb.ToString());
            char[] arrayData = data.ToCharArray();
            byte[] rawData = enc.GetBytes(arrayData);

            // Line feed hexadecimal values
            byte[] lineFeed = new byte[4];
            lineFeed[0] = 0x0A;
            lineFeed[1] = 0x0A;
            lineFeed[2] = 0x0A;
            lineFeed[3] = 0x0A;

            // Send the bytes over 
            try
            {
                clientSock.Send(rawData);
                clientSock.Send(lineFeed);
                clientSock.Send(cutPaper);
                clientSock.Close();
            }
            catch (Exception)
            {
                OnConnectionFailed(EventArgs.Empty); //bağlantı gerçekleşmediğini bildir.
            }         
        }
    }
}
