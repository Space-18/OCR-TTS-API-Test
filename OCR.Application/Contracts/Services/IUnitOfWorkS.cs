using OCR.Application.Contracts.Services.Commons;

namespace OCR.Application.Contracts.Services
{
    public interface IUnitOfWorkS : IDisposable
    {
        public IOcrService OcrService { get; }

        public IBaseService<TService> Service<TService>();
    }
}
