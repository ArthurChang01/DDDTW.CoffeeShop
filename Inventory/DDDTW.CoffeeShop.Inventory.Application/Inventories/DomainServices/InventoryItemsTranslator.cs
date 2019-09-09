using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.ViewModels;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.DomainServices
{
    public class InventoryItemsTranslator : ITranslator<InventoryItem, InventoryItemVM>
    {
        public InventoryItem Translate(InventoryItemVM input)
        {
            return new InventoryItem(input.Name, input.SKU, input.Price, input.Manufacturer, input.ItemCategory,
                input.InboundUnitName, input.Capacity);
        }
    }
}