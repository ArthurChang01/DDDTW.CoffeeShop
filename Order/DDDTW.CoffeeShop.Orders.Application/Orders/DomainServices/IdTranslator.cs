using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.DomainServices
{
    public class IdTranslator : ITranslator<OrderId, string>
    {
        public OrderId Translate(string input)
        {
            string[] idString = input.Split('-');
            idString[1] = idString[1].Insert(4, "/").Insert(7, "/");
            return new OrderId(int.Parse(idString[2]), DateTimeOffset.Parse(idString[1]));
        }
    }
}