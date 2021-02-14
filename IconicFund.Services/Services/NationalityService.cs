using IconicFund.Models.Entities;
using IconicFund.Repositories;
using IconicFund.Services.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IconicFund.Services.Services
{
    public class NationalityService : INationalityService
    {
        private readonly IBaseRepository repository;
        public NationalityService(IBaseRepository _repository)
        {
            repository = _repository;
        }
        public async Task<List<Nationality>> GetAll()
        {
            return await repository.GetAllAsync<Nationality>();
        }
        public async Task<List<City>> GetCities()
        {
            return await repository.GetAllAsync<City>();
        }
         public async  Task<List<Region>> GetCitiyRegions(int cityId)
        {
            return await repository.GetAllWhereAsync<Region>(a=>a.CityId== cityId);
        }
        public async Task<List<Region>> GetRegions() {
            return await repository.GetAllAsync<Region>();

        }

    }
}
