using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Exceptions
{
    public class AmountIncorrectException : DomainException
    {
        public AmountIncorrectException(string errorMessage = null, Exception inner = null)
            : base(nameof(Inventory), InventoryErrorCode.AmountIsNegative,
                errorMessage ?? "Input parameter: Amount can not be negative digital", inner)
        {
        }
    }
}