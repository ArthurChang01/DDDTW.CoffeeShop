using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.ViewModels;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DomainServices
{
    public class OrderItemsTranslator : ITranslator<IEnumerable<OrderItem>, IEnumerable<OrderItemVM>>
    {
        public IEnumerable<OrderItem> Translate(IEnumerable<OrderItemVM> input)
        {
            return input.Select(o => new OrderItem(new Product(o.Product.Id, o.Product.Name), o.Qty, o.Price));
        }
    }
}