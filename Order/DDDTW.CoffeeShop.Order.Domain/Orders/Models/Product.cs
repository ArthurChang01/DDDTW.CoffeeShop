using System.Collections.Generic;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.Models
{
    public class Product : ValueObject<Product>
    {
        #region Constructors

        public Product()
        {
            this.Id = string.Empty;
            this.Name = string.Empty;
        }

        public Product(string id, string name)
        {
            this.Id = id;
            this.Name = name;
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