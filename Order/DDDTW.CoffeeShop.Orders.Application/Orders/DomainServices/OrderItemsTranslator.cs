using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Results;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.DomainServices
{
    public class OrderItemsTranslator : ITranslator<IEnumerable<OrderItem>, IEnumerable<OrderItemRst>>
    {
        public IEnumerable<OrderItem> Translate(IEnumerable<OrderItemRst> input)
        {
            return input.Select(o => new OrderItem(o.ProductId, o.Qty, o.Price));
        }
    }
}