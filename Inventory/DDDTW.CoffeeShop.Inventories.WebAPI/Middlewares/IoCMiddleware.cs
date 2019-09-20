using Autofac;
using Autofac.Extensions.DependencyInjection;
using DDDTW.CoffeeShop.Inventories.Application;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DDDTW.CoffeeShop.Inventories.WebAPI.Middlewares
{
    public static class IoCMiddleware
    {
        public static IServiceProvider SetIoCService(this IServiceCollection services, IConfiguration config)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.AddMediatR(typeof(InventoryApplication).Assembly);

            containerBuilder.RegisterModule(new InventoryApplication(config));
            containerBuilder.Populate(services);

            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}