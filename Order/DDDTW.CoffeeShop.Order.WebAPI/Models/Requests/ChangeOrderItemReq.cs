using DDDTW.CoffeeShop.Order.WebAPI.Models.RequestModels;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Order.WebAPI.Models.Requests
{
    public class ChangeOrderItemReq
    {
        public IEnumerable<OrderItemRM> OrderItems { get; set; }
    }
}