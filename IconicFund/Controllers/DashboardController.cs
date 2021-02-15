using IconicFund.Services.IServices;
using IconicFund.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace IconicFund.FrontEnd.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {

        //private readonly ITransactionService transactionService;
        //private readonly IIncomingTransactionService incomingTransactionService;
    //    public DashboardController(ITransactionService transactionService, IIncomingTransactionService incomingTransactionService)
    //    {
    //        this.transactionService = transactionService;
    //        this.incomingTransactionService = incomingTransactionService;
    //    }

    //    [HttpGet]
    //    public IActionResult Index()
    //    {
    //        return View();
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> ReturnDashBoardData()
    //    {
    //        try
    //        {

    //            var random = new Random();
    //            DashBoardStatusChartViewModel results = new DashBoardStatusChartViewModel();

    //            #region TransactionDepartmentsBar
    //            List<ChartsDataViewModel> TransactionDepartmentsBar = new List<ChartsDataViewModel>();
    //            var TransactionDepartments = await transactionService.GetGroupedTransactionDepartmentsBar();
    //            foreach (var DepartmentCodes in TransactionDepartments.GroupBy(o => o.DepartmentCode))
    //            {
    //                TransactionDepartmentsBar.Add(new ChartsDataViewModel()
    //                {
    //                    label = DepartmentCodes.FirstOrDefault().DepartmentCode,
    //                    fields = new List<string>() { DepartmentCodes.FirstOrDefault().DepartmentCode },
    //                    value = new List<string>() { DepartmentCodes.Count().ToString() },
    //                    colorValue = String.Format("#{0:X6}", random.Next(0x1000000))
    //                });
    //            }
    //            #endregion

    //            #region TransactionCompletionsPie 
    //            List<ChartsDataViewModel> TransactionCompletionsPie = new List<ChartsDataViewModel>();
    //            var TransactionCompletions = await transactionService.GetGroupedTransactionCompletionsPie();
    //            foreach (var transaction in TransactionCompletions)
    //            {
    //                var diff = (transaction.ExportedTransaction.CreatedAt - transaction.IncomingTransaction.CreatedAt).Value.Days;
    //                TransactionCompletionsPie.Add(new ChartsDataViewModel()
    //                {
    //                    label = transaction.Title,
    //                    fields = new List<string>() { transaction.Title },
    //                    value = new List<string>() { diff.ToString() },
    //                    colorValue = String.Format("#{0:X6}", random.Next(0x1000000))
    //                });
    //            }
    //            #endregion

    //            #region TransactionsPeriorityBar 
    //            List<ChartsDataViewModel> TransactionsPeriorityBar = new List<ChartsDataViewModel>();
    //            var Transactions = await transactionService.GetAll();
    //            foreach (var transaction in Transactions.OrderBy(o => o.Priority).GroupBy(o => o.Priority))
    //            {
    //                var Priority = "";
    //                if (transaction.FirstOrDefault().Priority == Helpers.Enums.Priorities.Important)
    //                    Priority = GetDisplayName(Helpers.Enums.Priorities.Important);
    //                else if (transaction.FirstOrDefault().Priority == Helpers.Enums.Priorities.Urgent)
    //                    Priority = GetDisplayName(Helpers.Enums.Priorities.Urgent);
    //                else if (transaction.FirstOrDefault().Priority == Helpers.Enums.Priorities.Normal)
    //                    Priority = GetDisplayName(Helpers.Enums.Priorities.Normal);

    //                TransactionsPeriorityBar.Add(new ChartsDataViewModel()
    //                {
    //                    label = Priority,
    //                    fields = new List<string>() { Priority },
    //                    value = new List<string>() { transaction.Count().ToString() },
    //                    colorValue = String.Format("#{0:X6}", random.Next(0x1000000))
    //                });
    //            }
    //            #endregion

    //            List<ChartsDataViewModel> TransactionsIsSecretBar = new List<ChartsDataViewModel>();
    //            foreach (var transaction in Transactions.GroupBy(o => o.IsSecret))
    //            {
    //                var IsSecret = "";
    //                if (transaction.FirstOrDefault().IsSecret == true)
    //                    IsSecret = IconicFund.Resources.Labels.Secret;
    //                else
    //                    IsSecret = IconicFund.Resources.Labels.Normal2;

    //                TransactionsIsSecretBar.Add(new ChartsDataViewModel()
    //                {
    //                    label = IsSecret,
    //                    fields = new List<string>() { IsSecret },
    //                    value = new List<string>() { transaction.Count().ToString() },
    //                    colorValue = String.Format("#{0:X6}", random.Next(0x1000000))
    //                });
    //            }
                
    //            List<ChartsDataViewModel> IncomingTransactionsSourceBar = new List<ChartsDataViewModel>();
    //            var incomingTransactions =await incomingTransactionService.GetGroupedIncomingTransactionBar();
    //            foreach (var incTransaction in incomingTransactions.GroupBy(o => o.SourceId))
    //            {
                     

    //                IncomingTransactionsSourceBar.Add(new ChartsDataViewModel()
    //                {
    //                    label = incTransaction.FirstOrDefault().Source.Name,
    //                    fields = new List<string>() { incTransaction.FirstOrDefault().Source.Name },
    //                    value = new List<string>() { incTransaction.Count().ToString() },
    //                    colorValue = String.Format("#{0:X6}", random.Next(0x1000000))
    //                });
    //            }


    //            results.TransactionDepartmentsBar = TransactionDepartmentsBar;
    //            results.TransactionCompletionsPie = TransactionCompletionsPie;
    //            results.TransactionsPeriorityBar = TransactionsPeriorityBar;
    //            results.TransactionsIsSecretBar = TransactionsIsSecretBar;
    //            results.IncomingTransactionsSourceBar = IncomingTransactionsSourceBar;
    //            var data = new
    //            {
    //                list = results
    //            };

    //            return Ok(data);
    //        }
    //        catch (Exception e)
    //        {
    //            return Ok(new { success = false });
    //        }
    //    }

    //    public static string GetDisplayName(Enum enumValue)
    //    {
    //        return enumValue.GetType()
    //                        .GetMember(enumValue.ToString())
    //                        .First()
    //                        .GetCustomAttribute<DisplayAttribute>()
    //                        .GetName();
    //    }
    }
}