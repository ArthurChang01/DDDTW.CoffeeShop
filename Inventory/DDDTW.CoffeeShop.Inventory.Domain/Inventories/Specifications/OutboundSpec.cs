using DDDTW.CoffeeShop.CommonLib.BaseClasses;

namespace DDDTW.CoffeeShop.Inventory.Domain.Inventories.Specifications
{
    public class OutboundSpec : Specification<int>
    {
        public OutboundSpec(int qty, int amount)
            : base(qty, _ => qty - amount >= 0)
        {
        }
    }
}