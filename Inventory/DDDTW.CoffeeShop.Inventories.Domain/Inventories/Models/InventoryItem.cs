using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models
{
    public class InventoryItem : ValueObject<InventoryItem>
    {
        private readonly string inboundUnitName;

        #region Constructors

        public InventoryItem()
        {
            this.Name = string.Empty;
            this.SKU = string.Empty;
            this.Price = 0;
            this.Manufacturer = string.Empty;
            this.ItemCategory = ItemCategory.Milk;
            this.InboundUnitName = string.Empty;
            this.Capacity = 0;
        }

        public InventoryItem(string name, string sku, decimal price, string manufacturer, ItemCategory itemCategory, string inboundUnitName, int capacity)
        {
            this.inboundUnitName = inboundUnitName;
            this.Name = name;
            this.SKU = sku;
            this.Price = price;
            this.Manufacturer = manufacturer;
            this.ItemCategory = itemCategory;
            this.InboundUnitName = inboundUnitName;
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
    }
}