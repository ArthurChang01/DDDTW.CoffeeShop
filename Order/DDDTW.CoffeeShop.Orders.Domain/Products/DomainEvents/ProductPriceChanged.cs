using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;
using System;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Domain.Products.DomainEvents
{
    public class ProductPriceChanged : DomainEvent<ProductId>
    {
        public ProductPriceChanged(ProductId id, decimal price, decimal discount, DateTimeOffset modifiedDate)
        {
            this.EntityId = id;
            this.Price = price;
            this.Discount = discount;
            this.ModifiedDate = modifiedDate;
        }

        public override ProductId EntityId { get; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public DateTimeOffset ModifiedDate { get; set; }

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            yield return this.EntityId;
            yield return this.Price;
            yield return this.Discount;
            yield return this.ModifiedDate;
        }
    }
}