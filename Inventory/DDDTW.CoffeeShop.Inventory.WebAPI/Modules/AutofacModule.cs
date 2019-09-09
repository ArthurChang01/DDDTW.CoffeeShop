using Autofac;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.ViewModels;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DomainServices;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.Factories;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.Repositories;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventory.WebAPI.Modules
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IdTranslator>().As<ITranslator<InventoryId, string>>();
            builder.RegisterType<InventoryItemsTranslator>()
                .As<ITranslator<InventoryItem, InventoryItemVM>>();
            builder.RegisterType<InventoryConstrainsTranslator>()
                .As<ITranslator<IEnumerable<InventoryConstraint>, IEnumerable<InventoryConstraintVM>>>();

            builder.RegisterType<InventoryVMTranslator>().As<ITranslator<InventoryVM, Domain.Inventories.Models.Inventory>>();
            builder.RegisterType<InventoryFactory>().As<IInventoryFactory>();
            builder.RegisterType<InventoryRepository>().As<IInventoryRepository>().SingleInstance();
        }
    }
}