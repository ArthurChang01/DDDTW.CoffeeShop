namespace DDDTW.CoffeeShop.Orders.WebAPI.Models.Orders.RequestModels
{
    public class OrderItemRM
    {
        public string ProductId { get; set; }

        public int Qty { get; set; }

        public decimal Price { get; set; }
    }
}