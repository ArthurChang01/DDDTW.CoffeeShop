using DDDTW.CoffeeShop.Order.WebAPI.Models.ReadModels;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Order.WebAPI.Models.Requests
{
    public class AddOrderReq
    {
        public IEnumerable<OrderItemRM> Items { get; set; }
    }
}