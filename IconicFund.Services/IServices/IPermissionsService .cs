using IconicFund.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IconicFund.Services.IServices
{
   public interface IPermissionsService
    {
        Task<List<Permissions>> getAll();

        Task<List<Permissions>> Search( string Name = null);

        Task<string> GetPermissionNameById(int ID);


        string GetPermissionsListNamesByIDS(List<int> IDsList);
    }
}
