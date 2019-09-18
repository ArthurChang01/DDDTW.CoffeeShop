using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Results
{
    public class OrderItemRst : PropertyComparer<OrderItemRst>
    {
        #region Constructors

        public OrderItemRst()
        {
        }

        public OrderItemRst(ProductRst product, int qty, decimal price)
        {
            this.Product = product;
            this.Qty = qty;
            this.Price = price;
            this.Fee = this.Qty * this.Price;
        }

        public OrderItemRst(OrderItem item)
        {
            this.Product = new ProductRst(item.Product.Id, item.Product.Name);
            this.Qty = item.Qty;
            this.Price = item.Price;
            this.Fee = item.Fee;
        }

        #endregion Constructors

        #region Properties

        public ProductRst Product { get; private set; }

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