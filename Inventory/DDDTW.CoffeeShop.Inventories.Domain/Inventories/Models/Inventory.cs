using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.DomainEvents;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Exceptions;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Policies;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Specifications;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models
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

            InventoryPolicy.Verify(this);

            this.ApplyEvent(new InventoryCreated(this.Id, this.Qty, this.Item, this.constraints));
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
            if (new AmountSpec(amount).IsSatisfy() == false)
                throw new AmountIncorrectException();
            if (new InboundSpec(this, amount).IsSatisfy() == false)
                throw new OverQtyLimitationException(amount);

            this.Qty += amount;
            this.ApplyEvent(new Inbounded(this.Id, amount, this.Qty));
        }

        public void Outbound(int amount)
        {
            if (new AmountSpec(amount).IsSatisfy() == false)
                throw new AmountIncorrectException();
            if (new OutboundSpec(this.Qty, amount).IsSatisfy() == false)
                throw new InventoryShortageException(amount);

            this.Qty -= amount;
            this.ApplyEvent(new Outbounded(this.Id, amount, this.Qty));
        }

        #endregion Public methods
    }
}