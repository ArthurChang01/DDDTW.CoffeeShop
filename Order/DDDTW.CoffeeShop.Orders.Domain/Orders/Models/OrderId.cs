using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using System;

namespace DDDTW.CoffeeShop.Orders.Domain.Orders.Models
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