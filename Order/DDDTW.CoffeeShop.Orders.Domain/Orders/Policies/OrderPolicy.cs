using DDDTW.CoffeeShop.Orders.Domain.Orders.Exceptions;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Specifications;
using System;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Policies
{
    internal class OrderPolicy
    {
        public static void Verify(Order order)
        {
            List<Exception> exceptions = new List<Exception>();

            if (new OrderIdSpec(order.Id).IsSatisfy() == false)
                exceptions.Add(new OrderIdIsNullException());

            if (new OrderTableNoSpec(order.TableNo).IsSatisfy() == false)
                exceptions.Add(new TableNoEmptyException());

            if (new OrderItemSpec(order.OrderItems).IsSatisfy() == false)
                exceptions.Add(new OrderItemEmptyException());

            if (exceptions.Count > 0)
                throw new AggregateException(exceptions);
        }
    }
}