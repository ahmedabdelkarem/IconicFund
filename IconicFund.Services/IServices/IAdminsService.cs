using IconicFund.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IconicFund.Services.IServices
{
    public interface IAdminsService
    {
        Task<ICollection<Admin>> Search(string name = null, string mobileNumber = null, string email = null, string EmplyeeNo = null, bool? isActive = null, Guid? roleId = null, bool? IsManager = null, string DepartmentCode = null);

        Task Add(Admin admin, Guid _UserId);


        Task Update(Admin admin);

        Task Delete(Admin admin, Guid _UserId);

        Task<Admin> GetAdminById(Guid id);
        Task<Admin> GetById(Guid? id);

        Task<List<Role>> GetAllRoles();

        bool CheckLogin(string username, string password, out string errorMessage, out Admin account);

        Task<bool> IsNationalIdAlreadyExists(string nationalId);
        Task<bool> IsNationalIdAlreadyExists(string nationalId, Guid adminId);

        Task<List<Admin>> getAll();


        Task<List<SelectListItem>> GetDataForDropdown();


        Task<List<Admin>> GetAdminsByDepartments(List<string> DeptValues);

        Task<List<Guid>> GetUsersForDepartmentList(string DepartmentId);

        bool IsThisUserSystemAdmin(Guid UserId);
        bool IsSecretTransactionsAllowed(Guid UserId);

        string GetDefaultLetterStatementByAdminId(Guid AdminId);

        bool IsPhoneNumberExists(string PhoneNumber);
        bool IsEmailExists(string Email);

        Task<Admin> GetByUserName(string UserName);
        Task<string> GetUserDepartementCode(Guid id);
        Task<List<Guid>> GetSameDepartentUsers(Guid id);
        

    }
}