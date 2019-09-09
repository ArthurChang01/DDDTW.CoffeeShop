using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.DomainEvents
{
    public class OrderCreated : DomainEvent<OrderId>
    {
        #region Constructors

        public OrderCreated(OrderId id, IEnumerable<OrderItem> orderItems)
            : base(id)
        {
            this.OrderItems = orderItems;
        }

        #endregion Constructors

        #region Properties

        public IEnumerable<OrderItem> OrderItems { get; set; }

        #endregion Properties

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            foreach (var item in this.OrderItems)
            {
                yield return item;
            }
        }
    }
}