using System;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.Exceptions
{
    public class OrderIdIsNullException : Exception
    {
        private readonly OrderErrorCode errorCode = OrderErrorCode.OrderIdIsNull;
        private readonly string defaultErrorMessage = "Order Id can not be null";

        public OrderIdIsNullException(string errorMessage = null, Exception inner = null)
            : base(errorMessage, inner)
        {
            this.defaultErrorMessage = errorMessage ?? this.defaultErrorMessage;
        }

        public override string Message => $"Code: {this.errorCode}, Message: {this.defaultErrorMessage}";
    }
}