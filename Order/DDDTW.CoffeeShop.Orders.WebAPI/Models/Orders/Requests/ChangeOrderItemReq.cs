﻿using DDDTW.CoffeeShop.Orders.WebAPI.Models.Orders.RequestModels;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.WebAPI.Models.Orders.Requests
{
    public class ChangeOrderItemReq
    {
        public IEnumerable<OrderItemRM> Items { get; set; }
    }
}