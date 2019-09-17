using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Exceptions
{
    public class InventoryShortageException : DomainException
    {
        public InventoryShortageException(int amount, string errorMessage = "", Exception innerException = null)
            : base(nameof(Inventory), InventoryErrorCode.InventoryShortage,
                errorMessage ?? "Inventory is insufficient", innerException)
        {
            this.Data.Add("Amount", amount);
        }
    }
}