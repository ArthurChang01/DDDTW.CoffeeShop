using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using System;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models
{
    public class InventoryId : EntityId
    {
        public InventoryId()
        {
        }

        public InventoryId(long seqNo, DateTimeOffset date)
            : base(seqNo, date)
        {
        }

        public override string Abbr => "inv";
    }
}