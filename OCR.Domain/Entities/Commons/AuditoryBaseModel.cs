using System.ComponentModel.DataAnnotations;

namespace OCR.Domain.Entities.Commons
{
    public abstract class AuditoryBaseModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CreatedByUsername { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; } = null;
        public string UpdatedByUsername { get; set; } = string.Empty;
        public DateTime? UpdatedDate { get; set; } = null;
        public string DeletedByUsername { get; set; } = string.Empty;
        public DateTime? DeletedDate { get; set; } = null;
        public bool IsActive { get; set; } = true;
    }
}
