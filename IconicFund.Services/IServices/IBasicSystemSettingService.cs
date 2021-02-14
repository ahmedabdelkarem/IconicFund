﻿using IconicFund.Helpers.Enums;
using IconicFund.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IconicFund.Services.IServices
{
    public interface IBasicSystemSettingService
    {
       
        Task Add(BasicSystemSetting BasicSystemSetting, Guid _UserId);

        Task Update(BasicSystemSetting BasicSystemSetting);

        Task Delete(BasicSystemSetting BasicSystemSetting, Guid _UserId);

        Task<BasicSystemSetting> GetBasicSystemSettingById(int id);
        BasicSystemSetting GetLastBasicSystemSetting();

    }
}