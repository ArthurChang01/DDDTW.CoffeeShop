using System;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;

namespace DDDTW.CoffeeShop.Orders.Domain.Products.Exceptions
{
    public class NameIsEmptyException : DomainException
    {
        public NameIsEmptyException(string errorMsg = null, Exception inner = null)
            : base(nameof(Product), OrderErrorCode.ProductNameIsEmpty,
                errorMsg ?? $"Name can not be null or empty", inner)
        {
        }
    }
}