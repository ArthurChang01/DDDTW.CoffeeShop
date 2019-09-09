namespace DDDTW.CoffeeShop.Inventory.WebAPI.Models
{
    public class ChangeQtyModel
    {
        public InventoryOperation Operation { get; set; }

        public int Amount { get; set; }
    }
}