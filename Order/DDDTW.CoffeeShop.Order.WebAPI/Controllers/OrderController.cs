using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Commands;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.QueryModels;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.ViewModels;
using DDDTW.CoffeeShop.Order.Domain.Orders.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models = DDDTW.CoffeeShop.Order.Domain.Orders.Models;

namespace DDDTW.CoffeeShop.Order.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IOrderRepository repository;
        private readonly ITranslator<OrderVM, Models.Order> vmTranslator;

        public OrderController(
            IMediator mediator,
            IOrderRepository repository,
            ITranslator<OrderVM, Models.Order> vmTranslator)
        {
            this.mediator = mediator;
            this.repository = repository;
            this.vmTranslator = vmTranslator;
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(IEnumerable<OrderVM>))]
        public ActionResult<IEnumerable<OrderVM>> Get([FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            var result = this.repository.Get(new Specification<Models.Order>(x => true), pageNo, pageSize)
                .Select(o => this.vmTranslator.Translate(o));

            return this.Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(OrderVM))]
        public async ValueTask<ActionResult<OrderVM>> Get([FromRoute]string id)
        {
            var vm = await this.mediator.Send(new GetOrderQry() { Id = id });
            if (vm == null)
                return this.BadRequest();

            return this.Ok(vm);
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(CreatedResult))]
        public async ValueTask<ActionResult> Post([FromBody] CreateOrderCmd cmd)
        {
            var vm = await this.mediator.Send(cmd);
            return this.Created(new Uri($"{this.Request.GetDisplayUrl()}/api/Order/{vm.Id}"), vm);
        }

        [HttpPatch("orderItems")]
        public async ValueTask<ActionResult> Patch([FromBody] ChangeItemCmd cmd)
        {
            await this.mediator.Send(cmd);
            return this.Ok();
        }

        [HttpPatch("status/process")]
        public async ValueTask<ActionResult> Patch([FromBody] ProcessOrderCmd cmd)
        {
            await this.mediator.Send(cmd);
            return this.Ok();
        }

        [HttpPatch("status/deliver")]
        public async ValueTask<ActionResult> Patch([FromBody] DeliverOrderCmd cmd)
        {
            await this.mediator.Send(cmd);
            return this.Ok();
        }

        [HttpPatch("status/close")]
        public async ValueTask<ActionResult> Patch([FromBody] CloseOrderCmd cmd)
        {
            await this.mediator.Send(cmd);
            return this.Ok();
        }

        [HttpDelete("{id}")]
        public async ValueTask<ActionResult> Delete([FromRoute] string id)
        {
            await this.mediator.Send(new CancelOrderCmd() { Id = id });
            return this.Ok();
        }
    }
}