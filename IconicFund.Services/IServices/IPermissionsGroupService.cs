using IconicFund.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IconicFund.Services.IServices
{
   public interface IPermissionsGroupService
    {
        Task<List<PermissionGroup>> Search( string Name = null, DateTime? StartDate = null, DateTime? EndDate = null);

        Task Add(PermissionGroup permissionGroup, Guid _UserId);

        Task Update(PermissionGroup permissionGroup);

        Task Delete(PermissionGroup permissionGroup, Guid _UserId);

        Task<PermissionGroup> GetPermissionGroupById(string code);

        Task<List<PermissionGroup>> getAll();

        Task<bool> IsCodeExist(string Id);

        Task<string> GetPermissionListByGroupID(string code);
    }
}
