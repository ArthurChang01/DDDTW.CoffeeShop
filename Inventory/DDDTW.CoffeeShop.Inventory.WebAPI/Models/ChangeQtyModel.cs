namespace DDDTW.CoffeeShop.Inventory.WebAPI.Models
{
    public class ChangeQtyModel
    {
        public InventoryOperation ActionMode { get; set; }

        public int Amount { get; set; }
    }
}