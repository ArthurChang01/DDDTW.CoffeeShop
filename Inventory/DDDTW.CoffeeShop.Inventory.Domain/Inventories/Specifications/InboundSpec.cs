using System;
using System.Linq;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;

namespace DDDTW.CoffeeShop.Inventory.Domain.Inventories.Specifications
{
    internal class InboundSpec : Specification<Models.Inventory>
    {
        public InboundSpec(Models.Inventory inventory, int amount)
        {
            this.Entity = inventory;
            var constraint = inventory.Constraint.First(o => o.Type == InventoryConstraintType.MaxQty);
            var upperBound = Convert.ChangeType(constraint.Value, Type.GetType(constraint.DataTypeOfValue) ??
                                                                  throw new InvalidCastException());

            this.Predicate = _ => inventory.Qty + amount <= (int)upperBound;
        }
    }
}