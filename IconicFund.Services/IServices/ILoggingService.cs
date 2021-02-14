using IconicFund.Helpers.Enums;
using IconicFund.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IconicFund.Services.IServices
{
    public interface ILoggingService
    {
        Task LogActionData<T>(LoggingCategory _categoty, LoggingAction _action, T OldData , T NewData, Guid _UserId , string RowId) where T: class;

        Task<List<SystemLogging>> SearchInLogging(DateTime? DateFrom = null, DateTime? DateTo = null, string RowId = "" , 
            LoggingAction? LoggingAction = null, LoggingCategory? LoggingCategory = null);

         Task<Dictionary<string, string>> GetDataFromXML(string xml);

        Task<SystemLogging> GetById(long Id);

    }
}
