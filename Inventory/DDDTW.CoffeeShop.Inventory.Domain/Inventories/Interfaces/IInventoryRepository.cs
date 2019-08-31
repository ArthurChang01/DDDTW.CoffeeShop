using System.Collections.Generic;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;

namespace DDDTW.CoffeeShop.Inventory.Domain.Inventories.Interfaces
{
    public interface IInventoryRepository
    {
        InventoryId GenerateInventoryId();

        Models.Inventory GetBy(InventoryId id);

        IEnumerable<Models.Inventory> Get(Specification<Models.Inventory> spec, int pageNo = 1, int pageSize = 5);

        void Save(Models.Inventory inventory, IEnumerable<IDomainEvent> events);
    }
}