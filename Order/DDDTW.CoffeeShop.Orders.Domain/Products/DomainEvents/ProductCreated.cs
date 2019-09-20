using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;
using System;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Domain.Products.DomainEvents
{
    public class ProductCreated : DomainEvent<ProductId>
    {
        public ProductCreated(ProductId id, string name, string description, decimal price, decimal discount, DateTimeOffset createdDate, DateTimeOffset? modifiedDate)
        {
            this.EntityId = id;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Discount = discount;
            this.CreatedDate = createdDate;
            this.ModifiedDate = modifiedDate;
        }

        public override ProductId EntityId { get; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            yield return this.EntityId;
            yield return this.Name;
            yield return this.Description;
            yield return this.Price;
            yield return this.Discount;
            yield return this.CreatedDate;
            yield return this.ModifiedDate;
        }
    }
}