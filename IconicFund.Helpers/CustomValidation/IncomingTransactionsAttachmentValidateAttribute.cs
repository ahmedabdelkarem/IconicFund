using IconicFund.Resources;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace IconicFund.Helpers.CustomValidation
{
    public class IncomingTransactionsAttachmentValidateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var currentValue = (List<IFormFile>)value;

            if (currentValue != null && currentValue.Count > 0)
            {
                // check for required document formats "doc", "docx",
                var supportedTypes = new[] { "pdf", "png", "gif", "jpeg", "jpg" };

                 
                foreach (var file in currentValue)
                {
                    var FileExtension = Path.GetExtension(file.FileName).Substring(1).ToLower();
                    if (!supportedTypes.Contains(FileExtension))
                    {
                        return new ValidationResult(Messages.UnsupportedFileExtension);
                    }

                    // if we want to check for file size 
                    //if (file.Length > (5* 1024 * 1024))
                    //{

                    //}
                }
            
            }
             

            return ValidationResult.Success;
        }
    }
}
