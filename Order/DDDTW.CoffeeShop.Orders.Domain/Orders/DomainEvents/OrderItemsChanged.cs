using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.DomainEvents
{
    public class OrderItemsChanged : DomainEvent<OrderId>
    {
        #region Constructors

        public OrderItemsChanged(OrderId id, IEnumerable<OrderItem> changedItems, DateTimeOffset modifiedDate)
        {
            this.EntityId = id;
            this.ChangedItems = changedItems as List<OrderItem>;
            this.ModifiedDate = modifiedDate;
        }

        #endregion Constructors

        #region Properties

        public override OrderId EntityId { get; }

        public IReadOnlyList<OrderItem> ChangedItems { get; set; }

        public DateTimeOffset ModifiedDate { get; private set; }

        #endregion Properties

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            yield return this.EntityId;
            foreach (var item in this.ChangedItems)
            {
                yield return item;
            }
        }
    }
}