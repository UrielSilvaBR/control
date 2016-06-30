using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades.EnvioEmail
{
    public static class EmailHelper
    {
        public static void SendMail(string recipient, string subject, string body, string attachmentFilename)
        {
            SmtpClient smtpClient = new SmtpClient();
            //NetworkCredential basicCredential = new NetworkCredential(ConfigurationManager.AppSettings["smtpUser"].ToString(), ConfigurationManager.AppSettings["smtpPass"].ToString());
            NetworkCredential basicCredential = new NetworkCredential(Properties.Settings.Default.smtpUser, Properties.Settings.Default.smtpPass);
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress(Properties.Settings.Default.smtpUser);
            
            // setup up the host, increase the timeout to 5 minutes
            smtpClient.Host = Properties.Settings.Default.smtpServer;
            smtpClient.Port = Convert.ToInt32(Properties.Settings.Default.smtpPort);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;
            smtpClient.Timeout = (60 * 5 * 1000);

            message.From = fromAddress;
            message.Subject = subject + ", catiablack@hotmail.com, uriel@timepix.com.br";
            message.IsBodyHtml = false;
            message.Body = body;
            message.To.Add(recipient);

            if (attachmentFilename != null)
            {
                Attachment attachment = new Attachment(attachmentFilename, MediaTypeNames.Application.Octet);
                ContentDisposition disposition = attachment.ContentDisposition;
                disposition.CreationDate = File.GetCreationTime(attachmentFilename);
                disposition.ModificationDate = File.GetLastWriteTime(attachmentFilename);
                disposition.ReadDate = File.GetLastAccessTime(attachmentFilename);
                disposition.FileName = Path.GetFileName(attachmentFilename);
                disposition.Size = new FileInfo(attachmentFilename).Length;
                disposition.DispositionType = DispositionTypeNames.Attachment;
                message.Attachments.Add(attachment);
            }
            message.Attachments.Add(new Attachment(attachmentFilename));
            
            smtpClient.Send(message);
        }
    }

    
}
//stackoverflow.com/questions/2825950/sending-email-with-attachments-from-c-attachments-arrive-as-part-1-2-in-thunde