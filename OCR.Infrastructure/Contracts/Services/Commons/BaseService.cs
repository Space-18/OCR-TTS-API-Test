using OCR.Application.Contracts.Persistence;
using OCR.Application.Contracts.Services.Commons;

namespace OCR.Infrastructure.Contracts.Services.Commons
{
    internal class BaseService<TService> : IBaseService<TService>
    {
        public readonly IUnitOfWorkP _unitOfWorkP;

        public BaseService(IUnitOfWorkP unitOfWorkP)
        {
            _unitOfWorkP = unitOfWorkP;
        }
    }
}
