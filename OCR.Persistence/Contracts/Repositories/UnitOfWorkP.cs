using OCR.Application.Contracts.Persistence;
using OCR.Application.Contracts.Persistence.Commons;
using OCR.Domain.Entities.Commons;
using OCR.Persistence.Contracts.Repositories.Commons;
using System.Collections;

namespace OCR.Persistence.Contracts.Repositories
{
    internal class UnitOfWorkP : IUnitOfWorkP
    {
        private Hashtable? _repositories;
        private readonly ApplicationDBContext _context;
        private IFileRepository? _fileRepository;
        private bool disposedValue;

        public IFileRepository FileRepository => _fileRepository ??= new FileRepository(_context);

        public UnitOfWorkP(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public IBaseRepository<TEntity> Repository<TEntity>() where TEntity : AuditoryBaseModel
        {
            _repositories ??= new();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(BaseRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IBaseRepository<TEntity>)_repositories[type]!;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _repositories?.Clear();
                }
                else
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        ~UnitOfWorkP()
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
