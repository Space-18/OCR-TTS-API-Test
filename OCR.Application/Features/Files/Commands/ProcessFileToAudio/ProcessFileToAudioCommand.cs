using MediatR;
using Microsoft.AspNetCore.Http;

namespace OCR.Application.Features.Files.Commands.ProcessFileToAudio
{
    public record ProcessFileToAudioCommand(IFormFile File) : IRequest<(byte[], string)>;
}
