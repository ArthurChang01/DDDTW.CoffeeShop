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
    public class OutboundSvc : IRequestHandler<OutBoundMsg, Domain.Inventories.Models.Inventory>
    {
        private readonly ITranslator<InventoryId, string> idTranslator;
        private readonly IInventoryRepository repository;

        public OutboundSvc(ITranslator<InventoryId, string> idTranslator, IInventoryRepository repository)
        {
            this.idTranslator = idTranslator;
            this.repository = repository;
        }

        public Task<Inventory> Handle(OutBoundMsg request, CancellationToken cancellationToken)
        {
            var id = this.idTranslator.Translate(request.Id);
            var inventory = this.repository.GetBy(id);
            inventory.Outbound(new Outbound(request.Amount));

            this.repository.Save(inventory);

            return Task.FromResult(inventory);
        }
    }
}