﻿using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;

namespace DDDTW.CoffeeShop.Inventory.WebAPI.Models.RequestModels
{
    public class InventoryItemRM
    {
        public string Name { get; set; }

        public string SKU { get; set; }

        public decimal Price { get; set; }

        public string Manufacturer { get; set; }

        public ItemCategory ItemCategory { get; set; }

        public string InboundUnitName { get; set; }

        public int Capacity { get; set; }
    }
}