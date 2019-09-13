using DDDTW.CoffeeShop.Inventory.WebAPI.Models.RequestModels;

namespace DDDTW.CoffeeShop.Inventory.WebAPI.Models.Requests
{
    public class ChangeQtyReq
    {
        public InventoryOperation ActionMode { get; set; }

        public int Amount { get; set; }
    }
}