using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Products.Exceptions;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;
using DDDTW.CoffeeShop.Orders.Domain.Products.Specifications;
using System;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Domain.Products.Policies
{
    public static class ProductPricePolicy
    {
        public static void Verify(ProductPrice price)
        {
            var exceptions = new List<DomainException>();
            if (new PriceSpec(price.Price).IsSatisfy() == false)
                exceptions.Add(new PriceInappropriateException());

            if (new DiscountSpec(price.Discount).IsSatisfy() == false)
                exceptions.Add(new DiscountInappropriateException());

            if (exceptions.Count > 0)
                throw new AggregateException(exceptions);
        }
    }
}