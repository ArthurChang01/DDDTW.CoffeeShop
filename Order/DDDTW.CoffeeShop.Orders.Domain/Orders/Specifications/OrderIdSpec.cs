using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System.Text.RegularExpressions;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Specifications
{
    internal class OrderIdSpec : Specification<OrderId>
    {
        public OrderIdSpec(OrderId id)
        {
            this.Entity = id;
            this.Predicate = _ => id != null &&
                                  new Regex(@"ord-\d{8}-\d{1,}").IsMatch(id.ToString());
        }
    }
}