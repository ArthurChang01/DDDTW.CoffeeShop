using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Commands
{
    public class CreateInventory
    {
        public CreateInventory(
            InventoryId id, int qty, InventoryItem item, IEnumerable<InventoryConstraint> constraints)
        {
            this.Id = id;
            this.Qty = qty;
            this.Item = item;
            this.Constraints = constraints;
        }

        public InventoryId Id { get; private set; }

        public int Qty { get; private set; }

        public InventoryItem Item { get; private set; }

        public IEnumerable<InventoryConstraint> Constraints { get; private set; }
    }
}