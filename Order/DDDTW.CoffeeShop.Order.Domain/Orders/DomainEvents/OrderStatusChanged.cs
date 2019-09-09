using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.DomainEvents
{
    public class OrderStatusChanged : DomainEvent<OrderId>
    {
        #region Constructors

        public OrderStatusChanged(OrderId id, OrderStatus lastStatus, OrderStatus curStatus)
            : base(id)
        {
            this.LastStatus = lastStatus;
            this.CurrentStatus = curStatus;
        }

        #endregion Constructors

        #region Properties

        public OrderStatus LastStatus { get; set; }

        public OrderStatus CurrentStatus { get; set; }

        #endregion Properties

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            yield return this.LastStatus;
            yield return this.CurrentStatus;
        }
    }
}