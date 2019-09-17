using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Infrastructures.EventSourcings;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Commands;
using DDDTW.CoffeeShop.Orders.Domain.Orders.DomainEvents;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.Factories
{
    public class OrderFactory : ESFactoryBase<Order, OrderId>, IOrderFactory
    {
        protected override void ApplyEvents(IEnumerable<IDomainEvent> events)
        {
            if (events.Any() == false)
                throw new ArgumentException("Events can not be null or empty");

            foreach (var @event in events)
            {
                this.When((dynamic)@event);
            }
            this.aggregateRoot.UnsuppressedEvent();
        }

        private void When(OrderCreated @event)
        {
            var cmd = new CreateOrder(@event.EntityId, @event.TableNo, @event.OrderItems);
            this.aggregateRoot = Order.Create(cmd);
        }

        private void When(OrderItemsChanged @event)
        {
            this.aggregateRoot.ChangeItem(new ChangeItem(@event.ChangedItems));
        }

        private void When(OrderStatusChanged @event)
        {
            switch (@event.LastStatus)
            {
                case OrderStatus.Processing:
                    this.aggregateRoot.Process();
                    break;

                case OrderStatus.Deliver:
                    this.aggregateRoot.Deliver();
                    break;

                case OrderStatus.Closed:
                    this.aggregateRoot.Closed();
                    break;

                case OrderStatus.Cancel:
                    this.aggregateRoot.Cancel();
                    break;
            }
        }
    }
}