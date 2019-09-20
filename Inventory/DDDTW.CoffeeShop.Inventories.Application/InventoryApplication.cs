using Autofac;
using DDDTW.CoffeeShop.Inventories.Application.Inventories;
using Microsoft.Extensions.Configuration;

namespace DDDTW.CoffeeShop.Inventories.Application
{
    public class InventoryApplication : Module
    {
        private readonly IConfiguration config;

        public InventoryApplication(IConfiguration config)
        {
            this.config = config;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new InventoryModule(config));
        }
    }
}