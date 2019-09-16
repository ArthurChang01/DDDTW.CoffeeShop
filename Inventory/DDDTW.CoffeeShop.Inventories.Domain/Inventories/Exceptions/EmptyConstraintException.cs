using System;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Exceptions
{
    public class EmptyConstraintException : Exception
    {
        private readonly string errorCode = $"Inv-{(int)InventoryErrorCode.ConstraintIsEmpty}";
        private readonly string defaultErrorMessage = "Constraint is empty";

        public EmptyConstraintException(string errorMessage = "", Exception innerException = null)
            : base(errorMessage, innerException)
        {
            this.defaultErrorMessage =
                string.IsNullOrWhiteSpace(errorMessage) ? this.defaultErrorMessage : errorMessage;
        }

        public override string Message => $"Code: {this.errorCode}, Message: {this.defaultErrorMessage}";
    }
}