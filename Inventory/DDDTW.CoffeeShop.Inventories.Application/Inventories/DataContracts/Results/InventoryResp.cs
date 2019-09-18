using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Results
{
    public class InventoryRst : PropertyComparer<InventoryRst>
    {
        #region Constructors

        public InventoryRst()
        {
        }

        public InventoryRst(string id, int qty, InventoryItemRst item, IEnumerable<InventoryConstraintRst> constraints)
        {
            this.Id = id;
            this.Qty = qty;
            this.Item = item;
            this.Constraints = constraints;
        }

        public InventoryRst(Inventory inventory)
        {
            this.Id = inventory.Id.ToString();
            this.Qty = inventory.Qty;
            this.Item = new InventoryItemRst(inventory.Item);
            this.Constraints = inventory.Constraint.Select(o => new InventoryConstraintRst(o));
        }

        #endregion Constructors

        #region Properties

        public string Id { get; private set; }

        public int Qty { get; private set; }

        public InventoryItemRst Item { get; private set; }

        public IEnumerable<InventoryConstraintRst> Constraints { get; private set; }

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