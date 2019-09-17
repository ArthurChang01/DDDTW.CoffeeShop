using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.DomainEvents
{
    public class OrderStatusChanged : DomainEvent<OrderId>
    {
        #region Constructors

        public OrderStatusChanged(OrderId id, OrderStatus lastStatus, OrderStatus curStatus, DateTimeOffset modifiedDate)
            : base(id)
        {
            this.LastStatus = lastStatus;
            this.CurrentStatus = curStatus;
            this.ModifiedDate = modifiedDate;
        }

        #endregion Constructors

        #region Properties

        public OrderStatus LastStatus { get; set; }

        public OrderStatus CurrentStatus { get; set; }

        public DateTimeOffset ModifiedDate { get; set; }

        #endregion Properties

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            yield return this.LastStatus;
            yield return this.CurrentStatus;
        }
    }
}