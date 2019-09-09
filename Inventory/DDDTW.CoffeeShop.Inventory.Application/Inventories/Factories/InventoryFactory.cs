using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Infrastructures;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.DomainEvents;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Models = DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.Factories
{
    public class InventoryFactory : ESFactoryBase<Models.Inventory, Models.InventoryId>, IInventoryFactory
    {
        public InventoryFactory(Models.Inventory snapShot = null)
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
        }

        private void When(InventoryCreated @event)
        {
            this.aggregateRoot = new Models.Inventory(@event.EntityId, @event.Qty, @event.Item, @event.Constraints);
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