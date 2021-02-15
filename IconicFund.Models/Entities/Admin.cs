using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IconicFund.Models.Entities
{
    public class Admin
    {
        //public override string Id { get => base.Id; set => base.Id = value; }
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string NationalId { get; set; }

        [Required]
        public string EmplyeeNo { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string ThirdName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Title { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public string ProfileImage { get; set; }

        public string SignatureImage { get; set; }

        public string Fingerprint { get; set; }             // --> new


        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime ActivationStartDate { get; set; }

        public DateTime? ActivationEndDate { get; set; }

        public bool CanApprove { get; set; } = false;


        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsManager { get; set; } = false;            // --> new

        public ICollection<AdminRole> Roles { get; set; }


        public string DefaultLetterStatement { get; set; }

        #region Department                                      // --> new

        [ForeignKey("Department")]
        public string DepartmentCode { get; set; }

        #endregion

        public byte[] SignatureImageData { get; set; }

        public byte[] ProfileImageData { get; set; }


        public virtual ICollection<PermissionGroupAdmin> PermissionGroups { get; set; }


        public virtual ICollection<SystemLogging> LoggingData { get; set; }


       

    }
}
