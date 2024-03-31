using OCR.Application.Contracts.Persistence.Commons;
using OCR.Domain.Entities.Commons;

namespace OCR.Application.Contracts.Persistence
{
    public interface IUnitOfWorkP : IDisposable
    {
        public IFileRepository FileRepository { get; }
        IBaseRepository<TEntity> Repository<TEntity>() where TEntity : AuditoryBaseModel;
        Task<int> Complete();
    }
}
