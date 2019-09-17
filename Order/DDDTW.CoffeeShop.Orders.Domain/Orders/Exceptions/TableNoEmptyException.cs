using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using System;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Exceptions
{
    public class TableNoEmptyException : DomainException
    {
        public TableNoEmptyException(string errorMsg = null, Exception inner = null)
            : base("Order", OrderErrorCode.TableNoIsEmpty,
                errorMsg ?? "Table no can not be empty", inner)
        {
        }
    }
}