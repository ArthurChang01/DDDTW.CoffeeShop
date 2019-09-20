using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;
using System;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Domain.Products.DomainEvents
{
    public class ProductDisabled : DomainEvent<ProductId>
    {
        public ProductDisabled(ProductId id, DateTimeOffset modifiedDate)
        {
            this.EntityId = id;
            this.ModifiedDate = modifiedDate;
        }

        public override ProductId EntityId { get; }

        public DateTimeOffset ModifiedDate { get; set; }

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            yield return this.EntityId;
            yield return this.ModifiedDate;
        }
    }
}