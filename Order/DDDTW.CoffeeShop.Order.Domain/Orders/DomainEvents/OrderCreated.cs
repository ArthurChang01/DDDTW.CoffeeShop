using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using System;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.DomainEvents
{
    public class OrderCreated : DomainEvent<OrderId>
    {
        #region Constructors

        public OrderCreated(OrderId id, IEnumerable<OrderItem> orderItems, DateTimeOffset createdDate)
            : base(id)
        {
            this.OrderItems = orderItems;
            this.CreatedDate = createdDate;
        }

        #endregion Constructors

        #region Properties

        public IEnumerable<OrderItem> OrderItems { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        #endregion Properties

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            foreach (var item in this.OrderItems)
            {
                yield return item;
            }

            yield return this.CreatedDate;
        }
    }
}