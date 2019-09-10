using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Messages
{
    public class CreateOrderMsg : IRequest<OrderVM>
    {
        public IEnumerable<OrderItemVM> Items { get; set; }
    }
}