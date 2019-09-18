using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IRepository<Order, OrderId> repository;

        public OrderRepository(IRepository<Order, OrderId> repository)
        {
            this.repository = repository;
        }

        public async Task<OrderId> GenerateOrderId()
        {
            return new OrderId(await this.repository.Count(), DateTimeOffset.Now);
        }

        public async Task<IEnumerable<Order>> Get(Specification<Order> specification, int pageNo, int pageSize)
        {
            return (await this.repository.Get(s => s, specification))
                .Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public Task<Order> GetBy(OrderId id)
        {
            return this.repository.Get(id);
        }

        public Task Save(Order order)
        {
            return this.repository.Create(order);
        }
    }
}