using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Messages;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Results;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Commands;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.ApplicationServices
{
    public class AddInventorySvc : IRequestHandler<AddInventoryMsg, InventoryRst>
    {
        private readonly ITranslator<InventoryItem, InventoryItemRst> itemTranslator;
        private readonly ITranslator<IEnumerable<InventoryConstraint>, IEnumerable<InventoryConstraintRst>> constraintTranslator;
        private readonly IInventoryRepository repository;

        public AddInventorySvc(
            ITranslator<InventoryItem, InventoryItemRst> itemTranslator,
            ITranslator<IEnumerable<InventoryConstraint>, IEnumerable<InventoryConstraintRst>> constraintsTranslator,
            IInventoryRepository repository)
        {
            this.itemTranslator = itemTranslator;
            this.constraintTranslator = constraintsTranslator;
            this.repository = repository;
        }

        public async Task<InventoryRst> Handle(AddInventoryMsg request, CancellationToken cancellationToken)
        {
            var id = await this.repository.GenerateInventoryId();
            var item = this.itemTranslator.Translate(request.Item);
            var constraints = this.constraintTranslator.Translate(request.Constraints);
            var inventory = Inventory.Create(new CreateInventory(id, request.Qty, item, constraints));

            await this.repository.Save(inventory);

            return new InventoryRst(inventory);
        }
    }
}