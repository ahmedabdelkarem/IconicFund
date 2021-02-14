using IconicFund.Resources;
using System.ComponentModel.DataAnnotations;

namespace IconicFund.Helpers.Enums
{

    public enum IdTypes
    {
        [LocalizedDescription("IdTypePersonal", typeof(Labels))]
        Personal = 1,

        [LocalizedDescription("IdTypePassport", typeof(Labels))]
        Passport = 2,
        [LocalizedDescription("IdTypeResidency", typeof(Labels))]

        Residency = 3
    }

    public enum DepartmentTypes
    {
        [LocalizedDescription("FieldCenter", typeof(Labels))]
        FieldCenter = 1,                //مركز ميداني
        
        [LocalizedDescription("OrganizationalDepartment", typeof(Labels))]
        OrganizationalDepartment = 2    //إدارة تنظيمية
    }

    public enum AttachmentsTypes
    {
        Papers = 1,

        Envelope = 2
    }

    public enum IncomingTypes
    {
        [LocalizedDescription("InternalIncoming", typeof(Labels))]
        Internal = 1,       //وارد داخلي
        [LocalizedDescription("ExternalIncoming", typeof(Labels))]
        External = 2        //وارد خارجي
    }

    public enum ExportingTypes
    {
        Internal = 1,       //صادر داخلي

        External = 2        //صادر خارجي

    }

    public enum TransactionStatus
    {
        //وارد جديد - محال- محجوزة - محفوظة - معادة لتعديل الخطاب - مغلقة - مصدرة
        [Display(Name = "NewIncoming", ResourceType = typeof(Labels))]
        [LocalizedDescription("NewIncoming", typeof(Labels))]
        NewIncoming = 1,

        [Display(Name = "Transferred", ResourceType = typeof(Labels))]
        [LocalizedDescription("Transferred", typeof(Labels))]

        Transferred = 2,

        [Display(Name = "Reserved", ResourceType = typeof(Labels))]
        [LocalizedDescription("Reserved", typeof(Labels))]

        Reserved = 3,

        [Display(Name = "Saved", ResourceType = typeof(Labels))]
        [LocalizedDescription("Saved", typeof(Labels))]

        Saved = 4,

        [Display(Name = "ReturenedToEditLetter", ResourceType = typeof(Labels))]
        [LocalizedDescription("ReturenedToEditLetter", typeof(Labels))]

        ReturenedToEditLetter = 5,

        [Display(Name = "ClosedStatus", ResourceType = typeof(Labels))]
        [LocalizedDescription("ClosedStatus", typeof(Labels))]

        Closed = 6,

        [Display(Name = "Exported", ResourceType = typeof(Labels))]
        [LocalizedDescription("Exported", typeof(Labels))]
        Exported = 7
    }

    public enum ReferredLetterTypes
    {
        [Display(Name = "ReferredExport", ResourceType = typeof(Labels))]
        Export = 1, //صادر

         [Display(Name = "ReferredIncoming", ResourceType = typeof(Labels))]

        Incoming = 2, //وارد

         [Display(Name = "ReferredSafetyExport", ResourceType = typeof(Labels))]

        SafetyExport = 3        //صادر إدارة الدفاع المدني
    }

    public enum Priorities
    {
        [Display(Name = "Important", ResourceType = typeof(Labels))]
        Important = 1,          //مهم
        [Display(Name = "Urgent", ResourceType = typeof(Labels))]
        Urgent = 2,             //عاجل
        [Display(Name = "Normal", ResourceType = typeof(Labels))]
        Normal = 3,             //عادي
    }

    public enum DurationTypes
    {
        [LocalizedDescription("Hours", typeof(Labels))]
        Hours = 1,
        [LocalizedDescription("Days", typeof(Labels))]
        Days = 2
    }

    public enum PermissionTypes
    {
        ReadOnly = 1,
        Edit = 2
    }

    public enum RequestStatus
    {
        [Display(Name = "New", ResourceType = typeof(Labels))]
        New = 1,        //جديد
        [Display(Name = "Read", ResourceType = typeof(Labels))]
        Read = 2        //تم الاطلاع
    }

    public enum Requirements
    {
        [Display(Name = "View", ResourceType = typeof(Labels))] // for test
        test = 1,
        Requirement = 2
    }

    public enum ArchiveTransactionSide
    {
        [LocalizedDescription("Export", typeof(Labels))]
        Export = 1,             //صادر
        [LocalizedDescription("Incoming", typeof(Labels))]
        Incoming = 2,           //وارد
    }
    public enum ArchiveTransactionType
    {
        [LocalizedDescription("Internal", typeof(Labels))]
        Internal = 1,       //داخلي
        [LocalizedDescription("External", typeof(Labels))]
        External = 2        // خارجي
    }




    public enum SystemDateTypes
    {
        Hijri = 1,
        Gregorian = 2
    }

    public enum UserTransactions
    {
        Owner = 1,
        Copy = 2,
        Referral = 3,
        Announcement = 4
    }

    public enum Lookups
    {
        External = 1,
        TransactionType = 2,
        Folders = 3,
        HardCopyAttachmentsTypes = 4,
    }

    public enum LoggingCategory
    {
        Transactions = 1,
        Administrator = 2,
        Actions = 3,
        ArchiveAttachment = 4,
        Archive = 5,
        Department  = 6,
        ExportingAttachments = 7,
        ExportedTransaction = 8,
        ExternalEntity = 9,
        Folder = 10,
        TransactionType = 11,
        HardCopyAttachmentsType = 12,
        PermissionGroupAdmin = 13,
        PermissionGroup = 14,
        Referral = 15,
        Request = 16,
        TransactionAction = 17,
        TransactionLetter = 18,
        BasicSystemSetting = 19


    }

    public enum LoggingAction
    {
        Create = 1,
        Edit = 2,
        Delete = 3,
    }
    
    public enum DelegationStatus
    {
        [Display(Name = "Pending", ResourceType = typeof(Labels))]
        [LocalizedDescription("Pending", typeof(Labels))]
        Pending = 1,
        [Display(Name = "Accepted", ResourceType = typeof(Labels))]
        [LocalizedDescription("Accepted", typeof(Labels))]
        Accept = 2,
        [Display(Name = "Rejected", ResourceType = typeof(Labels))]
        [LocalizedDescription("Rejected", typeof(Labels))]
        Reject = 3,
    }

    public enum SerialNumberType
    {
        [Display(Name = "StringAndNumber", ResourceType = typeof(Labels))]
        [LocalizedDescription("StringAndNumber", typeof(Labels))]
        StringAndNumber = 1,

        [Display(Name = "GerogianDate", ResourceType = typeof(Labels))]
        [LocalizedDescription("GerogianDate", typeof(Labels))]
        GerogianDate = 2,

        [Display(Name = "HijriDate", ResourceType = typeof(Labels))]
        [LocalizedDescription("Rejected", typeof(Labels))]
        HijriDate = 3
    }
}
