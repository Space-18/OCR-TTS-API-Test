using OCR.Application;
using OCR.Application.Contracts.Persistence;
using OCR.Domain.Entities;
using OCR.Persistence.Contracts.Repositories.Commons;

namespace OCR.Persistence.Contracts.Repositories
{
    internal class FileRepository : BaseRepository<Files>, IFileRepository
    {
        public FileRepository(ApplicationDBContext context) : base(context)
        {
        }

        public async Task<string> SaveFile(Files file)
        {
            var entityState = await _context.Files.AddAsync(file);

            if (entityState.State == Microsoft.EntityFrameworkCore.EntityState.Added)
            {
                await _context.SaveChangesAsync();
            }

            var filePath = $"{GlobalConstants.GetWebRootPath()}\\{GlobalConstants.FilesLocalStorage}\\{file.FileCode}";

            return filePath ?? string.Empty;
        }
    }
}
