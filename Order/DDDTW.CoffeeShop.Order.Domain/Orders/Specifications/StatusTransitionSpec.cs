using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using System;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.Specifications
{
    internal class StatusTransitionSpec : Specification<OrderStatus>
    {
        public StatusTransitionSpec(OrderStatus curStatus, OrderStatus previousStatus, OrderStatus targetStatus)
        {
            this.Entity = curStatus;
            this.Predicate = ori => curStatus == previousStatus &&
                                    Math.Abs((byte)curStatus - (byte)targetStatus) == 1;
        }
    }
}