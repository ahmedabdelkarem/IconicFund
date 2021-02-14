using System;
using System.Collections.Generic;
using System.Text;

namespace IconicFund.Models
{
    public class MailServerConfigurations
    {
        public MailServerConfigurations()
        {
            Port = "587";
            EnableSSL = "true";
            MailServer = "smtp.office365.com";
            EmailFromAddress = "noreply@apit.net.sa";
            MailAuthUser = "noreply@apit.net.sa";
            MailAuthPass = "Tip@-2020";
        }
        public string MailServer { get; set; }
        public string Port { get; set; }
        public string EnableSSL { get; set; }
        public string EmailFromAddress { get; set; }
        public string MailAuthUser { get; set; }
        public string MailAuthPass { get; set; }
    }
}
