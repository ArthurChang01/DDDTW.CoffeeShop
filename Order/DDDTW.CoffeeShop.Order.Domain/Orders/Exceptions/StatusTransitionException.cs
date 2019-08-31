using System;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.Exceptions
{
    public class StatusTransitionException : Exception
    {
        private readonly OrderErrorCode errorCode = OrderErrorCode.StatusTransit;
        private readonly string defaultErrorMessage;

        public StatusTransitionException(OrderStatus origStatus, OrderStatus targetStatus, string errorMessage = "", Exception inner = null)
            : base(errorMessage, inner)
        {
            this.defaultErrorMessage = string.IsNullOrEmpty(errorMessage) ?
                $"Can not transit order status from {origStatus} to {targetStatus}" :
                errorMessage;
        }

        public override string Message => $"Code: {this.errorCode}, Message: {this.defaultErrorMessage}";
    }
}