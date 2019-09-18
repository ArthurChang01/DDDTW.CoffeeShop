using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Results;
using MediatR;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Messages
{
    public class ChangeItemMsg : IRequest<Unit>
    {
        public ChangeItemMsg(string id, IEnumerable<OrderItemRst> orderItems)
        {
            this.Id = id;
            this.Items = orderItems;
        }

        public string Id { get; set; }
        public IEnumerable<OrderItemRst> Items { get; set; }
    }
}