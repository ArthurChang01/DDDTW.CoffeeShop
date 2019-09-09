using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.ViewModels
{
    public class InventoryItemVM : ValueObject<InventoryItemVM>
    {
        #region Constructors

        public InventoryItemVM()
        {
        }

        public InventoryItemVM(InventoryItem item)
        {
            this.Name = item.Name;
            this.SKU = item.SKU;
            this.Price = item.Price;
            this.Manufacturer = item.Manufacturer;
            this.ItemCategory = item.ItemCategory;
            this.InboundUnitName = item.InboundUnitName;
            this.Capacity = item.Capacity;
        }

        public InventoryItemVM(string name, string sku, decimal price, string manufacturer, ItemCategory category, string unitName, int capacity)
        {
            this.Name = name;
            this.SKU = sku;
            this.Price = price;
            this.Manufacturer = manufacturer;
            this.ItemCategory = category;
            this.InboundUnitName = unitName;
            this.Capacity = capacity;
        }

        #endregion Constructors

        public string Name { get; set; }

        public string SKU { get; set; }

        public decimal Price { get; set; }

        public string Manufacturer { get; set; }

        public ItemCategory ItemCategory { get; set; }

        public string InboundUnitName { get; set; }

        public int Capacity { get; set; }

        #region Overrides of ValueObject<InventoryItemVM>

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Name;
            yield return this.SKU;
            yield return this.Price;
            yield return this.Manufacturer;
            yield return this.ItemCategory;
            yield return this.InboundUnitName;
            yield return this.Capacity;
        }

        #endregion Overrides of ValueObject<InventoryItemVM>
    }
}