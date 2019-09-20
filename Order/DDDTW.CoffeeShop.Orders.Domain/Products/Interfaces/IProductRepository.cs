using DDDTW.CoffeeShop.Orders.Domain.Products.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Orders.Domain.Products.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductId> GenerateProductId();

        Task<IEnumerable<Product>> Get(int pageNo, int pageSize);

        Task<Product> GetBy(ProductId id);

        Task Save(Product product);
    }
}