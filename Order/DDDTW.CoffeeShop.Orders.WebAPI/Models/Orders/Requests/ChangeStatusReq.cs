using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;

namespace DDDTW.CoffeeShop.Orders.WebAPI.Models.Orders.Requests
{
    public class ChangeStatusReq
    {
        public OrderStatus OrderStatus { get; set; }
    }
}