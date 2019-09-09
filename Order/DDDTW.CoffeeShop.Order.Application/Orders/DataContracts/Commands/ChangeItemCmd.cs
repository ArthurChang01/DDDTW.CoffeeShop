using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using MediatR;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Commands
{
    public class ChangeItemCmd : IRequest<Unit>
    {
        public string Id { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }
}