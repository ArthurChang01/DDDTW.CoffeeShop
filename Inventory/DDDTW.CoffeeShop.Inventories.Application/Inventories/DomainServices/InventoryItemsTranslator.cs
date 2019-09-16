using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Responses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.DomainServices
{
    public class InventoryItemsTranslator : ITranslator<InventoryItem, InventoryItemResp>
    {
        public InventoryItem Translate(InventoryItemResp input)
        {
            return new InventoryItem(input.Name, input.SKU, input.Price, input.Manufacturer, input.ItemCategory,
                input.InboundUnitName, input.Capacity);
        }
    }
}