using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IRepository<Inventory, InventoryId> repository;

        public InventoryRepository(IRepository<Inventory, InventoryId> repository)
        {
            this.repository = repository;
        }

        public async Task<InventoryId> GenerateInventoryId()
        {
            return new InventoryId(await this.repository.Count(), DateTimeOffset.Now);
        }

        public async Task<Inventory> GetBy(InventoryId id)
        {
            return await this.repository.Get(id);
        }

        public async Task<IEnumerable<Inventory>> Get(Specification<Inventory> spec = null, int pageNo = 1, int pageSize = 5)
        {
            return (await this.repository.Get(s => s, spec))
                .Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public Task Save(Inventory inventory)
        {
            return this.repository.Create(inventory);
        }
    }
}