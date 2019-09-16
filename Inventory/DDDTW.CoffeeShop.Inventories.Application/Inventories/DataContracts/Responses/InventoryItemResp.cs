using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Responses
{
    public class InventoryItemResp : PropertyComparer<InventoryItemResp>
    {
        #region Constructors

        public InventoryItemResp()
        {
        }

        public InventoryItemResp(InventoryItem item)
        {
            this.Name = item.Name;
            this.SKU = item.SKU;
            this.Price = item.Price;
            this.Manufacturer = item.Manufacturer;
            this.ItemCategory = item.ItemCategory;
            this.InboundUnitName = item.InboundUnitName;
            this.Capacity = item.Capacity;
        }

        public InventoryItemResp(string name, string sku, decimal price, string manufacturer, ItemCategory category, string unitName, int capacity)
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

        #region Properties

        public string Name { get; private set; }

        public string SKU { get; private set; }

        public decimal Price { get; private set; }

        public string Manufacturer { get; private set; }

        public ItemCategory ItemCategory { get; private set; }

        public string InboundUnitName { get; private set; }

        public int Capacity { get; private set; }

        #endregion Properties

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