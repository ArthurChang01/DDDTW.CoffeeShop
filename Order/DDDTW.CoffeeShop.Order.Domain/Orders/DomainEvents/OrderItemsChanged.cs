using System.Collections.Generic;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.DomainEvents
{
    public class OrderItemsChanged : DomainEvent<OrderId>
    {
        public OrderItemsChanged(OrderId id, IEnumerable<OrderItem> changedItems)
        {
            this.EntityId = id;
            this.ChangedItems = changedItems as List<OrderItem>;
        }

        public IReadOnlyList<OrderItem> ChangedItems { get; set; }

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            foreach (var item in this.ChangedItems)
            {
                yield return item;
            }
        }
    }
}