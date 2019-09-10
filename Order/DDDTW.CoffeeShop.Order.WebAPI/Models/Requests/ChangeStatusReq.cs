using DDDTW.CoffeeShop.Order.Domain.Orders.Models;

namespace DDDTW.CoffeeShop.Order.WebAPI.Models.Requests
{
    public class ChangeStatusReq
    {
        public OrderStatus OrderStatus { get; set; }
    }
}