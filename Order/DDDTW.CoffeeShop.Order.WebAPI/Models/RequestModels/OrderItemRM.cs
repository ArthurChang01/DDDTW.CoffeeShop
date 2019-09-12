namespace DDDTW.CoffeeShop.Order.WebAPI.Models.RequestModels
{
    public class OrderItemRM
    {
        public ProductRM Product { get; set; }

        public int Qty { get; set; }

        public decimal Price { get; set; }
    }
}