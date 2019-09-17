using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Exceptions
{
    public class OverQtyLimitationException : DomainException
    {
        public OverQtyLimitationException(int amount, string errorMessage = null, Exception innerException = null)
            : base(nameof(Inventory), InventoryErrorCode.OverUpperBound,
                errorMessage ?? "Qty is over upper bound", innerException)
        {
            this.Data.Add("Amount", amount);
        }
    }
}