namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Models
{
    public enum OrderStatus
    {
        Initial = 0,
        Processing = 1,
        Deliver = 2,
        Closed = 3,
        Cancel = 4
    }
}