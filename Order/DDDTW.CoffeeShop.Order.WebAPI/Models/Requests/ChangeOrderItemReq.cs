using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.ViewModels;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Order.WebAPI.Models.Requests
{
    public class ChangeOrderItemReq
    {
        public IEnumerable<OrderItemVM> OrderItems { get; set; }
    }
}