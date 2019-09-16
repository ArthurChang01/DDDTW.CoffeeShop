using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Responses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.DomainServices
{
    public class InventoryConstrainsTranslator : ITranslator<IEnumerable<InventoryConstraint>, IEnumerable<InventoryConstraintResp>>
    {
        public IEnumerable<InventoryConstraint> Translate(IEnumerable<InventoryConstraintResp> input)
        {
            return input.Select(o => new InventoryConstraint(
                (InventoryConstraintType)Enum.Parse(typeof(InventoryConstraintType), o.Type),
                o.Value,
                (TypeCode)Enum.Parse(typeof(TypeCode), o.DataTypeOfValue)));
        }
    }
}