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
            : base(id)
        {
            this.ChangedItems = changedItems as List<OrderItem>;
            this.ModifiedDate = modifiedDate;
        }

        #endregion Constructors

        #region Properties

        public IReadOnlyList<OrderItem> ChangedItems { get; set; }

        public DateTimeOffset ModifiedDate { get; private set; }

        #endregion Properties

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            foreach (var item in this.ChangedItems)
            {
                yield return item;
            }
        }
    }
}