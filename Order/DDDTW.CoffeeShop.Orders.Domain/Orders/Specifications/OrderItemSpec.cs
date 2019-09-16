using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Specifications
{
    internal class OrderItemSpec : Specification<IEnumerable<OrderItem>>
    {
        public OrderItemSpec(IEnumerable<OrderItem> items)
            : base(q => items != null && items.Any())
        {
        }
    }
}