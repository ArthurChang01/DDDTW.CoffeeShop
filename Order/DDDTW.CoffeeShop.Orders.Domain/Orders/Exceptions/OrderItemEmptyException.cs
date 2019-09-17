using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using System;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Exceptions
{
    public class OrderItemEmptyException : DomainException
    {
        public OrderItemEmptyException(string errorMessage = null, Exception inner = null)
            : base("Order", OrderErrorCode.OrderItemsAreEmptyOrNull,
                errorMessage ?? "OrderItem can not be empty or null", inner)
        {
        }
    }
}