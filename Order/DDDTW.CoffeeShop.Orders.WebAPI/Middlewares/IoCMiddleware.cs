using Autofac;
using Autofac.Extensions.DependencyInjection;
using DDDTW.CoffeeShop.Orders.Application;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DDDTW.CoffeeShop.Orders.WebAPI.Middlewares
{
    public static class IoCService
    {
        public static IServiceProvider SetIoCService(this IServiceCollection services, IConfiguration config)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.AddMediatR(typeof(OrderApplication).Assembly);

            OrderApplication.Load(containerBuilder, config);
            containerBuilder.Populate(services);

            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}