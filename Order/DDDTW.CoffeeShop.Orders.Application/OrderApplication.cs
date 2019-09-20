using Autofac;
using DDDTW.CoffeeShop.Orders.Application.Orders;
using DDDTW.CoffeeShop.Orders.Application.Products;
using Microsoft.Extensions.Configuration;

namespace DDDTW.CoffeeShop.Orders.Application
{
    public static class OrderApplication
    {
        public static void Load(ContainerBuilder builder, IConfiguration config)
        {
            builder.RegisterModule(new OrderModule(config));
            builder.RegisterModule(new ProductModule(config));
        }
    }
}