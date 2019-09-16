using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System;
using System.Linq;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Specifications
{
    internal class InboundSpec : Specification<Models.Inventory>
    {
        public InboundSpec(Models.Inventory inventory, int amount)
        {
            this.Entity = inventory;
            var constraint = inventory.Constraint.First(o => o.Type == InventoryConstraintType.MaxQty);
            var upperBound = Convert.ChangeType(constraint.Value, Type.GetType($"System.{constraint.DataTypeOfValue}") ??
                                                                  throw new InvalidCastException());

            this.Predicate = _ => inventory.Qty + amount <= (int)upperBound;
        }
    }
}