using IconicFund.Models.Entities;
using IconicFund.Services.IServices;
using IconicFund.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace IconicFund.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ILoggingService loggingService;

        public HomeController(ILogger<HomeController> logger , ILoggingService loggingService)
        {

            _logger = logger;
            this.loggingService = loggingService;
        }

        public async Task<IActionResult> Index()
        {

            

          

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
