using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Messages;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Responses;
using DDDTW.CoffeeShop.Inventories.WebAPI.Models.RequestModels;
using DDDTW.CoffeeShop.Inventories.WebAPI.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Inventories.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IMediator mediator;

        public InventoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryResp>>> Get([FromQuery] int pageNo,
            [FromQuery] int pageSize)
        {
            var result = await this.mediator.Send(new GetAllInventoryMsg(pageNo, pageSize));

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(InventoryResp))]
        public async Task<ActionResult<InventoryResp>> Get([FromRoute] string id)
        {
            var result = await this.mediator.Send(new GetInventoryMsg() { Id = id });
            if (result == null)
                return this.BadRequest();

            return this.Ok(result);
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(CreatedResult))]
        public async Task<ActionResult> Post([FromBody] AddInventoryReq request)
        {
            var item = this.TransformToItemResp(request.Item);
            var constraints = this.TransformToConstraints(request.Constraints);
            var msg = new AddInventoryMsg(request.Qty, item, constraints);

            var vm = await this.mediator.Send(msg);
            return this.Created(new Uri($"{this.Request.GetDisplayUrl()}/api/Inventory/{vm.Id}"), vm);
        }

        [HttpPut("{id}/qty")]
        public async Task<ActionResult> Put([FromRoute] string id, [FromBody] ChangeQtyReq model)
        {
            if (model.ActionMode == InventoryOperation.Inbound)
                await this.mediator.Send(new InboundMsg() { Id = id, Amount = model.Amount });
            else if (model.ActionMode == InventoryOperation.Outbound)
                await this.mediator.Send(new OutBoundMsg() { Id = id, Amount = model.Amount });

            return this.Ok();
        }

        #region Private Methods

        private InventoryItemResp TransformToItemResp(InventoryItemRM rm)
        {
            return new InventoryItemResp(rm.Name, rm.SKU, rm.Price, rm.Manufacturer, rm.ItemCategory,
                rm.InboundUnitName, rm.Capacity);
        }

        private IEnumerable<InventoryConstraintResp> TransformToConstraints(
            IEnumerable<InventoryConstraintRM> constraints)
        {
            return constraints.Select(o => new InventoryConstraintResp(o.Type, o.Value, o.DataTypeOfValue));
        }

        #endregion Private Methods
    }
}