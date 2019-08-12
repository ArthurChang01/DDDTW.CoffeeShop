using System;

namespace DDDTW.CoffeeShop.Inventory.Domain.Inventories.Exceptions
{
    public class NegativeQtyException : Exception
    {
        private readonly string errorCode = $"Inv-{(int)InventoryErrorCode.QtyIsNegative}";
        private readonly string defaultErrorMessage = "Qty can not be negative digital";

        public NegativeQtyException(int qty, string errorMessage = "", Exception innerException = null)
            : base(errorMessage, innerException)
        {
            this.defaultErrorMessage = string.IsNullOrEmpty(errorMessage) ? this.defaultErrorMessage : errorMessage;

            this.Data.Add("Parameter", qty);
        }

        public override string Message => $"Code: {this.errorCode}, Message: {this.defaultErrorMessage}";
    }
}