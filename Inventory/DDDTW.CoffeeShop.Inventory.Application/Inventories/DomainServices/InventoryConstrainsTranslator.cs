using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.ViewModels;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.DomainServices
{
    public class InventoryConstrainsTranslator : ITranslator<IEnumerable<InventoryConstraint>, IEnumerable<InventoryConstraintVM>>
    {
        public IEnumerable<InventoryConstraint> Translate(IEnumerable<InventoryConstraintVM> input)
        {
            return input.Select(o => new InventoryConstraint(
                (InventoryConstraintType)Enum.Parse(typeof(InventoryConstraintType), o.Type),
                o.Value,
                (InventoryConstraintValueType)Enum.Parse(typeof(InventoryConstraintValueType), o.DataTypeOfValue)));
        }
    }
}