using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.DomainEvents
{
    public class InventoryCreated : DomainEvent<InventoryId>
    {
        #region Constructors

        public InventoryCreated(InventoryId id, int qty, InventoryItem item, List<InventoryConstraint> constraints)
        {
            this.EntityId = id;
            this.Qty = qty;
            this.Item = item;
            this.Constraints = constraints;
        }

        #endregion Constructors

        #region Properties

        public override InventoryId EntityId { get; }

        public int Qty { get; set; }

        public InventoryItem Item { get; set; }

        public IEnumerable<InventoryConstraint> Constraints { get; set; }

        #endregion Properties

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            yield return this.EntityId;
            yield return this.Qty;
            yield return this.Item;
            foreach (var constraint in this.Constraints)
            {
                yield return constraint;
            }
        }
    }
}