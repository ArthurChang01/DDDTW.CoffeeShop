using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Messages;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Commands;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.ApplicationServices
{
    public class InboundSvc : IRequestHandler<InboundMsg, Inventory>
    {
        private readonly ITranslator<InventoryId, string> idTranslator;
        private readonly IInventoryRepository repository;

        public InboundSvc(ITranslator<InventoryId, string> inventoryIdTranslator, IInventoryRepository repository)
        {
            this.idTranslator = inventoryIdTranslator;
            this.repository = repository;
        }

        public async Task<Inventory> Handle(InboundMsg request, CancellationToken cancellationToken)
        {
            var id = this.idTranslator.Translate(request.Id);
            var inventory = await this.repository.GetBy(id);
            inventory.Inbound(new Inbound(request.Amount));

            await this.repository.Save(inventory);

            return inventory;
        }
    }
}