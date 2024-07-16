using MediatR;
using Microsoft.AspNetCore.Mvc;
using Validata.Infrastructure.Handlers.OrderHandlers;

namespace Validata.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _mediator.Send(new GetOpenOrdersCommand());

            return Ok(orders);
        }

        [HttpGet("{clientId:Guid}")]
        public async Task<IActionResult> GetClientOrder(Guid clientId)
        {
            var order = await _mediator.Send(new GetOrderCommand(clientId));

            return Ok(order);
        }

        [HttpPut("Close")]
        public async Task<IActionResult> CloseOrder(Guid id)
        {
            await _mediator.Send(new CloseOrederCommand(id));

            return Ok();
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(AddProductToOrderCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }
    }
}
