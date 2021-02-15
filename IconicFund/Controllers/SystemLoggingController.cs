using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;
using IconicFund.Helpers;
using IconicFund.Helpers.Enums;
using IconicFund.Services.IServices;
using IconicFund.Web.Core;
using IconicFund.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace IconicFund.Web.Controllers
{
    [Authorize]
    public class SystemLoggingController : Controller
    {
        private readonly ILoggingService loggingService;


        #region CTR
        public SystemLoggingController(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }
        #endregion



        [PermissionAuthorize(new[] { "ViewSystemLogging" })]
        public async Task<IActionResult> Index(int page = 1, DateTime? DateFrom = null,
            DateTime? DateTo = null, string RowId = "", LoggingAction? LoggingAction = null, LoggingCategory? LoggingCategory = null)
        {

            DateFrom = (DateFrom != null && DateFrom != DateTime.MinValue) ?
           ConvertDatetime.ConvertToGregorianDate((DateTime)DateFrom) : DateFrom;

            DateTo = (DateTo != null && DateTo != DateTime.MinValue) ?
          ConvertDatetime.ConvertToGregorianDate((DateTime)DateTo) : DateTo;



            var _model = new LoggingViewModel
            {
                DateFrom = DateFrom,
                DateTo = DateTo,
                RowID = RowId,
                LoggingAction = LoggingAction,
                LoggingCategory = LoggingCategory
            };


            var _Data = await loggingService.SearchInLogging(DateFrom, DateTo, RowId , LoggingAction , LoggingCategory);

            _model.LoggingList = _Data.Select(a => new LoggingViewModelItems
            {
                ID = a.ID,
                ActionDate = a.ActionDate,
                LoggingAction = a.LoggingAction,
                LoggingCategory = a.LoggingCategory,
                RowID = a.RowID,
                UserName = a.UserData?.FirstName + " " + a.UserData?.SecondName + " " + a.UserData?.ThirdName,


            }).ToPagedList(page, Constants.PageSize);




            return View(_model);
        }



        #region Get  Log Data Details 
        public async Task<IActionResult> GetLogDetailsPartial(long LogID)
        {
            var _rowData =await loggingService.GetById(LogID);

            var _model = new LoggingDataViewModel();

            _model.ActionType = _rowData.LoggingAction;

            _model.OldData =await loggingService.GetDataFromXML(_rowData.OldData);

            _model.NewData = _rowData.LoggingAction == LoggingAction.Edit ? await loggingService.GetDataFromXML(_rowData.NewData) :
                new Dictionary<string, string>();

            return PartialView("_LoggingDataPartialView" , _model);
        }
        #endregion
    }
}
