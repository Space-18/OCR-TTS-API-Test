using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OCR.Presentation.Controllers.Commons
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SharedController : ControllerBase
    {
        public readonly IMediator _mediator;

        public SharedController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
