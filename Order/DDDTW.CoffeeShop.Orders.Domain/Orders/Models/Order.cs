using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Commands;
using DDDTW.CoffeeShop.Orders.Domain.Orders.DomainEvents;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Exceptions;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Policies;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Models
{
    public class Order : AggregateRoot<OrderId>
    {
        private List<OrderItem> orderItems;

        #region Constructors

        public Order()
        {
            this.Id = new OrderId();
            this.TableNo = string.Empty;
            this.Status = OrderStatus.Initial;
            this.orderItems = new List<OrderItem>();
            this.CreatedDate = DateTimeOffset.Now;
        }

        private Order(OrderId id, string tableNo, OrderStatus status, IEnumerable<OrderItem> items, DateTimeOffset created, DateTimeOffset? modified = null)
        {
            this.Id = id;
            this.TableNo = tableNo;
            this.Status = status;
            this.orderItems = items as List<OrderItem> ??
                              items?.ToList() ??
                              new List<OrderItem>();
            this.CreatedDate = created;
            this.ModifiedDate = modified;
        }

        public Order(IEnumerable<IDomainEvent<OrderId>> events)
        {
            var projections = new Dictionary<string, Action<IDomainEvent<OrderId>>>()
           {
               {nameof(OrderCreated), evt => this.When((OrderCreated)evt) },
               {nameof(OrderItemsChanged), evt => this.When((OrderItemsChanged)evt) },
               {nameof(OrderStatusChanged), evt => this.When((OrderStatusChanged)evt) }
           };

            foreach (var @event in events)
            {
                projections[@event.GetType().Name](@event);
            }
        }

        #endregion Constructors

        #region Properties

        public string TableNo { get; private set; }

        public OrderStatus Status { get; private set; }

        public IReadOnlyList<OrderItem> OrderItems => this.orderItems;

        public decimal TotalFee => this.OrderItems.Sum(item => item.Fee);

        public DateTimeOffset CreatedDate { get; private set; }

        public DateTimeOffset? ModifiedDate { get; private set; }

        #endregion Properties

        #region Public Methods

        public static Order Create(CreateOrder cmd)
        {
            var order = new Order(cmd.Id, cmd.TableNo, cmd.Status, cmd.Items, DateTimeOffset.Now);
            OrderPolicy.Verify(order);

            order.ApplyEvent(new OrderCreated(order.Id, order.TableNo, order.OrderItems, order.CreatedDate));

            return order;
        }

        public void ChangeItem(ChangeItem cmd)
        {
            if (cmd.Items == null || cmd.Items?.Any() == false) return;

            var newItemList = this.orderItems.Union(cmd.Items);

            this.orderItems.Clear();
            this.orderItems.AddRange(newItemList);

            this.ModifiedDate = DateTimeOffset.Now;

            this.ApplyEvent(new OrderItemsChanged(this.Id, cmd.Items, this.ModifiedDate.Value));
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

        #region Event Projection Methods

        public void When(OrderCreated @event)
        {
            this.Id = @event.EntityId;
            this.TableNo = @event.TableNo;
            this.Status = OrderStatus.Initial;
            this.orderItems = @event.OrderItems?.ToList() ?? new List<OrderItem>();
            this.CreatedDate = @event.CreatedDate;
        }

        public void When(OrderItemsChanged @event)
        {
            this.orderItems = @event.ChangedItems?.ToList() ?? new List<OrderItem>();
            this.ModifiedDate = @event.ModifiedDate;
        }

        public void When(OrderStatusChanged @event)
        {
            this.SuppressEvent();
            switch (@event.CurrentStatus)
            {
                case OrderStatus.Processing:
                    this.Process();
                    break;

                case OrderStatus.Deliver:
                    this.Deliver();
                    break;

                case OrderStatus.Closed:
                    this.Closed();
                    break;

                case OrderStatus.Cancel:
                    this.Cancel();
                    break;
            }
            this.UnsuppressedEvent();
        }

        #endregion Event Projection Methods

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

            this.ApplyEvent(new OrderStatusChanged(this.Id, originalStatus, this.Status, this.ModifiedDate.Value));
        }

        #endregion Private Methods
    }
}