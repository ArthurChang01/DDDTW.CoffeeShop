using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Order.Domain.Orders.DomainEvents;
using DDDTW.CoffeeShop.Order.Domain.Orders.Exceptions;
using DDDTW.CoffeeShop.Order.Domain.Orders.Policies;
using DDDTW.CoffeeShop.Order.Domain.Orders.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.Models
{
    public class Order : AggregateRoot<OrderId>
    {
        private readonly List<OrderItem> orderItems;

        #region Constructors

        public Order()
        {
            this.Id = new OrderId();
            this.Status = OrderStatus.Initial;
            this.orderItems = new List<OrderItem>();
            this.CreatedDate = DateTimeOffset.Now;
        }

        public Order(OrderId id, OrderStatus status, IEnumerable<OrderItem> items, DateTimeOffset created, DateTimeOffset? modified = null)
        {
            this.Id = id;
            this.Status = status;
            this.orderItems = items as List<OrderItem> ??
                              items?.ToList() ??
                              new List<OrderItem>();
            this.CreatedDate = created;
            this.ModifiedDate = modified;

            var policy = new OrderPolicy(this);
            if (policy.IsValid() == false)
                throw policy.GetWrapperException;

            this.ApplyEvent(new OrderCreated(this.Id, this.OrderItems));
        }

        #endregion Constructors

        #region Properties

        public OrderStatus Status { get; private set; }

        public IReadOnlyList<OrderItem> OrderItems => this.orderItems;

        public decimal TotalFee => this.OrderItems.Sum(item => item.Fee);

        public DateTimeOffset CreatedDate { get; private set; }

        public DateTimeOffset? ModifiedDate { get; private set; }

        #endregion Properties

        #region Public Methods

        public void ChangeItem(IEnumerable<OrderItem> changedItems)
        {
            var newItemList = this.orderItems.Union(changedItems);

            this.orderItems.Clear();
            this.orderItems.AddRange(newItemList);

            this.ApplyEvent(new OrderItemsChanged(this.Id, changedItems));
        }

        public void Process()
        {
            if (this.Status == OrderStatus.Processing) return;

            var spec = new StatusTransitionSpec(this.Status, OrderStatus.Initial, OrderStatus.Processing);
            if (spec.IsSatisfy() == false)
                throw new StatusTransitionException(this.Status, OrderStatus.Processing);

            this.ChangeStatus(OrderStatus.Processing);
        }

        public void Deliver()
        {
            if (this.Status == OrderStatus.Deliver) return;

            var spec = new StatusTransitionSpec(this.Status, OrderStatus.Processing, OrderStatus.Deliver);
            if (spec.IsSatisfy() == false)
                throw new StatusTransitionException(this.Status, OrderStatus.Deliver);

            this.ChangeStatus(OrderStatus.Deliver);
        }

        public void Closed()
        {
            if (this.Status == OrderStatus.Closed) return;

            var spec = new StatusTransitionSpec(this.Status, OrderStatus.Deliver, OrderStatus.Closed);
            if (spec.IsSatisfy() == false)
                throw new StatusTransitionException(this.Status, OrderStatus.Closed);

            this.ChangeStatus(OrderStatus.Closed);
        }

        public void Cancel()
        {
            this.ChangeStatus(OrderStatus.Cancel);
        }

        #endregion Public Methods

        #region Private Methods

        private void ChangeStatus(OrderStatus status)
        {
            var originalStatus = this.Status;
            this.Status = status;
            this.ModifiedDate = DateTimeOffset.Now;

            this.ApplyEvent(new OrderStatusChanged(this.Id, originalStatus, this.Status));
        }

        #endregion Private Methods
    }
}