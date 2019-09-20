using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;
using System;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Domain.Products.DomainEvents
{
    public class ProductDescriptionChanged : DomainEvent<ProductId>
    {
        public ProductDescriptionChanged(ProductId id, string description, DateTimeOffset modifiedDate)
        {
            this.EntityId = id;
            this.Description = description;
            this.ModifiedDate = modifiedDate;
        }

        public override ProductId EntityId { get; }

        public string Description { get; private set; }

        public DateTimeOffset ModifiedDate { get; private set; }

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            yield return this.EntityId;
            yield return this.Description;
            yield return this.ModifiedDate;
        }
    }
}