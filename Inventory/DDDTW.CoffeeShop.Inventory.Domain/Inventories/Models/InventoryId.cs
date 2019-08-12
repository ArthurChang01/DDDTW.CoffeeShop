using System;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;

namespace DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models
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