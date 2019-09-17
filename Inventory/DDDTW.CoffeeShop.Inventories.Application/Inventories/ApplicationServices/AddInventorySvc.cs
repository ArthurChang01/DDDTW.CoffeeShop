using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Messages;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Responses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Commands;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.ApplicationServices
{
    public class AddInventorySvc : IRequestHandler<AddInventoryMsg, InventoryResp>
    {
        private readonly ITranslator<InventoryItem, InventoryItemResp> itemTranslator;
        private readonly ITranslator<IEnumerable<InventoryConstraint>, IEnumerable<InventoryConstraintResp>> constraintTranslator;
        private readonly IInventoryRepository repository;

        public AddInventorySvc(
            ITranslator<InventoryItem, InventoryItemResp> itemTranslator,
            ITranslator<IEnumerable<InventoryConstraint>, IEnumerable<InventoryConstraintResp>> constraintsTranslator,
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
            var inventory = Inventory.Create(new CreateInventory(id, request.Qty, item, constraints));

            this.repository.Save(inventory);

            var vm = new InventoryResp(inventory);

            return await Task.FromResult(vm);
        }
    }
}