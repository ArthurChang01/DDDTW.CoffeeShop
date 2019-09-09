using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Inventory.Domain.Inventories.Specifications
{
    internal class ConstraintSpec : Specification<IEnumerable<InventoryConstraint>>
    {
        public ConstraintSpec(IEnumerable<InventoryConstraint> constraints)
            : base(constraints, q => q.All(o => Convert.ChangeType(o.Value, Type.GetType($"System.{o.DataTypeOfValue}")) != null))
        {
        }
    }
}