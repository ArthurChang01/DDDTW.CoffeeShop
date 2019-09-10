using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.ViewModels;
using MediatR;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Messages
{
    public class GetOrderMsg : IRequest<OrderVM>
    {
        public string Id { get; set; }
    }
}