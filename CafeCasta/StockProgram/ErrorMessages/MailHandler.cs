using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.ErrorMessages
{
    public delegate void MailEventHandler(object sender, EventArgs e);
    public enum MailServerNames { Keskinoglu = 0, Gmail }
    public class Credentials
    {
        private string _userName;
        private string _password;
        private List< string> _mailTo;
        private string _mailFrom;
        private string _mailCC;
        private string _mailBCC;

        #region get-set methods

        public string userName
        {
            set { _userName = value; }
            get { return _userName; }
        }

        public string password
        {
            set { _password = value; }
            get { return _password; }
        }

        public List< string> mailTo
        {
            set { _mailTo = value; }
            get { return _mailTo; }
        }
        public string mailBCC
        {
            set { _mailBCC = value; }
            get { return _mailBCC; }
        }
        public string mailCC
        {
            set { _mailCC = value; }
            get { return _mailCC; }
        }

        public string mailFrom
        {
            set { _mailFrom = value; }
            get { return _mailFrom; }
        }
        #endregion


        public static string computerName;

        public Credentials()
        {
            Credentials.computerName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
            mailTo = new List<string>();
        }
    }
    public class StandartMail : Credentials
    {
        private MailServerNames mailServerName;
        private string subject;    // mail topic
        private string content;  // mail details
        private System.Net.Mail.SmtpClient smtpClient;
        public event MailEventHandler MailSent;
        public event MailEventHandler MailSending;

        private static bool mailSent = false;

        public StandartMail(MailServerNames mailServerName)
        {
            this.MailServerName = mailServerName;
            //CheckMailServer();
        }
        public StandartMail(MailServerNames mailServerName, string e_mail_to, string e_mail_Bcc)
        {
            this.MailServerName = mailServerName;
            mailTo.Add( e_mail_to);
            mailBCC = e_mail_Bcc;
            //CheckMailServer();
        }

        #region MailEvents
        /// <summary>
        /// fires when mail successfully sent
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMailSent(EventArgs e)
        {
            if (MailSent != null)
                MailSent(this, e);
        }

        /// <summary>
        /// fires when mail sending begins
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMailSending(EventArgs e)
        {
            if (MailSending != null)
                MailSending(this, e);
        }
        #endregion
  
         #region  Get Set Methods

        /// <summary>
        /// sets subject of mail
        /// </summary>
        public string Subject
        {
            set { subject = value; }
        }
        /// <summary>
        /// sets error details. it can be null.
        /// </summary>
        public string Content
        {
            set { content = value; }
        }
        /// <summary>
        /// returns the state of mail 
        /// </summary>
        public static bool IsMailSent
        {
            get { return mailSent; }
        }

        public MailServerNames MailServerName
        {
            set { mailServerName = value; }
            get { return mailServerName; }
        }
        #endregion

        /// <summary>
        /// Checks server name either gmail or keskinoglu
        /// </summary>
        private void CheckMailServer()
        {
            switch (Convert.ToInt16(MailServerName))
            {
                case 0: InitializeKeskMail();
                    break;
                case 1: InitializeGmail();
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// sets mail credentails for keskinoglu email server
        /// </summary>
        private void InitializeKeskMail()
        {
            base.userName = "keskinoglu/temp.mustafa"; //keskinoglu username
            base.password = "Tm2012"; //keskinoglu password;
            if (mailTo.Count <= 0)
            {
                base.mailTo.Add ( "m.korkmaz@keskinoglu.com.tr");
            }
            if (mailFrom==null)
            {
                base.mailFrom = "temp.mustafa@keskinoglu.com.tr";
            }
         
        }

        /// <summary>
        /// sets mail credentails for gmail server
        /// </summary>
        private void InitializeGmail()
        {
            base.userName = "info@createchsoftware.net"; //gmail username
            base.password = "19051907"; //gmail password;
            base.mailTo.Add ( "m.korkmaz@createchsoftware.net");
        //    base.mailTo.Add("taylan.sbrcn@gmail.com");
            base.mailFrom = "info@createchsoftware.net";
        }

        /// <summary>
        /// Sends report mail from user to software developer
        /// </summary>
        /// <param name="exc"></param>
        /// <returns></returns>
        public bool Send()
        {
            OnMailSending(EventArgs.Empty);
            CheckMailServer();
            try
            {
                string displayName = computerName;

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                System.Net.NetworkCredential cred = new System.Net.NetworkCredential(base.userName, base.password);

                foreach (string mail_to in  base.mailTo) // tüm göndermek istediğimiz adresleri ekleyelim
                {
                    mail.To.Add(mail_to);
                }

                if (mailBCC!=null)
                {
                    mail.Bcc.Add(mailBCC);
                }
                if (mailCC != null)
                {
                    mail.CC.Add(mailCC);
                }
                mail.Subject = subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.From = new System.Net.Mail.MailAddress(base.mailFrom, displayName);
                mail.IsBodyHtml = true;
                mail.Body = SetMailBody();
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                SetSmtpClient();
                smtpClient.Credentials = cred;
                smtpClient.Send(mail);
                mail.Dispose();
                OnMailSent(EventArgs.Empty);
                return true;

            }
            catch (Exception)
            {

                return false;
            }

        }

        /// <summary>
        /// Sets the client's SMTP
        /// </summary>
        /// <param name="_content"></param>
        /// <returns></returns>
        private void SetSmtpClient()
        {
            switch (Convert.ToInt16(mailServerName))
            {
                case 0: smtpClient = new System.Net.Mail.SmtpClient("email.keskinoglu.com.tr", 25);
                    smtpClient.EnableSsl = false;
                    break;
                case 1: smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                    smtpClient.EnableSsl = true;
                    break;
                default:
                    return;
            }
            smtpClient.UseDefaultCredentials = false;
            smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            return;

        }

        /// <summary>
        /// Sets the body for mail
        /// </summary>
        /// <param name="_content"></param>
        /// <returns></returns>
        protected virtual string SetMailBody()
        {
            string body = string.Empty;

            body += "Bu e-posta " + computerName + " kullanıcısı tarafından otomatik olarak oluşturulmuştur.";

            body += "E-posta içeriği: " + content;

            return body;

        }

    }
    public class ExceptionMail : StandartMail
    {
        private Exception exception;

        private string errorSource;

        public string ErrorSource
        {
            set
            {
                errorSource = value;
            }
        }

        private string errorInfo;

        public string ErrorInfo
        {
            set
            {
                errorInfo = value;
            }
        }

        public ExceptionMail(Exception exc, MailServerNames mailServerName)
            : base(mailServerName)
        {
            this.exception = exc;
        }
        protected override string SetMailBody()
        {
            string body = string.Empty;

            body += "Bu e-posta " + computerName + " kullanıcısı tarafından otomatik olarak oluşturulmuştur.";

            if (errorInfo != null)
            {
                body += Environment.NewLine + "Sistemdeki olası hata: " + errorInfo;
            }
            body += Environment.NewLine + "Sistemdeki hata mesajı: " + exception.Message;

            if (errorSource!=null)
            {
                body += Environment.NewLine + "Sistemdeki hata kaynağı: " + errorSource;
            }

            return body;

        }


    }
}
