using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Responses;
using MediatR;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Messages
{
    public class AddInventoryMsg : IRequest<InventoryResp>
    {
        public AddInventoryMsg(int qty, InventoryItemResp item, IEnumerable<InventoryConstraintResp> constraints)
        {
            this.Qty = qty;
            this.Item = item;
            this.Constraints = constraints;
        }

        public int Qty { get; private set; }

        public InventoryItemResp Item { get; private set; }

        public IEnumerable<InventoryConstraintResp> Constraints { get; private set; }
    }
}