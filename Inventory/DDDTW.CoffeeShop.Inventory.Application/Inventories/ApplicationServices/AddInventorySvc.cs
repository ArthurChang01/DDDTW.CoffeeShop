using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Commands;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.ViewModels;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models = DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.ApplicationServices
{
    public class AddInventorySvc : IRequestHandler<AddInventoryCmd, InventoryVM>
    {
        private readonly ITranslator<Models.InventoryItem, InventoryItemVM> itemTranslator;
        private readonly ITranslator<IEnumerable<Models.InventoryConstraint>, IEnumerable<InventoryConstraintVM>> constraintTranslator;
        private readonly ITranslator<InventoryVM, Models.Inventory> vmTranslator;
        private readonly IInventoryRepository repository;

        public AddInventorySvc(
            ITranslator<Models.InventoryItem, InventoryItemVM> itemTranslator,
            ITranslator<IEnumerable<Models.InventoryConstraint>, IEnumerable<InventoryConstraintVM>> constraintsTranslator,
            ITranslator<InventoryVM, Models.Inventory> vmTranslator,
            IInventoryRepository repository)
        {
            this.itemTranslator = itemTranslator;
            this.constraintTranslator = constraintsTranslator;
            this.vmTranslator = vmTranslator;
            this.repository = repository;
        }

        public async Task<InventoryVM> Handle(AddInventoryCmd request, CancellationToken cancellationToken)
        {
            var id = this.repository.GenerateInventoryId();
            var item = this.itemTranslator.Translate(request.Item);
            var constraints = this.constraintTranslator.Translate(request.Constraints);
            var inventory = new Models.Inventory(id, request.Qty, item, constraints);

            this.repository.Save(inventory, inventory.DomainEvents);

            var vm = this.vmTranslator.Translate(inventory);

            return await Task.FromResult(vm);
        }
    }
}