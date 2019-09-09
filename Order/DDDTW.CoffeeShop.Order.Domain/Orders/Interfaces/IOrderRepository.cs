using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Models.Order> Get(Specification<Models.Order> specification, int pageNo, int pageSize);

        OrderId GenerateOrderId();

        Models.Order GetBy(OrderId id);

        void Save(Models.Order order, IReadOnlyCollection<IDomainEvent> events);
    }
}