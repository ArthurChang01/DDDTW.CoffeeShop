using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.ViewModels
{
    public class InventoryVM : ValueObject<InventoryVM>
    {
        public string Id { get; set; }

        public int Qty { get; set; }

        public InventoryItemVM Item { get; set; }

        public IEnumerable<InventoryConstraintVM> Constraints { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Id;
            yield return this.Qty;
            yield return this.Item;
            foreach (var constraint in this.Constraints)
            {
                yield return constraint;
            }
        }
    }
}