using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Commands;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;
using MediatR;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.ApplicationServices
{
    public class InboundSvc : IRequestHandler<InboundCmd, Domain.Inventories.Models.Inventory>
    {
        private readonly ITranslator<InventoryId, string> idTranslator;
        private readonly IInventoryRepository repository;

        public InboundSvc(ITranslator<InventoryId, string> inventoryIdTranslator, IInventoryRepository repository)
        {
            this.idTranslator = inventoryIdTranslator;
            this.repository = repository;
        }

        public Task<Domain.Inventories.Models.Inventory> Handle(InboundCmd request, CancellationToken cancellationToken)
        {
            var id = this.idTranslator.Translate(request.Id);
            var inventory = this.repository.GetBy(id);
            inventory.Inbound(request.Amount);

            this.repository.Save(inventory, inventory.DomainEvents.First());

            return Task.FromResult(inventory);
        }
    }
}