using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Infrastructures.EventSourcings;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.Repositories
{
    public class OrderRepository : ESRepositoryBase<Order, OrderId>, IOrderRepository
    {
        public OrderRepository(IOrderFactory factory)
            : base(factory)
        {
        }

        public OrderId GenerateOrderId()
        {
            return new OrderId(base.Count(), DateTimeOffset.Now);
        }

        public IEnumerable<Order> Get(Specification<Order> specification, int pageNo, int pageSize)
        {
            return base.Get(s => s, specification)
                .Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public Order GetBy(OrderId id)
        {
            return base.Get(id);
        }

        public void Save(Order order)
        {
            base.Append(order);
        }
    }
}