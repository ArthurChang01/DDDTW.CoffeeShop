using Autofac;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Infrastructures.EventSourcings;
using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Results;
using DDDTW.CoffeeShop.Orders.Application.Orders.DomainServices;
using DDDTW.CoffeeShop.Orders.Application.Orders.Repositories;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Application
{
    public static class OrderApplication
    {
        public static void Load(ContainerBuilder builder, IConfiguration config)
        {
            builder.RegisterType<IdTranslator>().As<ITranslator<OrderId, string>>();
            builder.RegisterType<OrderItemsTranslator>().As<ITranslator<IEnumerable<OrderItem>, IEnumerable<OrderItemRst>>>();
            builder.RegisterType<ESRepositoryBase<Order, OrderId>>().As<IRepository<Order, OrderId>>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
        }
    }
}