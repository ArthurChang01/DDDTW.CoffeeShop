using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Commands
{
    public class CreateOrder
    {
        public CreateOrder(OrderId id, string tableNo, OrderStatus status, IEnumerable<OrderItem> items)
        {
            this.Id = id;
            this.TableNo = tableNo;
            this.Status = status;
            this.Items = items;
        }

        public OrderId Id { get; private set; }

        public string TableNo { get; private set; }

        public OrderStatus Status { get; private set; }

        public IEnumerable<OrderItem> Items { get; private set; }
    }
}