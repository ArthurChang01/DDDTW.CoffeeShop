using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.ViewModels;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.DomainServices
{
    public class InventoryVMTranslator : ITranslator<InventoryVM, Domain.Inventories.Models.Inventory>
    {
        public InventoryVM Translate(Domain.Inventories.Models.Inventory input)
        {
            return new InventoryVM()
            {
                Id = input.Id?.ToString() ?? string.Empty,
                Item = input.Item,
                Constraints = input.Constraint
            };
        }
    }
}