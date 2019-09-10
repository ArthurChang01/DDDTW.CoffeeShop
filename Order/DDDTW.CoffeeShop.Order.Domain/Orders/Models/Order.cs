using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Commands;
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
            : base(false)
        {
            this.Id = new OrderId();
            this.Status = OrderStatus.Initial;
            this.orderItems = new List<OrderItem>();
            this.CreatedDate = DateTimeOffset.Now;
        }

        public Order(OrderId id, OrderStatus status, IEnumerable<OrderItem> items, DateTimeOffset created, DateTimeOffset? modified = null, bool suppressEvent = false)
            : base(suppressEvent)
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

            this.ApplyEvent(new OrderCreated(this.Id, this.OrderItems, this.CreatedDate));
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

        public void ChangeItem(ChangeItemCmd cmd)
        {
            var newItemList = this.orderItems.Union(cmd.Items);

            this.orderItems.Clear();
            this.orderItems.AddRange(newItemList);

            this.ApplyEvent(new OrderItemsChanged(this.Id, cmd.Items));
        }

        public void Process()
        {
            var status = OrderStatus.Processing;
            this.VerifyStatus(OrderStatus.Initial, status);

            this.ChangeStatus(status);
        }

        public void Deliver()
        {
            var status = OrderStatus.Deliver;
            this.VerifyStatus(OrderStatus.Processing, status);

            this.ChangeStatus(status);
        }

        public void Closed()
        {
            var status = OrderStatus.Closed;
            this.VerifyStatus(OrderStatus.Deliver, status);

            this.ChangeStatus(status);
        }

        public void Cancel()
        {
            this.ChangeStatus(OrderStatus.Cancel);
        }

        #endregion Public Methods

        #region Private Methods

        private void VerifyStatus(OrderStatus previousStatus, OrderStatus targetStatus)
        {
            if (this.Status == targetStatus) return;

            var spec = new StatusTransitionSpec(this.Status, previousStatus, targetStatus);
            if (spec.IsSatisfy() == false)
                throw new StatusTransitionException(this.Status, targetStatus);
        }

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