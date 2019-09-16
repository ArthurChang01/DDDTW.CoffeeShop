using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Exceptions;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Policies
{
    internal class InventoryPolicy
    {
        public static void Verify(Inventory inventory)
        {
            var exceptions = new List<Exception>();
            if (inventory.Qty < 0)
                exceptions.Add(new NegativeQtyException(inventory.Qty));

            if (inventory.Item == null)
                exceptions.Add(new InventoryItemIsNullException());

            if (inventory.Constraint.Any() == false)
                exceptions.Add(new EmptyConstraintException());

            if (new ConstraintSpec(inventory.Constraint).IsSatisfy() == false)
                exceptions.Add(new ConstraintValueIncorrectException());

            if (exceptions.Count > 0)
                throw new AggregateException(exceptions);
        }
    }
}