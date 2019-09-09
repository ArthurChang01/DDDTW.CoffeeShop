using System;

namespace DDDTW.CoffeeShop.Inventory.Domain.Inventories.Exceptions
{
    public class ConstraintValueIncorrectException : Exception
    {
        private readonly InventoryErrorCode errorCode = InventoryErrorCode.ConstraintValueIncorrect;
        private readonly string defaultErrorMessage = "Constraint value and value type is not matched";

        public ConstraintValueIncorrectException(string errorMessage = null, Exception inner = null)
            : base(errorMessage, inner)
        {
            this.defaultErrorMessage = errorMessage ?? this.defaultErrorMessage;
        }

        public override string Message => $"Code: {this.errorCode}, Message: {this.defaultErrorMessage}";
    }
}