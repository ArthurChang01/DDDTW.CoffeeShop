namespace DDDTW.CoffeeShop.Order.Domain.Orders.Models
{
    public enum OrderStatus : byte
    {
        Initial = 0,
        Processing = 1,
        Deliver = 2,
        Closed = 3
    }
}