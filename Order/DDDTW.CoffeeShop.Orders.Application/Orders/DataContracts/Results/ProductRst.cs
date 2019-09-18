using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Results
{
    public class ProductRst : PropertyComparer<ProductRst>
    {
        #region Constructors

        public ProductRst()
        {
        }

        public ProductRst(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public ProductRst(Product product)
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