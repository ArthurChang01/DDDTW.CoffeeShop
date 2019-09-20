using DDDTW.CoffeeShop.CommonLib.BaseClasses;

namespace DDDTW.CoffeeShop.Orders.Domain.Products.Specifications
{
    internal class DiscountSpec : Specification<decimal>
    {
        public DiscountSpec(decimal discount)
            : base(discount, q => q >= 0)
        {
        }
    }
}