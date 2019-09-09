using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.ViewModels;
using MediatR;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.QueryModels
{
    public class GetOrderQry : IRequest<OrderVM>
    {
        public string Id { get; set; }
    }
}