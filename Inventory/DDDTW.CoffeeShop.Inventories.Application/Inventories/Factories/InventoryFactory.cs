using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Infrastructures.EventSourcings;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.DomainEvents;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.Factories
{
    public class InventoryFactory : ESFactoryBase<Inventory, InventoryId>, IInventoryFactory
    {
        public InventoryFactory(Inventory snapShot = null)
        {
            this.aggregateRoot = snapShot;
        }

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

        private void When(InventoryCreated @event)
        {
            this.aggregateRoot = new Inventory(@event.EntityId, @event.Qty, @event.Item, @event.Constraints);
        }

        private void When(Inbounded @event)
        {
            aggregateRoot.Inbound(@event.Amount);
        }

        private void When(Outbounded @event)
        {
            aggregateRoot.Outbound(@event.Amount);
        }
    }
}