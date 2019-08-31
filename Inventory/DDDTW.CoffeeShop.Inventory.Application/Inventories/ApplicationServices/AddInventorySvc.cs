using System.Threading;
using System.Threading.Tasks;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Commands;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.ViewModels;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Interfaces;
using MediatR;
using Models = DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.ApplicationServices
{
    public class AddInventorySvc : IRequestHandler<AddInventoryCmd, InventoryVM>
    {
        private readonly ITranslator<InventoryVM, Models.Inventory> vmTranslator;
        private readonly IInventoryRepository repository;

        public AddInventorySvc(
            ITranslator<InventoryVM, Models.Inventory> vmTranslator,
            IInventoryRepository repository)
        {
            this.vmTranslator = vmTranslator;
            this.repository = repository;
        }

        public async Task<InventoryVM> Handle(AddInventoryCmd request, CancellationToken cancellationToken)
        {
            var id = this.repository.GenerateInventoryId();
            var inventory = new Models.Inventory(id, request.Qty, request.Item, request.Constraints);

            this.repository.Save(inventory, inventory.DomainEvents);

            var vm = this.vmTranslator.Translate(inventory);

            return await Task.FromResult(vm);
        }
    }
}