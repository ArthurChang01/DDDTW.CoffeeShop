using System.Collections.Generic;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.ViewModels;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;
using MediatR;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Commands
{
    public class AddInventoryCmd : IRequest<InventoryVM>
    {
        public int Qty { get; set; }

        public InventoryItem Item { get; set; }

        public IEnumerable<InventoryConstraint> Constraints { get; set; }
    }
}