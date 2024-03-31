using OCR.Application.Contracts.Persistence;
using OCR.Application.Contracts.Services;
using OCR.Infrastructure.Contracts.Services.Commons;
using Tesseract;

namespace OCR.Infrastructure.Contracts.Services
{
    internal class OcrService : BaseService<IOcrService>, IOcrService
    {
        public OcrService(IUnitOfWorkP unitOfWorkP) : base(unitOfWorkP)
        {
        }

        public string ProcessFileInMemory(byte[] image)
        {
            string textResult = string.Empty;

            using (var engine = new TesseractEngine(@"./tessdata", "spa", EngineMode.Default))
            {
                using var img = Pix.LoadFromMemory(image);
                using var page = engine.Process(img);
                textResult = page.GetText();
            }

            return textResult;
        }

        public string ProcessSavedFile(string filePath)
        {
            string textResult = string.Empty;

            using (var engine = new TesseractEngine(@"./tessdata", "spa", EngineMode.Default))
            {
                using var img = Pix.LoadFromFile(filePath);
                using var page = engine.Process(img);
                textResult = page.GetText();
            }

            return textResult;
        }
    }
}
