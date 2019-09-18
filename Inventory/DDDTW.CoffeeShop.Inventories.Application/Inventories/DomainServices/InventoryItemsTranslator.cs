using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Results;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.DomainServices
{
    public class InventoryItemsTranslator : ITranslator<InventoryItem, InventoryItemRst>
    {
        public InventoryItem Translate(InventoryItemRst input)
        {
            return new InventoryItem(input.Name, input.SKU, input.Price, input.Manufacturer, input.ItemCategory,
                input.InboundUnitName, input.Capacity);
        }
    }
}