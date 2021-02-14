using IconicFund.Repositories;
using IconicFund.Services.IServices;

namespace IconicFund.Services.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IBaseRepository repository;
        public AttachmentService(IBaseRepository _repository)
        {
            repository = _repository;
        }      

    }
}
