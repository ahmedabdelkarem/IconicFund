using IconicFund.Helpers.CustomValidation;
using IconicFund.Models.Entities;
using IconicFund.Resources;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace IconicFund.Web.Models
{
    public class AttachmentsViewModels
    {
        public string [] SavesAttachments { get; set; }

        [IncomingTransactionsAttachmentValidate]
        [Display(Name = "Attachments", ResourceType = typeof(Labels))]
        public List<IFormFile> Attachments { get; set; }

        public string IncomingNumber { get; set; }

        public string TransId { get; set; }


        public string DivName { get; set; }


    }
}
