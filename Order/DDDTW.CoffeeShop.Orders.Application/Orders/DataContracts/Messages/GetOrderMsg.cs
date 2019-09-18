using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Results;
using MediatR;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Messages
{
    public class GetOrderMsg : IRequest<OrderRst>
    {
        public GetOrderMsg(string id)
        {
            this.Id = id;
        }

        public string Id { get; private set; }
    }
}