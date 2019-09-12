using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Responses;
using MediatR;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Messages
{
    public class GetOrderMsg : IRequest<OrderResp>
    {
        public GetOrderMsg(string id)
        {
            this.Id = id;
        }

        public string Id { get; private set; }
    }
}