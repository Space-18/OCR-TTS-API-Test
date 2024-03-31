using Microsoft.EntityFrameworkCore;
using OCR.Domain.Entities;
using OCR.Persistence.Helper;

namespace OCR.Persistence
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
            Files = Set<Files>();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AuditoryHelper.ApplyAuditoryInformation(this);
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Files> Files { get; set; }
    }
}
