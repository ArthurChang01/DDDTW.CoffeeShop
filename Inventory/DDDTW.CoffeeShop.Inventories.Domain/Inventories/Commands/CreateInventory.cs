using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Commands
{
    public class CreateInventory
    {
        public CreateInventory(
            InventoryId id, int qty, InventoryItem item, IEnumerable<InventoryConstraint> constraints, bool suppressEvent = false)
        {
            this.Id = id;
            this.Qty = qty;
            this.Item = item;
            this.Constraints = constraints;
            this.SuppressEvent = suppressEvent;
        }

        public InventoryId Id { get; set; }

        public int Qty { get; set; }

        public InventoryItem Item { get; set; }

        public IEnumerable<InventoryConstraint> Constraints { get; set; }

        public bool SuppressEvent { get; } = false;
    }
}