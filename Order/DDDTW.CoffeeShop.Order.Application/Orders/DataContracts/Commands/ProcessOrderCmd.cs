using MediatR;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Commands
{
    public class ProcessOrderCmd : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}