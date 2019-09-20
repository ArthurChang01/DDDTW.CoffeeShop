using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;
using System;

namespace DDDTW.CoffeeShop.Orders.Application.Products.DomainService
{
    public class IdTranslator : ITranslator<ProductId, string>
    {
        public ProductId Translate(string input)
        {
            string[] idString = input.Split('-');
            idString[1] = idString[1].Insert(4, "/").Insert(7, "/");
            return new ProductId(int.Parse(idString[2]), DateTimeOffset.Parse(idString[1]));
        }
    }
}