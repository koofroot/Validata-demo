using MediatR;
using Microsoft.AspNetCore.Mvc;
using Validata.Infrastructure.Handlers.CustomerHandlers;

namespace Validata.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCustomerAsync(CreateCustomerCommand command)
        {
            var response = await _mediator.Send(command);

            // TODO: must return 201
            return Ok(response);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateCustomerAsync(UpdateCustomerCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomerAsync(DeleteCustomerCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomerAsync(Guid id)
        {
            var response = await _mediator.Send(new GetCustomerCommand(id));

            return Ok(response);
        }
    }
}
