using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Commands;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.DomainEvents;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Exceptions;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Policies;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models
{
    public class Inventory : AggregateRoot<InventoryId>
    {
        private List<InventoryConstraint> constraints;

        #region Constructors

        public Inventory()
        {
            this.Id = new InventoryId();
            this.Qty = 0;
            this.Item = new InventoryItem();
            this.constraints = new List<InventoryConstraint>();
        }

        private Inventory(InventoryId id, int qty, InventoryItem item, IEnumerable<InventoryConstraint> constraint)
        {
            this.Id = id;
            this.Qty = qty;
            this.Item = item;
            this.constraints = constraint as List<InventoryConstraint> ??
                               constraint?.ToList() ??
                               new List<InventoryConstraint>();
        }

        public Inventory(IEnumerable<IDomainEvent> events)
        {
            if (events == null || events.Any() == false)
                throw new ArgumentException("Events can not be empty or null");

            var projections = new Dictionary<string, Action<IDomainEvent>>()
            {
                {nameof(InventoryCreated), evt => this.When((InventoryCreated)evt) },
                {nameof(Inbounded), evt => this.When((Inbounded)evt) },
                {nameof(Outbounded), evt => this.When((Outbounded)evt) }
            };

            foreach (var @event in events)
            {
                projections[@event.GetType().Name](@event);
            }
        }

        #endregion Constructors

        #region Properties

        public int Qty { get; private set; }

        public InventoryItem Item { get; private set; }

        public IReadOnlyList<InventoryConstraint> Constraint => this.constraints;

        #endregion Properties

        #region Public methods

        public static Inventory Create(CreateInventory cmd)
        {
            var inventory = new Inventory(cmd.Id, cmd.Qty, cmd.Item, cmd.Constraints);
            InventoryPolicy.Verify(inventory);

            inventory.ApplyEvent(new InventoryCreated(inventory.Id, inventory.Qty, inventory.Item, inventory.constraints));

            return inventory;
        }

        public void Inbound(Inbound cmd)
        {
            if (new AmountSpec(cmd.Amount).IsSatisfy() == false)
                throw new AmountIncorrectException();
            if (new InboundSpec(this, cmd.Amount).IsSatisfy() == false)
                throw new OverQtyLimitationException(cmd.Amount);

            this.Qty += cmd.Amount;
            this.ApplyEvent(new Inbounded(this.Id, cmd.Amount, this.Qty));
        }

        public void Outbound(Outbound cmd)
        {
            if (new AmountSpec(cmd.Amount).IsSatisfy() == false)
                throw new AmountIncorrectException();
            if (new OutboundSpec(this.Qty, cmd.Amount).IsSatisfy() == false)
                throw new InventoryShortageException(cmd.Amount);

            this.Qty -= cmd.Amount;
            this.ApplyEvent(new Outbounded(this.Id, cmd.Amount, this.Qty));
        }

        #endregion Public methods

        #region Event Projection Methods

        private void When(InventoryCreated @event)
        {
            this.Id = @event.EntityId;
            this.Qty = @event.Qty;
            this.Item = @event.Item;
            this.constraints = @event.Constraints?.ToList() ?? new List<InventoryConstraint>();
        }

        private void When(Inbounded @event)
        {
            this.Qty += @event.Qty;
        }

        private void When(Outbounded @event)
        {
            this.Qty -= @event.Qty;
        }

        #endregion Event Projection Methods
    }
}