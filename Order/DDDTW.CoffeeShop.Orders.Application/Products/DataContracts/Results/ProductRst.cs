using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;
using System;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Application.Products.DataContracts.Results
{
    public class ProductRst : PropertyComparer<ProductRst>
    {
        #region Constructors

        public ProductRst()
        {
        }

        public ProductRst(Product product)
        {
            this.Id = product.Id.ToString();
            this.Name = product.Name;
            this.Description = product.Description;
            this.Price = product.Price.Price;
            this.Discount = product.Price.Discount;
            this.FinalPrice = product.FinalPrice;
            this.CreatedDate = product.CreatedDate;
        }

        #endregion Constructors

        #region Properties

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal FinalPrice { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        #endregion Properties

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Name;
            yield return this.Description;
            yield return this.Price;
            yield return this.Discount;
            yield return this.FinalPrice;
            yield return this.CreatedDate;
        }
    }
}