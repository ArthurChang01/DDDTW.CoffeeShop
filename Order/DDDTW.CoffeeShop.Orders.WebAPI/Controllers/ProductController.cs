using DDDTW.CoffeeShop.Orders.Application.Products.DataContracts.Messages;
using DDDTW.CoffeeShop.Orders.Application.Products.DataContracts.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Orders.WebAPI.Controllers
{
    [Route("api/Order/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductRst>>> Get([FromQuery]int pageNo, [FromQuery]int pageSize)
        {
            var result = await this.mediator.Send(new GetProductMsg(pageNo, pageSize));
            if (result == null || result.Any() == false)
                return new BadRequestResult();

            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductRst>> Get([FromRoute] string id)
        {
            var result = await this.mediator.Send(new GetProductByMsg(id));
            if (result == null)
                return new BadRequestResult();

            return new OkObjectResult(result);
        }
    }
}