using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Responses
{
    public class OrderItemResp : PropertyComparer<OrderItemResp>
    {
        #region Constructors

        public OrderItemResp()
        {
        }

        public OrderItemResp(ProductResp product, int qty, decimal price)
        {
            this.Product = product;
            this.Qty = qty;
            this.Price = price;
            this.Fee = this.Qty * this.Price;
        }

        public OrderItemResp(OrderItem item)
        {
            this.Product = new ProductResp(item.Product.Id, item.Product.Name);
            this.Qty = item.Qty;
            this.Price = item.Price;
            this.Fee = item.Fee;
        }

        #endregion Constructors

        #region Properties

        public ProductResp Product { get; private set; }

        public int Qty { get; private set; }

        public decimal Price { get; private set; }

        public decimal Fee { get; private set; }

        #endregion Properties

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Price;
            yield return this.Qty;
            yield return this.Price;
            yield return this.Fee;
        }
    }
}