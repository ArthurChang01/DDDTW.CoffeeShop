using Autofac;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Responses;
using DDDTW.CoffeeShop.Orders.Application.Orders.DomainServices;
using DDDTW.CoffeeShop.Orders.Application.Orders.Factories;
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
            builder.RegisterType<OrderItemsTranslator>().As<ITranslator<IEnumerable<OrderItem>, IEnumerable<OrderItemResp>>>();
            builder.RegisterType<OrderFactory>().As<IOrderFactory>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
        }
    }
}