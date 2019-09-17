using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Commands
{
    public class CreateOrder
    {
        public CreateOrder(OrderId id, string tableNo, IEnumerable<OrderItem> items, bool suppressEvent = false)
        {
            this.Id = id;
            this.TableNo = tableNo;
            this.Items = items;
            this.SuppressEvent = suppressEvent;
        }

        public OrderId Id { get; set; }

        public string TableNo { get; set; }

        public IEnumerable<OrderItem> Items { get; set; }

        public bool SuppressEvent { get; } = false;
    }
}