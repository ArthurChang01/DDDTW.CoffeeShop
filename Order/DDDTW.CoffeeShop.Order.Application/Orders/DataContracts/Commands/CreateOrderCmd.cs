using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.ViewModels;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using MediatR;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Commands
{
    public class CreateOrderCmd : IRequest<OrderVM>
    {
        public IEnumerable<OrderItem> Items { get; set; }
    }
}