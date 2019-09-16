using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Responses;
using MediatR;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Messages
{
    public class ChangeItemMsg : IRequest<Unit>
    {
        public ChangeItemMsg(string id, IEnumerable<OrderItemResp> orderItems)
        {
            this.Id = id;
            this.Items = orderItems;
        }

        public string Id { get; set; }
        public IEnumerable<OrderItemResp> Items { get; set; }
    }
}