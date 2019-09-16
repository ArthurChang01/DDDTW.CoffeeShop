using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Interfaces
{
    public interface IInventoryRepository
    {
        InventoryId GenerateInventoryId();

        Models.Inventory GetBy(InventoryId id);

        IEnumerable<Models.Inventory> Get(Specification<Models.Inventory> spec, int pageNo = 1, int pageSize = 5);

        void Save(Models.Inventory inventory);
    }
}