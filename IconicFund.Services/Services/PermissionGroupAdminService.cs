using IconicFund.Models.Entities;
using IconicFund.Services.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IconicFund.Repositories;
using IconicFund.Helpers.Enums;
using System.Linq;

namespace IconicFund.Services.Services
{
    
    public class PermissionGroupAdminService : IPermissionGroupAdminService
    {
        private readonly IBaseRepository repository;
        private readonly ILoggingService loggingService;
        private readonly IPermissionsGroupService PermissionsGroupService;

        public PermissionGroupAdminService(IBaseRepository repository, IHasherService hasherService, ILoggingService loggingService, IPermissionsGroupService PermissionsGroupService)
        {
            this.repository = repository;
            this.loggingService = loggingService;
            this.PermissionsGroupService = PermissionsGroupService;
        }
        public async Task Add(PermissionGroupAdmin permissionGroupAdmins, Guid _UserId)
        {
            repository.AddAndReturnObject<PermissionGroupAdmin>(ref permissionGroupAdmins);
            await repository.SaveChangesAsync();

            await loggingService.LogActionData<PermissionGroupAdmin>(LoggingCategory.PermissionGroupAdmin, LoggingAction.Create,
             permissionGroupAdmins, null, _UserId, permissionGroupAdmins.AdminId.ToString());


        }



        public async Task Delete(PermissionGroupAdmin permissionGroupAdmins, Guid _UserId)
        {
            repository.Remove<PermissionGroupAdmin>(permissionGroupAdmins);

            await loggingService.LogActionData<PermissionGroupAdmin>(LoggingCategory.PermissionGroupAdmin, LoggingAction.Delete,
            permissionGroupAdmins, null, _UserId, permissionGroupAdmins.AdminId.ToString());


            await repository.SaveChangesAsync();
        }

        public async Task<List<PermissionGroupAdmin>> GetAdminsByGroupId(string code)
        {
            var admins = await repository.GetAllWhereAsync<PermissionGroupAdmin>(i => i.PermissionGroupCode == code);
            if (admins != null)
            {
                return admins;
            }
            return null;
        }
        public async Task<string> GetAdminGroupByID (Guid AdminID )
        {
            PermissionGroupAdmin GroupCode =await  repository.FirstOrDefaultAsync<PermissionGroupAdmin>(i => i.AdminId == AdminID);
            if(GroupCode != null)
            {
                return GroupCode.PermissionGroupCode;
            }
            return null;
        }
        public async Task<List<string>> GetAdminGroupsByID(Guid AdminID)
        {
            var GroupCodes = await repository.GetAllWhereAsync<PermissionGroupAdmin>(i => i.AdminId == AdminID);
            if (GroupCodes != null)
            {
                return GroupCodes.Select(i => i.PermissionGroupCode).ToList();
            }
            return null;


        }
        public List<PermissionGroup> GetAdminGroupsByAdminID(Guid AdminID)
        {
            List<PermissionGroup> PermissionGroups = new List<PermissionGroup>();
            var GroupCodes = repository.GetAllWhere<PermissionGroupAdmin>(i => i.AdminId == AdminID).Select(i => i.PermissionGroupCode).ToList();
            if (GroupCodes.Count != 0)
            {
                foreach (var GroupCode in GroupCodes)
                {
                    var PermissionGroup = PermissionsGroupService.GetPermissionGroupById(GroupCode);
                    PermissionGroups.Add(PermissionGroup.Result);
                }
                return PermissionGroups;
            }
            return null;

        }

    }
}
