namespace IconicFund.Helpers
{
    public class Constants
    {
        public const int PageSize = 10;

        public const string SuccessMessage = "SuccessMessage";
        public const string WarningMessage = "WarningMessage";
        public const string ErrorMessage = "ErrorMessage";

        public const string ChangePasswordSuccessMessage = "ChangePasswordSuccessMessage";
        public const string ChangePasswordErrorMessage = "ChangePasswordErrorMessage";

        public const string MeccaLongitude = "39.8257";
        public const string MeccaLatitude = "21.4229";
        public const string FileLocation = "/Documents";


        #region Roles IDs

        public const string MainAdminRoleId = "FADD0B95-5356-4C8F-80D4-3C46E7B6BCEB";
        public const string SaftyRoleId = "BE891F47-4737-4E4B-9AE3-174987838292";
        public const string LiftsRoleId = "24BAF0C4-896E-4130-88C0-2F7ABF8BFA85";
        public const string EngineeringOfficeRoleId = "6CA056EA-D5F2-466A-917F-E59114DC6861";

        public const string MainAdminId = "AE868F87-59A1-42C5-978C-5207DB3D24F8";


        #endregion

        #region Folders
        
        public const string AdminsUploadDirectory = "/Uploads/Admins/";
        public const string AttachmentsUploadDirectory = "/Uploads/Attachments/";

        #endregion

        #region Claims

        public const string FullNameClaimType = "FullName";
        public const string PermissionsClaimType = "Permissions";

        #endregion

        #region localization
        public static string ARCultureCode => "ar";
        //public static string ARCultureCode => "ar-EG";
        public static string ENCultureCode => "en";
        #endregion
    }
}
