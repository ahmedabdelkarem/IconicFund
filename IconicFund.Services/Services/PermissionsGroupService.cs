using IconicFund.Helpers;
using IconicFund.Helpers.Enums;
using IconicFund.Models.Entities;
using IconicFund.Repositories;
using IconicFund.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconicFund.Services.Services
{
    public class PermissionsGroupService : IPermissionsGroupService
    {
        private readonly IBaseRepository repository;
        private readonly ILoggingService loggingService;

        public PermissionsGroupService(IBaseRepository repository, IHasherService hasherService, ILoggingService loggingService)
        {
            this.repository = repository;
            this.loggingService = loggingService;
        }

        public async Task<List<PermissionGroup>> Search( string Name = null , DateTime? StartDate = null, DateTime? EndDate = null)        
        {

            var PermissionGroup = await repository.
                        GetAllWhereAsync<PermissionGroup>(i => (i.isActive == true) 
                        && (string.IsNullOrEmpty(Name) || i.Name.ToLower().Contains(Name.ToLower()))
                        && (StartDate.HasValue == false || i.StartDate == ConvertDatetime.ConvertToGregorianDate(StartDate.Value))
                        && (EndDate.HasValue == false || i.EndDate == ConvertDatetime.ConvertToGregorianDate(EndDate.Value)));

            return PermissionGroup;
        }
        public async Task Add(PermissionGroup permissionGroup, Guid _UserId)
        {
            repository.AddAndReturnObject<PermissionGroup>(ref permissionGroup);
            await repository.SaveChangesAsync();

            await loggingService.LogActionData<PermissionGroup>(LoggingCategory.PermissionGroup, LoggingAction.Create,
            permissionGroup, null, _UserId, permissionGroup.Code.ToString());



        }

        public async Task Update(PermissionGroup permissionGroup)
        {
            repository.Update<PermissionGroup>(permissionGroup);
            await repository.SaveChangesAsync();
        }

        public async Task Delete(PermissionGroup permissionGroup, Guid _UserId)
        {
            //repository.Remove<PermissionGroup>(permissionGroup);
            permissionGroup.isActive = false;
            repository.Update<PermissionGroup>(permissionGroup);
            await repository.SaveChangesAsync();

            await loggingService.LogActionData<PermissionGroup>(LoggingCategory.PermissionGroup, LoggingAction.Delete,
           permissionGroup, null, _UserId, permissionGroup.Code.ToString());



        }

        public async Task<PermissionGroup> GetPermissionGroupById(string code)
        {
            return await repository.FirstOrDefaultAsync<PermissionGroup>(i => i.Code == code && i.isActive == true);
        }

        public async Task<List<PermissionGroup>> getAll()
        {
            return await repository.GetAllWhereAsync<PermissionGroup>(x => x.isActive == true);
        }

        public async Task<bool> IsCodeExist(string code)
        {
            return await repository.AnyAsync<PermissionGroup>(i => i.Code.ToLower() == code.ToLower());
        }

        public async Task<string> GetPermissionListByGroupID(string code)
        {
            return repository.FirstOrDefaultAsync<PermissionGroup>(i => i.Code == code).Result.PermissionsList;

        }

    }
}


