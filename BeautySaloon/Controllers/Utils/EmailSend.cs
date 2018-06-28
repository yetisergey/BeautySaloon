namespace BeautySaloon.Controllers.Utils
{
    using System.Net;
    using System.Net.Mail;
    using System.Threading;
    public static class EmailSend
    {
        /// <summary>
        /// Отправка письма на почтовый ящик
        /// </summary>
        /// <param name="message"></param>
        public static void SendMail(string message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(Properties.Settings.Default.MailFrom);
                mail.To.Add(new MailAddress(Properties.Settings.Default.MailTo));
                mail.Subject = "Новая запись!";
                mail.Body = message;
                Thread th = new Thread(delegate ()
                {
                    using (SmtpClient sc = new SmtpClient(Properties.Settings.Default.MailFromHost, 587))
                    {
                        try
                        {
                            sc.EnableSsl = true;
                            sc.Credentials = new NetworkCredential(Properties.Settings.Default.MailFrom, Properties.Settings.Default.MailFromPassword);
                            sc.Send(mail);
                        }
                        catch
                        {
                        }
                    }
                });
                th.Start();
            }
            catch
            {
            }
        }
    }
}