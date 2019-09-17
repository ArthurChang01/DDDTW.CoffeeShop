﻿using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Responses;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.DomainServices
{
    public class OrderItemsTranslator : ITranslator<IEnumerable<OrderItem>, IEnumerable<OrderItemResp>>
    {
        public IEnumerable<OrderItem> Translate(IEnumerable<OrderItemResp> input)
        {
            return input.Select(o => new OrderItem(new Product(o.Product.Id, o.Product.Name), o.Qty, o.Price));
        }
    }
}