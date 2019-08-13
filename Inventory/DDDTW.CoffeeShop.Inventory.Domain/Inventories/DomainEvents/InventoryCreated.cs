using System.Collections.Generic;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;

namespace DDDTW.CoffeeShop.Inventory.Domain.Inventories.DomainEvents
{
    public class InventoryCreated : DomainEvent<InventoryId>
    {
        public InventoryCreated(InventoryId id, int qty, InventoryItem item, List<InventoryConstraint> constraints)
        {
            this.EntityId = id;
            this.Qty = qty;
            this.Item = item;
            this.Constraints = constraints;
        }

        public int Qty { get; set; }

        public InventoryItem Item { get; set; }

        public IEnumerable<InventoryConstraint> Constraints { get; set; }

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            yield return this.Qty;
            yield return this.Item;
            foreach (var constraint in this.Constraints)
            {
                yield return constraint;
            }
        }
    }
}