using DDDTW.CoffeeShop.Inventories.WebAPI.Models.RequestModels;

namespace DDDTW.CoffeeShop.Inventories.WebAPI.Models.Requests
{
    public class ChangeQtyReq
    {
        public InventoryOperation ActionMode { get; set; }

        public int Amount { get; set; }
    }
}