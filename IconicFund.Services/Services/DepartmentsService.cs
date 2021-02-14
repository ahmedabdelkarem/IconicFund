using IconicFund.Helpers;
using IconicFund.Helpers.Enums;
using IconicFund.Models.Entities;
using IconicFund.Repositories;
using IconicFund.Resources;
using IconicFund.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IconicFund.Services.Services
{
    public class DepartmentsService : IDepartmentsService
    {
        private readonly IBaseRepository repository;
        private readonly IHasherService hasherService;
        private readonly ILoggingService loggingService;

        public DepartmentsService(IBaseRepository repository, IHasherService hasherService ,ILoggingService loggingService)
        {
            this.repository = repository;
            this.hasherService = hasherService;
            this.loggingService = loggingService;
        }

        public async Task<List<Department>> Search(string Code = null, string NameAr = null, string NameEn = null, string Email = null, DepartmentTypes? Type = null, string ParentDepartmentCode = null)
        {
            var Departments = await repository.GetAllWhereAsync<Department>(i =>  (string.IsNullOrEmpty(Code) || i.Code.ToLower().Contains(Code.ToLower()))
                                                                   && (string.IsNullOrEmpty(NameAr) || (i.NameAr != null && i.NameAr.ToLower().Contains(NameAr.ToLower())))
                                                                   && (string.IsNullOrEmpty(NameEn) || (i.NameEn != null && i.NameEn.ToLower().Contains(NameEn.ToLower())))
                                                                   && (string.IsNullOrEmpty(Email) || (i.Email != null && i.Email.ToLower().Contains(Email.ToLower())))
                                                                   && (string.IsNullOrEmpty(ParentDepartmentCode) || (i.ParentDepartmentCode != null && i.ParentDepartmentCode.ToLower().Contains(ParentDepartmentCode.ToLower())))
                                                                   && (!Type.HasValue || i.Type == Type));

            return Departments;
        }

     

        public async Task Add(Department Department, Guid _UserId)
        {
          
            repository.Add<Department>(Department);
            await repository.SaveChangesAsync();

            await loggingService.LogActionData<Department>(LoggingCategory.Department, LoggingAction.Create,
                     Department, null, _UserId, Department.Code);

        }

        public async Task Update(Department Department)
        {
          
            repository.Update<Department>(Department);
            await repository.SaveChangesAsync();
        }

       

        public async Task Delete(Department Department, Guid _UserId)
        {
            repository.Remove<Department>(Department);
            //Department.IsActive = false;
            //repository.Update<Department>(Department);
            await loggingService.LogActionData<Department>(LoggingCategory.Department, LoggingAction.Delete,
                     Department, null, _UserId, Department.Code);

            await repository.SaveChangesAsync();

            
        }

        public async Task<Department> GetDepartmentById(string Code)
        {
            return (await repository.FirstOrDefaultAsync<Department>(i => i.Code == Code));
        }
        public async Task<bool> HasChild(string Code)
        {
            var HasChild = (await repository.FirstOrDefaultAsync<Department>(i => i.ParentDepartmentCode == Code));
            return HasChild ==null ? false :true;
        }
        public async Task<List<Department>> getAll()
        {
            return await repository.GetAllAsync<Department>();
        }
        public async Task<bool> IsCodeExist(string code)
        {
            return await repository.AnyAsync<Department>(i => i.Code.ToLower() == code.ToLower() );
        }
        public async Task<bool> IsEmailExist(string Email)
        {
            return await repository.AnyAsync<Department>(i => i.Email.ToLower() == Email.ToLower() );
        }

        public async Task<List<SelectListItem>> GetListForDropdown(string Lang)
        {
            var Data =  repository.GetAll<Department>().Select(a =>
            new SelectListItem
            {
                Text = Lang.Contains("en")? a.NameEn : a.NameAr,
                Value = a.Code
            }).ToList();

            return Data;

         }

        public async Task<string> IsThisUserIsDepartmentManager(Guid UserId)
        {
            var _check = await repository.FirstOrDefaultAsync<Department>(a => a.ManagerId == UserId);
            return (_check != null? _check.Code : "");
        }
        
        
    }
}