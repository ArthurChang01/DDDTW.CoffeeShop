using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;

namespace DDDTW.CoffeeShop.Inventory.Domain.Inventories.Interfaces
{
    public interface IInventoryFactory : IFactory<Models.Inventory, InventoryId>
    {
    }
}