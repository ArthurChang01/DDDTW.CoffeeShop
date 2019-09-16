using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Interfaces
{
    public interface IOrderFactory : IFactory<Models.Order, OrderId>
    {
    }
}