using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IconicFund.Context.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NationalId = table.Column<string>(nullable: false),
                    EmplyeeNo = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    SecondName = table.Column<string>(nullable: true),
                    ThirdName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    ProfileImage = table.Column<string>(nullable: true),
                    SignatureImage = table.Column<string>(nullable: true),
                    Fingerprint = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ActivationStartDate = table.Column<DateTime>(nullable: false),
                    ActivationEndDate = table.Column<DateTime>(nullable: true),
                    CanApprove = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    IsManager = table.Column<bool>(nullable: false),
                    DefaultLetterStatement = table.Column<string>(nullable: true),
                    DepartmentCode = table.Column<string>(nullable: true),
                    SignatureImageData = table.Column<byte[]>(nullable: true),
                    ProfileImageData = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lkp_PasswordComplexity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplexityName_Ar = table.Column<string>(nullable: false),
                    ComplexityName_En = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lkp_PasswordComplexity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionGroups",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ViewSecretTransactions = table.Column<bool>(nullable: false),
                    PermissionsList = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionGroups", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ApplicationID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemLogging",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoggingCategory = table.Column<int>(nullable: false),
                    LoggingAction = table.Column<int>(nullable: false),
                    RowID = table.Column<string>(maxLength: 100, nullable: true),
                    ActionDate = table.Column<DateTime>(nullable: false),
                    OldData = table.Column<string>(nullable: true),
                    NewData = table.Column<string>(nullable: true),
                    UserID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemLogging", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SystemLogging_Admins_UserID",
                        column: x => x.UserID,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BasicSystemSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemTitle_En = table.Column<string>(nullable: false),
                    SystemTitle_Ar = table.Column<string>(nullable: false),
                    SystemLogo = table.Column<string>(nullable: true),
                    SessionTime = table.Column<double>(nullable: false),
                    MaxFileSize = table.Column<int>(nullable: true),
                    ManyWrongLoginAvailability = table.Column<int>(nullable: false),
                    PasswordExpiredAfter = table.Column<int>(nullable: false),
                    MinPassword = table.Column<int>(nullable: false),
                    PasswordComplexityId_Fk = table.Column<int>(nullable: false),
                    Header = table.Column<string>(nullable: true),
                    Footer = table.Column<string>(nullable: true),
                    IsAllowToUserToLoginManyTime = table.Column<bool>(nullable: false),
                    GroupPermissionCode = table.Column<string>(nullable: true),
                    IncomingSerialNumberPrefix = table.Column<string>(nullable: true),
                    IncomingSerialNumberStartValue = table.Column<int>(nullable: false),
                    IncomingSerialNumberDigitsCount = table.Column<int>(nullable: true),
                    IncomingSerialNumberPostfix = table.Column<string>(nullable: true),
                    ExportSerialNumberPrefix = table.Column<string>(nullable: true),
                    ExportSerialNumberStartValue = table.Column<int>(nullable: false),
                    ExportSerialNumberDigitsCount = table.Column<int>(nullable: true),
                    ExportSerialNumberPostfix = table.Column<string>(nullable: true),
                    IncomingPrefixType = table.Column<int>(nullable: false),
                    ExportPrefixType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicSystemSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasicSystemSetting_PermissionGroups_GroupPermissionCode",
                        column: x => x.GroupPermissionCode,
                        principalTable: "PermissionGroups",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BasicSystemSetting_Lkp_PasswordComplexity_PasswordComplexityId_Fk",
                        column: x => x.PasswordComplexityId_Fk,
                        principalTable: "Lkp_PasswordComplexity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionGroupAdmins",
                columns: table => new
                {
                    PermissionGroupCode = table.Column<string>(nullable: false),
                    AdminId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionGroupAdmins", x => new { x.PermissionGroupCode, x.AdminId });
                    table.ForeignKey(
                        name: "FK_PermissionGroupAdmins_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionGroupAdmins_PermissionGroups_PermissionGroupCode",
                        column: x => x.PermissionGroupCode,
                        principalTable: "PermissionGroups",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdminRoles",
                columns: table => new
                {
                    AdminId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminRoles", x => new { x.AdminId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AdminRoles_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdminRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminRoles_RoleId",
                table: "AdminRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicSystemSetting_GroupPermissionCode",
                table: "BasicSystemSetting",
                column: "GroupPermissionCode");

            migrationBuilder.CreateIndex(
                name: "IX_BasicSystemSetting_PasswordComplexityId_Fk",
                table: "BasicSystemSetting",
                column: "PasswordComplexityId_Fk");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroupAdmins_AdminId",
                table: "PermissionGroupAdmins",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CityId",
                table: "Regions",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemLogging_UserID",
                table: "SystemLogging",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminRoles");

            migrationBuilder.DropTable(
                name: "BasicSystemSetting");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "PermissionGroupAdmins");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "SystemLogging");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Lkp_PasswordComplexity");

            migrationBuilder.DropTable(
                name: "PermissionGroups");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Admins");
        }
    }
}
