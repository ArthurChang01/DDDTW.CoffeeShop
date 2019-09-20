using Autofac;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Infrastructures.Repositories.EventSourcings;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Results;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DomainServices;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.Repositories;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories
{
    internal class InventoryModule : Module
    {
        private readonly IConfiguration config;

        public InventoryModule(IConfiguration config)
        {
            this.config = config;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IdTranslator>().As<ITranslator<InventoryId, string>>();
            builder.RegisterType<InventoryItemsTranslator>()
                .As<ITranslator<InventoryItem, InventoryItemRst>>();
            builder.RegisterType<InventoryConstrainsTranslator>()
                .As<ITranslator<IEnumerable<InventoryConstraint>, IEnumerable<InventoryConstraintRst>>>();

            builder.Register(ctx =>
                {
                    var esConfig = ctx.Resolve<IOptionsMonitor<ESConfig>>().CurrentValue;
                    return new ESRepositoryBase<Inventory, InventoryId>(esConfig);
                })
                .As<IRepository<Inventory, InventoryId>>();
            builder.RegisterType<InventoryRepository>().As<IInventoryRepository>();
        }
    }
}