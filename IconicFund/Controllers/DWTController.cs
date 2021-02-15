using AutoMapper;
using IconicFund.CaptchaProvider;
using IconicFund.Helpers;
using IconicFund.Models;
using IconicFund.Models.Entities;
using IconicFund.Resources;
using IconicFund.Services.IServices;
using IconicFund.Web.Core;
using IconicFund.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IconicFund.FrontEnd.Controllers
{
    [AllowAnonymous]
    public class DWTController : Controller
    {

        //private readonly IIncomingAttachmentsService incomingAttachmentsService;
        private readonly IHostingEnvironment hostingEnvironment;
        public DWTController( IHostingEnvironment hostingEnvironment)
        {
            //this.incomingAttachmentsService = incomingAttachmentsService;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index(string SaveIn)
        {
            ViewData["SaveIn"] = SaveIn;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadTwainAsync()
        {
           // var file = Request.Files[0];
            var files = HttpContext.Request.Form.Files;

            // upload path
            string UploadPath = Path.Combine(hostingEnvironment.WebRootPath, "Documents");
             
            string FileName = Guid.NewGuid().ToString() + "-" + String.Join("", files[0].FileName.Split(' ', '*', '#', ',', '%'));

            string FullPath = Path.Combine(UploadPath, FileName);

            using (var stream = new FileStream(FullPath, FileMode.Create))
            {
                await files[0].CopyToAsync(stream);
            }

            return Ok(FileName);
            
        }

         
    }
}