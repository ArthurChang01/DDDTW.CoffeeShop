using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Exceptions
{
    public class NegativeQtyException : DomainException
    {
        public NegativeQtyException(int qty, string errorMessage = null, Exception inner = null)
            : base(nameof(Inventory), InventoryErrorCode.QtyIsNegative,
                errorMessage ?? "Qty can not be negative digital", inner)
        {
            this.Data.Add("Qty", qty);
        }
    }
}