using DDDTW.CoffeeShop.CommonLib.BaseClasses;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Specifications
{
    internal class OutboundSpec : Specification<int>
    {
        public OutboundSpec(int qty, int amount)
            : base(qty, _ => qty - amount >= 0)
        {
        }
    }
}