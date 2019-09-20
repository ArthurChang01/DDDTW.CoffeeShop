using DDDTW.CoffeeShop.CommonLib.BaseClasses;

namespace DDDTW.CoffeeShop.Orders.Domain.Products.Specifications
{
    internal class DescriptionSpec : Specification<string>
    {
        public DescriptionSpec(string desc)
            : base(desc, q => string.IsNullOrWhiteSpace(q) == false)
        {
        }
    }
}