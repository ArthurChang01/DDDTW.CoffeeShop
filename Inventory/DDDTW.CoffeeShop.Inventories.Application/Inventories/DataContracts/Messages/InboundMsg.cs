using MediatR;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Messages
{
    public class InboundMsg : IRequest<Domain.Inventories.Models.Inventory>
    {
        public string Id { get; set; }

        public int Amount { get; set; }
    }
}