using DDDTW.CoffeeShop.Orders.WebAPI.Models.Orders.RequestModels;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.WebAPI.Models.Orders.Requests
{
    public class AddOrderReq
    {
        public IEnumerable<OrderItemRM> Items { get; set; }
    }
}