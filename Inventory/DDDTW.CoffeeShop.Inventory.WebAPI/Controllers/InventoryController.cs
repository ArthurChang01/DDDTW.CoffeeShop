using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Commands;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.QueryModels;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.ViewModels;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventory.WebAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Inventory.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IInventoryRepository repository;
        private readonly ITranslator<InventoryVM, Domain.Inventories.Models.Inventory> vmTranslator;

        public InventoryController(IMediator mediator, IInventoryRepository repository, ITranslator<InventoryVM, Domain.Inventories.Models.Inventory> vmTranslator)
        {
            this.mediator = mediator;
            this.repository = repository;
            this.vmTranslator = vmTranslator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InventoryVM>> Get([FromQuery] int pageNo,
            [FromQuery] int pageSize)
        {
            var result = this.repository.Get(new Specification<Domain.Inventories.Models.Inventory>(x => true), pageNo,
                    pageSize)
                .Select(o => this.vmTranslator.Translate(o));

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(InventoryVM))]
        public async Task<ActionResult<InventoryVM>> Get([FromRoute] string id)
        {
            var vm = await this.mediator.Send(new GetInventoryQry() { Id = id });
            if (vm == null)
                return this.BadRequest();

            return this.Ok(vm);
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(CreatedResult))]
        public async Task<ActionResult> Post([FromBody] AddInventoryCmd cmd)
        {
            var vm = await this.mediator.Send(cmd);
            return this.Created(new Uri($"{this.Request.GetDisplayUrl()}/api/Inventory/{vm.Id}"), vm);
        }

        [HttpPut("{id}/qty")]
        public async Task<ActionResult> Put([FromRoute] string id, [FromBody] ChangeQtyModel model)
        {
            if (model.Operation.Equals(InventoryOperation.Inbound))
                await this.mediator.Send(new InboundCmd() { Id = id, Amount = model.Amount });
            else
                await this.mediator.Send(new OutBoundCmd() { Id = id, Amount = model.Amount });

            return this.Ok();
        }
    }
}