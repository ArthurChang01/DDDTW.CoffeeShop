using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.ViewModels
{
    public class ProductVM : PropertyComparer<ProductVM>
    {
        #region Constructors

        public ProductVM()
        {
        }

        public ProductVM(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public ProductVM(Product product)
        {
            this.Id = product.Id;
            this.Name = product.Name;
        }

        #endregion Constructors

        public string Id { get; private set; }

        public string Name { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Id;
            yield return this.Name;
        }
    }
}