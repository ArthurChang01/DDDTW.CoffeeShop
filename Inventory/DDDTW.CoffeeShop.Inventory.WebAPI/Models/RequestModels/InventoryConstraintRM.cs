using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;
using System;

namespace DDDTW.CoffeeShop.Inventory.WebAPI.Models.RequestModels
{
    public class InventoryConstraintRM
    {
        public InventoryConstraintType Type { get; set; }

        public string Value { get; set; }

        public TypeCode DataTypeOfValue { get; set; }
    }
}