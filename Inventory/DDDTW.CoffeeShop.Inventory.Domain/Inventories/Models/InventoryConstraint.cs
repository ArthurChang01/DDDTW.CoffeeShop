using System.Collections.Generic;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;

namespace DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models
{
    public class InventoryConstraint : ValueObject<InventoryConstraint>
    {
        #region Constructors

        public InventoryConstraint()
        {
        }

        public InventoryConstraint(InventoryConstraintType type, string value, string valueType)
        {
            this.Type = type;
            this.Value = value;
            this.DataTypeOfValue = valueType;
        }

        #endregion Constructors

        #region Properties

        public InventoryConstraintType Type { get; private set; }

        public string Value { get; private set; }

        public string DataTypeOfValue { get; private set; }

        #endregion Properties

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Type;
            yield return this.Value;
            yield return this.DataTypeOfValue;
        }
    }
}