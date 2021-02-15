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
    public class DateTypesService : IDateTypesService
    {
        private readonly IBaseRepository repository; 

        public DateTypesService(IBaseRepository repository )
        {
            this.repository = repository;
          
        }
         
    
      
    }
}