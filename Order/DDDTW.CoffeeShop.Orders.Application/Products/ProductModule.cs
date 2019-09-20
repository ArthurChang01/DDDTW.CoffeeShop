using Autofac;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Infrastructures.Repositories.Mongos;
using DDDTW.CoffeeShop.Orders.Application.Products.DomainService;
using DDDTW.CoffeeShop.Orders.Application.Products.Repositories;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using DDDTW.CoffeeShop.Orders.Domain.Products.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DDDTW.CoffeeShop.Orders.Application.Products
{
    public class ProductModule : Module
    {
        private readonly IConfiguration config;

        public ProductModule(IConfiguration config)
        {
            this.config = config;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IdTranslator>().As<ITranslator<ProductId, string>>();
            builder.RegisterType<ProductDbMapper>().As<IMongoClassMappingRegister<Product>>();

            builder.Register(ctx =>
                {
                    var mongoConfig = ctx.Resolve<IOptionsMonitor<MongoConfig>>().CurrentValue;
                    mongoConfig.DatabaseName = $"{nameof(Order)}s";
                    mongoConfig.CollectionName = nameof(Product);

                    var mapper = ctx.Resolve<IMongoClassMappingRegister<Product>>();
                    return new MongoRepositoryBase<Product, ProductId>(mongoConfig, mapper);
                })
                .As<IRepository<Product, ProductId>>();

            builder.RegisterType<ProductRepository>().As<IProductRepository>();
        }
    }
}