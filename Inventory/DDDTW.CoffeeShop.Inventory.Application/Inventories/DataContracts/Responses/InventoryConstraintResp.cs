using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;
using System;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Responses
{
    public class InventoryConstraintResp : PropertyComparer<InventoryConstraintResp>
    {
        #region Constructors

        public InventoryConstraintResp()
        {
        }

        public InventoryConstraintResp(InventoryConstraint constraint)
        {
            this.Type = constraint.Type.ToString();
            this.Value = constraint.Value;
            this.DataTypeOfValue = constraint.DataTypeOfValue.ToString();
        }

        public InventoryConstraintResp(InventoryConstraintType type, string value, TypeCode typeOfValue)
        {
            this.Type = type.ToString();
            this.Value = value;
            this.DataTypeOfValue = typeOfValue.ToString();
        }

        #endregion Constructors

        #region Properties

        public string Type { get; private set; }

        public string Value { get; private set; }

        public string DataTypeOfValue { get; private set; }

        #endregion Properties

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