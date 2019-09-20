using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Models
{
    public class OrderItem : ValueObject<OrderItem>
    {
        #region Constructors

        public OrderItem()
        {
            this.ProductId = string.Empty;
            this.Qty = 0;
            this.Price = 0;
        }

        public OrderItem(string productId, int qty, decimal prices)
        {
            this.ProductId = productId;
            this.Qty = qty;
            this.Price = prices;
        }

        #endregion Constructors

        #region Properties

        public string ProductId { get; private set; }

        public int Qty { get; private set; }

        public decimal Price { get; private set; }

        public decimal Fee => this.Qty * this.Price;

        #endregion Properties

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.ProductId;
            yield return this.Qty;
            yield return this.Price;
        }
    }
}