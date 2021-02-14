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
    public class PasswordComplexityService : IPasswordComplexityService
    {
        private readonly IBaseRepository repository; 

        public PasswordComplexityService(IBaseRepository repository )
        {
            this.repository = repository;
          
        }
         
        public async Task<List<Lkp_PasswordComplexity>> getAll()
        {
            return await repository.GetAllAsync<Lkp_PasswordComplexity>();
        }
      
    }
}