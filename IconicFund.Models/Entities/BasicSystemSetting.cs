using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IconicFund.Models.Entities
{
    public class BasicSystemSetting
    {
        
        public int Id { get; set; }
  
        #region RequestDepartment

        [Required]
        [ForeignKey("Lkp_DateType")]
        public int DateTypeId_Fk { get; set; }
        public Lkp_DateType Lkp_DateType { get; set; }

        #endregion

        [Required]
        public string SystemTitle_En { get; set; }
        [Required]
        public string SystemTitle_Ar { get; set; }
      
        public string SystemLogo { get; set; }

        [Required]
        public double SessionTime { get; set; }
        public int? MaxFileSize { get; set; }
        [Required]
        public int ManyWrongLoginAvailability { get; set; }
        [Required]
        public int PasswordExpiredAfter { get; set; }
        [Required]
        public int MinPassword { get; set; }
       
        #region RequestDepartment

        [Required]
        [ForeignKey("Lkp_PasswordComplexity")]
        public int PasswordComplexityId_Fk { get; set; }
        public Lkp_PasswordComplexity Lkp_PasswordComplexity { get; set; }

        #endregion

        public string Header { get; set; }
        public string Footer { get; set; }
        public bool IsAllowToUserToLoginManyTime { get; set; }
       
        #region PermissionGroup
        [ForeignKey("PermissionGroup")]
        public string GroupPermissionCode { get; set; }
        public PermissionGroup PermissionGroup { get; set; }
        #endregion


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

        public int IncomingPrefixType { get; set; }
        public int ExportPrefixType { get; set; }

    }
}
