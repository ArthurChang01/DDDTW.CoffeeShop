using Autofac;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Infrastructures.Repositories.EventSourcings;
using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Results;
using DDDTW.CoffeeShop.Orders.Application.Orders.DomainServices;
using DDDTW.CoffeeShop.Orders.Application.Orders.Repositories;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Application.Orders
{
    public class OrderModule : Module
    {
        private readonly IConfiguration config;

        public OrderModule(IConfiguration config)
        {
            this.config = config;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IdTranslator>().As<ITranslator<OrderId, string>>();
            builder.RegisterType<OrderItemsTranslator>().As<ITranslator<IEnumerable<OrderItem>, IEnumerable<OrderItemRst>>>();
            builder.Register(ctx =>
                {
                    var esConfig = ctx.Resolve<IOptionsMonitor<ESConfig>>().CurrentValue;
                    return new ESRepositoryBase<Order, OrderId>(esConfig);
                })
                .As<IRepository<Order, OrderId>>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
        }
    }
}