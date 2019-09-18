using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Messages;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Results;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.ApplicationServices
{
    public class GetAllInventorySvc : IRequestHandler<GetAllInventoryMsg, IEnumerable<InventoryRst>>
    {
        private readonly IInventoryRepository repository;

        public GetAllInventorySvc(
            IInventoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<InventoryRst>> Handle(GetAllInventoryMsg request, CancellationToken cancellationToken)
        {
            var results = (await this.repository.Get(new Specification<Domain.Inventories.Models.Inventory>(s => true),
                request.PageNo, request.PageSize))
                .Select(o => new InventoryRst(o));

            return results;
        }
    }
}