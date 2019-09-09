using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventory.Domain.Inventories.DomainEvents
{
    public class Outbounded : DomainEvent<InventoryId>
    {
        #region Constructors

        public Outbounded(InventoryId id, int amount, int qty)
            : base(id)
        {
            this.Amount = amount;
            this.Qty = qty;
        }

        #endregion Constructors

        #region Properties

        public int Amount { get; private set; }

        public int Qty { get; private set; }

        #endregion Properties

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            yield return this.Amount;
        }
    }
}