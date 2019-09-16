using DDDTW.CoffeeShop.CommonLib.BaseClasses;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Specifications
{
    internal class AmountSpec : Specification<int>
    {
        public AmountSpec(int amount)
            : base(amount, q => q >= 0)
        {
        }
    }
}