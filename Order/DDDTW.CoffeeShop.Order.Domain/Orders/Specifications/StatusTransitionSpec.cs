using System;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.Specifications
{
    internal class StatusTransitionSpec : Specification<OrderStatus>
    {
        public StatusTransitionSpec(OrderStatus curStatus, OrderStatus expectedBasedStatus, OrderStatus targetStatus)
        {
            this.Entity = curStatus;
            this.Predicate = ori => curStatus == expectedBasedStatus &&
                                    Math.Abs((byte)curStatus - (byte)targetStatus) == 1;
        }
    }
}