using System;

namespace DDDTW.CoffeeShop.Inventory.Domain.Inventories.Exceptions
{
    public class InventoryItemIsNullException : Exception
    {
        private readonly string errorCode = $"Inv-{(int)InventoryErrorCode.InventoryItemIsNull}";
        private readonly string defaultErrorMessage = "Inventory Item can not be null";

        public InventoryItemIsNullException(string errorMessage = "", Exception innerException = null)
            : base(errorMessage, innerException)
        {
            this.defaultErrorMessage =
                string.IsNullOrWhiteSpace(errorMessage) ? this.defaultErrorMessage : errorMessage;
        }

        public override string Message => $"Code: {this.errorCode}, Message: {this.defaultErrorMessage}";
    }
}