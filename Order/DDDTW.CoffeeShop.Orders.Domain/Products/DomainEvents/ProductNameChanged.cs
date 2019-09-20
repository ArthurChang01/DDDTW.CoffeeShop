using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;
using System;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Domain.Products.DomainEvents
{
    public class ProductNameChanged : DomainEvent<ProductId>
    {
        public ProductNameChanged(ProductId id, string name, DateTimeOffset modifiedDate)
        {
            this.EntityId = id;
            this.Name = name;
            this.ModifiedDate = modifiedDate;
        }

        public override ProductId EntityId { get; }

        public string Name { get; private set; }

        public DateTimeOffset ModifiedDate { get; private set; }

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            yield return this.EntityId;
            yield return this.Name;
            yield return this.ModifiedDate;
        }
    }
}