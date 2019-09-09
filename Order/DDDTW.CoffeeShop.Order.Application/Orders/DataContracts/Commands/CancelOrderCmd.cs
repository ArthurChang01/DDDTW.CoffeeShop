using MediatR;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Commands
{
    public class CancelOrderCmd : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}