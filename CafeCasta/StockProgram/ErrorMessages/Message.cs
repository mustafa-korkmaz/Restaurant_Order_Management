using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.ErrorMessages
{
    public  class Message
    {
        private string title;
        public  System.Windows.Forms.DialogResult WriteMessage(string messageContent, System.Windows.Forms.MessageBoxIcon messageBoxIcon,System.Windows.Forms.MessageBoxButtons messageBoxButtons)
        {
            switch (messageBoxIcon)
            {
                case System.Windows.Forms.MessageBoxIcon.Warning: title = "Uyarı";
                    break;
                case System.Windows.Forms.MessageBoxIcon.Error: title = "Hata";
                    break;
                case System.Windows.Forms.MessageBoxIcon.Information: title = "Bilgi";
                    break;
                case System.Windows.Forms.MessageBoxIcon.None: title = "";
                    break;
                 default:
                    break;
            }
            return (System.Windows.Forms.MessageBox.Show(messageContent, title, messageBoxButtons, messageBoxIcon));// if (MessageBox.Show("Kümesiniz şu anda", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
        }
        
    }
}
