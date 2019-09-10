using Autofac;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.ViewModels;
using DDDTW.CoffeeShop.Order.Application.Orders.DomainServices;
using DDDTW.CoffeeShop.Order.Application.Orders.Factories;
using DDDTW.CoffeeShop.Order.Application.Orders.Repositories;
using DDDTW.CoffeeShop.Order.Domain.Orders.Interfaces;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using Microsoft.Extensions.Configuration;

namespace DDDTW.CoffeeShop.Order.Application
{
    public static class OrderApplication
    {
        public static void Load(ContainerBuilder builder, IConfiguration config)
        {
            builder.RegisterType<IdTranslator>().As<ITranslator<OrderId, string>>();
            builder.RegisterType<OrderVMTranslator>().As<ITranslator<OrderVM, Domain.Orders.Models.Order>>();
            builder.RegisterType<OrderFactory>().As<IOrderFactory>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
        }
    }
}