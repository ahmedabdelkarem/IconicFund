using IconicFund.Helpers.Enums;
using IconicFund.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IconicFund.Services.IServices
{
    public interface IPasswordComplexityService
    { 
        Task<List<Lkp_PasswordComplexity>> getAll();
 
    }
}