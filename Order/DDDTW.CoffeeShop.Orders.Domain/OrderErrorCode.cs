using DDDTW.CoffeeShop.CommonLib.BaseClasses;

namespace DDDTW.CoffeeShop.Orders.Domain
{
    public class OrderErrorCode : Enumeration
    {
        public static readonly OrderErrorCode StatusTransit = new OrderErrorCode(0, nameof(StatusTransit));
        public static readonly OrderErrorCode OrderIdIsNull = new OrderErrorCode(1, nameof(OrderIdIsNull));
        public static readonly OrderErrorCode OrderItemsAreEmptyOrNull = new OrderErrorCode(2, nameof(OrderItemsAreEmptyOrNull));
        public static readonly OrderErrorCode TableNoIsEmpty = new OrderErrorCode(3, nameof(OrderItemsAreEmptyOrNull));
        public static readonly OrderErrorCode ProductNameIsEmpty = new OrderErrorCode(4, nameof(ProductNameIsEmpty));
        public static readonly Enumeration ProductDescriptionIsEmpty = new OrderErrorCode(5, nameof(ProductDescriptionIsEmpty));
        public static readonly OrderErrorCode ProductDiscountIsInappropriate = new OrderErrorCode(6, nameof(ProductDiscountIsInappropriate));
        public static readonly OrderErrorCode PriceIsInappropriate = new OrderErrorCode(7, nameof(PriceIsInappropriate));

        public OrderErrorCode(int id, string name) : base(id, name)
        {
        }
    }
}