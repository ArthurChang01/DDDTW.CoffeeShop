using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Specifications
{
    internal class ConstraintSpec : Specification<IEnumerable<InventoryConstraint>>
    {
        private readonly IDictionary<InventoryConstraintType, TypeCode> constraintMap =
            new Dictionary<InventoryConstraintType, TypeCode>()
        {
            { InventoryConstraintType.MaxQty, TypeCode.Int32}
        };

        public ConstraintSpec(IEnumerable<InventoryConstraint> constraints)
        {
            this.Entity = constraints;
            this.Predicate = q => q.All(o =>
                (constraintMap.ContainsKey(o.Type) && constraintMap[o.Type] == o.DataTypeOfValue) &&
                Convert.ChangeType(o.Value, o.DataTypeOfValue) != null);
        }
    }
}