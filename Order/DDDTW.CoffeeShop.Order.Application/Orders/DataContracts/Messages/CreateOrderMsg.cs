using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Responses;
using MediatR;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Messages
{
    public class CreateOrderMsg : IRequest<OrderResp>
    {
        public CreateOrderMsg(IEnumerable<OrderItemResp> items)
        {
            this.Items = items;
        }

        public IEnumerable<OrderItemResp> Items { get; private set; }
    }
}