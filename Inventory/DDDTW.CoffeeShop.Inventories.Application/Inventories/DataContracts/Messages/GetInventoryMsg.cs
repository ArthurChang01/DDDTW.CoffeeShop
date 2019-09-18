using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Results;
using MediatR;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Messages
{
    public class GetInventoryMsg : IRequest<InventoryRst>
    {
        public string Id { get; set; }
    }
}