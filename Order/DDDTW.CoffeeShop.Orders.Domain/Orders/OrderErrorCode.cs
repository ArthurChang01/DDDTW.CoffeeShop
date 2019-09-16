namespace DDDTW.CoffeeShop.Orders.Domain.Orders
{
    public enum OrderErrorCode
    {
        StatusTransit = 0,
        OrderIdIsNull = 1,
        OrderItemIsEmptyOrNull = 2
    }
}