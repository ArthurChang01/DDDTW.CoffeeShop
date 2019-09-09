using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Commands
{
    public class AddInventoryCmd : IRequest<InventoryVM>
    {
        public int Qty { get; set; }

        public InventoryItemVM Item { get; set; }

        public IEnumerable<InventoryConstraintVM> Constraints { get; set; }
    }
}