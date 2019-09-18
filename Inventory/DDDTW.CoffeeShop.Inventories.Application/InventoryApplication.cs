using Autofac;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Infrastructures.EventSourcings;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Results;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DomainServices;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.Repositories;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventories.Application
{
    public class InventoryApplication : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IdTranslator>().As<ITranslator<InventoryId, string>>();
            builder.RegisterType<InventoryItemsTranslator>()
                .As<ITranslator<InventoryItem, InventoryItemRst>>();
            builder.RegisterType<InventoryConstrainsTranslator>()
                .As<ITranslator<IEnumerable<InventoryConstraint>, IEnumerable<InventoryConstraintRst>>>();

            builder.RegisterType<ESRepositoryBase<Inventory, InventoryId>>().As<IRepository<Inventory, InventoryId>>();
            builder.RegisterType<InventoryRepository>().As<IInventoryRepository>();
        }
    }
}