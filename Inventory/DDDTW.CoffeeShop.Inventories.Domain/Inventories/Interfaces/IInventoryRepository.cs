using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Interfaces
{
    public interface IInventoryRepository
    {
        Task<InventoryId> GenerateInventoryId();

        Task<Inventory> GetBy(InventoryId id);

        Task<IEnumerable<Inventory>> Get(Specification<Inventory> spec, int pageNo = 1, int pageSize = 5);

        Task Save(Inventory inventory);
    }
}