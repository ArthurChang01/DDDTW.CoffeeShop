using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Exceptions
{
    public class StatusTransitionException : Exception
    {
        private readonly OrderErrorCode errorCode = OrderErrorCode.StatusTransit;
        private readonly string defaultErrorMessage;

        public StatusTransitionException(OrderStatus curStatus, OrderStatus targetStatus, string errorMessage = "", Exception inner = null)
            : base(errorMessage, inner)
        {
            this.defaultErrorMessage = string.IsNullOrEmpty(errorMessage) ?
                $"Can not transit order status from {curStatus} to {targetStatus}" :
                errorMessage;
        }

        public override string Message => $"Code: {this.errorCode}, Message: {this.defaultErrorMessage}";
    }
}