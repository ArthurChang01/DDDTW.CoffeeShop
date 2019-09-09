using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.DomainEvents
{
    public class OrderItemsChanged : DomainEvent<OrderId>
    {
        #region Constructors

        public OrderItemsChanged(OrderId id, IEnumerable<OrderItem> changedItems)
            : base(id)
        {
            this.ChangedItems = changedItems as List<OrderItem>;
        }

        #endregion Constructors

        #region Properties

        public IReadOnlyList<OrderItem> ChangedItems { get; set; }

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