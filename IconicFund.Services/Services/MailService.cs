using IconicFund.Models;
using IconicFund.Services.IServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace IconicFund.Services.Services
{
   
    public class MailService : IMailService
    {
        private const int Timeout = 5000;

        private readonly ILogger _logger;
        private readonly IOptions<MailServerConfigurations> _mailconfig;

        public MailService(IOptions<MailServerConfigurations> mailconfig, ILoggerFactory loggerFactory)
        {
            _mailconfig = mailconfig;
            _logger = loggerFactory.CreateLogger<MailService>();
        }

        public void Send(string subject, string body, string recipient, string recipientCC = null, string attachmentFile = null)
        {
            MailMessage message = null;

            try
            {
                //Read mail configurations
                var host = _mailconfig.Value.MailServer;
                var port = Convert.ToInt32(_mailconfig.Value.Port);
                var user = _mailconfig.Value.MailAuthUser;
                var pass = _mailconfig.Value.MailAuthPass;
                var ssl = Convert.ToBoolean(_mailconfig.Value.EnableSSL);
                var sender = _mailconfig.Value.EmailFromAddress;

                // We do not catch the error here... let it pass direct to the caller
                Attachment att = null;
                message = new MailMessage(sender, recipient, subject, body) { };
                message.IsBodyHtml = true;
                if (recipientCC != null)
                {
                    message.Bcc.Add(recipientCC);
                }
                //var smtp = new SmtpClient(_host, _port);
                //var smtpClient smtp = new SmtpClient();

                if (!String.IsNullOrEmpty(attachmentFile))
                {
                    if (File.Exists(attachmentFile))
                    {
                        att = new Attachment(attachmentFile);
                        message.Attachments.Add(att);
                    }
                }

                SmtpClient smtp = new SmtpClient();
                if (user.Length > 0 && pass.Length > 0)
                {
                    smtp.Port = port;
                    smtp.EnableSsl = ssl;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(user, pass);
                    smtp.Host = host;
                    smtp.Timeout = Timeout;
                }

                smtp.Send(message);

                if (att != null)
                    att.Dispose();
                message.Dispose();
                smtp.Dispose();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format("{0} \n\rEmail Recipient: {1}", ex.ToString(), recipient));
            }
        }
    }

}
