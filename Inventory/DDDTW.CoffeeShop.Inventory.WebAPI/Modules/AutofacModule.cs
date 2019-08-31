using Autofac;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.ViewModels;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DomainServices;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.Repositories;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;

namespace DDDTW.CoffeeShop.Inventory.WebAPI.Modules
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IdTranslator>().As<ITranslator<InventoryId, string>>();
            builder.RegisterType<InventoryVMTranslator>().As<InventoryVM, Domain.Inventories.Models.Inventory>();
            builder.RegisterType<InventoryRepository>().As<IInventoryRepository>().SingleInstance();
        }
    }
}