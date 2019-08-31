using System;
using System.Collections.Generic;
using System.Linq;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Infrastructures;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;

using Models = DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.Repositories
{
    public class InventoryRepository : ESRepositoryBase<Models.Inventory, InventoryId>, IInventoryRepository
    {
        public InventoryId GenerateInventoryId()
        {
            return new InventoryId(base.Count(), DateTimeOffset.Now);
        }

        public Models.Inventory GetBy(InventoryId id)
        {
            return base.Get(id);
        }

        public IEnumerable<Models.Inventory> Get(Specification<Models.Inventory> spec = null, int pageNo = 1, int pageSize = 5)
        {
            return base.Get(s => s, spec).Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public void Save(Models.Inventory inventory, IEnumerable<IDomainEvent> events)
        {
            base.Append(inventory, events);
        }
    }
}