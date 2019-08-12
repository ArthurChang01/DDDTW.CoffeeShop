using System;

namespace DDDTW.CoffeeShop.Inventory.Domain.Inventories.Exceptions
{
    public class OverQtyLimitationException : Exception
    {
        private readonly string errorCode = $"Inv-{(int)InventoryErrorCode.OverUpperBound}";
        private readonly string defaultErrorMessage = "Qty is over upper-bound";

        public OverQtyLimitationException(int amount, string errorMessage = "", Exception innerException = null)
            : base(errorMessage, innerException)
        {
            this.defaultErrorMessage =
                string.IsNullOrWhiteSpace(errorMessage) ? this.defaultErrorMessage : errorMessage;

            this.Data.Add("Parameter", amount);
        }

        public override string Message => $"Code: {this.errorCode}, Message: {this.defaultErrorMessage}";
    }
}