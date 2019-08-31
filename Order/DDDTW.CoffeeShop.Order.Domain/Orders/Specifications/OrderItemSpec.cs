using System.Collections.Generic;
using System.Linq;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.Specifications
{
    internal class OrderItemSpec : Specification<IEnumerable<OrderItem>>
    {
        public OrderItemSpec(IEnumerable<OrderItem> items)
            : base(q => items != null && items.Any())
        {
        }
    }
}