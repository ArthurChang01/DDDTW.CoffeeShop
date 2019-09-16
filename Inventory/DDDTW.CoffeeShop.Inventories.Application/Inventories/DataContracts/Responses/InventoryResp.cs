using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Responses
{
    public class InventoryResp : PropertyComparer<InventoryResp>
    {
        #region Constructors

        public InventoryResp()
        {
        }

        public InventoryResp(string id, int qty, InventoryItemResp item, IEnumerable<InventoryConstraintResp> constraints)
        {
            this.Id = id;
            this.Qty = qty;
            this.Item = item;
            this.Constraints = constraints;
        }

        public InventoryResp(Inventory inventory)
        {
            this.Id = inventory.Id.ToString();
            this.Qty = inventory.Qty;
            this.Item = new InventoryItemResp(inventory.Item);
            this.Constraints = inventory.Constraint.Select(o => new InventoryConstraintResp(o));
        }

        #endregion Constructors

        #region Properties

        public string Id { get; private set; }

        public int Qty { get; private set; }

        public InventoryItemResp Item { get; private set; }

        public IEnumerable<InventoryConstraintResp> Constraints { get; private set; }

        #endregion Properties

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Id;
            yield return this.Qty;
            yield return this.Item;
            foreach (var constraint in this.Constraints)
            {
                yield return constraint;
            }
        }
    }
}