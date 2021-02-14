using IconicFund.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IconicFund.Services.IServices
{
    public interface INationalityService
    {
        Task<List<Nationality>> GetAll();

        Task<List<City>> GetCities();
        Task<List<Region>> GetRegions();

        Task<List<Region>> GetCitiyRegions(int cityId);


    }
}
