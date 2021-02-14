using System;
using System.Collections.Generic;
using System.Text;

namespace IconicFund.Services.IServices
{
   
    public interface IMailService
    {
        void Send(string subject, string body, string recipient, string recipientCC = null, string attachmentFile = null);
    }
}
