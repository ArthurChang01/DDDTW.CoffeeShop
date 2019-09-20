using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Products.Exceptions;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;
using DDDTW.CoffeeShop.Orders.Domain.Products.Specifications;
using System;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Domain.Products.Policies
{
    internal static class ProductPolicy
    {
        public static void Verify(Product product)
        {
            var exceptions = new List<DomainException>();
            if (new NameSpec(product.Name).IsSatisfy() == false)
                exceptions.Add(new NameIsEmptyException());

            if (new DescriptionSpec(product.Description).IsSatisfy() == false)
                exceptions.Add(new DescriptionIsEmptyException());

            if (exceptions.Count > 0)
                throw new AggregateException(exceptions);
        }
    }
}