using System.Collections.Generic;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.ViewModels
{
    public class InventoryVM : ValueObject<InventoryVM>
    {
        public string Id { get; set; }

        public InventoryItem Item { get; set; }

        public IEnumerable<InventoryConstraint> Constraints { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Id;
            yield return this.Item;
            foreach (var constraint in this.Constraints)
            {
                yield return constraint;
            }
        }
    }
}