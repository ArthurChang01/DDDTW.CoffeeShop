using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Messages;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.ViewModels;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using DDDTW.CoffeeShop.Order.WebAPI.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Order.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(IEnumerable<OrderVM>))]
        public ActionResult<IEnumerable<OrderVM>> Get([FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            var result = this.mediator.Send(new GetAllOrderMsg(pageNo, pageSize));

            return this.Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(OrderVM))]
        public async ValueTask<ActionResult<OrderVM>> Get([FromRoute]string id)
        {
            var vm = await this.mediator.Send(new GetOrderMsg() { Id = id });
            if (vm == null)
                return this.BadRequest();

            return this.Ok(vm);
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(CreatedResult))]
        public async ValueTask<ActionResult> Post([FromBody] AddOrderReq req)
        {
            var cmd = new CreateOrderMsg() { Items = req.Items.Select(o => new OrderItemVM(new ProductVM(o.Product.Id, o.Product.Name), o.Qty, o.Price)) };
            var vm = await this.mediator.Send(cmd);
            return this.Created(new Uri($"{this.Request.GetDisplayUrl()}/api/Order/{vm.Id}"), vm);
        }

        [HttpPatch("{id}/orderItems")]
        public async ValueTask<ActionResult> Patch([FromRoute]string id, [FromBody] ChangeOrderItemReq req)
        {
            var cmd = new ChangeItemMsg(id, req.OrderItems);
            await this.mediator.Send(cmd);
            return this.Ok();
        }

        [HttpPatch("{id}/status")]
        public async ValueTask<ActionResult> Patch([FromRoute]string id, [FromBody] ChangeStatusReq req)
        {
            var cmd = this.GetMsg(id, req);
            await this.mediator.Send(cmd);
            return this.Ok();
        }

        [HttpDelete("{id}")]
        public async ValueTask<ActionResult> Delete([FromRoute] string id)
        {
            var cmd = new CancelOrderMsg(id);
            await this.mediator.Send(cmd);
            return this.Ok();
        }

        private IRequest<Unit> GetMsg(string id, ChangeStatusReq dto)
        {
            IRequest<Unit> cmd = null;
            switch (dto.OrderStatus)
            {
                case OrderStatus.Processing:
                    cmd = new ProcessOrderMsg() { Id = id };
                    break;

                case OrderStatus.Deliver:
                    cmd = new DeliverOrderMsg() { Id = id };
                    break;

                case OrderStatus.Closed:
                    cmd = new CloseOrderMsg() { Id = id };
                    break;
            }

            return cmd;
        }
    }
}