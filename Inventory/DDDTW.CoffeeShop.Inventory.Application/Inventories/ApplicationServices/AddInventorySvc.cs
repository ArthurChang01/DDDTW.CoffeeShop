using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Messages;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Responses;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models = DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.ApplicationServices
{
    public class AddInventorySvc : IRequestHandler<AddInventoryMsg, InventoryResp>
    {
        private readonly ITranslator<Models.InventoryItem, InventoryItemResp> itemTranslator;
        private readonly ITranslator<IEnumerable<Models.InventoryConstraint>, IEnumerable<InventoryConstraintResp>> constraintTranslator;
        private readonly IInventoryRepository repository;

        public AddInventorySvc(
            ITranslator<Models.InventoryItem, InventoryItemResp> itemTranslator,
            ITranslator<IEnumerable<Models.InventoryConstraint>, IEnumerable<InventoryConstraintResp>> constraintsTranslator,
            IInventoryRepository repository)
        {
            this.itemTranslator = itemTranslator;
            this.constraintTranslator = constraintsTranslator;
            this.repository = repository;
        }

        public async Task<InventoryResp> Handle(AddInventoryMsg request, CancellationToken cancellationToken)
        {
            var id = this.repository.GenerateInventoryId();
            var item = this.itemTranslator.Translate(request.Item);
            var constraints = this.constraintTranslator.Translate(request.Constraints);
            var inventory = new Models.Inventory(id, request.Qty, item, constraints);

            this.repository.Save(inventory, inventory.DomainEvents);

            var vm = new InventoryResp(inventory);

            return await Task.FromResult(vm);
        }
    }
}