using System.Collections.Generic;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.DomainEvents
{
    public class OrderCreated : DomainEvent<OrderId>
    {
        public OrderCreated(OrderId id, IEnumerable<OrderItem> orderItems)
        {
            this.EntityId = id;
            this.OrderItems = orderItems;
        }

        public IEnumerable<OrderItem> OrderItems { get; set; }

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            foreach (var item in this.OrderItems)
            {
                yield return item;
            }
        }
    }
}