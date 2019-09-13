using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Messages;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Responses;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.ApplicationServices
{
    public class GetInventorySvc : IRequestHandler<GetInventoryMsg, InventoryResp>
    {
        private readonly ITranslator<InventoryId, string> idTranslator;
        private readonly IInventoryRepository repository;

        public GetInventorySvc(ITranslator<InventoryId, string> idTranslator,
             IInventoryRepository repository)
        {
            this.idTranslator = idTranslator;
            this.repository = repository;
        }

        public Task<InventoryResp> Handle(GetInventoryMsg request, CancellationToken cancellationToken)
        {
            InventoryId id = idTranslator.Translate(request.Id);
            var inventory = this.repository.GetBy(id) ?? throw new ArgumentException();
            var vm = new InventoryResp(inventory);

            return Task.FromResult(vm);
        }
    }
}