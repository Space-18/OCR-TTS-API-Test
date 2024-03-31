using MediatR;
using Microsoft.AspNetCore.Http;

namespace OCR.Application.Features.Files.Commands.ProcessFileInMemory
{
    public record ProcessFileInMemoryCommand(IFormFile File) : IRequest<string>;
}
