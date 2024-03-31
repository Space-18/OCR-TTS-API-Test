using MediatR;
using Microsoft.AspNetCore.Http;

namespace OCR.Application.Features.Files.Commands.ProcessSavedFile
{
    public record ProcessSavedFileCommand(IFormFile File) : IRequest<string>;
}
