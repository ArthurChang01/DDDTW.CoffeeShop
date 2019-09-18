using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> Get(Specification<Models.Order> specification, int pageNo, int pageSize);

        Task<OrderId> GenerateOrderId();

        Task<Order> GetBy(OrderId id);

        Task Save(Order order);
    }
}