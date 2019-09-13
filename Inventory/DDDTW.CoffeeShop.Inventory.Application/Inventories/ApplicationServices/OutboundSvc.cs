﻿using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Messages;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.ApplicationServices
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

        public Task<Domain.Inventories.Models.Inventory> Handle(OutBoundMsg request, CancellationToken cancellationToken)
        {
            var id = this.idTranslator.Translate(request.Id);
            var inventory = this.repository.GetBy(id);
            inventory.Outbound(request.Amount);

            this.repository.Save(inventory, inventory.DomainEvents);

            return Task.FromResult(inventory);
        }
    }
}