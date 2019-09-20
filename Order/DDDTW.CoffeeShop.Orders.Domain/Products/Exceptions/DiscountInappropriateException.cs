using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;
using System;

namespace DDDTW.CoffeeShop.Orders.Domain.Products.Exceptions
{
    public class DiscountInappropriateException : DomainException
    {
        public DiscountInappropriateException(string errorMsg = null, Exception inner = null)
            : base(nameof(Product), OrderErrorCode.ProductDiscountIsInappropriate,
                errorMsg ?? "Discount should not be less than 0", inner)
        {
        }
    }
}