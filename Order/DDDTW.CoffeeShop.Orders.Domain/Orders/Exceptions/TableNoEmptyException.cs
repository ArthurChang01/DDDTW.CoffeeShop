using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Exceptions
{
    public class TableNoEmptyException : DomainException
    {
        public TableNoEmptyException(string errorMsg = null, Exception inner = null)
            : base(nameof(Order), OrderErrorCode.TableNoIsEmpty,
                errorMsg ?? "Table no can not be empty", inner)
        {
        }
    }
}