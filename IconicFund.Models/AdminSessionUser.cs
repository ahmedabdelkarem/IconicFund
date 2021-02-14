using System;

namespace IconicFund.Models
{
    public class AdminSessionUser
    {
        public Guid Id { get; set; }

        public string NationalId { get; set; }

        public string FullName { get; set; }

        public string PermissionsList { get; set; }
        public string GroupPermissionCode { get; set; }

        public string Code { get; set; }
        public string Action { get; set; } = "Index";
        public string Controller { get; set; } = "Home";
    }
}
