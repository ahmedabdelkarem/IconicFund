using IconicFund.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace IconicFund.Models.Entities
{
    public class SystemSettings
    {
        [Key]
        public int Id { get; set; }


        #region Incoming Serial Number Settings 

        public string IncomingSerialNumberPrefix { get; set; }              //بادئة الوارد

        [Required]
        public int IncomingSerialNumberStartValue { get; set; } = 1;        //بداية تسلسل الوارد

        public int? IncomingSerialNumberDigitsCount { get; set; }           //عدد الخانات لتسلسل الوارد

        public string IncomingSerialNumberPostfix { get; set; }             //خاتمة الوارد

        #endregion

        #region Export Serial Number Settings 

        public string ExportSerialNumberPrefix { get; set; }              //بادئة الصادر

        [Required]
        public int ExportSerialNumberStartValue { get; set; } = 1;        //بداية تسلسل الصادر

        public int? ExportSerialNumberDigitsCount { get; set; }           //عدد الخانات لتسلسل الصادر

        public string ExportSerialNumberPostfix { get; set; }             //خاتمة الصادر

        #endregion

        [Required]
        public SystemDateTypes SystemDateType { get; set; }

        public bool? UserDatePrefix { get; set; }

        [Required]
        public string DisplayedApplicationName { get; set; }



    }
}
