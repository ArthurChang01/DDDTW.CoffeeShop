using DDDTW.CoffeeShop.CommonLib.BaseClasses;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Specifications
{
    internal class OrderTableNoSpec : Specification<string>
    {
        public OrderTableNoSpec(string tableNo)
            : base(q => string.IsNullOrWhiteSpace(tableNo) == false)
        {
        }
    }
}