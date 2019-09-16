using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Interfaces
{
    public interface IInventoryFactory : IFactory<Models.Inventory, InventoryId>
    {
    }
}