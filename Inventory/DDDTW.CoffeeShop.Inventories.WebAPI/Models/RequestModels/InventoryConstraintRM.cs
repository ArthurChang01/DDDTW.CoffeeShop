using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System;

namespace DDDTW.CoffeeShop.Inventories.WebAPI.Models.RequestModels
{
    public class InventoryConstraintRM
    {
        public InventoryConstraintType Type { get; set; }

        public string Value { get; set; }

        public TypeCode DataTypeOfValue { get; set; }
    }
}