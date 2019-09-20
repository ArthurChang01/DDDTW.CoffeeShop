using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Messages;
using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Results;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using DDDTW.CoffeeShop.Orders.WebAPI.Models.Orders.RequestModels;
using DDDTW.CoffeeShop.Orders.WebAPI.Models.Orders.Requests;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Orders.WebAPI.Controllers
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
        [ProducesDefaultResponseType(typeof(IEnumerable<OrderRst>))]
        public async Task<ActionResult<IEnumerable<OrderRst>>> Get([FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            var result = await this.mediator.Send(new GetAllOrderMsg(pageNo, pageSize));

            return this.Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(OrderRst))]
        public async ValueTask<ActionResult<OrderRst>> Get([FromRoute]string id)
        {
            var vm = await this.mediator.Send(new GetOrderMsg(id));
            if (vm == null)
                return this.BadRequest();

            return this.Ok(vm);
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(CreatedResult))]
        public async ValueTask<ActionResult> Post([FromBody] AddOrderReq req)
        {
            var cmd = new CreateOrderMsg("0", this.TransformToOrderItemVM(req.Items));
            var vm = await this.mediator.Send(cmd);
            return this.Created(new Uri($"{this.Request.GetDisplayUrl()}/api/Order/{vm.Id}"), vm);
        }

        [HttpPut("{id}/orderItems")]
        public async ValueTask<ActionResult> Patch([FromRoute]string id, [FromBody] ChangeOrderItemReq req)
        {
            var cmd = new ChangeItemMsg(id, this.TransformToOrderItemVM(req.Items));
            await this.mediator.Send(cmd);
            return this.Ok();
        }

        [HttpPut("{id}/status")]
        public async ValueTask<ActionResult> Patch([FromRoute]string id, [FromBody] ChangeStatusReq req)
        {
            var cmd = this.GetMsg(id, req);
            await this.mediator.Send(cmd);
            return this.Ok();
        }

        [HttpDelete("{id}")]
        public async ValueTask<ActionResult> Delete([FromRoute] string id)
        {
            try
            {
                var cmd = new CancelOrderMsg(id);
                await this.mediator.Send(cmd);
                return this.Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Private Methods

        private IEnumerable<OrderItemRst> TransformToOrderItemVM(IEnumerable<OrderItemRM> item)
        {
            return item.Select(o => new OrderItemRst(new Uri($"{Request.GetDisplayUrl()}/Order/Product/{o.ProductId}").ToString(), o.Qty, o.Price));
        }

        private IRequest<Unit> GetMsg(string id, ChangeStatusReq dto)
        {
            IRequest<Unit> cmd = null;
            switch (dto.OrderStatus)
            {
                case OrderStatus.Processing:
                    cmd = new ProcessOrderMsg(id);
                    break;

                case OrderStatus.Deliver:
                    cmd = new DeliverOrderMsg(id);
                    break;

                case OrderStatus.Closed:
                    cmd = new CloseOrderMsg(id);
                    break;
            }

            return cmd;
        }

        #endregion Private Methods
    }
}