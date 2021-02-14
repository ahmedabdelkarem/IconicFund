using IconicFund.Helpers.Enums;
using IconicFund.Models.Entities;
using IconicFund.Repositories;
using IconicFund.Services.IServices;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconicFund.Services.Services
{
    public class PermissionsService : IPermissionsService
    {
        private readonly IBaseRepository repository;
        private readonly ILoggingService loggingService;

        public PermissionsService(IBaseRepository repository, IHasherService hasherService, ILoggingService loggingService)
        {
            this.repository = repository;
            this.loggingService = loggingService;
        }

        public async Task<List<Permissions>> getAll()
        {
            return await repository.GetAllAsync<Permissions>();
        }

        public async Task<List<Permissions>> Search( string Name = null)
        
        {
            var Permissions = await repository.
                        GetAllWhereAsync<Permissions>(i => string.IsNullOrEmpty(Name) || i.Name.ToLower().Contains(Name.ToLower()));


            return Permissions;
        }

        public async Task<string> GetPermissionNameById(int ID)
        {
            var Permission = await repository.FirstOrDefaultAsync<Permissions>(i => i.ID == ID);
            return Permission.Name;
        }

        public string GetPermissionsListNamesByIDS(List<int> IDsList)
        {
            var Permissions =  repository.GetAllWhere<Permissions>(i => IDsList.Contains(i.ID));
            var PermissionListName = Permissions.Select(i => i.Name).ToList();
            string permsNames = string.Join(',', PermissionListName);
            return permsNames;

        }

    }
}


