﻿using System.Collections.Generic;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;

namespace DDDTW.CoffeeShop.Inventory.Domain.Inventories.DomainEvents
{
    public class Inbounded : DomainEvent<InventoryId>
    {
        public Inbounded(InventoryId id, int amount, int qty)
        {
            this.Amount = amount;
            this.Qty = qty;
        }

        public int Amount { get; private set; }

        public int Qty { get; private set; }

        protected override IEnumerable<object> GetDerivedEventEqualityComponents()
        {
            yield return this.Amount;
            yield return this.Qty;
        }
    }
}