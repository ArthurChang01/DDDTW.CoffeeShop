using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Exceptions
{
    public class EmptyConstraintException : DomainException
    {
        public EmptyConstraintException(string errorMessage = "", Exception innerException = null)
            : base(nameof(Inventory), InventoryErrorCode.ConstraintIsEmpty,
                errorMessage ?? "Constraint is empty", innerException)
        {
        }
    }
}