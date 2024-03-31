using OCR.Domain.Entities.Commons;

namespace OCR.Domain.Entities
{
    public class Files : AuditoryBaseModel
    {
        public string FileName { get; set; } = null!;
        public string FileExtension { get; set; } = null!;
        public string FileCode { get; set; } = null!;
        public long FileSize { get; set; }
        public string ContentType { get; set; } = null!;
        public string? Autor { get; set; } = null;
        public string? Owner { get; set; } = null;
        public string? Machine { get; set; } = null;
    }
}
