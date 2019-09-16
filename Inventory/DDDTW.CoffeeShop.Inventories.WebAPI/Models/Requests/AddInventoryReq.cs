using DDDTW.CoffeeShop.Inventories.WebAPI.Models.RequestModels;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventories.WebAPI.Models.Requests
{
    public class AddInventoryReq
    {
        public int Qty { get; set; }

        public InventoryItemRM Item { get; set; }

        public IEnumerable<InventoryConstraintRM> Constraints { get; set; }
    }
}