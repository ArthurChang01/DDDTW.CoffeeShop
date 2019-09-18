using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Results;
using MediatR;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Messages
{
    public class CreateOrderMsg : IRequest<OrderRst>
    {
        public CreateOrderMsg(string tableNo, IEnumerable<OrderItemRst> items)
        {
            this.TableNo = tableNo;
            this.Items = items;
        }

        public string TableNo { get; set; }

        public IEnumerable<OrderItemRst> Items { get; private set; }
    }
}