using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Infrastructures.EventSourcings;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Commands;
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
            var cmd = new CreateInventory(@event.EntityId, @event.Qty, @event.Item, @event.Constraints);
            this.aggregateRoot = Inventory.Create(cmd);
        }

        private void When(Inbounded @event)
        {
            var cmd = new Inbound(@event.Amount);
            aggregateRoot.Inbound(cmd);
        }

        private void When(Outbounded @event)
        {
            var cmd = new Outbound(@event.Amount);
            aggregateRoot.Outbound(cmd);
        }
    }
}