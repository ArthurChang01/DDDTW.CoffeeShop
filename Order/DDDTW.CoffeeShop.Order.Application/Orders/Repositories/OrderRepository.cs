using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Infrastructures;
using DDDTW.CoffeeShop.Order.Domain.Orders.Interfaces;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using Models = DDDTW.CoffeeShop.Order.Domain.Orders.Models;

namespace DDDTW.CoffeeShop.Order.Application.Orders.Repositories
{
    public class OrderRepository : ESRepositoryBase<Models.Order, OrderId>, IOrderRepository
    {
        public OrderId GenerateOrderId()
        {
            return new OrderId(base.Count(), DateTimeOffset.Now);
        }

        public IEnumerable<Models.Order> Get(Specification<Models.Order> specification, int pageNo, int pageSize)
        {
            return base.Get(s => s, specification)
                .Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public Models.Order GetBy(OrderId id)
        {
            return base.Get(id);
        }

        public void Save(Models.Order order, IReadOnlyCollection<IDomainEvent> events)
        {
            base.Append(order, events);
        }
    }
}