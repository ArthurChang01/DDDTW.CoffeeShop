using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Infrastructures.EventSourcings;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.Repositories
{
    public class InventoryRepository : ESRepositoryBase<Inventory, InventoryId>, IInventoryRepository
    {
        public InventoryRepository(IInventoryFactory factory)
            : base(factory)
        {
        }

        public InventoryId GenerateInventoryId()
        {
            return new InventoryId(base.Count(), DateTimeOffset.Now);
        }

        public Inventory GetBy(InventoryId id)
        {
            return base.Get(id);
        }

        public IEnumerable<Inventory> Get(Specification<Inventory> spec = null, int pageNo = 1, int pageSize = 5)
        {
            return base.Get(s => s, spec).Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public void Save(Inventory inventory)
        {
            base.Append(inventory);
        }
    }
}