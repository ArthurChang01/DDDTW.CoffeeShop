using MediatR;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Commands
{
    public class InboundCmd : IRequest<Domain.Inventories.Models.Inventory>
    {
        public string Id { get; set; }
        public int Amount { get; set; }
    }
}