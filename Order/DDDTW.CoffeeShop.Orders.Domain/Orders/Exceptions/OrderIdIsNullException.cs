using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using System;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Exceptions
{
    public class OrderIdIsNullException : DomainException
    {
        public OrderIdIsNullException(string errorMsg = null, Exception inner = null)
            : base("Order", OrderErrorCode.OrderIdIsNull,
                errorMsg ?? "Order Id can not be null", inner)
        {
        }
    }
}