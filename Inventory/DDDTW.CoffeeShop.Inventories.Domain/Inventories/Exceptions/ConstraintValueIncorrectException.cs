using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Exceptions
{
    public class ConstraintValueIncorrectException : DomainException
    {
        public ConstraintValueIncorrectException(string errorMessage = null, Exception inner = null)
            : base(nameof(Inventory), InventoryErrorCode.ConstraintValueIncorrect,
                errorMessage ?? "Constraint value and value type is not matched", inner)
        {
        }
    }
}