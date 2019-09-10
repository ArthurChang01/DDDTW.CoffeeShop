using MediatR;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Messages
{
    public class CloseOrderMsg : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}