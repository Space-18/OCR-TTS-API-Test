using OCR.Application.Contracts.Persistence.Commons;
using OCR.Domain.Entities;

namespace OCR.Application.Contracts.Persistence
{
    public interface IFileRepository : IBaseRepository<Files>
    {
        public Task<string> SaveFile(Files file);
    }
}
