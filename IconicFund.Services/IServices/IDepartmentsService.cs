using IconicFund.Helpers.Enums;
using IconicFund.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IconicFund.Services.IServices
{
    public interface IDepartmentsService
    {
        Task<List<Department>>  Search(string Code = null, string NameAr = null, string NameEn = null, string Email = null, DepartmentTypes? Type = null, string parentDepartment = null);

        Task Add(Department Department, Guid _UserId);

        Task Update(Department Department);

        Task Delete(Department Department, Guid _UserId);

        Task<Department> GetDepartmentById(string Code);
        Task<bool> HasChild(string Code);

        Task<List<Department>> getAll();

        Task<bool> IsCodeExist(string code);

        Task<List<SelectListItem>> GetListForDropdown(string Lang);

        Task<string> IsThisUserIsDepartmentManager(Guid UserId);
        

        Task<bool> IsEmailExist(string Email);


    }
}