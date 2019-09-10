using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.Interfaces
{
    public interface IOrderFactory : IFactory<Models.Order, OrderId>
    {
    }
}