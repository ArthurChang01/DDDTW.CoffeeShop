using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Results;
using MediatR;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Messages
{
    public class AddInventoryMsg : IRequest<InventoryRst>
    {
        public AddInventoryMsg(int qty, InventoryItemRst item, IEnumerable<InventoryConstraintRst> constraints)
        {
            this.Qty = qty;
            this.Item = item;
            this.Constraints = constraints;
        }

        public int Qty { get; private set; }

        public InventoryItemRst Item { get; private set; }

        public IEnumerable<InventoryConstraintRst> Constraints { get; private set; }
    }
}