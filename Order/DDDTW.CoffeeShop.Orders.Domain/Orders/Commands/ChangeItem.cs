using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Commands
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