using OCR.Application.Contracts.Services.Commons;

namespace OCR.Application.Contracts.Services
{
    public interface IOcrService : IBaseService<IOcrService>
    {
        public string ProcessFileInMemory(byte[] image);
        public string ProcessSavedFile(string filePath);
    }
}
