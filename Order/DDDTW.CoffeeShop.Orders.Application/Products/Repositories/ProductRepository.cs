using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Products.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Orders.Application.Products.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IRepository<Product, ProductId> repository;

        public ProductRepository(IRepository<Product, ProductId> repository)
        {
            this.repository = repository;
        }

        public async Task<ProductId> GenerateProductId()
        {
            return new ProductId(await this.repository.Count());
        }

        public async Task<IEnumerable<Product>> Get(int pageNo, int pageSize)
        {
            return (await this.repository.Get(s => s, new Specification<Product>(s => true)))
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public async Task<Product> GetBy(ProductId id)
        {
            return await this.repository.First(s => s, new Specification<Product>(o => o.Id.Equals(id)));
        }

        public Task Save(Product product)
        {
            return this.repository.Update(product);
        }
    }
}