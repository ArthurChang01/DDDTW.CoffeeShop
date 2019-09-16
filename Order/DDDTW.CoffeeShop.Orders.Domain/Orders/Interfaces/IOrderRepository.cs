using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Models.Order> Get(Specification<Models.Order> specification, int pageNo, int pageSize);

        OrderId GenerateOrderId();

        Models.Order GetBy(OrderId id);

        void Save(Models.Order order);
    }
}