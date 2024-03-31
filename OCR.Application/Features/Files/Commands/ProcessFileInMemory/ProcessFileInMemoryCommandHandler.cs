using MediatR;
using OCR.Application.Contracts.Services;
using OCR.Application.Exceptions;

namespace OCR.Application.Features.Files.Commands.ProcessFileInMemory
{
    internal class ProcessFileInMemoryCommandHandler : IRequestHandler<ProcessFileInMemoryCommand, string>
    {
        private readonly IUnitOfWorkS _unitOfWorkS;

        public ProcessFileInMemoryCommandHandler(IUnitOfWorkS unitOfWorkS)
        {
            _unitOfWorkS = unitOfWorkS;
        }

        public async Task<string> Handle(ProcessFileInMemoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                byte[] fileByte;

                using (var item = new MemoryStream())
                {
                    await request.File.CopyToAsync(item, cancellationToken);
                    fileByte = item.ToArray();
                }

                string result = _unitOfWorkS.OcrService.ProcessFileInMemory(fileByte);

                return result;
            }
            catch (IOException)
            {
                throw new BadRequestException(GlobalConstants.IOExceptionBadRequest);
            }
        }
    }
}
