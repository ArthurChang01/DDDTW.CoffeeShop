using System;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;

namespace DDDTW.CoffeeShop.Orders.Domain.Products.Exceptions
{
    public class DescriptionIsEmptyException : DomainException
    {
        public DescriptionIsEmptyException(string errorMsg = null, Exception inner = null)
            : base(nameof(Product), OrderErrorCode.ProductDescriptionIsEmpty,
                errorMsg ?? "Description can not be null or empty", inner)
        {
        }
    }
}