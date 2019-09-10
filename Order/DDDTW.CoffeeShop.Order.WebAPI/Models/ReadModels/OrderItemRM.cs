namespace DDDTW.CoffeeShop.Order.WebAPI.Models.ReadModels
{
    public class OrderItemRM
    {
        public ProductRM Product { get; set; }

        public int Qty { get; set; }

        public decimal Price { get; set; }
    }
}