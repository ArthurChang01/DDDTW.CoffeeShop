using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Responses;
using MediatR;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Messages
{
    public class GetInventoryMsg : IRequest<InventoryResp>
    {
        public string Id { get; set; }
    }
}