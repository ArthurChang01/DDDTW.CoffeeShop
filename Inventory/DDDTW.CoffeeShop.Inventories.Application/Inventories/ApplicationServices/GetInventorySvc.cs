using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Messages;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Results;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.ApplicationServices
{
    public class GetInventorySvc : IRequestHandler<GetInventoryMsg, InventoryRst>
    {
        private readonly ITranslator<InventoryId, string> idTranslator;
        private readonly IInventoryRepository repository;

        public GetInventorySvc(ITranslator<InventoryId, string> idTranslator,
             IInventoryRepository repository)
        {
            this.idTranslator = idTranslator;
            this.repository = repository;
        }

        public async Task<InventoryRst> Handle(GetInventoryMsg request, CancellationToken cancellationToken)
        {
            InventoryId id = idTranslator.Translate(request.Id);
            var inventory = await this.repository.GetBy(id) ?? throw new ArgumentException();
            return new InventoryRst(inventory);
        }
    }
}