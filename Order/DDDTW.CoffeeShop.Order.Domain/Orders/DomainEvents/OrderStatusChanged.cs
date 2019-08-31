using System.Collections.Generic;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.DomainEvents
{
    public class OrderStatusChanged : DomainEvent<OrderId>
    {
        public OrderStatusChanged(OrderId id, OrderStatus lastStatus, OrderStatus curStatus)
        {
            this.EntityId = id;
            this.LastStatus = lastStatus;
            this.CurrentStatus = curStatus;
        }

        public OrderStatus LastStatus { get; set; }

        public OrderStatus CurrentStatus { get; set; }

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            yield return this.LastStatus;
            yield return this.CurrentStatus;
        }
    }
}