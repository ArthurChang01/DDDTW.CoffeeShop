using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Infrastructures.EventSourcings;
using DDDTW.CoffeeShop.Order.Domain.Orders.Commands;
using DDDTW.CoffeeShop.Order.Domain.Orders.DomainEvents;
using DDDTW.CoffeeShop.Order.Domain.Orders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Models = DDDTW.CoffeeShop.Order.Domain.Orders.Models;

namespace DDDTW.CoffeeShop.Order.Application.Orders.Factories
{
    public class OrderFactory : ESFactoryBase<Models.Order, Models.OrderId>, IOrderFactory
    {
        protected override void ApplyEvents(IEnumerable<IDomainEvent> events)
        {
            if (events.Any() == false)
                throw new ArgumentException("Events can not be null or empty");

            foreach (var @event in events)
            {
                this.When((dynamic)@event);
            }
            this.aggregateRoot.UnsuppressEvent();
        }

        private void When(OrderCreated @event)
        {
            this.aggregateRoot = new Models.Order(@event.EntityId, Models.OrderStatus.Initial, @event.OrderItems, @event.CreatedDate, null, true);
        }

        private void When(OrderItemsChanged @event)
        {
            this.aggregateRoot.ChangeItem(new ChangeItem(@event.ChangedItems));
        }

        private void When(OrderStatusChanged @event)
        {
            switch (@event.LastStatus)
            {
                case Models.OrderStatus.Processing:
                    this.aggregateRoot.Process();
                    break;

                case Models.OrderStatus.Deliver:
                    this.aggregateRoot.Deliver();
                    break;

                case Models.OrderStatus.Closed:
                    this.aggregateRoot.Closed();
                    break;

                case Models.OrderStatus.Cancel:
                    this.aggregateRoot.Cancel();
                    break;
            }
        }
    }
}