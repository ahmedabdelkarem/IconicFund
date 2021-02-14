using IconicFund.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IconicFund.Services.IServices
{
    public interface IPermissionGroupAdminService
    {
        Task Add(PermissionGroupAdmin permissionGroupAdmins, Guid _UserId);

        Task Delete(PermissionGroupAdmin permissionGroupAdmins, Guid _UserId);

        Task<List<PermissionGroupAdmin>> GetAdminsByGroupId(string code);

        Task<string> GetAdminGroupByID(Guid AdminID);
        Task<List<string>> GetAdminGroupsByID(Guid AdminID);

        List<PermissionGroup> GetAdminGroupsByAdminID(Guid AdminID);


    }
}
