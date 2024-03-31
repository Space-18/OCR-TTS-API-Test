using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using OCR.Application.Features.Files.Commands.ProcessFileInMemory;
using OCR.Application.Features.Files.Commands.ProcessFileToAudio;
using OCR.Application.Features.Files.Commands.ProcessSavedFile;
using OCR.Presentation.Controllers.Commons;
using System.Net;

namespace OCR.Presentation.Controllers
{
    public class FilesController : SharedController
    {
        public FilesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("ProcessInMemory")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> ProcessFileInMemory(IFormFile file)
        {
            string result = await _mediator.Send(new ProcessFileInMemoryCommand(file));

            return Ok(result);
        }

        [HttpPost("ProcessAndSave")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> ProcessSavedFile(IFormFile file)
        {
            string result = await _mediator.Send(new ProcessSavedFileCommand(file));

            return Ok(result);
        }

        [HttpPost("ProcessFileToAudio")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> ProcessFileToAudio(IFormFile file)
        {
            (byte[] result, string path) = await _mediator.Send(new ProcessFileToAudioCommand(file));

            if (new FileExtensionContentTypeProvider().TryGetContentType(path, out string? contentType))
            {
                contentType = contentType.ToLower();
            }

            return File(result, contentType ?? "application/octec-stream");
        }
    }
}
