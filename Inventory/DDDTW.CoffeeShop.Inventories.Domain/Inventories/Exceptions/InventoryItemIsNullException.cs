using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Exceptions
{
    public class InventoryItemIsNullException : DomainException
    {
        public InventoryItemIsNullException(string errorMessage = null, Exception innerException = null)
            : base(nameof(Inventory), InventoryErrorCode.InventoryItemIsNull,
                errorMessage ?? "Inventory item can not be null", innerException)
        {
        }
    }
}