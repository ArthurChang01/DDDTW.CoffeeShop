using System;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Exceptions
{
    public class OrderItemEmptyException : Exception
    {
        private readonly OrderErrorCode errorCode = OrderErrorCode.OrderItemIsEmptyOrNull;
        private readonly string defaultErrorMessage = "OrderItem can not be empty or null";

        public OrderItemEmptyException(string errorMessage = null, Exception inner = null)
            : base(errorMessage, inner)
        {
            this.defaultErrorMessage = errorMessage ?? this.defaultErrorMessage;
        }

        public override string Message => $"Code: {this.errorCode}, Message: {this.defaultErrorMessage}";
    }
}