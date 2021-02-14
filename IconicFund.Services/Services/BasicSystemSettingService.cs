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
    public class BasicSystemSettingService : IBasicSystemSettingService
    {
        private readonly IBaseRepository repository;
        private readonly IHasherService hasherService;
        private readonly ILoggingService loggingService;

        public BasicSystemSettingService(IBaseRepository repository, IHasherService hasherService ,ILoggingService loggingService)
        {
            this.repository = repository;
            this.hasherService = hasherService;
            this.loggingService = loggingService;
        }

        
        public async Task Add(BasicSystemSetting BasicSystemSetting, Guid _UserId)
        {
            repository.Add<BasicSystemSetting>(BasicSystemSetting);
            await repository.SaveChangesAsync();

            await loggingService.LogActionData<BasicSystemSetting>(LoggingCategory.BasicSystemSetting, LoggingAction.Create,
                     BasicSystemSetting, null, _UserId, BasicSystemSetting.Id.ToString());
        }

        public async Task Update(BasicSystemSetting BasicSystemSetting)
        {
            repository.Update<BasicSystemSetting>(BasicSystemSetting);
            await repository.SaveChangesAsync();
        }

        public async Task Delete(BasicSystemSetting BasicSystemSetting, Guid _UserId)
        {
            repository.Remove<BasicSystemSetting>(BasicSystemSetting);

            await loggingService.LogActionData<BasicSystemSetting>(LoggingCategory.BasicSystemSetting, LoggingAction.Delete,
                     BasicSystemSetting, null, _UserId, BasicSystemSetting.Id.ToString());

            await repository.SaveChangesAsync();
             
        }

        public async Task<BasicSystemSetting> GetBasicSystemSettingById(int id)
        {
            return (await repository.FirstOrDefaultAsync<BasicSystemSetting>(i => i.Id == id));
        }

        public  BasicSystemSetting GetLastBasicSystemSetting()
        {
            return  repository.GetAll<BasicSystemSetting>().LastOrDefault();
        }
        
    }
}