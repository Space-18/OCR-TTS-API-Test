using OCR.Application.Contracts.Persistence;
using OCR.Application.Contracts.Services;
using OCR.Application.Contracts.Services.Commons;
using System.Collections;

namespace OCR.Infrastructure.Contracts.Services
{
    internal class UnitOfWorkS : IUnitOfWorkS
    {
        private Hashtable? _services;
        private readonly IUnitOfWorkP _unitOfWorkP;
        private IOcrService? _ocrService;
        private bool disposedValue;

        public IOcrService OcrService => _ocrService ??= new OcrService(_unitOfWorkP);

        public UnitOfWorkS(IUnitOfWorkP unitOfWorkP)
        {
            _unitOfWorkP = unitOfWorkP;
        }

        public IBaseService<TService> Service<TService>()
        {
            _services ??= new();

            var type = typeof(TService).Name;

            if (!_services.ContainsKey(type))
            {
                var serviceType = typeof(TService);
                var serviceInstance = Activator.CreateInstance(serviceType.MakeGenericType(typeof(TService)), _unitOfWorkP);
                _services.Add(type, serviceInstance);
            }

            return (IBaseService<TService>)_services[type]!;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _services?.Clear();
                }
                else
                {
                    _unitOfWorkP.Dispose();
                }

                disposedValue = true;
            }
        }

        ~UnitOfWorkS()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
