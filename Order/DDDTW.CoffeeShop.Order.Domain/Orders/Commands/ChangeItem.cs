using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.Commands
{
    public class ChangeItem
    {
        public ChangeItem(IEnumerable<OrderItem> items)
        {
            this.Items = items;
        }

        public IEnumerable<OrderItem> Items { get; private set; }
    }
}