using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using System;

namespace DDDTW.CoffeeShop.Orders.Domain.Products.Models
{
    public class ProductId : EntityId
    {
        public ProductId()
            : base(0, DateTimeOffset.Now)
        {
        }

        public ProductId(long seqNo = 0, DateTimeOffset? occuredDate = null)
            : base(seqNo, occuredDate ?? DateTimeOffset.Now)
        {
        }

        public override string Abbr => "prd";
    }
}