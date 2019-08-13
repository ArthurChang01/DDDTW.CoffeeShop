using System;
using System.Threading;
using System.Threading.Tasks;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.QueryModels;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.ViewModels;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;
using MediatR;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.ApplicationServices
{
    public class GetInventorySvc : IRequestHandler<GetInventoryQry, InventoryVM>
    {
        private readonly ITranslator<InventoryId, string> idTranslator;
        private readonly ITranslator<InventoryVM, Domain.Inventories.Models.Inventory> vmTranslator;
        private readonly IInventoryRepository repository;

        public GetInventorySvc(ITranslator<InventoryId, string> idTranslator, ITranslator<InventoryVM, Domain.Inventories.Models.Inventory> vmTranslator, IInventoryRepository repository)
        {
            this.idTranslator = idTranslator;
            this.vmTranslator = vmTranslator;
            this.repository = repository;
        }

        public Task<InventoryVM> Handle(GetInventoryQry request, CancellationToken cancellationToken)
        {
            InventoryId id = idTranslator.Translate(request.Id);
            var inventory = this.repository.GetBy(id) ?? throw new ArgumentException();
            var vm = this.vmTranslator.Translate(inventory);

            return Task.FromResult(vm);
        }
    }
}