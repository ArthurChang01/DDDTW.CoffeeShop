using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.ViewModels;
using System.Linq;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.DomainServices
{
    public class InventoryVMTranslator : ITranslator<InventoryVM, Domain.Inventories.Models.Inventory>
    {
        public InventoryVM Translate(Domain.Inventories.Models.Inventory input)
        {
            var item = new InventoryItemVM(input.Item);
            var constraints = input.Constraint.Select(o => new InventoryConstraintVM(o));

            return new InventoryVM()
            {
                Id = input.Id?.ToString() ?? string.Empty,
                Qty = input.Qty,
                Item = item,
                Constraints = constraints
            };
        }
    }
}