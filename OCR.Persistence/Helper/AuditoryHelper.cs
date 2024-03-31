using OCR.Domain.Entities.Commons;

namespace OCR.Persistence.Helper
{
    internal static class AuditoryHelper
    {
        public static void ApplyAuditoryInformation(ApplicationDBContext context)
        {
            foreach (var item in context.ChangeTracker.Entries<AuditoryBaseModel>())
            {
                var dateTime = DateTime.Now;
                switch (item.State)
                {
                    case Microsoft.EntityFrameworkCore.EntityState.Detached:
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Unchanged:
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Deleted:
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Modified:
                        item.Entity.UpdatedDate = dateTime;
                        item.Entity.UpdatedByUsername = "???";
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Added:
                        item.Entity.CreatedDate = dateTime;
                        item.Entity.CreatedByUsername = "???";
                        item.Entity.IsActive = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
