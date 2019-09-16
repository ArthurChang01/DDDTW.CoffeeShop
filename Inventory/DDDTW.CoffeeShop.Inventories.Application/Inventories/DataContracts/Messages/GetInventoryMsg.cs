using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Responses;
using MediatR;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Messages
{
    public class GetInventoryMsg : IRequest<InventoryResp>
    {
        public string Id { get; set; }
    }
}