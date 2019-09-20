using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Products.Policies;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Domain.Products.Models
{
    public class ProductPrice : ValueObject<ProductPrice>
    {
        public ProductPrice()
        {
            this.Price = 0;
            this.Discount = 1;
        }

        public ProductPrice(decimal price, decimal discount = 1)
        {
            this.Price = price;
            this.Discount = discount;

            ProductPricePolicy.Verify(this);
        }

        public decimal Price { get; private set; }

        public decimal Discount { get; private set; }

        public decimal FinalPrice => this.Price * (this.Discount == 0 ? 1 : this.Discount);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Price;
            yield return this.Discount;
        }
    }
}