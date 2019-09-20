using DDDTW.CoffeeShop.CommonLib.BaseClasses;

namespace DDDTW.CoffeeShop.Orders.Domain.Products.Specifications
{
    internal class PriceSpec : Specification<decimal>
    {
        public PriceSpec(decimal price)
            : base(price, q => q >= 0)
        {
        }
    }
}