using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OCR.Application.Contracts.Persistence;
using OCR.Application.Contracts.Services;
using OCR.Application.Exceptions;

namespace OCR.Application.Features.Files.Commands.ProcessSavedFile
{
    internal class ProcessSavedFileCommandHandler : IRequestHandler<ProcessSavedFileCommand, string>
    {
        private readonly IUnitOfWorkS _unitOfWorkS;
        private readonly IUnitOfWorkP _unitOfWorkP;
        private readonly string ContainerName = GlobalConstants.FilesLocalStorage;

        public ProcessSavedFileCommandHandler(IUnitOfWorkS unitOfWorkS, IUnitOfWorkP unitOfWorkP)
        {
            _unitOfWorkS = unitOfWorkS;
            _unitOfWorkP = unitOfWorkP;
        }

        public async Task<string> Handle(ProcessSavedFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string fileCodeName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";

                var file = ConvertFile(request.File, fileCodeName);

                await SavePhysicalFile(request.File, fileCodeName);

                string filePath = await _unitOfWorkP.FileRepository.SaveFile(file);

                string result = _unitOfWorkS.OcrService.ProcessSavedFile(filePath);

                return result;
            }
            catch (IOException)
            {
                throw new BadRequestException(GlobalConstants.IOExceptionBadRequest);
            }
        }

        private Domain.Entities.Files ConvertFile(IFormFile file, string fileCodeName)
        {
            Domain.Entities.Files fileEntity = new()
            {
                FileName = file.FileName,
                FileSize = file.Length,
                FileExtension = Path.GetExtension(file.FileName),
                FileCode = fileCodeName,
                ContentType = file.ContentType,
                Autor = string.Empty,
                Machine = string.Empty,
                Owner = string.Empty
            };

            return fileEntity;
        }

        private async Task SavePhysicalFile(IFormFile file, string fileCodeName)
        {
            string folder = Path.Combine(GlobalConstants.GetWebRootPath(), ContainerName);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string path = Path.Combine(folder, fileCodeName);

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var content = ms.ToArray();
            await File.WriteAllBytesAsync(path, content);
        }
    }
}
