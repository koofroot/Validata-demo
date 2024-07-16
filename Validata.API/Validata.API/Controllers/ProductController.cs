using MediatR;
using Microsoft.AspNetCore.Mvc;
using Validata.Infrastructure.Handlers.ProductHandlers;

namespace Validata.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateProductCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

    }
}
