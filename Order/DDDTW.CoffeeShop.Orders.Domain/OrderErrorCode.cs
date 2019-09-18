using DDDTW.CoffeeShop.CommonLib.BaseClasses;

namespace DDDTW.CoffeeShop.Orders.Domain
{
    public class OrderErrorCode : Enumeration
    {
        public static readonly OrderErrorCode StatusTransit = new OrderErrorCode(0, nameof(StatusTransit));
        public static readonly OrderErrorCode OrderIdIsNull = new OrderErrorCode(1, nameof(OrderIdIsNull));
        public static readonly OrderErrorCode OrderItemsAreEmptyOrNull = new OrderErrorCode(2, nameof(OrderItemsAreEmptyOrNull));
        public static readonly OrderErrorCode TableNoIsEmpty = new OrderErrorCode(3, nameof(OrderItemsAreEmptyOrNull));

        public OrderErrorCode(int id, string name) : base(id, name)
        {
        }
    }
}