using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.DomainEvents
{
    public class Outbounded : DomainEvent<InventoryId>
    {
        #region Constructors

        public Outbounded(InventoryId id, int amount, int qty)
        {
            this.EntityId = id;
            this.Amount = amount;
            this.Qty = qty;
        }

        #endregion Constructors

        #region Properties

        public override InventoryId EntityId { get; }

        public int Amount { get; private set; }

        public int Qty { get; private set; }

        #endregion Properties

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            yield return this.EntityId;
            yield return this.Amount;
        }
    }
}