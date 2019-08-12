using System;
using System.Collections.Generic;
using System.Linq;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.DomainEvents;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Exceptions;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Specifications;

namespace DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models
{
    public class Inventory : AggregateRoot<InventoryId>
    {
        private readonly List<InventoryConstraint> constraints;

        #region Constructors

        public Inventory()
        {
            this.Id = new InventoryId();
            this.Qty = 0;
            this.Item = new InventoryItem();
            this.constraints = new List<InventoryConstraint>();
        }

        public Inventory(InventoryId id, int qty, InventoryItem item, IEnumerable<InventoryConstraint> ieConstraints)
        {
            this.Id = id;
            this.Qty = qty;
            this.Item = item;
            this.constraints = ieConstraints as List<InventoryConstraint> ??
                               ieConstraints?.ToList() ??
                               new List<InventoryConstraint>();
        }

        #endregion Constructors

        #region Properties

        public int Qty { get; private set; }

        public InventoryItem Item { get; private set; }

        public IReadOnlyList<InventoryConstraint> Constraint => this.constraints;

        #endregion Properties

        #region Public methods

        public void Inbound(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("amount can not be negative digital");
            if (new InboundSpec(this, amount).IsSatisfy() == false)
                throw new OverQtyLimitationException(amount);

            this.Qty += amount;
            this.ApplyEvent(new Inbounded(this.Id, amount, this.Qty));
        }

        public void Outbound(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("amount can not be negative digital");
            if (new OutboundSpec(this.Qty, amount).IsSatisfy() == false)
                throw new InventoryShortageException(amount);

            this.Qty -= amount;
            this.ApplyEvent(new Outbounded(this.Id, amount, this.Qty));
        }

        #endregion Public methods
    }
}