using IconicFund.Resources;
using System.ComponentModel.DataAnnotations;

namespace IconicFund.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "Username", ResourceType = typeof(Labels))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Labels))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Labels))]
        public bool RememberMe { get; set; } = false;

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [StringLength(4)]
        public string CaptchaCode { get; set; }
    }


    public class changePasswordViewModel
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [DataType(DataType.Password)]
        [Display(Name = "CurrentPassword", ResourceType = typeof(Labels))]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(Labels))]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessageResourceName = "ConfirmPasswordNotMatched", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "ConfirmNewPassword", ResourceType = typeof(Labels))]
        public string ConfirmNewPassword { get; set; }

    }

    public class setPasswordViewModel
    {
        

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(Labels))]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessageResourceName = "ConfirmPasswordNotMatched", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "ConfirmNewPassword", ResourceType = typeof(Labels))]
        public string ConfirmNewPassword { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "Username", ResourceType = typeof(Labels))]
        public string UserName { get; set; }

    }

    public class ConfirmViewModel
    {

        public bool RememberMe { get; set; }


        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [StringLength(5)]
        [Display(Name = "ConfirmationCode", ResourceType = typeof(Labels))]

        public string Code { get; set; }
    }

}
