using System;

namespace DDDTW.CoffeeShop.Inventory.Domain.Inventories.Exceptions
{
    public class InventoryShortageException : Exception
    {
        private readonly string errorCode = $"Inv-{(int)InventoryErrorCode.InventoryShortage}";
        private readonly string defaultErrorMessage = "Inventory is not enough";

        public InventoryShortageException(int amount, string errorMessage = "", Exception innerException = null)
            : base(errorMessage, innerException)
        {
            this.defaultErrorMessage =
                string.IsNullOrWhiteSpace(errorMessage) ? this.defaultErrorMessage : errorMessage;

            this.Data.Add("Parameter", amount);
        }

        public override string Message => $"Code: {this.errorCode}, Message: {this.defaultErrorMessage}";
    }
}