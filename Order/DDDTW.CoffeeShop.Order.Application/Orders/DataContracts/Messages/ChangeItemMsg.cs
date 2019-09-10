using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Messages
{
    public class ChangeItemMsg : IRequest<Unit>
    {
        public ChangeItemMsg(string id, IEnumerable<OrderItemVM> orderItems)
        {
            this.Id = id;
            this.Items = orderItems;
        }

        public string Id { get; set; }
        public IEnumerable<OrderItemVM> Items { get; set; }
    }
}