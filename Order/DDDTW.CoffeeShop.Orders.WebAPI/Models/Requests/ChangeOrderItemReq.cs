using DDDTW.CoffeeShop.Orders.WebAPI.Models.RequestModels;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.WebAPI.Models.Requests
{
    public class ChangeOrderItemReq
    {
        public IEnumerable<OrderItemRM> OrderItems { get; set; }
    }
}