using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Responses
{
    public class ProductResp : PropertyComparer<ProductResp>
    {
        #region Constructors

        public ProductResp()
        {
        }

        public ProductResp(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public ProductResp(Product product)
        {
            this.Id = product.Id;
            this.Name = product.Name;
        }

        #endregion Constructors

        #region Properties

        public string Id { get; private set; }

        public string Name { get; private set; }

        #endregion Properties

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Id;
            yield return this.Name;
        }
    }
}