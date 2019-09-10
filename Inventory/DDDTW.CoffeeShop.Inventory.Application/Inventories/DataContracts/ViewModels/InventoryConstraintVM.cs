using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;
using System;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.ViewModels
{
    public class InventoryConstraintVM : ValueObject<InventoryConstraintVM>
    {
        public InventoryConstraintVM()
        {
        }

        public InventoryConstraintVM(InventoryConstraint constraint)
        {
            this.Type = constraint.Type.ToString();
            this.Value = constraint.Value;
            this.DataTypeOfValue = constraint.DataTypeOfValue.ToString();
        }

        public InventoryConstraintVM(InventoryConstraintType type, string value, TypeCode typeOfValue)
        {
            this.Type = type.ToString();
            this.Value = value;
            this.DataTypeOfValue = typeOfValue.ToString();
        }

        public string Type { get; set; }

        public string Value { get; set; }

        public string DataTypeOfValue { get; set; }

        #region Overrides of ValueObject<InventoryConstraintVM>

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Type;
            yield return this.Value;
            yield return this.DataTypeOfValue;
        }

        #endregion Overrides of ValueObject<InventoryConstraintVM>
    }
}