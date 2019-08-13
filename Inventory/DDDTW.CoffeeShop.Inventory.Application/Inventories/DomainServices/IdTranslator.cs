using System;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.DomainServices
{
    public class IdTranslator : ITranslator<InventoryId, string>
    {
        public InventoryId Translate(string input)
        {
            string[] idString = input.Split('-');
            idString[1] = idString[1].Insert(4, "/").Insert(7, "/");
            return new InventoryId(int.Parse(idString[2]), DateTimeOffset.Parse(idString[1]));
        }
    }
}