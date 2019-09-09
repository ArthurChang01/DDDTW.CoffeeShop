using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Exceptions;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Specifications;
using System.Linq;

namespace DDDTW.CoffeeShop.Inventory.Domain.Inventories.Policies
{
    internal class InventoryPolicy : Policy<Models.Inventory>
    {
        public InventoryPolicy(Models.Inventory aggregateRoot)
            : base(aggregateRoot)
        {
        }

        public override bool IsValid()
        {
            if (this.aggregateRoot.Qty < 0)
                this.exceptions.Add(new NegativeQtyException(aggregateRoot.Qty));

            if (this.aggregateRoot.Item == null)
                this.exceptions.Add(new InventoryItemIsNullException());

            if (this.aggregateRoot.Constraint.Any() == false)
                this.exceptions.Add(new EmptyConstraintException());

            if (new ConstraintSpec(this.aggregateRoot.Constraint).IsSatisfy() == false)
                this.exceptions.Add(new ConstraintValueIncorrectException());

            return this.exceptions.Count == 0;
        }
    }
}