using System;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;

namespace DDDTW.CoffeeShop.Order.Domain.Orders.Models
{
    public class OrderId : EntityId
    {
        public OrderId()
        {
        }

        public OrderId(long seqNo, DateTimeOffset createdDate)
            : base(seqNo, createdDate)
        {
        }

        public override string Abbr => "ord";
    }
}