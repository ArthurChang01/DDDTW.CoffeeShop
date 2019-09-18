using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Exceptions
{
    public class StatusTransitionException : DomainException
    {
        public StatusTransitionException(OrderStatus curStatus, OrderStatus targetStatus, string errorMessage = null, Exception inner = null)
            : base(nameof(Order), OrderErrorCode.StatusTransit,
                errorMessage ?? $"Can not transit order status from {curStatus} to {targetStatus}", inner)
        {
        }
    }
}