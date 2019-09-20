using DDDTW.CoffeeShop.CommonLib.BaseClasses;

namespace DDDTW.CoffeeShop.Orders.Domain.Products.Specifications
{
    internal class NameSpec : Specification<string>
    {
        public NameSpec(string name)
            : base(name, q => string.IsNullOrWhiteSpace(q) == false)
        {
        }
    }
}