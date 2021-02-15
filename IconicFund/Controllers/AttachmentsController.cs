using AutoMapper;
using IconicFund.Helpers;
using IconicFund.Models.Entities;
using IconicFund.Resources;
using IconicFund.Services.IServices;
using IconicFund.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using X.PagedList;

namespace IconicFund.Web.Controllers
{
    [Authorize]
    public class AttachmentsController : Controller
    {
        private readonly IAttachmentService attachmentService;
        private readonly IMapper mapper;
        public AttachmentsController(IAttachmentService _attachmentService, IMapper _mapper)
        {
            attachmentService = _attachmentService;
            mapper = _mapper;
        }
     
    }
}
