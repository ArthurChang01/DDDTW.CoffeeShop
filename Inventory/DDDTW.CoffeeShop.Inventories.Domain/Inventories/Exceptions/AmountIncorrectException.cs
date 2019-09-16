using System;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Exceptions
{
    public class AmountIncorrectException : Exception
    {
        private readonly InventoryErrorCode errorCode = InventoryErrorCode.AmountIsNegative;
        private readonly string defaultErrorMessage = "Input parameter: Amount can not be negative digital";

        public AmountIncorrectException(string errorMessage = null, Exception inner = null)
            : base(errorMessage, inner)
        {
            this.defaultErrorMessage = errorMessage ?? this.defaultErrorMessage;
        }

        #region Overrides of Exception

        public override string Message => $"ErrorCode:{this.errorCode}, Message: {this.defaultErrorMessage}";

        #endregion Overrides of Exception
    }
}