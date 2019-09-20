using System;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;

namespace DDDTW.CoffeeShop.Orders.Domain.Products.Exceptions
{
    public class PriceInappropriateException : DomainException
    {
        public PriceInappropriateException(string errorMsg = null, Exception inner = null)
            : base(nameof(Product), OrderErrorCode.PriceIsInappropriate, 
                errorMsg ?? "Price should bigger than 0", inner)
        {
        }
    }
}