using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Responses;
using MediatR;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Messages
{
    public class CreateOrderMsg : IRequest<OrderResp>
    {
        public CreateOrderMsg(string tableNo, IEnumerable<OrderItemResp> items)
        {
            this.TableNo = tableNo;
            this.Items = items;
        }

        public string TableNo { get; set; }

        public IEnumerable<OrderItemResp> Items { get; private set; }
    }
}