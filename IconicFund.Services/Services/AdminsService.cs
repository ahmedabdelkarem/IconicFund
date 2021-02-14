using IconicFund.Helpers;
using IconicFund.Helpers.Enums;
using IconicFund.Models;
using IconicFund.Models.Entities;
using IconicFund.Repositories;
using IconicFund.Resources;
using IconicFund.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace IconicFund.Services.Services
{
    public class AdminsService : IAdminsService
    {
        private readonly IBaseRepository repository;
        private readonly IHasherService hasherService;
        private readonly ILoggingService loggingService;
        private readonly IPermissionGroupAdminService PermissionGroupAdminService;

        public AdminsService(IBaseRepository repository, IHasherService hasherService, ILoggingService loggingService, IPermissionGroupAdminService PermissionGroupAdminService)
        {
            this.repository = repository;
            this.hasherService = hasherService;
            this.loggingService = loggingService;
            this.PermissionGroupAdminService = PermissionGroupAdminService;
        }

        /*
        public async Task<ICollection<Admin>> Search(string name = null, string email = null, bool? isActive = null, string roleId = null) //bool includeMainAdmin,
        {
            var loggedInAdminId = _sessionService.Admin?.Id;

            //Get list of all admins in any Admin role (filtered by the specified role if any)
            var adminsIDs = await _dbContext.Roles.Include(i => i.Admins)
                                                .Where(i => string.IsNullOrEmpty(roleId) || i.Id == roleId)
                                                .Where(i => i.Type == Helpers.Enums.RoleTypes.Admin)        // || (includeMainAdmin && i.Type == Helpers.Enums.RoleTypes.MainAdmin))
                                                .SelectMany(i => i.Admins)
                                                .Select(i => i.AdminId).Distinct()
                                                .ToListAsync();

            //Get Admins details with filteration
            return await _dbContext.Admins.Include(i => i.Roles)
                                        .ThenInclude(i => i.Role)

                                        .Where(i => adminsIDs.Contains(i.Id) && i.Id != loggedInAdminId)
                                        .Where(i => string.IsNullOrEmpty(name) || i.FullName.ToLower().Contains(name.ToLower()))
                                        .Where(i => string.IsNullOrEmpty(email) || i.Email.ToLower().Contains(email.ToLower()))
                                        .Where(i => !isActive.HasValue || i.IsActive == isActive)
                                        .Where(i => i.MarkedAsDeleted == false)
                                        .OrderByDescending(i => i.CreationDate)
                                        .ToListAsync();
        }
        */

        public async Task<ICollection<Admin>> Search(string name = null, string mobileNumber = null, string email = null, string EmplyeeNo = null, bool? isActive = null, Guid? roleId = null, bool? IsManager = null, string DepartmentCode = null)
        {
            var nameToLower = name?.ToLower();

            var admins = await repository.GetAllWhereAsync<Admin>(i => (i.Id != Guid.Parse(Constants.MainAdminId))
                                                                    && (string.IsNullOrEmpty(mobileNumber) || i.MobileNumber.Contains(mobileNumber))
                                                                    && (string.IsNullOrEmpty(EmplyeeNo) || i.EmplyeeNo == EmplyeeNo)
                                                                   // && (string.IsNullOrEmpty(name) || string.Concat(i.FirstName,i.SecondName,i.ThirdName,i.LastName).Contains(name))
                                                                    && (string.IsNullOrEmpty(email) || (i.Email != null && i.Email.ToLower().Contains(email.ToLower())))
                                                                    && (string.IsNullOrEmpty(DepartmentCode) || (i.Department.NameAr != null && i.Department.Code.ToLower().Contains(DepartmentCode.ToLower())))
                                                                    && (isActive.HasValue == false || i.IsActive == isActive)
                                                                    && (IsManager.HasValue == false || i.IsManager == IsManager)
                                                                    && (roleId.HasValue == false || i.Roles.Select(r => r.RoleId).Contains(roleId.Value)),

                                                                    "Roles", "Roles.Role");

            if (!string.IsNullOrEmpty(name))
            {
                nameToLower = nameToLower.Replace(" ", string.Empty);
                admins = admins.Where(i => string.Concat(i.FirstName ,i.SecondName ,i.ThirdName , i.LastName).ToString().ToLower().Contains(nameToLower)).ToList();
            }

            return admins;
        }

        public async Task Add(Admin admin, Guid _UserId)
        {
            //if (IsEmailUsedForAnotherAccount(admin.Email))
            //{
            //    throw new Exception(ExceptionsTypes.EmailExists.ToString());
            //}

            repository.AddAndReturnObject<Admin>(ref admin);
            await repository.SaveChangesAsync();

            await loggingService.LogActionData<Admin>(LoggingCategory.Administrator, LoggingAction.Create,
                       admin, null, _UserId, admin.Id.ToString());

        }

        public async Task Update(Admin admin)
        {
            //if (IsEmailUsedForAnotherAccount(admin.Email, admin.Id))
            //{
            //    throw new Exception(ExceptionsTypes.EmailExists.ToString());
            //}

            repository.Update<Admin>(admin);
            await repository.SaveChangesAsync();
        }

        //public async Task Update(Admin admin)
        //{
        //    if (IsEmailUsedForAnotherAccount(admin.Email, admin.Id))
        //    {
        //        throw new Exception(ExceptionsTypes.EmailExists.ToString());
        //    }

        //    _dbContext.Admins.Attach(admin);

        //    await _dbContext.SaveChangesAsync();
        //}

        public async Task Delete(Admin admin, Guid _UserId)
        {
            //repository.Remove<Admin>(admin);

            admin.IsActive = false;
            repository.Update<Admin>(admin);
            await repository.SaveChangesAsync();
            await loggingService.LogActionData<Admin>(LoggingCategory.Administrator, LoggingAction.Delete,
                       admin, null, _UserId, admin.Id.ToString());
        }

        public async Task<Admin> GetAdminById(Guid id)
        {
            return (await repository.GetAllWhereAsync<Admin>(i => i.Id == id, "Roles", "Roles.Role")).FirstOrDefault();
        }
        public async Task<Admin> GetById(Guid? id)
        {
            return (await repository.GetAllWhereAsync<Admin>(i => i.Id == id, new string[] { "PermissionGroups", "PermissionGroups.PermissionGroup" })).FirstOrDefault();
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await repository.GetAllAsync<Role>();
        }

        public bool CheckLogin(string username, string password, out string errorMessage, out Admin account)
        {
            errorMessage = null;

            //Check whether this account is exist or not
            var hashedPassword = hasherService.ComputeSha256Hash(password);

            account = repository.FirstOrDefault<Admin>(i => i.NationalId.Trim() == username.Trim() && i.Password == hashedPassword, new string[] { "PermissionGroups", "PermissionGroups.PermissionGroup" });

            if (account == null)
            {
                errorMessage = Messages.LoginFailed;
                return false;
            }


            if (account.IsActive == false || account.ActivationStartDate.Date > DateTime.Now.Date || (account.ActivationEndDate.HasValue && account.ActivationEndDate.Value.Date <= DateTime.Now.Date))
            {
                errorMessage = Messages.InactiveAccount;
                return false;
            }

            return true;
        }

        public async Task<bool> IsNationalIdAlreadyExists(string nationalId)
        {
            return await repository.AnyAsync<Admin>(i => i.NationalId.Trim() == nationalId.Trim());
        }

        public async Task<bool> IsNationalIdAlreadyExists(string nationalId, Guid adminId)
        {
            return await repository.AnyAsync<Admin>(i => i.NationalId.Trim() == nationalId.Trim() && i.Id != adminId);
        }

        public async Task<List<Admin>> getAll()
        {
            return await repository.GetAllAsync<Admin>();
        }

        public async Task<List<SelectListItem>> GetDataForDropdown()
        {


            var admins = repository.GetAllWhereAsync<Admin>(i => i.IsActive == true).Result.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.FirstName + " " + a.SecondName + " " + a.ThirdName + " " + a.LastName

            }).ToList();



            return admins;
        }


        public async Task<List<Admin>> GetAdminsByDepartments(List<string> DeptValues)
        {
            var admins = new List<Admin>();
            foreach (var item in DeptValues)
            {
                admins.AddRange(await repository.GetAllWhereAsync<Admin>(a => a.DepartmentCode.Contains(item)));
            }
            return admins;
        }

        public async Task<string> GetUserDepartementCode(Guid id)
        {
            return (await repository.GetAllWhereAsync<Admin>(i => i.Id == id)).FirstOrDefault().DepartmentCode;
        }
        public async Task<List<Guid>> GetSameDepartentUsers(Guid id)
        {
            var DepartementCode =await GetUserDepartementCode(id);
            return (await repository.GetAllWhereAsync<Admin>(i => i.DepartmentCode == DepartementCode)).Select(i => i.Id).ToList();
        }
        
        //public Admin GetAdminByName(string adminName)
        //{
        //    return _dbContext.Admins.Include(i => i.Roles).SingleOrDefault(i => i.AdminName == adminName);
        //}


        //private bool IsEmailUsedForAnotherAccount(string email)
        //{
        //    return _dbContext.Admins.Any(u => u.Email.Trim().ToLower() == email.Trim().ToLower());
        //}

        //private bool IsEmailUsedForAnotherAccount(string email, string adminId)
        //{
        //    return _dbContext.Admins.Any(u => u.Id != adminId && (u.Email.Trim().ToLower() == email.Trim().ToLower()));
        //}

        //private void AddToRoles(Admin admin, List<string> rolesIds)
        //{
        //    //add to roles
        //    admin.Roles = new List<AdminRole>();

        //    //Add the roles to the admin
        //    AdminRole adminRole;
        //    foreach (var roleId in rolesIds)
        //    {
        //        adminRole = new AdminRole() { RoleId = roleId, AdminId = admin.Id };
        //        admin.Roles.Add(adminRole);
        //    }
        //}

        public async Task<List<Guid>> GetUsersForDepartmentList(string DepartmentId)
        {
            var _users = new List<Guid>();

            if (!string.IsNullOrEmpty(DepartmentId))
            {

                // get all users id in depatment 
                var _getUsera = (await repository.GetAllWhereAsync<Admin>(a => a.DepartmentCode == DepartmentId)).Select(a => a.Id).ToList();

                // add users id in list 
                _users.AddRange(_getUsera);

            }


            return _users;
        }

        public bool IsThisUserSystemAdmin(Guid UserId)
        {

            var _checkAdmin = repository.GetAllWhere<AdminRole>(a => (a.Role.Name.Contains("مدير النظام") || a.RoleId.Equals("fadd0b95-5356-4c8f-80d4-3c46e7b6bceb")) && a.AdminId == UserId).Any();

            return _checkAdmin;
        }

        public bool IsSecretTransactionsAllowed(Guid UserId)
        {
            var UserGroups = PermissionGroupAdminService.GetAdminGroupsByAdminID(UserId);
            return UserGroups.Any(a => a.ViewSecretTransactions == true);
        }

        public string GetDefaultLetterStatementByAdminId(Guid AdminId)
        {
            return repository.GetAllWhere<Admin>(a => a.Id == AdminId).Select(a => a.DefaultLetterStatement).FirstOrDefault();
        }

        public bool IsPhoneNumberExists(string PhoneNumber)
        {
            var MobileExistCount = repository.GetAllWhere<Admin>(a => a.IsActive && a.MobileNumber == PhoneNumber).Count();
            if (MobileExistCount > 0)
                return true;

            return false;

        }
        public bool IsEmailExists(string Email)
        {
            var EmailExistCount = repository.GetAllWhere<Admin>(a => a.IsActive && a.Email == Email).Count();
            if (EmailExistCount > 0)
                return true;

            return false;
        }

        public async Task<Admin> GetByUserName(string UserName)
        {
            return await repository.FirstOrDefaultAsync<Admin>(i => i.Email == UserName && i.IsActive);

        }


    }
}