using MediatR;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Messages
{
    public class DeliverOrderMsg : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}