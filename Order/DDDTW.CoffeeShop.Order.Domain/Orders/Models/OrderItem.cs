using System.Collections.Generic;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.Models
{
    public class OrderItem : ValueObject<OrderItem>
    {
        #region Constructors

        public OrderItem()
        {
            this.Product = new Product();
            this.Qty = 0;
            this.Price = 0;
        }

        public OrderItem(Product product, int qty, decimal prices)
        {
            this.Product = product;
            this.Qty = qty;
            this.Price = prices;
        }

        #endregion Constructors

        #region Properties

        public Product Product { get; private set; }

        public int Qty { get; private set; }

        public decimal Price { get; private set; }

        public decimal Fee => this.Qty * this.Price;

        #endregion Properties

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Product;
            yield return this.Qty;
            yield return this.Price;
        }
    }
}