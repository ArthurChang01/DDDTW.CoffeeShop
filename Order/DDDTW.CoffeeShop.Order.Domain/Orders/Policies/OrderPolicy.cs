using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Exceptions;
using DDDTW.CoffeeShop.Order.Domain.Orders.Specifications;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.Policies
{
    internal class OrderPolicy : Policy<Models.Order>
    {
        public OrderPolicy(Models.Order order)
            : base(order)
        {
        }

        public override bool IsValid()
        {
            if (new OrderIdSpec(this.aggregateRoot.Id).IsSatisfy() == false)
                this.exceptions.Add(new OrderIdIsNullException());

            if (new OrderItemSpec(this.aggregateRoot.OrderItems).IsSatisfy() == false)
                this.exceptions.Add(new OrderItemEmptyException());

            return this.exceptions.Count == 0;
        }
    }
}