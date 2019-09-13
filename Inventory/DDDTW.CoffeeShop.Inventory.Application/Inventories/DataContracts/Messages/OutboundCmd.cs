using MediatR;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Messages
{
    public class OutBoundMsg : IRequest<Domain.Inventories.Models.Inventory>
    {
        public string Id { get; set; }

        public int Amount { get; set; }
    }
}